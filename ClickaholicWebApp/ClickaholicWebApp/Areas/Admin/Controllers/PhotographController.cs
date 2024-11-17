using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Photograph;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClickaholicWebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/photograph")]
    //[Authorize(Roles = "admin")]

    public class PhotographController : Controller
    {
        private readonly IPhotographService _photographService;

        public PhotographController(IPhotographService photographService)
        {
            _photographService = photographService;
        }

        [HttpGet("list", Name = "admin-photograph-list")]
        public async Task<IActionResult> ListAsync()
        {
            return View(await _photographService.GetPhotographList());
        }
        [HttpGet("add-photograph", Name = "admin-photograph-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View(new PhotographAdd());
        }
        [HttpPost("add-photograph", Name = "admin-photograph-add")]
        public async Task<IActionResult> AddAsync(PhotographAdd model)
        {
            if (!ModelState.IsValid) { return View(model); }
            await _photographService.AddPhotograph(model);
            return RedirectToRoute("admin-photograph-list");
        }

        [HttpGet("update-photograph/{id}", Name = "admin-photograph-update")]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _photographService.GetPhotograph(id);
            if (model is null) return NotFound();
            return View(model);
        }
        [HttpPost("update-photograph/{id}", Name = "admin-photograph-update")]
        public async Task<IActionResult> UpdateAsync(int id, PhotographUpdate model)
        {
            if (!ModelState.IsValid) { return View(model); }
            await _photographService.UpdatePhotograph(id, model);
            return RedirectToRoute("admin-photograph-list");

        }

        [HttpPost("delete-photograph/{id}", Name = "admin-photograph-delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResult = await _photographService.DeletePhotograph(id);
            if (deleteResult == false) return NotFound();
            return RedirectToRoute("admin-photograph-list");
        }
    }
}
