using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeSOSProject.ServiceLayer;
using CodeSOSProject.ViewModels;

namespace CodeSOS.Controllers
{
    public class HomeController : Controller
    {
        IQuestionService questionService;

        //Constructor
        public HomeController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        // GET: Home/Index
        public ActionResult Index()
        {
            
           List<QuestionViewModel> qvm = this.questionService.GetQuestions().Take(10).ToList();
            return View(qvm);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}