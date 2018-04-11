using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MLS.Domain
{
    // Add profile data for application users by adding properties to this class
    public class AppUser : IdentityUser
    {
        // Extended Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
    }
}
