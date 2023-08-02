using System.Collections.Generic;
using CodeLearn.Core.DTOs.Courses;
using CodeLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeLearn.Web.Pages.Admin.Courses
{
    public class IndexModel : PageModel
    {
        private ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<ShowCourseForAdminViewModel> ListCourse { get; set; }

        public void OnGet()
        {
            ListCourse = _courseService.GetCoursesForAdmin();
        }
    }
}