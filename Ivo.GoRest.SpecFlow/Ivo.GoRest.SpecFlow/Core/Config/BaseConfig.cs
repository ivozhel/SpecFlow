using Microsoft.Extensions.Configuration;

namespace Ivo.GoRest.SpecFlow.Core.Config
{
    public class BaseConfig
    {
        public BaseConfig()
        {
            var config = new ConfigurationBuilder().AddJsonFile("specflow.json").Build();
            HttpClientConfig = config.GetSection("HttpClient").Get<HttpClientConfig>();
        }

        public HttpClientConfig HttpClientConfig { get; set; }

    }
}
