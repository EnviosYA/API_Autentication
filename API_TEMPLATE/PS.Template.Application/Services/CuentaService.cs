using PS.Template.Application.Services.Base;
using PS.Template.Domain.DTO;
using PS.Template.Domain.Entities;
using PS.Template.Domain.Interfaces.Repositories;
using PS.Template.Domain.Interfaces.RequestApis;
using PS.Template.Domain.Interfaces.Service;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Claims;

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
            
            DatosCuentasDTO cuentaDTO =  _repository.FindDataAccount(userInfo);
            if (cuentaDTO != null)
            {
                IEnumerable<ResponseGetAllUsuarios> user = GetDataApi(cuentaDTO.IdUsuario);
                foreach (var item in user)
                {
                    cuentaDTO.NameUser = item.Nombre;
                    cuentaDTO.LastNameUser = item.Apellido;
                }
            }
            
            return cuentaDTO;
        }
        public virtual Cuenta AltaCuenta(CuentaDTO account)
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

                return cuenta;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<ResponseGetAllUsuarios> GetDataApi(int usuario)
        {
            string uri = _request.GetUri(2);
            RestRequest request = new RestRequest(Method.GET);
            request.AddQueryParameter("id", usuario.ToString());
            IEnumerable<ResponseGetAllUsuarios> user= _request.ConsultarApiRest<ResponseGetAllUsuarios>(uri, request);

            return user;
        }
    }
}