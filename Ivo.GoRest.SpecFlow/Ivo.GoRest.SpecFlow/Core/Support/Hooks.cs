using System.Net.Http.Headers;
using Ivo.GoRest.SpecFlow.Core.Config;
using Ivo.GoRest.SpecFlow.Core.ContextContainers;
using TechTalk.SpecFlow.Infrastructure;

namespace Ivo.GoRest.SpecFlow.Support
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ISpecFlowOutputHelper _outputHelper;
        private UserContextContainer _testContext;
        private readonly BaseConfig _baseConfig;

        public Hooks(ISpecFlowOutputHelper outputHelper, UserContextContainer testContext, BaseConfig baseConfig)
        {
            _outputHelper = outputHelper;
            _testContext = testContext;
            _baseConfig = baseConfig;
        }

        [BeforeScenario]
        public void TearUp()
        {
            _testContext.HttpClient = new HttpClient();
        }

        [BeforeScenario("Authenticate")]
        public void Authenticate()
        {
            _testContext.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _baseConfig.HttpClientConfig.Token);
        }

        [AfterScenario]
        public async Task DeleteCreatedUsers()
        {
                var result = await _testContext.HttpClient.DeleteAsync(_baseConfig.HttpClientConfig.BaseUrl + "users/"+ _testContext.UserIdToDelete);
                _outputHelper.WriteLine($"User with {_testContext.UserIdToDelete} delete have {result.StatusCode} status code");
        }
    }
}
