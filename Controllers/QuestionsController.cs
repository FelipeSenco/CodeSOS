using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeSOSProject.ViewModels;
using CodeSOSProject.ServiceLayer;
using CodeSOS.CustomFilters;

namespace CodeSOS.Controllers
{
    public class QuestionsController : Controller
    {
        IQuestionService questionService;
        IAnswerService answerService;
        ICategoryService categoryService;

        //Constructor
        public QuestionsController(IQuestionService questionService, IAnswerService answerService, ICategoryService categoryService)
        {
            this.questionService = questionService;
            this.answerService = answerService;
            this.categoryService = categoryService;
        }

        // GET: Questions/View
        public ActionResult ViewQuestion(int id)
        {
            this.questionService.UpdateQuestionViewsCount(id, 1);
            int userID = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel qvm = this.questionService.GetQuestionByQuestionID(id, userID);
            return View(qvm);
        }

        //AddAnswer Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]
        public ActionResult AddAnswer(NewAnswerViewModel navm)
        {
            navm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            navm.AnswerDateAndTime = DateTime.Now;
            navm.VotesCount = 0;

            if (ModelState.IsValid)
            {
                this.answerService.InsertAnswer(navm);
                return RedirectToAction("ViewQuestion", new { id = navm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                QuestionViewModel qvm = this.questionService.GetQuestionByQuestionID(navm.QuestionID, navm.UserID);
                return View("ViewQuestion", qvm.QuestionID);
            }
        }


    }
}