using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
