


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GymFinal.Models;
using Microsoft.AspNetCore.NodeServices;
using Newtonsoft.Json;
using GymFinal.Data;
using System.Collections.Immutable;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace GymFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GymFinalContext _context;
        public HomeController(ILogger<HomeController> logger, GymFinalContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewBag.Message = HttpContext.Session.GetString("Test");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //Graph
        public IActionResult Stats()
        {
            StatsViewModel dash = new StatsViewModel();
            var TypesCount = _context.StudioClass
              .GroupBy(p => p.Type)
              .Select(g => new
              {
                  Type = g.Key,
                  Count = g.Count()
              }).ToList();
            dash.types = TypesCount.Count();
            dash.Classes = _context.StudioClass.ToImmutableList();
            dash.TrainersCount = _context.Trainers.ToList().Count();
            dash.ClassesCount = _context.StudioClass.Count();
            return View(dash);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
