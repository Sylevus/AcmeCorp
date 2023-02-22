using System.ComponentModel.DataAnnotations;

namespace AcmeCorps.Data.Models
{
    public partial class UserRoles
    {

        [Key]
        public long UserRolesId { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public virtual RolesMaster Role { get; set; }
        public virtual UsersMaster User { get; set; }
    }
}
