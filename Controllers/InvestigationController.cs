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
using System.Composition;

namespace NEMESYS.Controllers
{
    // Controller for handling investigation-related actions
    public class InvestigationController : Controller
    {
        private readonly INEMESYSRepository _nemesysRepository;
        private readonly IInvestigationRepository _investigationRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<InvestigationController> _logger;
        private readonly IEmailSender _emailSender;

        // Constructor for InvestigationController, injecting dependencies
        public InvestigationController(IInvestigationRepository repo, UserManager<ApplicationUser> userManager, INEMESYSRepository nemesysRepository, IEmailSender mail, ILogger<InvestigationController> logger, IEmailSender emailSender)
        {
            _nemesysRepository = nemesysRepository;
            _investigationRepository = repo;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        // Action for the investigation list page
        public IActionResult Index()
        {
            try
            {
                var investigations = _investigationRepository.GetAllInvestigations();
                var model = new InvestigationListViewModel()
                {
                    TotalEntries = investigations.Count(),
                    Investigations = investigations
                };
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return View("Error");
            }
        }

        // Action for the investigation details page
        public IActionResult Details(int id)
        {
            try
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
                        UpdatedDate = investigation.UpdatedDate,
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
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return View("Error");
            }
        }

        // Action for the investigation creation page (GET)
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

        // Action for the investigation creation (POST)
        [HttpPost]
        [Authorize(Roles = "Investigator")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Content")] EditInvestigationViewModel newInvestigation, [FromForm] int reportId)
        {
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

                    var callbackUrl = Url.Action("Details", "Investigation", new { id = investigation.Id }, protocol: Request.Scheme);

                    // Sending an email to the report owner to alert them of an investigation
                    _emailSender.SendEmailAsync(
                        // To
                        report.User.Email,
                        // Subject
                        "Investigation Alert!",
                        // Content
                        $"Attention {report.User.ReporterAlias}!<br>" +
                        $"Your report titled '{report.Title}' has just been investigated!<br>" +
                        $"Investigation ID: {investigation.Id}<br>" +
                        $"Go to Investigation via NEMESYS: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>{callbackUrl}</a>"
                    );
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(newInvestigation);
            }
        }

        // Action for editing an investigation (GET)
        [HttpGet]
        [Authorize(Roles = "Investigator")]
        public IActionResult Edit(int id)
        {
            var existingInv = _investigationRepository.GetInvestigationById(id);
            if (existingInv != null)
            {
                var user = _userManager.GetUserAsync(User).Result;

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
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // Action for editing an investigation (POST)
        [HttpPost]
        [Authorize(Roles = "Investigator")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, [Bind("Content")] EditInvestigationViewModel updatedInvestigation)
        {
            var modelToUpdate = _investigationRepository.GetInvestigationById(id);
            if (modelToUpdate == null)
            {
                return NotFound();
            }

            var user = _userManager.GetUserAsync(User).Result;
            var currentUserId = _userManager.GetUserId(User);
            if (modelToUpdate.UserId == currentUserId)
            {
                if (ModelState.IsValid)
                {
                    modelToUpdate.Content = updatedInvestigation.Content;
                    modelToUpdate.UserId = _userManager.GetUserId(User);

                    var report = _nemesysRepository.GetReportByInv(modelToUpdate);
                    _investigationRepository.Update(modelToUpdate);

                    var callbackUrl = Url.Action("Details", "Investigation", new { id = modelToUpdate.Id }, protocol: Request.Scheme);

                    // Sending an email to the report owner to alert them of an investigation update
                    _emailSender.SendEmailAsync(
                        // To
                        report.User.Email,
                        // Subject
                        "Investigation Update Alert!",
                        // Content
                        $"Attention {report.User.ReporterAlias}!<br>" +
                        $"Your report titled '{report.Title}' s Investigation just been updated!<br>" +
                        $"Investigation ID: {modelToUpdate.Id}<br>" +
                        $"Go to Investigation via NEMESYS: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>{callbackUrl}</a>"
                    );
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

        // Action for deleting an investigation
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
            Report report = _nemesysRepository.GetReportByInv(investigation);
            report.InvestigationId = null;
            _nemesysRepository.UpdateReportPost(report);
            _investigationRepository.Delete(investigation);
            return RedirectToAction("Index");
        }
    }
}
