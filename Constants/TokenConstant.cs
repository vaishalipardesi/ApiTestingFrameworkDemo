using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTestingFrameworkDemo.Tests.Configuration.Constants
{
    public class TokenConstant
    {
        public string AuthUrl = "https://logisticsidentity-dev.logisticsapps.js-devops.co.uk/identityapi/account/login";
        public string UserLogOnName = "JwtTokenConfig--Username";
        public  string UserPassword = "JwtTokenConfig--Password";
        public  string ConnectionType = "Integration";
        public  string KeyVaultName = "dma-kv-dev";
        public string ClientID = "DepotManagement";
        public Int16 TokenExpiryMinutes = 10;
    }
}
