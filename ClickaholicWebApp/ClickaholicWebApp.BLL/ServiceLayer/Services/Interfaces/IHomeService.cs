using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces
{
    public interface IHomeService
    {
        Task<HomeViewModel> GetHomeViewModel();
    }
}
