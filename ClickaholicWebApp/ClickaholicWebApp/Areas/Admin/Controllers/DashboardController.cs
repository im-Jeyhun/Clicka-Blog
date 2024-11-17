using Microsoft.AspNetCore.Mvc;

namespace ClickaholicWebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/dashboard")]
    //[Authorize(Roles = "admin")]

    public class DashboardController : Controller
    {
        [HttpGet("index",Name ="admin-dashboard-index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
