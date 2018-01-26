namespace Domain.Models.Sys
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PermGroups")]
    public partial class PermGroup : BaseEntity
    {
        /// <summary>
        /// 父编号
        /// </summary>
        [StringLength(64)]
        public string PId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(64)]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(128)]
        public string Name { get; set; }
        
        public virtual System.Collections.Generic.ICollection<PermItem> PermItems { get; set; } = new System.Collections.Generic.HashSet<PermItem>();
    }
}
