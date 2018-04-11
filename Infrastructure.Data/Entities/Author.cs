using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MLS.Infrastructure.Data.Entities
{
    public class Author : BaseEntity
    {
        [MaxLength(1000)]
        public string Name { get; set; }

        public virtual ICollection<BookAuthor> Books { get; set; }
    }
}
