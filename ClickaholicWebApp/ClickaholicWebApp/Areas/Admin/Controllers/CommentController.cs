using ClickaholicWebApp.BLL.DomainModel.Entities;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Blog;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Comments;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClickaholicWebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/comment")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("list/{blogId}", Name = "admin-comment-list")]
        public async Task<IActionResult> ListAsync(int blogId)
        {
            return View(await _commentService.GetCommentList(blogId));
        }
        [HttpGet("add-comment/{blogId}", Name = "admin-comment-add")]
        public async Task<IActionResult> AddAsync(int blogId)
        {
            return View(new CommentAdd { BlogId = blogId });
        }
        [HttpPost("add-comment/{blogId}", Name = "admin-comment-add")]
        public async Task<IActionResult> AddAsync(int blogId , CommentAdd model)
        {
            if (!ModelState.IsValid) { return View(model); }
            await _commentService.AddComment(model);
            return RedirectToRoute("admin-comment-list", new { blogId = model.BlogId });
        }

        [HttpGet("update-comment/{id}", Name = "admin-comment-update")]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _commentService.GetComment(id);
            if (model is null) return NotFound();
            return View(model);
        }
        [HttpPost("update-comment/{id}", Name = "admin-comment-update")]
        public async Task<IActionResult> UpdateAsync(int id, CommentUpdate model)
        {
            if (!ModelState.IsValid) { return View(model); }
            await _commentService.UpdateComment(id, model);
            return RedirectToRoute("admin-comment-list", new { blogId = model.BlogId });
        }

        [HttpPost("delete-comment/{id}/{blogId}", Name = "admin-comment-delete")]
        public async Task<IActionResult> DeleteAsync(int id,int blogId)
        {
            var deleteResult = await _commentService.DeleteComment(id);
            if (deleteResult == false) return NotFound();
            return RedirectToRoute("admin-comment-list", new { blogId = blogId });
        }
    }
}
