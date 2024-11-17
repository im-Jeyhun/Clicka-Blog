using ClickaholicWebApp.BLL.DomainModel.Entities;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces
{
    public interface IUserService
    {
        public bool IsAuthenticated { get; }
        public Task<User> CurrentUser { get; }

        Task<bool> CheckPasswordAsync(string? email, string? password);

        string GetCurrentUserFullName();
        Task SignInAsync(int id, string? role = null, bool rememberMe = default);
        Task SignInAsync(string? email, string? password, string? role = null, bool rememberMe = default);
        Task CreateAsync(RegisterViewModel model);
        Task SignOutAsync();
        Task<User> GetUserByEmailAndRole(string email , string role);
    }
}
