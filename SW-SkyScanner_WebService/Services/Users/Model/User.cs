using System;

namespace SW_SkyScanner_WebService.Services.Users.Model
{
    public class User : ICloneable
    {
        public User(int id, string name, string password, string airport)
        {
            Id = id;
            Name = name;
            Password = password;
            Airport = airport;
        }

        /// <summary>
        /// User numeric id
        /// </summary>
        private int Id { get;}
        
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Code of the airport where the user is located
        /// </summary>
        public string Airport { get; set; }

        public int getId()
        {
            return Id;
        }
        
        public object Clone()
        {
            return new User(Id, Name, Password, Airport);
        }

        
    }
}