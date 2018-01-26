namespace Domain.Models.Sys
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RolePerms")]
    public partial class RolePerm : BaseEntity
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleId { get; set; }

        public virtual Role Role { get; set; }

        /// <summary>
        /// 权限编号
        /// </summary>
        public string PermId { get; set; }
        
        public virtual PermItem PermItem { get; set; }
    }
}
