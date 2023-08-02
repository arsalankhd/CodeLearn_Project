using System;
using System.ComponentModel.DataAnnotations;

namespace CodeLearn.Core.DTOs.Users
{
    public class ChargeWalletViewModel
    {
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }
    }

    public class WalletViewModel
    {
        public int Amount { get; set; }
        public int Type { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
    }
}
