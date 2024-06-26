using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTestingFrameworkDemo.Tests.Models
{
    public class AuthRequest
    {
        [JsonProperty("applicationId")]
        public string ApplicationId { get; set; }
        [JsonProperty("clientid")]
        public string ClientId { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("connectionType")]
        public string ConnectionType { get; set; }
       
    }
}
