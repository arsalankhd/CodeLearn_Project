using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CodeLearn.DataLayer.Entities.User;

namespace CodeLearn.DataLayer.Entities.Order
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }
        [Display(Name = "کد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد.")]
        public string DiscountCode { get; set; }
        [Display(Name = "درصد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int DiscountPercent { get; set; }
        public int? UsableCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        #region Relations

        public virtual List<UserDiscountCode> UserDiscountCodes { get; set; }

        #endregion
    }
}
