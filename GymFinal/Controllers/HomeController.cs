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
        //Graph
        private readonly GymFinalContext _context;
        //Graph

        public HomeController(ILogger<HomeController> logger, GymFinalContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }



        //Session
        public IActionResult About()
        {
            ViewBag.Message = HttpContext.Session.GetString("Test");
            return View();
        }
        //Session


        public IActionResult Privacy()
        {
            return View();
        }

        //Graph
        public IActionResult Stats()
        {
            StatsViewModel dash = new StatsViewModel();
            ///dash.TrainersCount = _context.Trainers.ToList().Count();
            ////dash.UsersCount = _context.Members.Count();
            dash.Classes = _context.StudioClass.ToImmutableList();
            //added
            dash.TrainersCount = _context.Trainers.ToList().Count();
            dash.ClassesCount = _context.StudioClass.Count();
            return View(dash);
        }
        //Graph

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
