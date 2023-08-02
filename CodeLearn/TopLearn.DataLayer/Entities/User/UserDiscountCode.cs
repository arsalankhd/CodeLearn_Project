using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeLearn.DataLayer.Entities.Order;

namespace CodeLearn.DataLayer.Entities.User
{
    public class UserDiscountCode
    {
        [Key]
        public int UD_Id { get; set; }
        public int UserId { get; set; }
        public int DiscountId { get; set; }

        #region Relations

        [ForeignKey("UserId")]
        public virtual CodeLearn.DataLayer.Entities.User.User User { get; set; }
        [ForeignKey("DiscountId")]
        public virtual Discount Discount { get; set; }

        #endregion
    }
}
