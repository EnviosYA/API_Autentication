using PS.Template.Application.Services.Base;
using PS.Template.Domain.DTO;
using PS.Template.Domain.Entities;
using PS.Template.Domain.Interfaces.Repositories;
using PS.Template.Domain.Interfaces.RequestApis;
using PS.Template.Domain.Interfaces.Service;
using RestSharp;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
            userInfo.Password = HashPassword(userInfo.Password);

            DatosCuentasDTO cuentaDTO = _repository.FindDataAccount(userInfo);

            if (cuentaDTO != null)
            {
                ResponseGetAllUsuarios user = GetDataApi(cuentaDTO.IdUsuario);
                if (user != null)
                {
                    cuentaDTO.NameUser = user.Nombre;
                    cuentaDTO.LastNameUser = user.Apellido;
                }
                else
                {
                    cuentaDTO = null;
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
                    Contraseña = HashPassword(account.Contraseña),
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
        public virtual bool ValidarCuenta(string mail)
        {
            return _repository.FindMail(mail);
        }


        public ResponseGetAllUsuarios GetDataApi(int usuario)
        {
            string uri = _request.GetUri(2);
            RestRequest request = new RestRequest(Method.GET);
            request.AddQueryParameter("id", usuario.ToString());
            ResponseGetAllUsuarios user = _request.ConsultarApiRest<ResponseGetAllUsuarios>(uri, request).First();

            return user;
        }

        private string HashPassword(string keword)
        {
            string hash;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(keword);
                byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }

            return hash;
        }
    }
}