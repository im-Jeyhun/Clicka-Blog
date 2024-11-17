using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentItem> GetCommentList(int blogId);
        Task AddComment(CommentAdd model);
        Task<CommentUpdate> GetComment(int id );
        Task UpdateComment(int id, CommentUpdate model);
        Task<bool> DeleteComment(int id);
    }
}
