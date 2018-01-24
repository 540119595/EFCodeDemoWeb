namespace Domain.Models.Sys
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserInfos")]
    public class UserInfo : BaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        [StringLength(128)]
        public string Address { get; set; }
        
        public User User { get; set; }
    }
}
