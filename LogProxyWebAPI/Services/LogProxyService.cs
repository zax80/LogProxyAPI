using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace LogProxyWebAPI.Services
{
    public interface ILogProxyService
    {
        Task<Entities.User> Authenticate(string username, string password);

        Task<IEnumerable<Entities.User>> GetAllUsers();

        Task<Entities.Root> GetAllMessages();

        Task<Entities.Response.Root> AddMessage(string title, string text);
    }

    public class LogProxyService : ILogProxyService, IDisposable
    {
        private  HttpClient httpClient;
        private const string urlGetAllMessages = "https://api.airtable.com/v0/appD1b1YjWoXkUJwR/Messages?maxRecords=2&view=Grid%20view";
        private const string urlAddMessages = "https://api.airtable.com/v0/appD1b1YjWoXkUJwR/Messages";
        private const string token = "key46INqjpp7lMzjd";

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private readonly List<Entities.User> _users = new()
        {
            new Entities.User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        public async Task<LogProxyWebAPI.Entities.User> Authenticate(string username, string password)
        {
            // wrapped in "await Task.Run" to mimic fetching user from a db
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            // on auth fail: null is returned because user is not found
            // on auth success: user object is returned
            return user;
        }

        public async Task<IEnumerable<LogProxyWebAPI.Entities.User>> GetAllUsers()
        {
            // wrapped in "await Task.Run" to mimic fetching users from a db
            return await Task.Run(() => _users);
        }

        public async Task<Entities.Root> GetAllMessages()
        {
            //Create the request sender object
            httpClient = new HttpClient();

            //Set the headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);

            //Get the response content
            return await httpClient.GetFromJsonAsync<Entities.Root>(urlGetAllMessages);
        }

        public async Task<Entities.Response.Root> AddMessage(string title, string text)
        {
            Entities.Request.Root _request = new();
            Entities.Root _rootAllMessages = await GetAllMessages();
            string _recNumber = (_rootAllMessages.Records.Count + 1).ToString();
            // set values
            Entities.Request.Fields _fields = new()
            {
                ID = _recNumber,
                Summary = title,
                Message = text,
                ReceivedAt = DateTime.Now
            };

            _request.records = new List<Entities.Request.Record>
            {
                new Entities.Request.Record() { Fields = _fields }
            };

            //Create the request sender object
            httpClient = new HttpClient();

            //Set the headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var data = JsonConvert.SerializeObject(_request);

            /* Initialize the request content 
               and 
               Send the POST
            */
            var response = await httpClient.PostAsync(urlAddMessages, new StringContent(data, UTF8Encoding.UTF8, "application/json"));

            //Check for error status
            response.EnsureSuccessStatusCode();

            //Get the response content
            return await response.Content.ReadFromJsonAsync<Entities.Response.Root>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}