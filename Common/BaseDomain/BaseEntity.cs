namespace Common.BaseDomain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class BaseEntity : IBaseEntity<string>
    {
        private string _id = "";

        [Key]
        [Column("Id")]
        [StringLength(64)]
        public virtual string Id
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_id))
                {
                    //string tmp = Guid.NewGuid().ToString("N") + GetType().Name;
                    string tmp = DateTime.Now.ToString("yyMMddHHmmssffff") + this.GetType().Name;
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(tmp);
                    string base64 = Convert.ToBase64String(bytes);
                    _id = base64.Replace("/", "_").Replace("+", "-");
                }
                return _id;
            }
            set
            {
                _id = value;
            }
        }
    }
}
