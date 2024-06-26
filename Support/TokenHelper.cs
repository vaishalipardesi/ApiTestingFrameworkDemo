
using ApiTestingFrameworkDemo.Tests.Configuration;
using ApiTestingFrameworkDemo.Tests.Configuration.Constants;
using ApiTestingFrameworkDemo.Tests.Interfaces;
using ApiTestingFrameworkDemo.Tests.Models;
using ApiTestingFrameworkDemo.Tests.Helper;
using Newtonsoft.Json;
using RestSharp;

namespace ApiTestingFrameworkDemo.Tests.Helper
{
    public class TokenHelper: ITokenHelper
    {
        private readonly TokenConstant _tokenconstant;
        public TokenHelper(TokenConstant tokenconstant)
        {
            _tokenconstant = tokenconstant;           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<AuthResponse> GenerateAuthTokenAsync()
        {
             AuthResponse authResponse = new AuthResponse();
            try
            {
                var authRequest = new AuthRequest
                {
                    ClientId = "DepotManagement",
                    UserName = AzureHelper.GetSecret(_tokenconstant.KeyVaultName, _tokenconstant.UserLogOnName),
                    Password =  AzureHelper.GetSecret(_tokenconstant.KeyVaultName, _tokenconstant.UserPassword),
                    ConnectionType = _tokenconstant.ConnectionType
                };
                authResponse =  await GetAuthTokenAsync(_tokenconstant.AuthUrl, authRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return authResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="authRequest"></param>
        /// <returns></returns>
        public async Task<AuthResponse> GetAuthTokenAsync(string url, AuthRequest authRequest)
        {
            var client_ = new RestClient(url);
            string json = JsonConvert.SerializeObject(authRequest);
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json; charset=\"UTF-8\"");
            request.AddHeader("clientid", _tokenconstant.ClientID);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(json);
            var response= await client_.ExecutePostAsync(request);
            var responseData = JsonConvert.DeserializeObject<AuthResponse>(response.Content);

            //if (!response.IsSuccessStatusCode)
            //    throw new ApiException
            //    {
            //        StatusCode = (int)response.StatusCode,
            //        Content = await response.Content.ReadAsStringAsync()
            //    };

            return responseData;
        }
 
    }
}
