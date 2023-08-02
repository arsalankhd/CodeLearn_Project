using System.ComponentModel.DataAnnotations;

namespace CodeLearn.DataLayer.Entities.User
{
    public class UserRole
    {
        public UserRole()
        {

        }

        [Key]
        public int UR_Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        #region Relations

        public virtual CodeLearn.DataLayer.Entities.User.User User { get; set; }
        public virtual Role Role { get; set; }

        #endregion
    }
}
