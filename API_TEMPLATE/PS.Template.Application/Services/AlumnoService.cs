using PS.Template.Domain.Commands;
using PS.Template.Domain.DTO;
using PS.Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using PS.Template.Domain.Service;
using PS.Template.Application.Services.Base;

namespace PS.Template.Application.Services
{

    public class AlumnoService : BaseService<Alumno>, IAlumnoService
    {
                
        public AlumnoService(IAlumnoRepository repository) : base(repository)
        {
        }
        /*
        public Alumno CreateAlumno(AlumnoDto alumno)
        {
            var entity = new Alumno
            {
                AlumnoId = Guid.NewGuid(),
                Apellido = alumno.Apellido,
                Nombre = alumno.Nombre,
                CursoId = Guid.Parse(alumno.CursoId),
                Legajo = alumno.Legajo
            };

            _repository.Add<Alumno>(entity);

            return entity;
        }*/
    }
}
