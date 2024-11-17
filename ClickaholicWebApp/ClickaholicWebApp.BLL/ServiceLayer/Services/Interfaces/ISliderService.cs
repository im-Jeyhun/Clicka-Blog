using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderList>> GetSliderList();
        Task AddSlider(SliderAdd model);
        Task<SliderUpdate> GetSlider(int id);
        Task UpdateSlider(int id, SliderUpdate model);
        Task<bool> DeleteSlider(int id);
    }
}
