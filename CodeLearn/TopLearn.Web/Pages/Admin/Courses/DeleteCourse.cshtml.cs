using CodeLearn.Core.DTOs.Courses;
using CodeLearn.Core.Security;
using CodeLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeLearn.Web.Pages.Admin.Courses
{
    [PermissionChecker(2005)]
    public class DeleteCourseModel : PageModel
    {
        private ICourseService _courseService;

        public DeleteCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public ShowCourseForAdminViewModel Course { get; set; }
        public void OnGet(int id)
        {
            ViewData["CourseId"] = id;
            Course = _courseService.GetCourseForAdminById(id);
        }

        public IActionResult OnPost(int courseId)
        {
            _courseService.DeleteCourse(courseId);
            return RedirectToPage("Index");
        }
    }
}
