﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Photograph
{
    public class PhotographUpdate
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}
