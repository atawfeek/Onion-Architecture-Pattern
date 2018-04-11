using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MLS.Infrastructure.Data.Entities
{
    public class BookCategory
    {
        public int BookId { get; set; }

        public int CategoryId { get; set; }

        public Book Book { get; set; }

        public Category Category { get; set; }
    }
}
