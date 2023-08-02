using System.Collections.Generic;
using CodeLearn.DataLayer.Entities.Course;

namespace CodeLearn.Core.DTOs.Courses
{
    public class ShowCourseListItemViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int Price { get; set; }
        public List<CourseEpisode> CourseEpisodes { get; set; }
    }
}
