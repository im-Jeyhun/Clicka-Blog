using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Admin.Photograph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces
{
    public interface IPhotographService
    {
        Task<List<PhotographList>> GetPhotographList();
        Task AddPhotograph(PhotographAdd model);
        Task<PhotographUpdate> GetPhotograph(int id);
        Task UpdatePhotograph(int id, PhotographUpdate model);
        Task<bool> DeletePhotograph(int id);
    }
}
