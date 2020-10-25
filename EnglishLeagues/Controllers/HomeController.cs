using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EnglishLeagues.Models;
using EnglishLeagues.Services;

namespace EnglishLeagues.Controllers
{
    public class HomeController : Controller
    {
        private LeagueService _leagueService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, LeagueService service)
        {
            _logger = logger;
            _leagueService = service;
        }

        /*public IActionResult Index()
        {
            return View();
        }*/
        public IActionResult Index()
        {
            ViewBag.Message = "Hello ASP.NET Core";
            ViewBag.BestTeams = _leagueService.GetBestAttackTeams();
            ViewBag.BestDefTeams = _leagueService.GetBestDefTeams();
            ViewBag.BestStatTeams = _leagueService.GetBestStatisticTeams();
            ViewBag.MostResultDay = _leagueService.GetMostResultDay();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
