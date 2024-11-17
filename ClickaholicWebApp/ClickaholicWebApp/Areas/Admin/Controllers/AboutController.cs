using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.About;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ClickaholicWebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/about")]
    //[Authorize(Roles = "admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet("update", Name = "admin-about-update")]
        public async Task<IActionResult> Update()
        {
            var about = await _aboutService.GetAbout();
            if (about == null) return View(new UpdateAbout());

            return View(about);
        }
        [HttpPost("update", Name = "admin-about-update")]
        public async Task<IActionResult> Update(UpdateAbout updateAbout)
        {
            if(!ModelState.IsValid) return View(updateAbout);

            var about = await _aboutService.GetAbout();
            if (about == null)
            {
                await _aboutService.AddAbout(updateAbout);
            }
            else
            {
                await _aboutService.UpdateAbout(updateAbout);
            }
            return RedirectToRoute("admin-about-update");
        }
    }
}
