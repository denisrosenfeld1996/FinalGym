using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GymFinal.Data;
using GymFinal.Models;
using Microsoft.AspNetCore.Identity;

namespace GymFinal.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the BariiLiiiUser class
    public class GymFinalUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }

        ////denis
        //public Dictionary<string, int> mapOfClass { get; set; }
        //public string bestClasses { get; set; }
    }
}
