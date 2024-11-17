using ClickaholicWebApp.BLL.DomainModel.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile formFile, UploadDirectory uploadDirectory);
        public Task DeleteAsync(string? fileName, UploadDirectory uploadDirectory);
        public string GetFileUrl(string? fileName, UploadDirectory uploadDirectory);
    }
}
