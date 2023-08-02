using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeLearn.DataLayer.Entities.Course
{
    public class CourseLevel
    {
        [Key]
        public int LevelId { get; set; }

        [Required]
        [MaxLength(150)]
        public string LevelTitle { get; set; }

        #region Relations

        public List<CodeLearn.DataLayer.Entities.Course.Course> Courses { get; set; }

        #endregion
    }
}
