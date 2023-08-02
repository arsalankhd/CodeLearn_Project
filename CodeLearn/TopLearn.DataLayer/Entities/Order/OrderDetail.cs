using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeLearn.DataLayer.Entities.Order
{
    public class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int Price { get; set; }


        #region Relations

        [ForeignKey("OrderId")]
        public virtual CodeLearn.DataLayer.Entities.Order.Order Order { get; set; }
        [ForeignKey("CourseId")]
        public virtual CodeLearn.DataLayer.Entities.Course.Course Course { get; set; }

        #endregion
    }
}
