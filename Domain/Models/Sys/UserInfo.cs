namespace Domain.Models.Sys
{
    using Common.BaseDomain;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserInfos")]
    public class UserInfo : BaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(64)]
        public string IDCard { get; set; }

        [StringLength(256)]
        public string Address { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        [StringLength(64)]
        public string QQ { get; set; }

        /// <summary>
        /// 关联EAS标识
        /// </summary>
        [StringLength(64)]
        public string LinkEAS { get; set; }

        /// <summary>
        /// 关联微信标识
        /// </summary>
        [StringLength(64)]
        public string LinkWeChat { get; set; }

        /// <summary>
        /// 关联QQ标识
        /// </summary>
        [StringLength(64)]
        public string LinkQQ { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(1024)]
        public string Description { get; set; }

        public virtual User User { get; set; }
    }
}
