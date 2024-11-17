using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Blog;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClickaholicWebApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;

        public BlogController(IBlogService blogService, ICommentService commentService)
        {
            _blogService = blogService;
            _commentService = commentService;
        }

        [HttpGet("index", Name = "client-blog-index")]
        public async Task<IActionResult> Index()
        {
            return View( await _blogService.GetBlogList());
        }
        [HttpGet("item/{id}", Name = "client-blog-item")]
        public async Task<IActionResult> Item(int id)
        {
            return View(await _blogService.GetBlogItem(id));
        }
        [HttpPost("post/{blogId}", Name ="client-blog-comment-post")]
        public async Task<IActionResult> PostComment(int blogId , BlogItem blogItem)
        {
            blogItem.CommentModel.BlogId = blogId;
            await _commentService.AddComment(blogItem.CommentModel);
            return RedirectToRoute("client-blog-item", new { id = blogItem.CommentModel.BlogId });
        }
    }
}
