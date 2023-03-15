using Travel.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Business.Contracts
{
    public interface IRestClient<T> where T : class
    {
        T GetAsync(string token, string uriParams, Dictionary<string, string> headerParameters = null);
    }
}
