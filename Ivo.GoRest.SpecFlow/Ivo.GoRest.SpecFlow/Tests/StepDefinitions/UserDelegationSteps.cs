using System.Text;
using Ivo.GoRest.SpecFlow.Core.Config;
using Ivo.GoRest.SpecFlow.Core.ContextContainers;
using Ivo.GoRest.SpecFlow.Core.Support.Models;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace Ivo.GoRest.SpecFlow.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private UserContextContainer _context;
        private BaseConfig _baseConfig;

        public CalculatorStepDefinitions(UserContextContainer context, BaseConfig baseConfig)
        {
            _context = context;
            _baseConfig = baseConfig;
        }
        //Get
        [Given(@"I want to prepare a request")]
        public void GivenIWantToPrepareARequest()
        {
        }

        [When(@"I get all users from the (.*) endpoint")]
        public async Task WhenIGetAllUsersFromTheUsersEndpoint(string endpoint)
        {
            _context.Response = await _context.HttpClient.GetAsync(_baseConfig.HttpClientConfig.BaseUrl + endpoint);
        }
        //Create

        [Given(@"I have the following user data Name:(.*), Email:(.*), Gender:(.*), Satus:(.*)")]
        public void GivenIHaveTheFollowingUserDataNameAsdsadEmailAsdsadasdKek_ComGenderMaleSatusActive(string name, string email, string gender, string status)
        {
            _context.TestUser = new User
            {
                Name = name,
                Email = email,
                Gender = gender,
                Status = status
            };
        }

        [When(@"I send a request to the (.*) endpoint")]
        public async Task WhenISendARequestToTheUsersEndpoint(string endpoint)
        {
            var user = JsonConvert.SerializeObject(_context.TestUser);
            var content = new StringContent(user, Encoding.UTF8, "application/json");
            //_response =  _httpClient.PostAsync(_baseConfig.HttpClientConfig.BaseUrl + endpoint,content).Result;

            var requestBody = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}{endpoint}"),
                Content = content
            };

            _context.Response = await _context.HttpClient.SendAsync(requestBody);
        }
        //update
        [Given(@"I have a created user already with (.*)")]
        public async Task GivenIHaveACreatedUserAlreadyWithExisting(string idType)
        {
            var user = new User() { Name = "DummyDumm", Email = "dummyDumm@bla.com", Gender = "male", Status = "active" };
            var userSer = JsonConvert.SerializeObject(user);
            var content = new StringContent(userSer, Encoding.UTF8, "application/json");
            //_response =  _httpClient.PostAsync(_baseConfig.HttpClientConfig.BaseUrl + endpoint,content).Result;

            var requestBody = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}{"users"}"),
                Content = content
            };

            var response = await _context.HttpClient.SendAsync(requestBody);
            _context.TestUser = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            _context.UserIdToDelete = _context.TestUser.Id;

            if (idType == "null")
                _context.TestUser.Id = 0;

        }

        [Given(@"Want to update his first name to (.*)")]
        public void GivenWantToUpdateHisFirstNameToPesho(string newName)
        {
            _context.NameToUpdate = newName;
        }

        [When(@"I send a request to the users endpoint to update")]
        public async Task WhenISendARequestToTheUsersEndpointToUpdate()
        {
            var user = JsonConvert.SerializeObject(new User { Name = _context.NameToUpdate, Email = _context.TestUser.Email, Gender = _context.TestUser.Gender, Status = _context.TestUser.Status });
            var content = new StringContent(user, Encoding.UTF8, "application/json");
            //_response =  _httpClient.PostAsync(_baseConfig.HttpClientConfig.BaseUrl + endpoint,content).Result;

            var requestBody = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_baseConfig.HttpClientConfig.BaseUrl}{"users"}/{_context.TestUser.Id}"),
                Content = content
            };

            _context.Response = await _context.HttpClient.SendAsync(requestBody);
        }

    }
}