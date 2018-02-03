namespace Domain.Models.Sys
{
    using Common.BaseDomain;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Users")]
    public partial class User : BaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [StringLength(128)]
        public string TrueName { get; set; }

        /// <summary>
        /// 盐
        /// </summary>
        [StringLength(16)]
        public string Salt { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(128)]
        public string Pwd { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [StringLength(128)]
        public string Email { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [StringLength(64)]
        public string Phone { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; } = true;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; } = false;

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreatTime { get; set; } = System.DateTime.Now;

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public System.DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int UType { get; set; }

        //================================以下是做表关联================================//

        /// <summary>
        /// 用户附加信息
        /// </summary>
        [ForeignKey("UserId")]
        public virtual UserInfo UserInfos { get; set; }

        /// <summary>
        /// 用户组Id
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// 用户分组
        /// </summary>
        public virtual UserGroup UserGroup { get; set; }

        public virtual System.Collections.Generic.ICollection<UserRole> UserRoles { get; set; } = new System.Collections.Generic.HashSet<UserRole>();
        //public virtual System.Collections.Generic.ICollection<UserInfo> UserInfos { get; set; }
    }
}
