using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SW_SkyScanner_WebService.Security;
using SW_SkyScanner_WebService.Services.Users.Model;

namespace SW_SkyScanner_WebService.Services.Users
{
    public class UserWS : IUserWs
    {
        private HttpClient _client;
        private string _apiBaseUrl;

        public UserWS()
        {
            _client = new HttpClient();
            _apiBaseUrl = "http://localhost:8080/SW-SkyScanner_UsersWebClient/usersapi/users";
        }

        public async Task<User> GetUser(string username)
        {
            User user = null;
            
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrl}/{username}")
                .GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
                user.Password = AesEncryptor.Decrypt(user.Password);
            }
            return user;
        }
        
        public async Task<User> GetUser(string username, string password)
        {
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrl}/{username}")
                .GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                User user = await response.Content.ReadAsAsync<User>();
                user.Password = AesEncryptor.Decrypt(user.Password);
                if (password.Equals(user.Password))
                    return user;
            }
            return null;
        }

        public async Task<User> Login(string username, string password)
        {
            User user = new User();
            user.Username = username;
            user.Password = AesEncryptor.Encrypt(password);

            HttpResponseMessage response = await _client.PostAsJsonAsync(
                $"{_apiBaseUrl}/{username}", user);
            
            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                user = await response.Content.ReadAsAsync<User>();
                user.Password = AesEncryptor.Decrypt(user.Password);
                if (password.Equals(user.Password))
                    return user;
            }
            return null;
        }

        public async Task<User> CreateUser(User user)
        {
            // Create a copy of the user with encrypted credentials to be sent over the network
            User secureUser = new User(user);
            secureUser.Password = AesEncryptor.Encrypt(user.Password);
            
            HttpResponseMessage response = await _client.PostAsJsonAsync(
                $"{_apiBaseUrl}", secureUser);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                user = await response.Content.ReadAsAsync<User>();
                user.Password = AesEncryptor.Decrypt(user.Password);
                return user;
            }
            
            return null;
        }
        
        public async Task<User> UpdateUser(User user)
        {
            // Create a copy of the user with encrypted credentials to be sent over the network
            User secureUser = new User(user);
            secureUser.Password = AesEncryptor.Encrypt(user.Password);
            
            HttpResponseMessage response = await _client.PutAsJsonAsync(
                $"{_apiBaseUrl}/{user.Username}", secureUser);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                user = await response.Content.ReadAsAsync<User>();
                user.Password = AesEncryptor.Decrypt(user.Password);
                return user;
            }
            
            return null;
        }

        public async Task<User> DeleteUser(string username, string password)
        {
            // Create a copy of the user with encrypted credentials to be sent over the network
            password = AesEncryptor.Encrypt(password);
            
            HttpResponseMessage response = await _client.DeleteAsync(
                $"{_apiBaseUrl}/{username}");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                User user = await response.Content.ReadAsAsync<User>();
                user.Password = AesEncryptor.Decrypt(user.Password);
                return user;
            }
            
            return null;
        }
        
        public Task<User> DeleteUser(User user)
        {
            return DeleteUser(user.Username, user.Password);
        }
    }
}