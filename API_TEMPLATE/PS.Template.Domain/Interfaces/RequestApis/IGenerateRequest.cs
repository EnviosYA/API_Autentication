using PS.Template.Domain.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Template.Domain.Interfaces.RequestApis
{
    public interface IGenerateRequest
    {
        public IEnumerable<T> ConsultarApiRest<T>(string uri, RestRequest request);
        public string GetUri(int option);
        public void LeerClaims();
    }
}
