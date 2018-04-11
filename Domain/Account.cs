using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLS.Domain
{
    [Table("Account")]
    public class Account : BaseEntity
    {
        public string AccountName { get; set; }
        public string Email { get; set; }

        public IList<User> Users { get; set; }
    }
}
