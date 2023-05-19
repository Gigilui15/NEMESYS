﻿using NEMESYS.Models.Interfaces;
using NEMESYS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NEMESYS.Models;

namespace NEMESYS.Controllers
{
    public class ReportController : Controller
    {
        private readonly INEMESYSRepository _NEMESYSRepository;

        //Using constructor dependency injection for the controller (i.e. when the controller is instnatiated, it will receive an instance of INEMESYSRepository according to the config in Program.cs
        public ReportController(INEMESYSRepository NEMESYSRepository)
        {
            _NEMESYSRepository = NEMESYSRepository;
        }

        public IActionResult Index()
        {
            //Constructing a View Model to be passed on to the view
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
                    }
                })
            };
            return View(model);
        }

            public IActionResult Details(int id)
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
                        }
                    };

                    return View(model);
                }

            }

            [HttpGet]
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
                        CategoryId = newReportPost.CategoryId
                    };

                    _NEMESYSRepository.CreateReportPost(report);
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
            public IActionResult Edit(int id)
            {
                var existingReportPost = _NEMESYSRepository.GetReportById(id);
                if (existingReportPost != null)
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
                    return RedirectToAction("Index");
            }

            [HttpPost]
            public IActionResult Edit([FromRoute] int id, [Bind("Id, Title, Content, ImageToUpload, CategoryId")] EditReportPostViewModel updatedReportPost)
            {
                var modelToUpdate = _NEMESYSRepository.GetReportById(id);
                if (modelToUpdate == null)
                {
                    return NotFound();
                }

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
                        var path = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\reports\\" + fileName;
                        using (var bits = new FileStream(path, FileMode.Create))
                        {
                        updatedReportPost.ImageToUpload.CopyTo(bits);
                        }
                        imageUrl = "/images/reports/" + fileName;
                    }
                    else
                        imageUrl = modelToUpdate.ImageUrl;

                    modelToUpdate.Title = updatedReportPost.Title;
                    modelToUpdate.Content = updatedReportPost.Content;
                    modelToUpdate.ImageUrl = imageUrl;
                    modelToUpdate.UpdatedDate = DateTime.Now;
                    modelToUpdate.CategoryId = updatedReportPost.CategoryId;

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
    }
}