using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MLS.Infrastructure.Data.Entities
{
    public class BookAuthor
    {
        public int BookId { get; set; }

        public int AuthorId { get; set; }

        public Book Book { get; set; }

        public Author Author { get; set; }
    }
}
