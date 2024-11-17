using ClickaholicWebApp.BLL.DomainModel.Contracts;
using ClickaholicWebApp.BLL.DomainModel.Entities;
using ClickaholicWebApp.BLL.Repository;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.About;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services
{
    public class AboutService : IAboutService
    {
        private readonly IRepository<About> _repository;
        private readonly IFileService _fileService;

        public AboutService(IRepository<About> repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task AddAbout(UpdateAbout model)
        {
            var About = new About
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
            };
            if (model.Image != null)
            {
                var imageName = await _fileService.UploadAsync(model.Image, UploadDirectory.About);
                About.ImageName = model.Image.FileName;
                About.ImageNameInFileSystem = imageName;
            }
            await _repository.AddAsync(About);
        }
        
        public async Task<UpdateAbout> GetAbout()
        {
            var About = await _repository.GetAsync();

            if (About is null) return default;

            var model = new UpdateAbout
            {
                Title = About.Title,
                Description = About.Description,
                ImageUrl = _fileService.GetFileUrl(About.ImageNameInFileSystem, UploadDirectory.About)
            };
            return model;
        }

        public async Task UpdateAbout(UpdateAbout model)
        {
            var About = await _repository.GetAsync();
            About.Title = model.Title;
            About.Description = model.Description;
            About.UpdatedAt = DateTime.Now;
            if (model.Image != null)
            {
                await _fileService.DeleteAsync(About.ImageNameInFileSystem, UploadDirectory.About);
                var fileName = await _fileService.UploadAsync(model.Image, UploadDirectory.About);
                About.ImageNameInFileSystem = fileName;
                About.ImageName = model.Image.FileName;
            }

            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAbout(int id)
        {
            var About = await _repository.GetByIdAsync(id);
            if (About is null) return false;

            _repository.Delete(About);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
