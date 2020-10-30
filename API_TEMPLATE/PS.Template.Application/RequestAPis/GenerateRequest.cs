using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PS.Template.Application.RequestAPis
{
    public class GenerateRequest
    {
        private static IConfiguration _configuration;

        public GenerateRequest(IConfiguration configuration )
        {
           _configuration = configuration;
        }
        public static string GetUri(int option)
        {
            string uri = null;
            switch (option)
            {
                case 1:
                    uri = _configuration.GetSection("URI_PRUEBA").Value;
                    break;
                case 2:
                    uri = null;
                    break;
                case 3:
                    uri = null;
                    break;
            }
            return uri;
        }

        public static void ConsultarApiRest(string uri, RestRequest request)
        {
            //RestRequest request;
            IRestClient client;
            IRestResponse queryResult;
            string hash;
            var headers = new Dictionary<string, string>();
            headers.Add("Content - Type", "application / json");
            headers.Add("Accept", "application/json");

            try
            {
                client = new RestClient(uri);
                client.AddDefaultHeader("Content-Type", "application/json");

                request.AddHeaders(headers);
                
                request.RequestFormat = DataFormat.Json;
                queryResult = client.Execute(request);

                if (queryResult.StatusCode == HttpStatusCode.OK)
                {
                    hash = queryResult.Content;
                    //hash = JsonConvert.DeserializeObject<IList<Sexo>>(queryResult.Content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errrrooooor: " + ex.Message);
            }
        }
    }
}