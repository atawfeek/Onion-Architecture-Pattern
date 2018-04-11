using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MLS.Domain
{
    public class User : BaseEntity
    {
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
    }
}
