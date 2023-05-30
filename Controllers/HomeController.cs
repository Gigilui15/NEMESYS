using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NEMESYS.Models;
using NEMESYS.Models.Interfaces;
using NEMESYS.ViewModels;
using System.Diagnostics;

namespace NEMESYS.Controllers
{


    public class HomeController : Controller
    {

        private readonly INEMESYSRepository _NEMESYSRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ReportController> _logger;

        public HomeController(

            INEMESYSRepository NEMESYSRepository,
            UserManager<ApplicationUser> userManager,
            ILogger<ReportController> logger)
        {
            _NEMESYSRepository = NEMESYSRepository;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

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

        public IActionResult Investigations()
        {
            return View();
        }

        public IActionResult HallOfFame()
        {
            return View();
        }
    }
}