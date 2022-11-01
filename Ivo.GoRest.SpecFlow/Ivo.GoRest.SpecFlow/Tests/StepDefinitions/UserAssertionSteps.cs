using System.Text;
using Ivo.GoRest.SpecFlow.Core.Config;
using Ivo.GoRest.SpecFlow.Core.ContextContainers;
using Ivo.GoRest.SpecFlow.Core.Support.Models;
using Newtonsoft.Json;

namespace Ivo.GoRest.SpecFlow.Tests.StepDefinitions
{
    [Binding]
    public class UserAssertionSteps
    {
        private UserContextContainer _context;
        public UserAssertionSteps(UserContextContainer context)
        {
            _context = context;
        }

        //Common
        [Then(@"The response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBeOK(string statusCode)
        {
            _context.Response.StatusCode.ToString().Should().Be(statusCode);
        }


        //Get all
        [Then(@"the response should contain a list of users")]
        public async Task ThenTheResponseShouldContainAListOfUsers()
        {
            if ((int)_context.Response.StatusCode / 100 == 4)
            {
                return;
            }
            var content = await _context.Response.Content.ReadAsStringAsync();
            var expectedResponse = JsonConvert.DeserializeObject<List<User>>(content);

            expectedResponse.Should().NotBeEmpty();
        }

        //Create User

        [Then(@"The user should be created successfully")]
        public async Task ThenTheUserShouldBeCreatedSuccessfully()
        {
            var code = (int)_context.Response.StatusCode;
            if (code / 100 == 4)
            {
                return;
            }
            var responsedUser = JsonConvert.DeserializeObject<User>(await _context.Response.Content.ReadAsStringAsync());
            _context.UserIdToDelete = responsedUser.Id;
            _context.TestUser.Name.Should().Be(responsedUser.Name);
            _context.TestUser.Email.Should().Be(responsedUser.Email);
            _context.TestUser.Gender.Should().Be(responsedUser.Gender);
            _context.TestUser.Status.Should().Be(responsedUser.Status);

        }
        [Then(@"The user should be updaates successfully")]
        public async Task ThenTheUserShouldBeUpdaatesSuccessfully()
        {
            var code = (int)_context.Response.StatusCode;
            if (code / 100 == 4)
            {
                return;
            }

            var updatedUser = JsonConvert.DeserializeObject<User>(await _context.Response.Content.ReadAsStringAsync());
            updatedUser.Name.Should().Be(_context.NameToUpdate);
        }






    }
}
