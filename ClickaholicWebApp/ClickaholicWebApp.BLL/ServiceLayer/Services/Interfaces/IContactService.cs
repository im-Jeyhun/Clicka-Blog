using ClickaholicWebApp.BLL.DomainModel.Entities;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<Contact>> GetContactList();
        Task<Contact> GetContact(int id);
        Task<bool> DeleteContact(int id);
        Task AddContact(ContactAdd contact);
    }
}
