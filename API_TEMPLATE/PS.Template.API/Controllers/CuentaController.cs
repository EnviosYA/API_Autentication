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
                return Ok(EjemploWeb(cuentaDTO));
            //return Ok(GenerateToken(cuentaDTO));
            else
                return Unauthorized();
        }
        [Authorize()]
        [HttpPost]
        public IActionResult Post(CuentaDTO account)
        {
            IEnumerable<Claim> cp = this.User.Claims;
             //claims = Claim.
            var cuenta = _service.AltaCuenta(account);
            if (cuenta != null)
            {

                return StatusCode(201, new ResponsePutOK()
                {
                    Id = cuenta.IdCuenta.ToString(),
                    StatusCode = "201",
                    Type = "Cuenta"
                }
                ); ;
            }
            else
            {
                var details = new ProblemDetails()
                {
                    Type = "https://localhost:44311/index.html",
                    Title = "Error al generar Cliente",
                    Detail = "No se ha podido realizar el alta de la cuenta",
                    Instance = Url.Action("Put", "account", new { id = account.Mail }),
                    Status = 501
                };
                return new ObjectResult(details);
                //{
                //    ContentTypes = { "application/problem+json" },
                //    StatusCode = 501,
                //};
            }
        }
        private string GenerateToken(DatosCuentasDTO data)
        {
            var header = new JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("your-secret-key-here")
                    ),
                    SecurityAlgorithms.HmacSha256)
            );

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, data.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, data.Mail),
                new Claim(JwtRegisteredClaimNames.Actort , data.DescTipCuenta),
                new Claim(JwtRegisteredClaimNames.Prn , data.DescEstado)
            };
            var payload = new JwtPayload(
                issuer: "enviosya.com.ar",
                audience: "enviosya.com.ar",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(20)
                );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string EjemploWeb(DatosCuentasDTO data)
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
