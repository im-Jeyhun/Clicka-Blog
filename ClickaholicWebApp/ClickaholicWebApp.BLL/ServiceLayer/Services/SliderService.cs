using ClickaholicWebApp.BLL.DomainModel.Contracts;
using ClickaholicWebApp.BLL.DomainModel.Entities;
using ClickaholicWebApp.BLL.Repository;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Slider;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services
{
    public class SliderService : ISliderService
    {
        private readonly IRepository<Slider> _repository;
        private readonly IFileService _fileService;

        public SliderService(IRepository<Slider> repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task AddSlider(SliderAdd model)
        {
            var Slider = new Slider
            {
                Title = model.Title,
                Description = model.Description,
            };
            if (model.Image != null)
            {
                var imageName = await _fileService.UploadAsync(model.Image, UploadDirectory.Slider);
                Slider.ImageName = model.Image.FileName;
                Slider.ImageNameInFileSystem = imageName;
            }
            await _repository.AddAsync(Slider);
        }

        public async Task<List<SliderList>> GetSliderList()
        {
            var SliderList = await _repository.GetAllAsync();
            var SliderLitVM = SliderList.Select(x => new SliderList
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = _fileService.GetFileUrl(x.ImageNameInFileSystem, UploadDirectory.Slider),
            }).ToList();

            return SliderLitVM;
        }

        public async Task<SliderUpdate> GetSlider(int id)
        {
            var Slider = await _repository.GetByIdAsync(id);

            if (Slider is null) return default;

            var model = new SliderUpdate
            {
                Id = Slider.Id,
                Title = Slider.Title,
                Description = Slider.Description,
                ImageUrl = _fileService.GetFileUrl(Slider.ImageNameInFileSystem, UploadDirectory.Slider)
            };
            return model;
        }

        public async Task UpdateSlider(int id, SliderUpdate model)
        {
            var Slider = await _repository.GetByIdAsync(model.Id);
            Slider.Title = model.Title;
            Slider.Description = model.Description;

            if (model.Image != null)
            {
                await _fileService.DeleteAsync(Slider.ImageNameInFileSystem, UploadDirectory.Slider);
                var fileName = await _fileService.UploadAsync(model.Image, UploadDirectory.Slider);
                Slider.ImageNameInFileSystem = fileName;
                Slider.ImageName = model.Image.FileName;
            }

            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteSlider(int id)
        {
            var Slider = await _repository.GetByIdAsync(id);
            if (Slider is null) return false;

            _repository.Delete(Slider);
            await _repository.SaveChangesAsync();

            return true;
        }

      
    }
}
