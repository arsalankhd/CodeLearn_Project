using System;
using System.Security.Claims;
using CodeLearn.Core.Services.Interfaces;
using CodeLearn.DataLayer.Entities.Question;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeLearn.Web.Controllers
{
    public class ForumController : Controller
    {
        private IForumService _forumService;

        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index(int? courseId, string filter = "")
        {
            ViewBag.CourseId = courseId;
            return View(_forumService.GetQuestions(courseId, filter));
        }

        #region Create Question

        [Authorize]
        public IActionResult CreateQuestion(int id)
        {
            Question question = new Question()
            {
                CourseId = id
            };

            return View(question);
        }

        [HttpPost]
        public IActionResult CreateQuestion(Question question)
        {
            var sanitizer = new HtmlSanitizer();
            question.Body = sanitizer.Sanitize(question.Body);
            if (!ModelState.IsValid)
                return View(question);
            question.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
            int questionId = _forumService.AddQuestion(question);
            return Redirect($"/Forum/ShowQuestion/{questionId}");
        }

        #endregion

        #region Show Question

        public IActionResult ShowQuestion(int id)
        {
            return View(_forumService.ShowQuestion(id));
        }

        #endregion

        #region Answer

        public IActionResult Answer(int id, string body)
        {
            var sanitizer = new HtmlSanitizer();
            body = sanitizer.Sanitize(body);
            if (!string.IsNullOrEmpty(body))
            {
                _forumService.AddAnswer(new Answer()
                {
                    BodyAnswer = body,
                    UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString())),
                    CreateDate = DateTime.Now,
                    QuestionId = id
                });
            }
            return RedirectToAction("ShowQuestion", new { id = id });
        }

        public IActionResult SelectTrueAnswer(int questionId, int answerId)
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier.ToString()));
            var question = _forumService.ShowQuestion(questionId);
            if (question.Question.UserId == currentUserId)
            {
                _forumService.ChangeIsTrueAnswer(questionId, answerId);
            }
            return RedirectToAction("ShowQuestion", new { id = questionId });
        }
        #endregion
    }
}
