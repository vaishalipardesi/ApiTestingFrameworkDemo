using ApiTestingFrameworkDemo.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTestingFrameworkDemo.Tests.Interfaces
{
    public interface ITokenHelper
    {
       public Task<AuthResponse> GetAuthTokenAsync(string url, AuthRequest authRequest);
        public Task<AuthResponse> GenerateAuthTokenAsync();
    }
}
