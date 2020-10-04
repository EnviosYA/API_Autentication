using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PS.Template.Domain.Entities;
using Microsoft.Extensions.Configuration;
using PS.Template.Domain.Interfaces.Service;
using PS.Template.Domain.DTO;

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
        public IActionResult Get([FromBody]UserInfo userInfo)
        {
            //if (!ModelState.IsValid)
            //{
            //    var details = new ValidationProblemDetails(ModelState);
            //    return new ObjectResult(details)
            //    {
            //        ContentTypes = { "application/problem+json" },
            //        StatusCode = 400,
            //    };
            //}
            DatosCuentasDTO cuentaDTO = _service.FindDataAccount(userInfo);

            if (cuentaDTO != null)
                return Ok(GenerateToken(cuentaDTO));
            else
                return Unauthorized();
        }

        [HttpPost]
        public IActionResult Post(CuentaDTO account)
        {
            if (false)//_service.AltaCuenta(account))
            {
                return Ok(204);
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
        private string GenerateToken(DatosCuentasDTO Data)
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
                new Claim(JwtRegisteredClaimNames.UniqueName, Data.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, Data.Mail),
                new Claim(JwtRegisteredClaimNames.Actort , Data.DescTipCuenta),
                new Claim(JwtRegisteredClaimNames.Prn , Data.DescEstado)
            };
            var payload = new JwtPayload(
                issuer : "enviosya.com.ar",
                audience : "enviosya.com.ar",
                claims : claims,
                notBefore : DateTime.Now,
                expires : DateTime.Now.AddMinutes(20)
                );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
