using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnicalQandAForum.CustomFilters;
using TechnicalQandAForum.ServiceLayer;
using TechnicalQandAForum.ViewModels;

namespace TechnicalQandAForum.Controllers
{
    public class QuestionsController : Controller
    {
        IQuestionService _questionService;
        ICategoryService _categoryService;
        IAnswerService _answerService;

        public QuestionsController(IQuestionService questionService, ICategoryService categoryService, IAnswerService answerService)
        {
            _questionService = questionService;
            _categoryService = categoryService;
            _answerService = answerService;
        }
        // GET: Questions
        public ActionResult View(int id)
        {
            int uid = Convert.ToInt32(Session["CurrentUserId"]);
            _questionService.UpdateQuestionViewsCount(id, 1);
            QuestionViewModel questionViewModel = _questionService.GetQuestionByQuestionId(id, uid);
            return View(questionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public ActionResult AddAnswer(NewAnswerViewModel navm)
        {
            navm.UserId = Convert.ToInt32(Session["CurrentUserId"]);
            navm.AnswerDateAndTime = DateTime.Now;
            navm.VotesCount = 0;
            if (ModelState.IsValid)
            {
                _answerService.InsertAnswer(navm);
                return RedirectToAction("View", "Questions", new { id = navm.QuestionId });
            }
            else
            {
                ModelState.AddModelError("error", "Invalid Data");
                QuestionViewModel questionViewModel = _questionService.GetQuestionByQuestionId(navm.QuestionId, navm.UserId);
                return View("View", questionViewModel);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public ActionResult EditAnswer(EditAnswerViewModel eavm)
        {
            eavm.UserId = Convert.ToInt32(Session["CurrentUserId"]);
            if (ModelState.IsValid)
            {
                _answerService.UpdateAnswer(eavm);
                return RedirectToAction("View", new { id = eavm.QuestionId });
            }
            else
            {
                ModelState.AddModelError("error", "Invalid Data");
                return RedirectToAction("View", new { id = eavm.QuestionId });
            }
        }

        [UserAuthorization]
        public ActionResult Create()
        {
            List<CategoryViewModel> categoryViewModels = _categoryService.GetCategories();
            ViewBag.Categories = categoryViewModels;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization]
        public ActionResult Create(NewQuestionViewModel nqvm)
        {
            nqvm.UserId = Convert.ToInt32(Session["CurrentUserId"]);
            nqvm.QuestionDateAndTime = DateTime.Now;
            nqvm.AnswersCount = 0;
            nqvm.ViewsCount = 0;
            nqvm.VotesCount = 0;
            if (ModelState.IsValid)
            {
                _questionService.InsertQuestion(nqvm);
                return RedirectToAction("Questions", "Home");
            }
            else
            {
                ModelState.AddModelError("error", "Invalid Data");
                return View();
            }
        }

    }
}