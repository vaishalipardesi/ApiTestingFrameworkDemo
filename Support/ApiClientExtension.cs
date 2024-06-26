using Flurl;
using Flurl.Http;
using Sainsburys.DepotManagement.Depots.Models;

namespace ApiTestingFrameworkDemo.Support
{

    public static class ApiClientExtension
    {
       public static async Task<string> GetFlurClientDataAsync(this FlurlClient client, string endpoint)
        {
            var m_client = TestBase.Instance.GetFlurlClient();
            var token = await TestBase.Instance.GetToken();

            HttpResponseMessage response = await StringExtensions.AppendPathSegments("", endpoint)
                     .WithClient(m_client).WithOAuthBearerToken(token)
             .GetAsync();

            TestBase.Instance.testData.AddResponseJson(response);
            return response.Content.ReadAsStringAsync().Result;
        }
        public static async Task<HttpResponseMessage> FlurlClientPostAsync(this FlurlClient client, DepotSubscriptionModel data, string endpoint)
        {
            var m_client = TestBase.Instance.GetFlurlClient();
            var token =  TestBase.Instance.GetToken();           

            string searchUrl = m_client.BaseUrl;
            var url = searchUrl
                .AppendPathSegment(endpoint)
                .WithOAuthBearerToken(token.ToString());

            HttpResponseMessage result = await url.PostJsonAsync(data);
            return result;
        }
    }
}
