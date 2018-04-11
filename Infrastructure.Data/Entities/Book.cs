using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MLS.Infrastructure.Data.Entities
{
    public class Book : BaseEntity
    {
        [ConcurrencyCheck]
        public string Description { get; set; }

        [MinLength(13)]
        [MaxLength(13)]
        [Required]
        public string ISBN13 { get; set; }
        public string ExternalId { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }
        public float Price { get; set; }
        public string TableOfContents { get; set; }
        public int NumberOfPages { get; set; }
        public Boolean HasImage { get; set; }

        [MaxLength(500)]
        public string Subtitle { get; set; }
        public string PublicationDate { get; set; }
        public virtual ICollection<BookAuthor> Authors { get; set; }
        public string Publisher { get; set; }
        public string Edition { get; set; }
        public virtual ICollection<BookCategory> Categories { get; set; }

        public Book()
        {
            HasImage = true;
        }
    }
}
