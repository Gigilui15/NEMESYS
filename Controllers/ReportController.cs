using NEMESYS.Models.Interfaces;
using NEMESYS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NEMESYS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace NEMESYS.Controllers
{
    public class ReportController : Controller
    {
        private readonly INEMESYSRepository _NEMESYSRepository;
        private readonly UserManager<IdentityUser> _userManager;

        //Using constructor dependency injection for the controller (i.e. when the controller is instnatiated, it will receive an instance of INEMESYSRepository according to the config in Program.cs
        public ReportController(INEMESYSRepository NEMESYSRepository)
        {
            _NEMESYSRepository = NEMESYSRepository;
        }

        public IActionResult Index()
        {
            //Constructing a View Model to be passed on to the view
            var blogPosts = _NEMESYSRepository.GetAllReports();
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
                    Author = new AuthorViewModel()
                    {
                        Id = b.UserId,
                        Name = (_userManager.FindByIdAsync(b.UserId).Result != null) ?
                            _userManager.FindByIdAsync(b.UserId).Result.UserName :
                            "Anonymous"
                    }
                })
            };

            return View(model);


        }

        public IActionResult Details(int id)
        {
            var blogPost = _NEMESYSRepository.GetReportById(id);
            if (blogPost == null)
                return NotFound();
            else
            {
                var model = new ReportPostViewModel()
                {
                    Id = blogPost.Id,
                    CreatedDate = blogPost.CreatedDate,
                    ImageUrl = blogPost.ImageUrl,
                    Title = blogPost.Title,
                    Content = blogPost.Content,
                    Category = new CategoryViewModel()
                    {
                        Id = blogPost.Category.Id,
                        Name = blogPost.Category.Name
                    },
                    Author = new AuthorViewModel()
                    {
                        Id = blogPost.UserId,
                        Name = (_userManager.FindByIdAsync(blogPost.UserId).Result != null) ?
                            _userManager.FindByIdAsync(blogPost.UserId).Result.UserName :
                            "Anonymous"
                    }
                };

                return View(model);
            }

        }

            [HttpGet]
            [Authorize]
            public IActionResult Create()
            {
                //Load all categories and create a list of CategoryViewModel
                var categoryList = _NEMESYSRepository.GetAllCategories().Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

                //Pass the list into an EditReportPostViewModel, which is used by the View (all other properties may be left blank, unless you want to add other default values
                var model = new EditReportPostViewModel()
                {
                    CategoryList = categoryList
                };

                //Pass model to View
                return View(model);
            }

            [HttpPost]
            [Authorize]
            [ValidateAntiForgeryToken]
            public IActionResult Create([Bind("Title, Content, ImageToUpload, CategoryId")] EditReportPostViewModel newReportPost)
            {
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (newReportPost.ImageToUpload != null)
                {
                    //At this point you should check size, extension etc...
                    //Then persist using a new name for consistency (e.g. new Guid)
                    var extension = "." + newReportPost.ImageToUpload.FileName.Split('.')[newReportPost.ImageToUpload.FileName.Split('.').Length - 1];
                    fileName = Guid.NewGuid().ToString() + extension;
                    var path = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\blogposts\\" + fileName;
                    using (var bits = new FileStream(path, FileMode.Create))
                    {
                        newReportPost.ImageToUpload.CopyTo(bits);
                    }
                }

                Report reportPost = new Report()
                {
                    Title = newReportPost.Title,
                    Content = newReportPost.Content,
                    CreatedDate = DateTime.UtcNow,
                    ImageUrl = "/images/blogposts/" + fileName,
                    CategoryId = newReportPost.CategoryId,
                    UserId = _userManager.GetUserId(User)
                };

                _NEMESYSRepository.CreateReportPost(reportPost);
                return RedirectToAction("Index");
            }
            else
            {
                //Load all categories and create a list of CategoryViewModel
                var categoryList = _NEMESYSRepository.GetAllCategories().Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

                //Re-attach to view model before sending back to the View (this is necessary so that the View can repopulate the drop down and pre-select according to the CategoryId
                newReportPost.CategoryList = categoryList;

                return View(newReportPost);
            }


        }

            [HttpGet]
            [Authorize]
            public IActionResult Edit(int id)
            {
                var existingReportPost = _NEMESYSRepository.GetReportById(id);
                if (existingReportPost != null)
                {
                    //Checking for ownership
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

                    //Load all categories and create a list of CategoryViewModel
                    var categoryList = _NEMESYSRepository.GetAllCategories().Select(c => new CategoryViewModel()
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList();

                    //Attach to view model - view will pre-select according to the value in CategoryId
                    model.CategoryList = categoryList;

                        return View(model);
                    }
                    else
                        return RedirectToAction("Index");  //or return Forbid()
                }
                else
                    return RedirectToAction("Index");
            }

            [HttpPost]
            [Authorize]
            [ValidateAntiForgeryToken]
            public IActionResult Edit([FromRoute] int id, [Bind("Id, Title, Content, ImageToUpload, CategoryId")] EditReportPostViewModel updatedReportPost)
            {
                var modelToUpdate = _NEMESYSRepository.GetReportById(id);
                if (modelToUpdate == null)
                {
                    return NotFound();
                }

            //Checking for ownership
            var currentUserId = _userManager.GetUserId(User);
            if (modelToUpdate.UserId == currentUserId)
            {

                if (ModelState.IsValid)
                {
                    string imageUrl = "";

                    if (updatedReportPost.ImageToUpload != null)
                    {
                        string fileName = "";
                        //At this point you should check size, extension etc...
                        //Then persist using a new name for consistency (e.g. new Guid)
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
                    //Load all categories and create a list of CategoryViewModel
                    var categoryList = _NEMESYSRepository.GetAllCategories().Select(c => new CategoryViewModel()
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList();

                    //Re-attach to view model before sending back to the View (this is necessary so that the View can repopulate the drop down and pre-select according to the CategoryId
                    updatedReportPost.CategoryList = categoryList;

                    return View(updatedReportPost);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
