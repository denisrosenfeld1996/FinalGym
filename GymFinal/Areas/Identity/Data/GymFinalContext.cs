using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymFinal.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GymFinal.Models;


namespace GymFinal.Data
{   
    public class GymFinalContext : IdentityDbContext<GymFinalUser>
    {
        public GymFinalContext(DbContextOptions<GymFinalContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<GymFinal.Models.StudioClass> StudioClass { get; set; }

        public DbSet<GymFinal.Models.Lesson> Lesson { get; set; }

        public DbSet<GymFinal.Models.Trainers> Trainers { get; set; }

        public DbSet<GymFinal.Models.Members> Members { get; set; }

        public DbSet<GymFinal.Models.StatsViewModel> StatsViewModel { get; set; }
        public object Best { get; internal set; }
    }
}
