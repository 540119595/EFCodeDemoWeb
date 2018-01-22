namespace Domain.Models.Sys
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserInfos")]
    public class UserInfo : BaseEntity
    {
        [StringLength(128)]
        public string Address { get; set; }
    }
}
