using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Slider;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClickaholicWebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/slider")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpGet("list", Name = "admin-slider-list")]
        public async Task<IActionResult> ListAsync()
        {
            return View(await _sliderService.GetSliderList());
        }
        [HttpGet("add-slider", Name = "admin-slider-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View(new SliderAdd());
        }
        [HttpPost("add-slider", Name = "admin-slider-add")]
        public async Task<IActionResult> AddAsync(SliderAdd model)
        {
            if (!ModelState.IsValid) { return View(model); }
            await _sliderService.AddSlider(model);
            return RedirectToRoute("admin-slider-list");
        }

        [HttpGet("update-slider/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _sliderService.GetSlider(id);
            if (model is null) return NotFound();
            return View(model);
        }
        [HttpPost("update-slider/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> UpdateAsync(int id, SliderUpdate model)
        {
            if (!ModelState.IsValid) { return View(model); }
            await _sliderService.UpdateSlider(id, model);
            return RedirectToRoute("admin-slider-list");
        }

        [HttpPost("delete-slider/{id}", Name = "admin-slider-delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResult = await _sliderService.DeleteSlider(id);
            if (deleteResult == false) return NotFound();
            return RedirectToRoute("admin-slider-list");
        }
    }
}
