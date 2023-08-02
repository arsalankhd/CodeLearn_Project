using System.Threading.Tasks;
using CodeLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeLearn.Web.ViewComponents
{
    public class LatestCourseViewComponent : ViewComponent
    {
        private ICourseService _courseService;

        public LatestCourseViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("LatestCourse", _courseService.GetCourse().Item1));
        }
    }
}
