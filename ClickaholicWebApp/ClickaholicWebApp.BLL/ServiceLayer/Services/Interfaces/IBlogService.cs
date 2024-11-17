using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogList>> GetBlogList();
        Task AddBlog(BlogAdd model);
        Task<UpdateBlog> GetBlog(int id);
        Task UpdateBlog(int id, UpdateBlog model);
        Task<bool> DeleteBlog(int id);

        Task<BlogItem> GetBlogItem(int id);
    }
}
