using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLS.Infrastructure.Data.Entities
{
    [Table("Account")]
    public class Account : BaseEntity
    {
        public Account()
        {
            this.Users = new HashSet<User>();
        }

        [Required]
        [StringLength(500)]
        public string AccountName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
