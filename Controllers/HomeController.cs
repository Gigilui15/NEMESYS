using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NEMESYS.Models;
using NEMESYS.Models.Interfaces;
using NEMESYS.ViewModels;
using System.Diagnostics;

namespace NEMESYS.Controllers
{
    // Home controller for handling home-related actions
    public class HomeController : Controller
    {
        private readonly INEMESYSRepository _NEMESYSRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ReportController> _logger;

        // Constructor for HomeController, injecting dependencies
        public HomeController(
            INEMESYSRepository NEMESYSRepository,
            UserManager<ApplicationUser> userManager,
            ILogger<ReportController> logger)
        {
            _NEMESYSRepository = NEMESYSRepository;
            _userManager = userManager;
            _logger = logger;
        }

        // Action for the home page
        public IActionResult Index()
        {
            return View();
        }

        // Action for the about page
        public IActionResult About()
        {
            return View();
        }

        // Action for the dashboard page, accessible only by users with the "Investigator" role
        [HttpGet]
        [Authorize(Roles = "Investigator")]
        public IActionResult Dashboard()
        {
            try
            {
                ViewBag.Title = "Report Dashboard";

                var model = new ReportDashboardViewModel();
                model.TotalRegisteredUsers = _userManager.Users.Count();
                model.TotalEntries = _NEMESYSRepository.GetAllReports().Count();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }

        // Action for the investigations page
        public IActionResult Investigations()
        {
            return View();
        }

        // Action for the Hall of Fame page
        public IActionResult HallOfFame()
        {
            return View();
        }
    }
}
