using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SW_SkyScanner_WebService.Services.Users.Model;

namespace SW_SkyScanner_WebService.Services.Users
{
    public class UserWS : IUserWs
    {
        private HttpClient _client;
        private AesEncryptor _encryptor;
        private string _apiBaseUrl;

        public UserWS()
        {
            _client = new HttpClient();
            _encryptor = new AesEncryptor();
            _apiBaseUrl = ""; // TODO url of the users web service
        }
        
        public async Task<User> GetUser(int id)
        {
            User user = null;
            
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrl}/users/{id}").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
            }
            return user;
        }
        
        public async Task<User> GetUser(string username)
        {
            User user = null;
            
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrl}/users/user/{username}")
                .GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
            }
            return user;
        }
        
        public async Task<User> GetUser(string username, string password)
        {
            HttpResponseMessage response = _client.GetAsync($"{_apiBaseUrl}/users/user/{username}")
                .GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                User user = await response.Content.ReadAsAsync<User>();
                if (password.Equals(user.Password))
                    return user;
            }
            return null;
        }
        
        public async Task<User> CreateUser(User user)
        {
            // Create a copy of the user with encrypted credentials to be sent over the network
            User secureUser = (User)user.Clone();
            secureUser.Name = _encryptor.Encrypt(user.Name);
            secureUser.Password = _encryptor.Encrypt(user.Password);
            
            HttpResponseMessage response = _client.PostAsJsonAsync(
                $"{_apiBaseUrl}/users", secureUser).GetAwaiter().GetResult();
            
            // Exception if the server does noe return an OK code.
            response.EnsureSuccessStatusCode(); 

            // Return the original user if success
            return user;
        }
        
        public async Task<User> UpdateUser(User user)
        {
            // Create a copy of the user with encrypted credentials to be sent over the network
            User secureUser = (User)user.Clone();
            secureUser.Name = _encryptor.Encrypt(user.Name);
            secureUser.Password = _encryptor.Encrypt(user.Password);
            
            HttpResponseMessage response = _client.PutAsJsonAsync(
                $"{_apiBaseUrl}/users/{user.getId()}", user).GetAwaiter().GetResult();
            
            response.EnsureSuccessStatusCode();

            // Return the original user if success
            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync($"api/products/{id}").GetAwaiter().GetResult();
            
            // Code 204.
            return response.StatusCode == HttpStatusCode.NoContent;
        }
        
        public async Task<bool> DeleteUser(User user)
        {
            return await DeleteUser(user.getId());
        }
    }
}