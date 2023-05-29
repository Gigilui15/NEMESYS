using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NEMESYS.Models.Interfaces;
using NEMESYS.Models;
using NEMESYS.ViewModels;
using System.Data;
using System.Text.Encodings.Web;
using NEMESYS.Services;

namespace NEMESYS.Controllers
{
    public class InvestigationController : Controller
    {
        private readonly INEMESYSRepository _nemesysRepository;
        private readonly IInvestigationRepository _investigationRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public InvestigationController(IInvestigationRepository repo, UserManager<ApplicationUser> userManager, INEMESYSRepository nemesysRepository, IEmailSender mail, ILogger<InvestigationController> logger)
        { 
            _nemesysRepository = nemesysRepository;
            _investigationRepository = repo;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var investigations = _investigationRepository.GetAllInvestigations();
            var model = new InvestigationListViewModel()
            {
                TotalEntries = investigations.Count(),
                Investigations = investigations
            };
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var investigation = _investigationRepository.GetInvestigationById(id);
            if (investigation == null)
                return NotFound();
            else
            {
                var model = new InvestigationViewModel()
                {
                    Id = investigation.Id,
                    CreatedDate = investigation.CreatedDate,
                    UpdatedDate= investigation.UpdatedDate,
                    Description = investigation.Content,
                    User = new UserViewModel
                    {
                        Id = investigation.UserId,
                        Name = (_userManager.FindByIdAsync(investigation.UserId).Result != null) ? _userManager.FindByIdAsync(investigation.UserId).Result.UserName : "Anonymous"
                    },
                };

                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Investigator")]
        public IActionResult Create(int reportId)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var roles = _userManager.GetRolesAsync(user).Result;

            ViewBag.ReportId = reportId;
            var model = new EditInvestigationViewModel();
            return View(model);

        }

        [HttpPost]
        [Authorize(Roles = "Investigator")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Content")] EditInvestigationViewModel newInvestigation, [FromForm] int reportId)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var roles = _userManager.GetRolesAsync(user).Result;

            if (ModelState.IsValid)
            {
                Investigation investigation = new Investigation()
                {

                    Content = newInvestigation.Content,
                    CreatedDate = DateTime.UtcNow,
                    UserId = _userManager.GetUserId(User),
                    UpdatedDate = DateTime.UtcNow,

                };
                var report = _nemesysRepository.GetReportById(reportId);

                if (report == null)
                {
                    return NotFound();
                }


                else
                {
                    _investigationRepository.Create(investigation);
                    report.InvestigationId = investigation.Id;
                    _nemesysRepository.UpdateReportPost(report);

                    var callbackUrl = Url.Page(
                        "/Investigation/Details",
                        pageHandler: null,
                        values: new { id = investigation.Id },
                        protocol: Request.Scheme);

                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(newInvestigation);
            }



        }
        [HttpGet]
        [Authorize(Roles = "Investigator")]
        public IActionResult Edit(int id)
        {
            var existingInv = _investigationRepository.GetInvestigationById(id);
            if (existingInv != null)
            {
                var user = _userManager.GetUserAsync(User).Result;
                var roles = _userManager.GetRolesAsync(user).Result;

                var currentUserId = _userManager.GetUserId(User);
                if (existingInv.UserId == currentUserId)
                {
                    EditInvestigationViewModel model = new EditInvestigationViewModel()
                    {
                        Id = existingInv.Id,
                        Content = existingInv.Content,
                    };
                    return View(model);
                }
                else
                    return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");

        }

        [HttpPost]
        [Authorize(Roles = "Investigator")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, [Bind("Content")]
        EditInvestigationViewModel updatedInvestigation)
        {
            var modelToUpdate = _investigationRepository.GetInvestigationById(id);
            if (modelToUpdate == null)
            {
                return NotFound();
            }

            var user = _userManager.GetUserAsync(User).Result;
            var roles = _userManager.GetRolesAsync(user).Result;

            var currentUserId = _userManager.GetUserId(User);
            if (modelToUpdate.UserId == currentUserId)
            {

                if (ModelState.IsValid)
                {
                    modelToUpdate.Content = updatedInvestigation.Content;
                    modelToUpdate.UserId = _userManager.GetUserId(User);


                    _investigationRepository.Update(modelToUpdate);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(updatedInvestigation);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }


        }

        public IActionResult Delete(int id)
        {
            var investigation = _investigationRepository.GetInvestigationById(id);
            string userId = _userManager.GetUserId(User);

            if (investigation == null)
            {
                return NotFound();
            }

            if (investigation.UserId != userId)
            {
                return Forbid();
            }

            _investigationRepository.Delete(investigation);

            return RedirectToAction("Index");
        }


    }
}

