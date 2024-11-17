using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Authentication;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickaholicWebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/auth")]
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("login", Name = "admin-auth-login")]
        public IActionResult Login()
        {
            return View(new Login());
        }
        [HttpPost("login", Name = "admin-auth-login")]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!await _userService.CheckPasswordAsync(model!.Email, model!.Password))
            {
                ModelState.AddModelError(String.Empty, "Email or password is not correct");
                return View(model);
            }

            if (await _userService.GetUserByEmailAndRole(model.Email, "admin") != null)
            {
                await _userService.SignInAsync(model!.Email, model!.Password, "admin");
                return RedirectToRoute("admin-dashboard-index");
            }
            else
            {
                ModelState.AddModelError(String.Empty, "You are not admin");
                return View(model);
            }

        }

    }
}
