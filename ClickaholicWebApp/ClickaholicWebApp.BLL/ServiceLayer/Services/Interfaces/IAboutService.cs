using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.About;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces
{
    public interface IAboutService
    {
        Task AddAbout(UpdateAbout model);
        Task<UpdateAbout> GetAbout();
        Task UpdateAbout(UpdateAbout model);
        Task<bool> DeleteAbout(int id);
    }
}
