using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeLearn.DataLayer.Entities.Question
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string BodyAnswer { get; set; }
        public bool IsTrue { get; set; } = false;
        [Required]
        public DateTime CreateDate { get; set; }

        #region Relations

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        [ForeignKey("UserId")]
        public User.User User { get; set; }

        #endregion
    }
}
