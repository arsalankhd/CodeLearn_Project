using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeLearn.DataLayer.Entities.Course
{
    public class UserCourse
    {
        [Key]
        public int UC_Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }

        #region Relations

        [ForeignKey("UserId")]
        public User.User User { get; set; }
        [ForeignKey("CourseId")]
        public CodeLearn.DataLayer.Entities.Course.Course Course { get; set; }

        #endregion
    }
}
