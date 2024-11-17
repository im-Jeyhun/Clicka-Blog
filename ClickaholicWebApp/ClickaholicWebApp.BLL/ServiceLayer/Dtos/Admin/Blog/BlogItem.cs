using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Blog
{
    public class BlogItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int? UserId { get; set; }
        public List<CommentList>? Comments { get; set; }
        public List<BlogList>? SuggestedBlogs { get; set; }
        public CommentAdd? CommentModel {  get; set; }
    }
}
