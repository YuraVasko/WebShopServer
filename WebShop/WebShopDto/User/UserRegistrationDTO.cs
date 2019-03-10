using System;

namespace WebShopDto.User
{
    public class UserRegistrationDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthdayDate { get; set; }
    }
}
