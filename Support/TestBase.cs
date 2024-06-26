using ApiTestingFrameworkDemo.Tests.Configuration.Constants;
using ApiTestingFrameworkDemo.Tests.Helper;
using ApiTestingFrameworkDemo.Tests.Interfaces;
using ApiTestingFrameworkDemo.Tests.Models;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Reflection;

namespace ApiTestingFrameworkDemo.Support
{
    public partial class TestBase
    {
        private IConfiguration configuration;
        private Dictionary<string, string> appSettings;
        public Dictionary<object, object> testData;
        public HttpResponseMessage Response { get; set; }

        #region Singleton
        private static TestBase instance;
        private static readonly object lockObject = new object();

        private ITokenHelper _tokenHelper;
        private TokenConstant _tokenConstant;
        public FlurlClient m_client;

        private TestBase()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json");
            testData = new Dictionary<object, object> ();
            configuration = configBuilder.Build();
            appSettings = DictionaryExtension.GetDictionary(configuration, "AppSettings");
            testData.Add("responseCounter", 0);

            _tokenConstant = new TokenConstant();
            _tokenHelper = new TokenHelper(_tokenConstant);
        }

        public static TestBase Instance
        {
            get
            {
                // Double-checked locking for thread safety
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new TestBase();
                        }
                    }
                }

                return instance;
            }
        }
        #endregion
        public FlurlClient GetFlurlClient()
        {
            if(m_client == null)
            {
                Uri apiURL = new Uri(appSettings["TestApiUrl"]);
                m_client =  new FlurlClient(apiURL.AbsoluteUri);
            }
            return m_client;
        }
        public async Task<string> GetToken()
        {
            AuthResponse authResponse = new AuthResponse();
            var client_ = new RestClient(_tokenConstant.AuthUrl);
            authResponse = await _tokenHelper.GenerateAuthTokenAsync();
            return authResponse.AccessToken;
        }

        public static void ClearInstance()
        {
            lock (lockObject)
            {
                instance = null;
            }
        }
    }
}
