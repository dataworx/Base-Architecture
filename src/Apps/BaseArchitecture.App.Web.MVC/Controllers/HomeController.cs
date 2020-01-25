using BaseArchitecture.App.Web.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using BaseArchitecture.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace BaseArchitecture.App.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataContext dataContext;

        public HomeController(ILogger<HomeController> logger, IDataContext dataContext)
        {
            _logger = logger;
            this.dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetUsers()
        {
            var users = await this.dataContext.Users.ToListAsync();
            return Json(users);
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
