using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnicalQandAForum.ServiceLayer;
using TechnicalQandAForum.ViewModels;

namespace TechnicalQandAForum.Controllers
{
    public class HomeController : Controller
    {
        IQuestionService _questionService;
        ICategoryService _categoryService;

        public HomeController(IQuestionService questionService, ICategoryService categoryService)
        {
            _questionService = questionService;
            _categoryService = categoryService;
        }
        // GET: Home
        public ActionResult Index()
        {
            List<QuestionViewModel> questionViewModels = _questionService.GetQuestions().Take(10).ToList();
            return View(questionViewModels);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Categories()
        {
            List<CategoryViewModel> categoryViewModels = _categoryService.GetCategories();
            return View(categoryViewModels);
        }
        [Route("allquestions")]
        public ActionResult Questions()
        {
            List<QuestionViewModel> questionViewModels = _questionService.GetQuestions().Take(10).ToList();
            return View(questionViewModels);
        }
        public ActionResult Search(string str)
        {
            List<QuestionViewModel> questionViewModels = new List<QuestionViewModel>();
            if (!string.IsNullOrEmpty(str))
            {
                questionViewModels =
                    _questionService.GetQuestions()
                    .Where(q => q.QuestionName.ToLower().Contains(str.ToLower()) ||
                    q.Category.CategoryName.ToLower().Contains(str.ToLower())).ToList();
                ViewBag.str = str;
            }
            return View(questionViewModels);
        }
    }
}