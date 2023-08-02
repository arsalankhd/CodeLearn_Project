using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeLearn.Core.DTOs.Courses;
using CodeLearn.Core.Security;
using CodeLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    [PermissionChecker(2006)]
    public class MasterController : Controller
    {
        private ICourseService _courseService;
        private IUserService _userService;

        public MasterController(ICourseService courseService, IUserService userService)
        {
            _courseService = courseService;
            _userService = userService;
        }

        [HttpGet("master-courses")]
        public IActionResult MasterCoursesList()
        {
            var courses = _courseService.GetAllMasterCourses(User.Identity.Name);
            return View(courses);
        }

        [HttpGet("course-episodes/{courseId}")]
        public IActionResult EpisodeList(int courseId)
        {
            var course = _courseService.GetCourseById(courseId);

            if (course == null)
            {
                return NotFound();
            }

            if (course.TeacherId != _userService.GetUserIdByUserName(User.Identity.Name))
            {
                return RedirectToAction("MasterCoursesList", "Master");
            }

            var episodes = _courseService.GetListEpisodeCourse(courseId);

            ViewBag.CourseId = courseId;

            return View(episodes);
        }

        [HttpGet("add-episode/{courseId}")]
        public IActionResult AddEpisode(int courseId)
        {
            var course = _courseService.GetCourseById(courseId);

            if (course == null)
            {
                return NotFound();
            }

            if (course.TeacherId != _userService.GetUserIdByUserName(User.Identity.Name))
            {
                return RedirectToAction("MasterCoursesList", "Master");
            }

            var result = new AddEpisodeViewModel()
            {
                CourseId = courseId,
                IsFree = true
            };
            return View(result);
        }

        [HttpPost("add-episode/{courseId}")]
        public IActionResult AddEpisode(AddEpisodeViewModel episodeViewModel)
        {
            if (!ModelState.IsValid)
                return View(episodeViewModel);

            if (string.IsNullOrEmpty(episodeViewModel.EpisodeFileName))
            {
                return View(episodeViewModel);
            }

            var result = _courseService.AddEpisode(episodeViewModel, User.Identity.Name);

            if (result)
            {
                return RedirectToAction("EpisodeList", "Master", new { courseId = episodeViewModel.CourseId });
            }

            return View(episodeViewModel);
        }

        public IActionResult DropzoneTarget(List<IFormFile> files, int courseId)
        {
            if (files != null && files.Any())
            {
                foreach (var file in files)
                {
                    if (_courseService.CheckExistFile(file.FileName))
                    {
                        ViewData["IsExistFile"] = true;
                        return new JsonResult(new { status = "ErrorFile" });
                    }

                    var fileName = file.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles/");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var filePath = path + fileName;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return new JsonResult(new { data = fileName, status = "Success" });
                }
            }

            return new JsonResult(new { status = "Error" });
        }
    }
}
