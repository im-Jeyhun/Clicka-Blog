using ClickaholicWebApp.BLL.DomainModel.Entities;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Blog;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Photograph;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Dtos.Client
{
    public class HomeViewModel
    {
        public List<SliderList> Sliders { get; set; }
        public List<PhotographList> Photographs { get; set; }
        public List<BlogList> Blogs { get; set; }
    }
}
