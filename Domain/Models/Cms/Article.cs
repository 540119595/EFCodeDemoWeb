namespace Domain.Models.Cms
{
    using Common.BaseDomain;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Articles")]
    public class Article : BaseEntity
    {
        [StringLength(128)]
        public string Title { get; set; }
    }
}
