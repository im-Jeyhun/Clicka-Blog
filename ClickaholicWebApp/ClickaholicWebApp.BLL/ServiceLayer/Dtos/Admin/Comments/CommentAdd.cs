﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Comments
{
    public class CommentAdd
    {
        public int BlogId { get; set; }
        public string CommenterName { get; set; }
        public string CommenterEmail { get; set; }
        public string CommenterWebSite { get; set; }
        public string Content { get; set; }
    }
}