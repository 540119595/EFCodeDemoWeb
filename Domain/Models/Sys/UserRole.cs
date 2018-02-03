namespace Domain.Models.Sys
{
    using Common.BaseDomain;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserRoles")]
    public partial class UserRole : BaseEntity
    {
        [ForeignKey("Users")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("Roles")]
        public string RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
