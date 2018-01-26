namespace Domain.Models.Sys
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserGroups")]
    public class UserGroup : BaseEntity
    {
        /// <summary>
        /// 父编号
        /// </summary>
        public string PId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }


        public virtual System.Collections.Generic.ICollection<User> Users { get; set; } = new System.Collections.Generic.HashSet<User>();
    }
}
