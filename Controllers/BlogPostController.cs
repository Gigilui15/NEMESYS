using NEMESYS.Models.Interfaces;
using NEMESYS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace NEMESYS.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly INEMESYSRepository _NEMESYSRepository;

        //Using constructor dependency injection for the controller (i.e. when the controller is instnatiated, it will receive an instance of IBloggyRepository according to the config in Program.cs
        public BlogPostController(INEMESYSRepository NEMESYSRepository)
        {
            _NEMESYSRepository = NEMESYSRepository;
        }

        public IActionResult Index()
        {
            //Constructing a View Model to be passed on to the view
            var blogPosts = _NEMESYSRepository.GetAllBlogPosts();
            var model = new BlogPostListViewModel()
            {
                TotalEntries = blogPosts.Count(),
                BlogPosts = blogPosts
            };
            return View(model);

        }
    }
}
