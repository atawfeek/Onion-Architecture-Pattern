using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MLS.Infrastructure.Data.Entities
{
    public class Category : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<BookCategory> Books { get; set; }
    }
}
