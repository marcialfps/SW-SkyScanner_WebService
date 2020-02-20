using System.Threading.Tasks;
using SW_SkyScanner_WebService.Services.Users.Model;

namespace SW_SkyScanner_WebService.Services.Users
{
    public interface IUserWs
    {

        /// <summary>
        /// Tries to GET a user given his username.
        /// </summary>
        /// <param name="username">Username of the user to get</param>
        /// <returns>The user matched or null if no user is matched.</returns>
        Task<User> GetUser(string username);

        /// <summary>
        /// Tries to GET a user given his username and password.
        /// </summary>
        /// <param name="username">Username of the user to get</param>
        /// <param name="password">Password of the user to get</param>
        /// <returns>The user matched or null if no user is matched or the password given is incorrect.</returns>
        Task<User> GetUser(string username, string password);
        
        /// <summary>
        /// Tries log in an existing user.
        /// </summary>
        /// <param name="username">Username of the user to log in</param>
        /// <param name="password">Password of the user to log in</param>
        /// <returns>The given user if it was logged in, null otherwise</returns>
        Task<User> Login (string username, string password);

        /// <summary>
        /// Tries to POST a new user. Serializes the user into JSON format and sends the request to the service.
        /// </summary>
        /// <param name="user">User to be created</param>
        /// <returns>The given user if it was created correctly.</returns>
        Task<User> CreateUser(User user);

        /// <summary>
        /// Tries to PUT new data on an existing user. Serializes the user into JSON format and sends the request to the service.
        /// </summary>
        /// <param name="user">User to be updated</param>
        /// <returns>The given user if it was updated correctly.</returns>
        Task<User> UpdateUser(User user);

        /// <summary>
        /// Tries to DELETE an existing user given his credentials.
        /// </summary>
        /// <param name="username">Username of the user to be deleted</param>
        /// <param name="password">Password of the user to be deleted</param>
        /// <returns>The deleted user on success, null on failure.</returns>
        Task<User> DeleteUser(string username, string password);
        
        /// <summary>
        /// Tries to DELETE an existing user.
        /// </summary>
        /// <param name="user">User to be deleted.</param>
        /// <returns>The deleted user on success, null on failure.</returns>
        Task<User> DeleteUser(User user);
    }
}