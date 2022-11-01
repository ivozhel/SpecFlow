using Ivo.GoRest.SpecFlow.Core.Support.Models;

namespace Ivo.GoRest.SpecFlow.Core.ContextContainers
{
    public class UserContextContainer
    {
        public User TestUser { get; set; }
        public HttpResponseMessage Response { get; set; }
        public HttpClient HttpClient { get; set; }
        public int UserIdToDelete { get; set; }
        public string NameToUpdate { get; set; }
    }
}
