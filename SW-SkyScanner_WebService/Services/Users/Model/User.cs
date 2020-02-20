using System;

namespace SW_SkyScanner_WebService.Services.Users.Model
{
    public class User
    {
        public User()
        {}

        public User(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Name = user.Name;
            Surname = user.Surname;
            Password = user.Password;
            Mail = user.Mail;
            Airport = user.Airport;
        }

        /// <summary>
        /// User numeric id
        /// </summary>
        private int Id { get; set; }
        
        /// <summary>
        /// User username
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// User real name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// User surname
        /// </summary>
        public string Surname { get; set; }
        
        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// User mail
        /// </summary>
        public string Mail { get; set; }
        
        /// <summary>
        /// Code of the airport where the user is located
        /// </summary>
        public string Airport { get; set; }

        /*public object Clone()
        {
            return new User(Id, Name, Password, Airport);
        }*/

        
    }
}