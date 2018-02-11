namespace Domain.Models.Sys
{
    using Common.BaseDomain;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PermItems")]
    public partial class PermItem : BaseEntity
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Descript { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RouteUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// 分组编号
        /// </summary>
        public string GroupId { get; set; }

        public virtual PermGroup PermGroup { get; set; }

        public virtual System.Collections.Generic.ICollection<RolePerm> RolePerms { get; set; } = new System.Collections.Generic.HashSet<RolePerm>();
    }
}
