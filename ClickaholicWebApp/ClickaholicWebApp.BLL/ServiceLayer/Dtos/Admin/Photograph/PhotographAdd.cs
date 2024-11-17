using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Photograph
{
    public class PhotographAdd
    {
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
