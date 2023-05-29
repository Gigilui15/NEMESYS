using NEMESYS.Models.Interfaces;
using NEMESYS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NEMESYS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace NEMESYS.Controllers
{
    public class ReportController : Controller
    {
        private readonly INEMESYSRepository _NEMESYSRepository;
        private readonly IInvestigationRepository _IInvestigationRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ReportController> _logger;

        // Using constructor dependency injection for the controller (i.e., when the controller is instantiated, it will receive an instance of INEMESYSRepository according to the config in Program.cs)
        public ReportController(INEMESYSRepository NEMESYSRepository, UserManager<ApplicationUser> userManager, ILogger<ReportController> logger)
        {
            _NEMESYSRepository = NEMESYSRepository;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                // Constructing a View Model to be passed on to the view
                var reportPosts = _NEMESYSRepository.GetAllReports();
                var model = new ReportPostListViewModel()
                {
                    TotalEntries = _NEMESYSRepository.GetAllReports().Count(),
                    ReportPosts = _NEMESYSRepository
                        .GetAllReports()
                        .OrderByDescending(b => b.CreatedDate)
                        .Select(b => new ReportPostViewModel
                        {
                            Id = b.Id,
                            CreatedDate = b.CreatedDate,
                            Content = b.Content,
                            ImageUrl = b.ImageUrl,
                            Title = b.Title,
                            Category = new CategoryViewModel()
                            {
                                Id = b.Category.Id,
                                Name = b.Category.Name
                            },
                            Reporter = new ReporterViewModel()
                            {
                                Id = b.UserId,
                                Name = (_userManager.FindByIdAsync(b.UserId).Result != null) ?
                                    _userManager.FindByIdAsync(b.UserId).Result.UserName :
                                    "Anonymous"
                            },
                            InvestigationId = b.InvestigationId ?? 0
                        })
                };
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }



        [Authorize]
        public IActionResult Details(int id)
        {
            try
            {
                var reportPost = _NEMESYSRepository.GetReportById(id);
                if (reportPost == null)
                    return NotFound();
                else
                {
                    var model = new ReportPostViewModel()
                    {
                        Id = reportPost.Id,
                        CreatedDate = reportPost.CreatedDate,
                        ImageUrl = reportPost.ImageUrl,
                        Title = reportPost.Title,
                        Content = reportPost.Content,
                        Category = new CategoryViewModel()
                        {
                            Id = reportPost.Category.Id,
                            Name = reportPost.Category.Name
                        },
                        Reporter = new ReporterViewModel()
                        {
                            Id = reportPost.UserId,
                            Name = (_userManager.FindByIdAsync(reportPost.UserId).Result != null) ?
                                _userManager.FindByIdAsync(reportPost.UserId).Result.UserName :
                                "Anonymous"
                        },
                        InvestigationId = reportPost.InvestigationId ?? 0
                    };

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize]

        public IActionResult Create()
        {
            try
            {
                // Load all categories and create a list of CategoryViewModel
                var categoryList = _NEMESYSRepository.GetAllCategories().Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

                // Pass the list into an EditReportPostViewModel, which is used by the View (all other properties may be left blank, unless you want to add other default values)
                var model = new EditReportPostViewModel()
                {
                    CategoryList = categoryList
                };

                // Pass model to View
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title, Content, ImageToUpload, CategoryId")] EditReportPostViewModel newReportPost)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    string fileName = "";
                    if (newReportPost.ImageToUpload != null)
                    {
                        var extension = "." + newReportPost.ImageToUpload.FileName.Split('.')[newReportPost.ImageToUpload.FileName.Split('.').Length - 1];
                        fileName = Guid.NewGuid().ToString() + extension;
                        var path = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\reports\\" + fileName;
                        using (var bits = new FileStream(path, FileMode.Create))
                        {
                            newReportPost.ImageToUpload.CopyTo(bits);
                        }
                    }

                    Report report = new Report()
                    {
                        Title = newReportPost.Title,
                        Content = newReportPost.Content,
                        CreatedDate = DateTime.UtcNow,
                        ImageUrl = "/images/reports/" + fileName,
                        CategoryId = newReportPost.CategoryId,
                        UserId = _userManager.GetUserId(User),
                    };

                    _NEMESYSRepository.CreateReportPost(report);
                    return RedirectToAction("Index");
                }
                else
                {
                    // Load all categories and create a list of CategoryViewModel
                    var categoryList = _NEMESYSRepository.GetAllCategories().Select(c => new CategoryViewModel()
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList();

                    // Re-attach to view model before sending back to the View (this is necessary so that the View can repopulate the drop-down and pre-select according to the CategoryId)
                    newReportPost.CategoryList = categoryList;

                    return View(newReportPost);
                }
            }
            catch (DbUpdateException e)
            {
                SqlException s = e.InnerException as SqlException;
                if (s != null)
                {
                    switch (s.Number)
                    {
                        case 547: //foreign key constraint
                            {
                                ModelState.AddModelError(string.Empty, string.Format("Foreign key for category with Id '{0}' does not exists.", newReportPost.CategoryId));
                                break;
                            }
                        default:
                            {
                                ModelState.AddModelError(string.Empty, "A database error occured - please contact your system administrator.");
                                break;
                            }
                    }
                    var categoryList = _NEMESYSRepository.GetAllCategories().Select(c => new CategoryViewModel()
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList();

                    //Re-attach to view model before sending back to the View (this is necessary so that the View can repopulate the drop down and pre-select according to the CategoryId
                    newReportPost.CategoryList = categoryList;
                }
                return View(newReportPost);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");

            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            try
            {


                var existingReportPost = _NEMESYSRepository.GetReportById(id);
                if (existingReportPost != null)
                {
                    // Checking for ownership
                    var currentUserId = _userManager.GetUserId(User);
                    if (existingReportPost.UserId == currentUserId)
                    {

                        EditReportPostViewModel model = new EditReportPostViewModel()
                        {
                            Id = existingReportPost.Id,
                            Title = existingReportPost.Title,
                            Content = existingReportPost.Content,
                            ImageUrl = existingReportPost.ImageUrl,
                            CategoryId = existingReportPost.CategoryId
                        };

                        // Load all categories and create a list of CategoryViewModel
                        var categoryList = _NEMESYSRepository.GetAllCategories().Select(c => new CategoryViewModel()
                        {
                            Id = c.Id,
                            Name = c.Name
                        }).ToList();

                        // Attach to view model - view will pre-select according to the value in CategoryId
                        model.CategoryList = categoryList;

                        return View(model);
                    }
                    else
                        return RedirectToAction("Index");  // or return Forbid()
                }
                else
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, [Bind("Id, Title, Content, ImageToUpload, CategoryId")] EditReportPostViewModel updatedReportPost)
        {
            try
            {

                var modelToUpdate = _NEMESYSRepository.GetReportById(id);
                if (modelToUpdate == null)
                {
                    return NotFound();
                }

                // Checking for ownership
                var currentUserId = _userManager.GetUserId(User);
                if (modelToUpdate.UserId == currentUserId)
                {

                    if (ModelState.IsValid)
                    {
                        string imageUrl = "";

                        if (updatedReportPost.ImageToUpload != null)
                        {
                            string fileName = "";
                            // At this point, you should check size, extension, etc.
                            // Then persist using a new name for consistency (e.g., new Guid)
                            var extension = "." + updatedReportPost.ImageToUpload.FileName.Split('.')[updatedReportPost.ImageToUpload.FileName.Split('.').Length - 1];
                            fileName = Guid.NewGuid().ToString() + extension;
                            var path = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\blogposts\\" + fileName;
                            using (var bits = new FileStream(path, FileMode.Create))
                            {
                                updatedReportPost.ImageToUpload.CopyTo(bits);
                            }
                            imageUrl = "/images/blogposts/" + fileName;
                        }
                        else
                            imageUrl = modelToUpdate.ImageUrl;

                        modelToUpdate.Title = updatedReportPost.Title;
                        modelToUpdate.Content = updatedReportPost.Content;
                        modelToUpdate.ImageUrl = imageUrl;
                        modelToUpdate.UpdatedDate = DateTime.Now;
                        modelToUpdate.CategoryId = updatedReportPost.CategoryId;
                        modelToUpdate.UserId = _userManager.GetUserId(User);

                        _NEMESYSRepository.UpdateReportPost(modelToUpdate);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Load all categories and create a list of CategoryViewModel
                        var categoryList = _NEMESYSRepository.GetAllCategories().Select(c => new CategoryViewModel()
                        {
                            Id = c.Id,
                            Name = c.Name
                        }).ToList();

                        // Re-attach to view model before sending back to the View (this is necessary so that the View can repopulate the drop-down and pre-select according to the CategoryId)
                        updatedReportPost.CategoryList = categoryList;

                        return View(updatedReportPost);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                var report = _NEMESYSRepository.GetReportById(id);
                string userId = _userManager.GetUserId(User);

                if (report == null)
                {
                    return NotFound();
                }
                else
                {
                    if (report.UserId == userId)
                    {
                        if (report.InvestigationId == 0) _IInvestigationRepository.Delete((int)report.InvestigationId);
                        _NEMESYSRepository.Delete(report);
                    }
                }
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {

                _logger.LogError(e.Message);
                return View("Error");
            }
        }
    }
}
