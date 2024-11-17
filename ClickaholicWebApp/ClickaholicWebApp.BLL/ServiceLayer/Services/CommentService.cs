using ClickaholicWebApp.BLL.DomainModel.Entities;
using ClickaholicWebApp.BLL.Repository;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Comments;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _repository;

        public CommentService(IRepository<Comment> repository)
        {
            _repository = repository;
        }

        public async Task AddComment(CommentAdd model)
        {
            var comment = new Comment
            {
                BlogId = model.BlogId,
                CommenterEmail = model.CommenterEmail,
                CommenterName = model.CommenterName,
                Content = model.Content,
                CommenterWebSite = model.CommenterWebSite,
                CreatedAt = DateTime.Now,
                IsAccepted = false,
            };
            await _repository.AddAsync(comment);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteComment(int id)
        {
            var comment = await _repository.GetByIdAsync(id);
            if (comment == null) return false;

            _repository.Delete(comment);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<CommentUpdate> GetComment(int id)
        {
            var comment = await _repository.GetByIdAsync(id);
            if (comment == null) return default;
            var model = new CommentUpdate
            {
                Id = comment.Id,
                BlogId = comment.BlogId,
                CommenterEmail = comment.CommenterEmail,
                CommenterName = comment.CommenterName,
                CommenterWebSite = comment.CommenterWebSite,
                Content = comment.Content,
                IsAccepted = comment.IsAccepted,
            };
            return model;
        }

        public async Task<CommentItem> GetCommentList(int blogId)
        {
            var list = await _repository.GetAllByCondition(x => x.BlogId == blogId);
            var modelItem = new CommentItem
            {
                BlogId = blogId,
                Comments = list.Select(x => new CommentList
                {
                    Id = x.Id,
                    BlogId = x.BlogId,
                    CommenterWebSite = x.CommenterWebSite,
                    Content = x.Content,
                    IsAccepted = x.IsAccepted,
                    CommenterEmail = x.CommenterEmail,
                    CommenterName = x.CommenterName
                }).ToList()
            };
            return (modelItem);
        }

        public async Task UpdateComment(int id, CommentUpdate model)
        {
            var comment = await _repository.GetByCondition(x => x.Id == id && x.BlogId == model.BlogId);
            if (comment == null) return;
            comment.Content = model.Content;
            comment.IsAccepted = model.IsAccepted;
            comment.UpdatedAt = DateTime.Now;
            comment.CommenterWebSite = model.CommenterWebSite;
            comment.CommenterEmail = model.CommenterEmail;
            comment.CommenterName = model.CommenterName;
            await _repository.SaveChangesAsync();

        }
    }
}
