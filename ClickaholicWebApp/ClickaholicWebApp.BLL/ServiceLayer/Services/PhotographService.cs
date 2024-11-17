using ClickaholicWebApp.BLL.DomainModel.Contracts;
using ClickaholicWebApp.BLL.DomainModel.Entities;
using ClickaholicWebApp.BLL.Repository;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Photograph;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services
{
    public class PhotographService : IPhotographService
    {
        private readonly IRepository<Photograph> _repository;
        private readonly IFileService _fileService;

        public PhotographService(IRepository<Photograph> repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task AddPhotograph(PhotographAdd model)
        {
            var Photograph = new Photograph
            {
                Description = model.Description,
            };
            if (model.Image != null)
            {
                var imageName = await _fileService.UploadAsync(model.Image, UploadDirectory.Photograph);
                Photograph.ImageName = model.Image.FileName;
                Photograph.ImageNameInFileSystem = imageName;
            }
            await _repository.AddAsync(Photograph);
        }

        public async Task<List<PhotographList>> GetPhotographList()
        {
            var PhotographList = await _repository.GetAllAsync();
            var PhotographLitVM = PhotographList.Select(x => new PhotographList
            {
                Id = x.Id,
                Description = x.Description,
                ImageUrl = _fileService.GetFileUrl(x.ImageNameInFileSystem, UploadDirectory.Photograph),
            }).ToList();

            return PhotographLitVM;
        }

        public async Task<PhotographUpdate> GetPhotograph(int id)
        {
            var Photograph = await _repository.GetByIdAsync(id);

            if (Photograph is null) return default;

            var model = new PhotographUpdate
            {
                Id = Photograph.Id,
                Description = Photograph.Description,
                ImageUrl = _fileService.GetFileUrl(Photograph.ImageNameInFileSystem, UploadDirectory.Photograph)
            };
            return model;
        }

        public async Task UpdatePhotograph(int id, PhotographUpdate model)
        {
            var Photograph = await _repository.GetByIdAsync(model.Id);
            Photograph.Description = model.Description;

            if (model.Image != null)
            {
                await _fileService.DeleteAsync(Photograph.ImageNameInFileSystem, UploadDirectory.Photograph);
                var fileName = await _fileService.UploadAsync(model.Image, UploadDirectory.Photograph);
                Photograph.ImageNameInFileSystem = fileName;
                Photograph.ImageName = model.Image.FileName;
            }

            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeletePhotograph(int id)
        {
            var Photograph = await _repository.GetByIdAsync(id);
            if (Photograph is null) return false;

            _repository.Delete(Photograph);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}

