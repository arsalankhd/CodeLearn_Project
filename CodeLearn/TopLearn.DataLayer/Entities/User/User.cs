﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CodeLearn.DataLayer.Entities.Course;

namespace CodeLearn.DataLayer.Entities.User
{
    public class User
    {
        public User()
        {

        }

        [Key]
        public int UserId { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string Password { get; set; }

        [Display(Name = "کد فعال سازی")]
        [MaxLength(50, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string ActiveCode { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string UserAvatar { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }

        public bool IsDelete { get; set; }

        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }

        public virtual List<Wallet.Wallet> Wallets { get; set; }

        public virtual List<CodeLearn.DataLayer.Entities.Course.Course> Courses { get; set; }

        public virtual List<CodeLearn.DataLayer.Entities.Order.Order> Orders { get; set; }
        public virtual List<UserCourse> UserCourses { get; set; }
        public virtual List<UserDiscountCode> UserDiscountCodes { get; set; }
        public virtual List<CourseComment> CourseComments { get; set; }
        public virtual List<CourseVote> CourseVotes { get; set; }
        public virtual List<Question.Question> Questions { get; set; }

        #endregion
    }
}