using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SW_SkyScanner_WebService.Security;
using SW_SkyScanner_WebService.Services.Users.Model;

namespace SW_SkyScanner_WebService.Services.Users
{
    public class UserWS : IUserWs
    {
        private HttpClient _client;
        private string _apiBaseUrl;
        private JsonSerializerSettings _lowerCaseJsonSerializer;

        public UserWS()
        {
            _client = new HttpClient();
            _apiBaseUrl = "http://localhost:8080/SW-SkyScanner_UsersWebClient/usersapi/users";
//            _apiBaseUrl = "http://156.35.95.125:8080/SW-SkyScanner_UsersWebClient-0.0.1-SNAPSHOT/usersapi/users";
//            _apiBaseUrl = "http://156.35.95.128:8080/SW-SkyScanner_UsersWebClient-0.0.1-SNAPSHOT/usersapi/users";
            
            // We need to send our user properties in lowe case to match properties of the users in the Java app
            _lowerCaseJsonSerializer = new JsonSerializerSettings();
            _lowerCaseJsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
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
                // If problem decrypting
                if (string.IsNullOrEmpty(user.Password))
                    return null;
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
                if (user == null)
                    return null;
                user.Password = AesEncryptor.Decrypt(user.Password);
                if (!string.IsNullOrEmpty(user.Password) && password.Equals(user.Password))
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
                if (!string.IsNullOrEmpty(user.Password) && password.Equals(user.Password))
                    return user;
            }
            return null;
        }

        public async Task<User> CreateUser(User user)
        {
            // Create a copy of the user with encrypted credentials to be sent over the network
            User secureUser = new User(user) {Password = AesEncryptor.Encrypt(user.Password)};

            // Form request
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{_apiBaseUrl}");
            request.Content = new StringContent(JsonConvert.SerializeObject(secureUser, _lowerCaseJsonSerializer),
                Encoding.UTF8, "application/json");

            // Get response
            HttpResponseMessage response = _client.SendAsync(request).GetAwaiter().GetResult();

            if (response.StatusCode == HttpStatusCode.Created)
            {
                user = await response.Content.ReadAsAsync<User>();
                user.Password = AesEncryptor.Decrypt(user.Password);
                if (!string.IsNullOrEmpty(user.Password))
                    return user;
            }
            
            return null;
        }
        
        public async Task<User> UpdateUser(User user)
        {
            // Create a copy of the user with encrypted credentials to be sent over the network
            User secureUser = new User(user) {Password = AesEncryptor.Encrypt(user.Password)};

            // Form request
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"{_apiBaseUrl}/{user.Username}");
            request.Content = new StringContent(JsonConvert.SerializeObject(secureUser, _lowerCaseJsonSerializer),
                Encoding.UTF8, "application/json");

            // Get response
            HttpResponseMessage response = _client.SendAsync(request).GetAwaiter().GetResult();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                user = await response.Content.ReadAsAsync<User>();
                user.Password = AesEncryptor.Decrypt(user.Password);
                if (!string.IsNullOrEmpty(user.Password))
                    return user;
            }
            
            return null;
        }

        public async Task<User> DeleteUser(string username, string password)
        {
            password = AesEncryptor.Encrypt(password);
            User user = new User {Username = username, Password = password};

            // Form request
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"{_apiBaseUrl}/{username}");
            request.Content = new StringContent(JsonConvert.SerializeObject(user, _lowerCaseJsonSerializer),
                Encoding.UTF8, "application/json");

            // Get response
            HttpResponseMessage response = _client.SendAsync(request).GetAwaiter().GetResult();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                user = await response.Content.ReadAsAsync<User>();
                user.Password = AesEncryptor.Decrypt(user.Password);
                if (!string.IsNullOrEmpty(user.Password))
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