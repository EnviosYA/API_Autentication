using PS.Template.Application.Services.Base;
using PS.Template.Domain.DTO;
using PS.Template.Domain.Entities;
using PS.Template.Domain.Interfaces.Repositories;
using PS.Template.Domain.Interfaces.RequestApis;
using PS.Template.Domain.Interfaces.Service;
using RestSharp;
using System;
using System.Collections.Generic;

namespace PS.Template.Application.Services
{
    public class CuentaService : BaseService<Cuenta>, ICuentaService
    {
        private readonly ICuentaRepository _repository;
        private readonly IGenerateRequest _request;

        public CuentaService(ICuentaRepository repository, IGenerateRequest generate) : base(repository)
        {
            _repository = repository;
            _request = generate;
        }
        public virtual DatosCuentasDTO FindDataAccount(UserInfo userInfo)
        {
            GetDataApi();
            return _repository.FindDataAccount(userInfo);
        }
        public virtual bool AltaCuenta(CuentaDTO account)
        {
            try
            {
                Cuenta cuenta = new Cuenta()
                {
                    Mail = account.Mail,
                    Contraseña = account.Contraseña,
                    IdEstado = 1,
                    IdUsuario = account.IdUsuario,
                    IdTipoCuenta = account.IdTipoCuenta,
                    FecAlta = DateTime.Now
                };
                _repository.Add(cuenta);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void GetDataApi()
        {
            string uri = _request.GetUri(2);
            RestRequest request = new RestRequest(Method.GET);
            request.AddQueryParameter("id", "1");
            IEnumerable<ResponseGetAllUsuarios> user= _request.ConsultarApiRest(uri, request);
        }
    }
}