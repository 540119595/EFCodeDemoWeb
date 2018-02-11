namespace Domain.Models.Sys
{
    using Common.BaseDomain;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Roles")]
    public partial class Role : BaseEntity
    {
        /// <summary>
        /// 角色父编号
        /// </summary>
        [StringLength(64)]
        public string PId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [StringLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(1024)]
        public string Description { get; set; }

        public virtual System.Collections.Generic.ICollection<UserRole> UserRoles { get; set; } = new System.Collections.Generic.HashSet<UserRole>();
        public virtual System.Collections.Generic.ICollection<RolePerm> RolePerms { get; set; } = new System.Collections.Generic.HashSet<RolePerm>();
    }
}
