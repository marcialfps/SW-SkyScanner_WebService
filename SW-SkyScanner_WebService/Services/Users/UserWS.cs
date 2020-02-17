using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
            _apiBaseUrl = ""; // TODO url of the users web service
        }
        
        public async Task<User> GetUser(int id)
        {
            User user = null;
            
            HttpResponseMessage response = await _client.GetAsync($"{_apiBaseUrl}/users/{id}");
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
            }
            return user;
        }
        
        public async Task<User> GetUser(string username)
        {
            User user = null;
            
            HttpResponseMessage response = await _client.GetAsync($"{_apiBaseUrl}/users/user/{username}");
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
            }
            return user;
        }
        
        public async Task<User> GetUser(string username, string password)
        {
            HttpResponseMessage response = await _client.GetAsync($"{_apiBaseUrl}/users/user/{username}");
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
            HttpResponseMessage response = await _client.PostAsJsonAsync(
                $"{_apiBaseUrl}/users", user);
            
            // Exception if the server does noe return an OK code.
            response.EnsureSuccessStatusCode(); 

            return user;
        }
        
        public async Task<User> UpdateUser(User user)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(
                $"{_apiBaseUrl}/users/{user.Id}", user);
            
            response.EnsureSuccessStatusCode();

            // Deserialize the updated user from the response body.
            user = await response.Content.ReadAsAsync<User>();
            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(
                $"api/products/{id}");
            
            // Code 204.
            return response.StatusCode == HttpStatusCode.NoContent;
        }
        
        public async Task<bool> DeleteUser(User user)
        {
            return await DeleteUser(user.Id);
        }
    }
}