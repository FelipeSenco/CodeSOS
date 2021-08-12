using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeSOSProject.ViewModels;
using CodeSOSProject.ServiceLayer;

namespace CodeSOS.Controllers
{
    public class QuestionsController : Controller
    {
        IQuestionService questionService;
        IAnswerService answerService;
        ICategoryService categoryService;

        public QuestionsController(IQuestionService questionService, IAnswerService answerService, ICategoryService categoryService)
        {
            this.questionService = questionService;
            this.answerService = answerService;
            this.categoryService = categoryService;
        }

        // GET: Questions/View
        public ActionResult ViewQuestion(int questionID)
        {
            this.questionService.UpdateQuestionViewsCount(questionID, 1);
            int userID = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel qvm = this.questionService.GetQuestionByQuestionID(questionID, userID);
            return View(qvm);
        }
    }
}