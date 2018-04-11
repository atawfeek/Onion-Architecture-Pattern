using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MLS.Infrastructure.Data.Entities
{
    public class User : BaseEntity
    {
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; } // navigation property
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }  // navigation property
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
    }
}
