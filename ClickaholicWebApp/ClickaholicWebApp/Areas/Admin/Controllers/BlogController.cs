using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Blog;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClickaholicWebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blog")]
    //[Authorize(Roles = "admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("list", Name = "admin-blog-list")]
        public async Task<IActionResult> ListAsync()
        {
            return View(await _blogService.GetBlogList());
        }
        [HttpGet("add-blog", Name = "admin-blog-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View(new BlogAdd());
        }
        [HttpPost("add-blog", Name = "admin-blog-add")]
        public async Task<IActionResult> AddAsync(BlogAdd model)
        {
            if (!ModelState.IsValid) { return View(model); }
            await _blogService.AddBlog(model);
            return RedirectToRoute("admin-blog-list");
        }

        [HttpGet("update-blog/{id}", Name = "admin-blog-update")]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _blogService.GetBlog(id);
            if (model is null) return NotFound();
            return View(model);
        }
        [HttpPost("update-blog/{id}", Name = "admin-blog-update")]
        public async Task<IActionResult> UpdateAsync(int id , UpdateBlog model)
        {
            if(!ModelState.IsValid) { return View(model); }
            await _blogService.UpdateBlog(id, model);
            return RedirectToRoute("admin-blog-list");

        }

        [HttpPost("delete-blog/{id}", Name = "admin-blog-delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteResult = await _blogService.DeleteBlog(id);
            if(deleteResult == false) return NotFound();
            return RedirectToRoute("admin-blog-list");
        }
    }
}