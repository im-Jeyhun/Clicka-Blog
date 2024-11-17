using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;
using ClickaholicWebApp.BLL.DomainModel.Entities;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Client;
using ClickaholicWebApp.BLL.Repository;

namespace ClickaholicWebApp.BLL.ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Task<User> _currentUser;

        public UserService(
            IHttpContextAccessor httpContextAccessor,IRepository<User> repository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
        }

        public bool IsAuthenticated
        {
            get => _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated;
        }

        public Task<User> CurrentUser
        {
            get
            {
                if (_currentUser is not null)
                {
                    return _currentUser;
                }

                var idClaim = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(C => C.Type == "id");
                if (idClaim is null)
                    throw new Exception("Identity cookie not found");

                _currentUser = GetUserAsync(int.Parse(idClaim.Value));

                return _currentUser;
            }
        }


        private async Task<User> GetUserAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public string GetCurrentUserFullName()
        {
            return $"{CurrentUser} {CurrentUser}";
        }

        public async Task<bool> CheckPasswordAsync(string? email, string? password)
        {
            var user = await _repository.GetByCondition(u => u.Email == email);
            return user is not null && password == user.Password;

        }

        public async Task SignInAsync(int id, string? role = null, bool rememberMe = default)
        {
            var claims = new List<Claim>
            {
                new Claim("id", id.ToString())
            };

            if (role is not null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(identity);

            var Remember = new AuthenticationProperties
            {
                IsPersistent = rememberMe
            };
            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, Remember);
        }

        public async Task SignInAsync(string? email, string? password, string? role = null, bool rememberMe = default)
        {
            var user = await _repository.GetByCondition(u => u.Email == email);

            if (user is not null && password ==user.Password)
            {
                await SignInAsync(user.Id, role, rememberMe);
            }

        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task CreateAsync(RegisterViewModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = BC.HashPassword(model.Password),

            };
            await _repository.AddAsync(user);
        }

        public Task<User> GetUserByEmailAndRole(string email, string role)
        {
            return _repository.GetByCondition(x => x.Email == email && x.Role.Name == role);
        }
    }
}
