using Newtonsoft.Json;

namespace ApiTestingFrameworkDemo.Tests.Models
{
    public class Result
    {
        [JsonProperty("result_code")]
        public string Resultcode { get; set; }

        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }
        [JsonProperty("identifier_key")]
        public string IdentifierKey { get; set; }

        [JsonProperty("identifier_value")]
        public string IdentifierValue { get; set; }
       
    }
}
