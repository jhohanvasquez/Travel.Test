using Newtonsoft.Json;
using Travel.Business.Contracts;
using Travel.Business.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Travel.DataAccess.Common
{
    public class RestClient<T> : IRestClient<T> where T : class
    {
        private string RestUri;

        public RestClient(string restUri)
        {
            this.RestUri = restUri;
        }

        public T GetAsync(string token, string uriParams, Dictionary<string, string> headerParameters = null)
        {
            try
            {
                string url = this.RestUri;

                url += (string.IsNullOrEmpty(uriParams) ? "" : uriParams);

                using (var client = new HttpClient())
                {
                    try
                    {
                        //Add authentication 
                        client.DefaultRequestHeaders.Clear();
                        if (!string.IsNullOrEmpty(token))
                            client.DefaultRequestHeaders.Add("Authorization", token);

                        if (headerParameters != null)
                        {
                            foreach (var param in headerParameters)
                            {
                                client.DefaultRequestHeaders.Add(param.Key, param.Value);
                            }
                        }

                        var response = client.GetStringAsync(url).Result;  // Blocking call! 
                        return JsonConvert.DeserializeObject<T>(response.ToString());
                    }
                    catch (HttpRequestException ex)
                    {
                        throw new Exception("Error en consumo de metodo get de servicio rest: " + this.RestUri, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en consumo de metodo get de servicio rest: " + this.RestUri, ex);
            }
        }
    }
}
