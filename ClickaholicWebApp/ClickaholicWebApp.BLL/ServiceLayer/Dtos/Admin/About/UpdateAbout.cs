using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.About
{
    public class UpdateAbout
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageName { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image {  get; set; }
    }
}
