using System;
using System.Collections.Generic;
using System.Linq;
using WebShopDAL.Interfaces;
using WebShopDAL.Models;
using WebShopDto.User;

namespace WebShopBLL.Services
{
    public class UserService
    {
        private IUnitOfWork _webShop;

        public UserService(IUnitOfWork webShop)
        {
            _webShop = webShop;
        }

        public void AddNewClient(UserRegistrationDTO newUser)
        {
            if (!IsEmailInUse(newUser.Login))
            {
                UserStatus status = _webShop.UserStatuses.Get(1);
                UserRole role = _webShop.UserRoles.Get(1);
                _webShop.Users.Create(new User
                {
                    UserEmailLogin = newUser.Login,
                    UserPassword = newUser.Password,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    BirthdayDate = newUser.BirthdayDate,
                    Basket = new Basket(),
                    Purchases = new List<Purchase>(),
                    UserStatus = status,
                    UserRole = role
                });
            }
        }

        public void AddNewAdmin(UserRegistrationDTO newUser)
        {
            if (!IsEmailInUse(newUser.Login))
            {
                UserStatus status = _webShop.UserStatuses.Get(1);
                UserRole role = _webShop.UserRoles.Get(2);
                _webShop.Users.Create(new User
                {
                    UserEmailLogin = newUser.Login,
                    UserPassword = newUser.Password,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    BirthdayDate = newUser.BirthdayDate,
                    Basket = new Basket(),
                    Purchases = new List<Purchase>(),
                    UserStatus = status,
                    UserRole = role
                });
            }
        }

        public void AddNewShopAdministrator(UserRegistrationDTO newUser)
        {
            if (!IsEmailInUse(newUser.Login))
            {
                UserStatus status = _webShop.UserStatuses.Get(1);
                UserRole role = _webShop.UserRoles.Get(3);
                _webShop.Users.Create(new User
                {
                    UserEmailLogin = newUser.Login,
                    UserPassword = newUser.Password,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    BirthdayDate = newUser.BirthdayDate,
                    Basket = new Basket(),
                    Purchases = new List<Purchase>(),
                    UserStatus = status,
                    UserRole = role
                });
            }
        }

        public void DeleteUserById(int id)
        {
            var user = _webShop.Users.Get(id);
            if ( user != null)
            {
                _webShop.Baskets.Delete(user.BasketId.GetValueOrDefault());
                _webShop.Users.Delete(id);
                _webShop.Save();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void BlockUserById(int id)
        {
            User user = _webShop.Users.Get(id);
            UserStatus status = _webShop.UserStatuses.Get(2);
            user.UserStatus = status;
            if (user != null)
            {
                _webShop.Users.Update(user);
                _webShop.Save();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public UserDTO GetUserData(int userId)
        {
            var user = _webShop.Users.Get(userId);
            if (user != null)
            {
                return new UserDTO
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BasketId = user.BasketId.GetValueOrDefault()
                };
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private bool IsEmailInUse(string email)
        {
            return _webShop.Users.Get(u => u.UserEmailLogin.ToLower() == email.ToLower()).Any();
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            List<UserDTO> result = new List<UserDTO>();
            _webShop.Users.GetQuery().Select(u => new UserDTO
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                BasketId = u.BasketId.GetValueOrDefault()
            });
            return result;
        }
    }
}
