using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using PS.Template.Domain.Interfaces.RequestApis;
using PS.Template.Domain.DTO;
using Newtonsoft.Json;

namespace PS.Template.Application.RequestAPis
{
    public class GenerateRequest : IGenerateRequest
    {
        private static IConfiguration _configuration;

        public GenerateRequest(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetUri(int option)
        {
            string uri = null;
            switch (option)
            {
                case 1:
                    uri = _configuration.GetSection("URL:URI_PRUEBA").Value;
                    break;
                case 2:
                    uri = _configuration.GetSection("URL:URI_USUARIO").Value;
                    break;
                case 3:
                    uri = null;
                    break;
            }
            return uri;
        }
        public IEnumerable<ResponseGetAllUsuarios> ConsultarApiRest(string uri, RestRequest request)
        {
            IRestClient client;
            IRestResponse queryResult;
            IEnumerable<ResponseGetAllUsuarios> hash = null;
            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "Accept", "application/json" }
            };
            try
            {
                //client = new RestClient(uri)
                //{
                //    Authenticator = new JwtAuthenticator("")
                //};
                client = new RestClient(uri);
                client.AddDefaultHeader("Content-Type", "application/json");
                request.AddHeaders(headers);
                request.RequestFormat = DataFormat.Json;
                queryResult = client.Execute(request);
                
                if (queryResult.StatusCode == HttpStatusCode.OK)
                {
                    hash = JsonConvert.DeserializeObject<IList<ResponseGetAllUsuarios>>(queryResult.Content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errrrooooor: " + ex.Message);
            }
            return hash;
        }
    }
}