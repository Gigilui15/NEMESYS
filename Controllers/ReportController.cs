using NEMESYS.Models.Interfaces;
using NEMESYS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace NEMESYS.Controllers
{
    public class ReportController : Controller
    {
        private readonly INEMESYSRepository _NEMESYSRepository;

        //Using constructor dependency injection for the controller (i.e. when the controller is instnatiated, it will receive an instance of IBloggyRepository according to the config in Program.cs
        public ReportController(INEMESYSRepository NEMESYSRepository)
        {
            _NEMESYSRepository = NEMESYSRepository;
        }

        public IActionResult Index()
        {
            //Constructing a View Model to be passed on to the view
            var reportPost = _NEMESYSRepository.GetAllReports();
            var model = new ReportPostListViewModel()
            {
                TotalEntries = reportPost.Count(),
                ReportPost = reportPost
            };
            return View(model);

        }
    }
}
