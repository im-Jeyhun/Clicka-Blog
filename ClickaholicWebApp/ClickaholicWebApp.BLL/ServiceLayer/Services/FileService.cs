using ClickaholicWebApp.BLL.DomainModel.Contracts;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService>? _logger;

        public FileService(ILogger<FileService>? logger)
        {
            _logger = logger;
        }
        public async Task<string> UploadAsync(IFormFile formFile, UploadDirectory uploadDirectory)
        {
            string directoryPath = GetUploadDirectory(uploadDirectory);

            CheckPathExists(directoryPath);

            var imageNameInSystem = CreateUniqueFileName(formFile.FileName);

            var uploadPath = Path.Combine(directoryPath, imageNameInSystem);

            try
            {
                using FileStream fileStream = new FileStream(uploadPath, FileMode.Create);
                await formFile.CopyToAsync(fileStream);
            }
            catch (Exception e)
            {

                _logger!.LogError(e, "Error occured in file service");

                throw;
            }


            return imageNameInSystem;
        }


        private string GetUploadDirectory(UploadDirectory uploadDirectory)
        {
            var initialPath = Path.Combine("wwwroot", "client", "assets", "images");
            switch (uploadDirectory)
            {
                case UploadDirectory.Blog:
                    return Path.Combine(initialPath, "Blog");
                case UploadDirectory.About:
                    return Path.Combine(initialPath, "About");
                case UploadDirectory.Slider:
                    return Path.Combine(initialPath, "Slider");
                case UploadDirectory.Photograph:
                    return Path.Combine(initialPath, "Photograph");
                case UploadDirectory.User:
                    return Path.Combine(initialPath, "User");

                default:
                    throw new Exception("Something went wrong");
            }
        }

        //patha gore muvafiq faylin olub olmamasini yoxlamaq yoxdursa yaratmaq
        private void CheckPathExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath)) // pathin yoxlanilmasi bu patha uygun folder yoxdursa yaradilmasi process
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        //sistem daxilinde qarmasiqliq olmasin deye daxil olan file-in adini uniqe sekilde saxlamaq
        private string CreateUniqueFileName(string formFile)
        {
            return $"{Guid.NewGuid()}{Path.GetExtension(formFile)}";
        }

        public async Task DeleteAsync(string? fileName, UploadDirectory uploadDirectory)
        {
            var deletePath = Path.Combine(GetUploadDirectory(uploadDirectory), fileName);

            await Task.Run(() => File.Delete(deletePath));
        }
        public string GetFileUrl(string? fileName, UploadDirectory uploadDirectory)
        {
            string initialSegment = "client/assets/images";

            switch (uploadDirectory)
            {
                case UploadDirectory.Blog:
                    return $"{initialSegment}/Blog/{fileName}";
                case UploadDirectory.Slider:
                    return $"{initialSegment}/Slider/{fileName}";
                case UploadDirectory.About:
                    return $"{initialSegment}/About/{fileName}";
                case UploadDirectory.Photograph:
                    return $"{initialSegment}/Photograph/{fileName}";
                case UploadDirectory.User:
                    return $"{initialSegment}/User/{fileName}";
                default:
                    throw new Exception("Something went wrong");
            }
        }



    }
}
