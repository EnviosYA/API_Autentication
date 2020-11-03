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

namespace PS.Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _service;
        public CuentaController(ICuentaService service)
        {
            _service = service;
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
            if (_service.AltaCuenta(account))
            {
                return Ok(201);
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
                return new ObjectResult(details)
                {
                    ContentTypes = { "application/problem+json" },
                    StatusCode = 501,
                };
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
        private string EjemploWeb(DatosCuentasDTO data )
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
            new Claim(JwtRegisteredClaimNames.UniqueName, data.Mail),
            };
            var payload = new JwtPayload(claims);

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
