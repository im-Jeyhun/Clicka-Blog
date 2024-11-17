using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClickaholicWebApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("gallery")]
    public class GalleryController : Controller
    {
        private readonly IPhotographService _photographService;

        public GalleryController(IPhotographService photographService)
        {
            _photographService = photographService;
        }

        [HttpGet("index", Name = "client-gallery-index")]
        public async Task<IActionResult> Index()
        {
            return View(await _photographService.GetPhotographList());
        }
    }
}
