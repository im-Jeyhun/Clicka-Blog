using ClickaholicWebApp.BLL.DomainModel.Entities;
using ClickaholicWebApp.BLL.Repository;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Blog;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Photograph;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Slider;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Client;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services
{
    public class HomeService : IHomeService
    {
        private readonly IRepository<Slider> _sliderRepository;
        private readonly IRepository<Photograph> _photographRepository;
        private readonly IRepository<Blog> _blogRepository;
        private readonly IFileService _fileService;

        public HomeService(IRepository<Slider> sliderRepository, IRepository<Photograph> photographRepository, IRepository<Blog> blogRepository, IFileService fileService)
        {
            _sliderRepository = sliderRepository;
            _photographRepository = photographRepository;
            _blogRepository = blogRepository;
            _fileService = fileService;
        }

        public async Task<HomeViewModel> GetHomeViewModel()
        {
            var sliders = await _sliderRepository.GetAllAsync();
            var sliderModel = sliders.Select(x => new SliderList
            {
                Description = x.Description,
                ImageUrl = _fileService.GetFileUrl(x.ImageNameInFileSystem, DomainModel.Contracts.UploadDirectory.Slider)
            }).ToList();

            var blogs = await _blogRepository.GetAllAsync();
            var blogModel = blogs.Select(x => new BlogList
            {
                Title = x.Title,
                Description = x.Description,
                ImageUrl = _fileService.GetFileUrl(x.ImageNameInFileSystem, DomainModel.Contracts.UploadDirectory.Blog)
            }).ToList();

            var photograps = await _photographRepository.GetAllAsync();
            var photographModel = photograps.Select(x => new PhotographList
            {
                Description = x.Description,
                ImageUrl = _fileService.GetFileUrl(x.ImageNameInFileSystem, DomainModel.Contracts.UploadDirectory.Photograph)
            }).ToList();

            var model = new HomeViewModel
            {
                Sliders = sliderModel,
                Blogs = blogModel,
                Photographs = photographModel
            };
            return model;
        }
    }
}
