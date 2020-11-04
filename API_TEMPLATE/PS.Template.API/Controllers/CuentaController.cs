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

        [HttpGet]
        public IActionResult Get([FromQuery] UserInfo userInfo)
        {
            DatosCuentasDTO cuentaDTO = _service.FindDataAccount(userInfo);

            if (cuentaDTO != null)
                return Ok(GenerateToken(cuentaDTO));
            else
                return Unauthorized();
        }

        [HttpPost]
        public IActionResult Post(CuentaDTO account)
        {
            var cuenta = _service.AltaCuenta(account);
            if (cuenta != null)
            {

                return new JsonResult(new ResponsePutOK()
                {
                    Id = cuenta.IdCuenta.ToString(),
                    Type = "Cuenta"
                }
                ) { StatusCode = 201 };
            }
            else
            {
                var details = new ProblemDetails()
                {
                    Type = "https://localhost:44311/index.html",
                    Title = "Error al generar Cliente",
                    Detail = "No se ha podido realizar el alta de la cuenta",
                    Instance = Url.Action("Put", "account", new { id = account.Mail }),
                };
                return new JsonResult(details) { StatusCode = 501 };
            }
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
                new Claim("account type" , data.DescTipCuenta),
                new Claim("State" , data.DescEstado),
                new Claim("Name",data.NameUser),
                new Claim("LastName",data.LastNameUser)
            };
            var payload = new JwtPayload(claims);

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
