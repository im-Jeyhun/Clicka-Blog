using ClickaholicWebApp.BLL.DomainModel.Contracts;
using ClickaholicWebApp.BLL.DomainModel.Entities;
using ClickaholicWebApp.BLL.Repository;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Blog;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Comments;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog> _repository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IFileService _fileService;

        public BlogService(IRepository<Blog> repository, IFileService fileService, IRepository<Comment> commentRepository)
        {
            _repository = repository;
            _fileService = fileService;
            _commentRepository = commentRepository;
        }

        public async Task AddBlog(BlogAdd model)
        {
            var blog = new Blog
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
            };
            if (model.Image != null)
            {
                var imageName = await _fileService.UploadAsync(model.Image, UploadDirectory.Blog);
                blog.ImageName = model.Image.FileName;
                blog.ImageNameInFileSystem = imageName;
            }
            await _repository.AddAsync(blog);
        }

        public async Task<List<BlogList>> GetBlogList()
        {
            var blogList = await _repository.GetAllAsync();
            var blogLitVM = blogList.Select(x => new BlogList
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                UserId = x.UserId,
                ImageUrl = _fileService.GetFileUrl(x.ImageNameInFileSystem, UploadDirectory.Blog),
            }).ToList();

            return blogLitVM;
        }

        public async Task<UpdateBlog> GetBlog(int id)
        {
            var blog = await _repository.GetByIdAsync(id);

            if (blog is null) return default;

            var model = new UpdateBlog
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                ImageUrl = _fileService.GetFileUrl(blog.ImageNameInFileSystem, UploadDirectory.Blog)
            };
            return model;
        }

        public async Task UpdateBlog(int id, UpdateBlog model)
        {
            var blog = await _repository.GetByIdAsync(model.Id);
            blog.Title = model.Title;
            blog.Description = model.Description;

            if (model.Image != null)
            {
                await _fileService.DeleteAsync(blog.ImageNameInFileSystem, UploadDirectory.Blog);
                var fileName = await _fileService.UploadAsync(model.Image, UploadDirectory.Blog);
                blog.ImageNameInFileSystem = fileName;
                blog.ImageName = model.Image.FileName;
            }

            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteBlog(int id)
        {
            var blog = await _repository.GetByIdAsync(id);
            if(blog is null) return false;

            _repository.Delete(blog);
            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<BlogItem> GetBlogItem(int id)
        {
            var blog = await _repository.GetByIdAsync(id);
            var comments = await _commentRepository.GetAllByCondition(x => x.BlogId == id && x.IsAccepted == true);
            var blogs = await _repository.GetAllAsync();
            var blogModel = blogs.Select(x => new BlogList
            {
                Title = x.Title,
                Description = x.Description,
                Id = x.Id,
                ImageUrl = _fileService.GetFileUrl(x.ImageNameInFileSystem, UploadDirectory.Blog),

            });
            var commentModel = comments.Select(x => new CommentList
            {
                BlogId = x.BlogId,
                CommenterWebSite = x.CommenterWebSite,
                CommenterEmail = x.CommenterEmail,
                CommenterName = x.CommenterName,
                Content = x.Content,
                Id = id,
                IsAccepted = x.IsAccepted,
                CreateTime = x.CreatedAt,
            }).ToList();
            if (blog is null) return default;
            var model = new BlogItem
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                ImageUrl = _fileService.GetFileUrl(blog.ImageNameInFileSystem, UploadDirectory.Blog),
                Comments = commentModel,
                SuggestedBlogs = blogModel.Take(2).ToList()
            };
            return model;
        }
    }
}
