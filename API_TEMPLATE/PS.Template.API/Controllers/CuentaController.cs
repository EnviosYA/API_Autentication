using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PS.Template.Domain.Entities;
using PS.Template.Domain.Interfaces.Service;
using PS.Template.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using TP2.Domain.DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace PS.Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _service;
        private readonly IConfiguration _configuration;
        public CuentaController(ICuentaService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public IActionResult Get(UserInfo userInfo)
        {
            DatosCuentasDTO cuentaDTO = _service.FindDataAccount(userInfo);

            if (cuentaDTO != null)
                return new JsonResult(Ok(GenerateToken(cuentaDTO)));
            else
                return new JsonResult(Unauthorized()) { StatusCode = 401 };
        }

        [HttpPost]
        public IActionResult Post(CuentaDTO account)
        {
            int status = 400;

            if (!_service.ValidarCuenta(account.Mail))
            {
                status = 501;
                var cuenta = _service.AltaCuenta(account);
                if (cuenta != null)
                {
                    return new JsonResult(new ResponsePutOK()
                    {
                        Id = cuenta.IdCuenta.ToString(),
                        Type = "Cuenta"
                    })
                    { StatusCode = 201 };
                }
            }

            var details = new ProblemDetails()
            {
                Type = "Cuenta",
                Title = "Error al generar Cliente",
                Detail = status == 400 ? "El usuario se encuentra registrado ":"No se ha podido realizar el alta de la cuenta",
                Instance = Url.Action("Put", "account", new { id = account.Mail }),
            };
            return new JsonResult(details) { StatusCode = status };
        }

        private string GenerateToken(DatosCuentasDTO data)
        {
            var header = new JwtHeader(
            new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration.GetSection("Autenticacion:SecretKey").Value)
                ),
                SecurityAlgorithms.HmacSha256)
         );

            var claims = new Claim[]
            {
                new Claim("IdUser", data.IdUsuario.ToString()),
                new Claim("Email", data.Mail),
                new Claim("accountType" , data.CodCuenta.ToString()),
                new Claim("State" , data.CodEstado.ToString()),
                new Claim("Name",data.NameUser),
                new Claim("LastName",data.LastNameUser)
            };
            var payload = new JwtPayload(
                 issuer: "Encioya.com",
                audience: "envioya.com",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(6)
                );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
