using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using WebShopDAL.EntityFramework;
using WebShopDAL.Models;
using WebShopDAL.UnitOfWork;
using WebShopDto.User;

namespace WebShopBLL.Services
{
    public class UserService
    {
        ShopUnitOfWork _shopUnitOfWork;
        UserStore _userStore;
        UserManager _userManager;

        public UserService(ShopUnitOfWork shopUnitOfWork)
        {
            _shopUnitOfWork = shopUnitOfWork;
            _userStore = new UserStore();
            _userManager = new UserManager();
        }

        public void AddNewUser(UserDTO userRegistration, string roleName)
        {
            var newUser = new User { UserName = userRegistration.Login, Email = userRegistration.Login, EmailConfirmed = true };

            var role = _shopUnitOfWork.RoleRepository.Get(c => c.Name == roleName).FirstOrDefault();

            newUser.Roles.Add(new IdentityUserRole() { RoleId = role.Id, UserId = newUser.Id });
            try
            {
                _shopUnitOfWork.UserRepository.Create(newUser);
            }
            catch(Exception ex)
            {
                if(ex is DbEntityValidationException || ex is DbUpdateException)
                {
                    throw new ArgumentException();
                }
                throw ex;
            }
            _userStore.SetPasswordHashAsync(newUser, _userManager.PasswordHasher.HashPassword(userRegistration.Password));

            _shopUnitOfWork.Save();
        }

        public void EditUserInfo(UserDTO userRegistration)
        {
            var user = _shopUnitOfWork.UserRepository.Get(u => u.UserName == userRegistration.Login).FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException();
            }
            try
            {
                _shopUnitOfWork.UserRepository.Update(user);
                _shopUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                if (ex is DbEntityValidationException || ex is DbUpdateException)
                {
                    throw new ArgumentException();
                }
                throw ex;
            }
        }

        public void DeleteUser(string userName)
        {
            var user = _shopUnitOfWork.UserRepository.Get(u => u.UserName == userName).FirstOrDefault();
            if (user == null)
            {
                throw new ArgumentException();
            }
            _shopUnitOfWork.UserRepository.Delete(user);
        }

        public bool HasUserPermission(string currentUserName, string userToModifyName)
        {
            return currentUserName == userToModifyName;
        }
    }
}
