using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTestingFrameworkDemo.Tests.Models
{
    public class AuthResponse
    {

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
    }
}
