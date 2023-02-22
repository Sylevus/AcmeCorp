using System.ComponentModel.DataAnnotations;

namespace AcmeCorps.Data.Models
{
    public partial class RolesMaster
    {
        public RolesMaster()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        [Key]
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
