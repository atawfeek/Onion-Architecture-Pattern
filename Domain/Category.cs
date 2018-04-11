using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MLS.Domain
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
