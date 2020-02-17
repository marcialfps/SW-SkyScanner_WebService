namespace SW_SkyScanner_WebService.Services.Users.Model
{
    public class User
    {
        
        /// <summary>
        /// User numeric id
        /// </summary>
        public int Id { get;}
        
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// User email
        /// </summary>
        public string Mail { get; set; }
        
        /// <summary>
        /// Code of the airport where the user is located
        /// </summary>
        public string Airport { get; set; }
    }
}