using System.Collections.Generic;
using WebShopDAL.Models;
using WebShopDto.User;

namespace WebShopBLL.DtoMappers
{
    class UserMapper
    {
        public User GetUserFromUserRegistrationDTO(UserRegistrationDTO userDto, UserStatus status, UserRole role)
        {
            return new User
            {
                UserEmailLogin = userDto.Login,
                UserPassword = userDto.Password,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                BirthdayDate = userDto.BirthdayDate,
                Basket = new Basket(),
                Purchases = new List<Purchase>(),
                UserStatus = status,
                UserRole = role
            };
        }

        public UserDTO GetUserDTOFromUserModel(User user)
        {
            return new UserDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BasketId = user.BasketId.GetValueOrDefault()
            };
        }
    }
}
