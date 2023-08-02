using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeLearn.DataLayer.Entities.Course
{
    public class CourseStatus
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [MaxLength(150)]
        public string StatusTitle { get; set; }

        #region Relations

        public List<CodeLearn.DataLayer.Entities.Course.Course> Courses { get; set; }

        #endregion

    }
}
