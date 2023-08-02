using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeLearn.DataLayer.Entities.Question
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Display(Name = "عنوان سوال")]
        [Required(ErrorMessage = "عنوان سوال را وارد کنید")]
        [MaxLength(400)]
        public string Title { get; set; }
        [Display(Name = "متن سوال")]
        [Required(ErrorMessage = "متن سوال را وارد کنید")]
        public string Body { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }

        #region Relations

        [ForeignKey("UserId")]
        public virtual User.User User { get; set; }
        [ForeignKey("CourseId")]
        public virtual CodeLearn.DataLayer.Entities.Course.Course Course { get; set; }
        public virtual List<Answer> Answers { get; set; }

        #endregion
    }
}
