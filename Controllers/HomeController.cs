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
        ICategoryService categoryService;

        //Constructor
        public HomeController(IQuestionService questionService, ICategoryService categoryService)
        {
            this.questionService = questionService;
            this.categoryService = categoryService;
        }

        // GET: Home/Index
        public ActionResult Index()
        {
            
           List<QuestionViewModel> qvm = this.questionService.GetQuestions().Take(10).ToList();
            return View(qvm);
        }

        // GET: Home/About
        public ActionResult About()
        {
            return View();
        }

        // GET: Home/Contact
        public ActionResult Contact()
        {
            return View();
        }

        // GET: Home/Categories
        public ActionResult Categories()
        {
            List<CategoryViewModel> cvm = this.categoryService.GetCategories();
            return View(cvm);
        }

        // GET: Home/Questions
        [Route("allquestions")]
        public ActionResult Questions()
        {
            List<QuestionViewModel> qvm = this.questionService.GetQuestions();
            return View(qvm);
        }
    }
}