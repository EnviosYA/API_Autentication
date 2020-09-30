using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PS.Template.Application.Services;
using PS.Template.Domain.DTO;
using PS.Template.Domain.Entities;
using PS.Template.Domain.Service;

namespace PS.Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoService _service;
        public AlumnoController(IAlumnoService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<bool> Post(AlumnoDto alumno)
        {
            try
            {
                var entitity = new Alumno
                {
                    AlumnoId = Guid.NewGuid(),
                    Apellido = alumno.Apellido,
                    Nombre = alumno.Nombre,
                    CursoId = Guid.Parse(alumno.CursoId),
                    Legajo = alumno.Legajo
                };
                _service.Add(entitity);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
        }


        [HttpGet]
        public ActionResult<bool> Get(AlumnoDto alumno)
        {
            return true;
        }
    }
}
