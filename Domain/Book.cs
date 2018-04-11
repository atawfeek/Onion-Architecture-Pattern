using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MLS.Domain
{
    public class Book : BaseEntity
    {
        public string Description { get; set; }
        public string Isbn13 { get; set; }
        public string ExternalId { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string TableOfContents { get; set; }
        public int NumberOfPages { get; set; }
        public string Subtitle { get; set; }
        public string PublicationDate { get; set; }
        public List<Author> Authors { get; set; }
        public List<Category> Categories { get; set; }
        public string Publisher { get; set; }
        public string Edition { get; set; }
        public string ImageUrl { get; set; }
        public Boolean HasImage { get; set; }

        public Book()
        {
            ImageUrl = string.Empty;
        }
    }
}
