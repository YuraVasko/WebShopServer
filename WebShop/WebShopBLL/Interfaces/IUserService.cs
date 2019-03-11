using System.Collections.Generic;
using WebShopDto.User;

namespace WebShopBLL.Interfaces
{
    interface IUserService
    {
        void AddNewClient(UserRegistrationDTO newUser);

        void AddNewAdmin(UserRegistrationDTO newUser);

        void AddNewShopAdministrator(UserRegistrationDTO newUser);

        void DeleteUserById(int id);

        void BlockUserById(int id);

        UserDTO GetUserData(int userId);

        IEnumerable<UserDTO> GetAllUsers();
    }
}
