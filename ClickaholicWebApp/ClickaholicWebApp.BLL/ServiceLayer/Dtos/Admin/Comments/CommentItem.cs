using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Comments
{
    public class CommentItem
    {
        public int BlogId { get; set; }
        public List<CommentList> Comments { get; set; }
    }
}
