using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTestingFrameworkDemo.Models.Response
{
    public class ODataResponse<T>
    {
        [JsonProperty("@odata.context")]
        public string ODataContext { get; set; }
        [JsonProperty("value")]
        public IEnumerable<T> Value { get; set; }
        [JsonProperty("@odata.nextLink")]
        public string ODataNextLink { get; set; }
    }
}
