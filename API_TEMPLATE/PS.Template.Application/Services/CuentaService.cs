using PS.Template.Application.RequestAPis;
using PS.Template.Application.Services.Base;
using PS.Template.Domain.DTO;
using PS.Template.Domain.Entities;
using PS.Template.Domain.Interfaces.Repositories;
using PS.Template.Domain.Interfaces.Service;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Template.Application.Services
{
    public class CuentaService : BaseService<Cuenta>, ICuentaService
    {
        private readonly ICuentaRepository _repository;
        public CuentaService(ICuentaRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public virtual DatosCuentasDTO FindDataAccount(UserInfo userInfo)
        {
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
            string uri = GenerateRequest.GetUri(1);
            RestRequest request = new RestRequest(Method.GET);
            GenerateRequest.ConsultarApiRest(uri, request);
        }
    }
}
