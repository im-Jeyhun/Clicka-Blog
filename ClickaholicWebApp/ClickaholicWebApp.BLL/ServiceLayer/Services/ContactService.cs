using ClickaholicWebApp.BLL.DomainModel.Entities;
using ClickaholicWebApp.BLL.Repository;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Client;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> _repository;

        public ContactService(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        public async Task AddContact(ContactAdd model)
        {
            var contact = new Contact
            {
                Email = model.Email,
                Name = model.Name,
                Message = model.Message,
            };
            await _repository.AddAsync(contact);
        }

        public async Task<bool> DeleteContact(int id)
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact == null) return false;
            _repository.Delete(contact);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<Contact> GetContact(int id)
        {
           return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Contact>> GetContactList()
        {
            var list = await _repository.GetAllAsync();

            List<Contact> model = list.ToList();
           return (model);
        }
    }
}
