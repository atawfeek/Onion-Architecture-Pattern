using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MLS.Domain
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
