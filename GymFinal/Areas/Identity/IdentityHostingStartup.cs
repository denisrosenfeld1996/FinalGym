using System;
using GymFinal.Areas.Identity.Data;
using GymFinal.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(GymFinal.Areas.Identity.IdentityHostingStartup))]
namespace GymFinal.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<GymFinalContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("GymFinalContextConnection")));

                services.AddDefaultIdentity<GymFinalUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<GymFinalContext>();
        });
        }
    }
}