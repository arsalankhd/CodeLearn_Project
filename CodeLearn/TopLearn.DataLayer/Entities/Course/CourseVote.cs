using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeLearn.DataLayer.Entities.Course
{
    public class CourseVote
    {
        [Key]
        public int VoteId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public bool Vote { get; set; }
        public DateTime VoteDate { get; set; } = DateTime.Now;

        #region Relations

        [ForeignKey("CourseId")]
        public CodeLearn.DataLayer.Entities.Course.Course Course { get; set; }
        [ForeignKey("UserId")]
        public User.User User { get; set; }

        #endregion
    }
}
