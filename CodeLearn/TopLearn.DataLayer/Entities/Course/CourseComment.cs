using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeLearn.DataLayer.Entities.Course
{
    public class CourseComment
    {
        [Key]
        public int CommentId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        [MaxLength(700)]
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsAdminRead { get; set; }

        #region Relations

        [ForeignKey("UserId")]
        public virtual User.User User { get; set; }
        [ForeignKey("CourseId")]
        public virtual CodeLearn.DataLayer.Entities.Course.Course Course { get; set; }

        #endregion
    }
}
