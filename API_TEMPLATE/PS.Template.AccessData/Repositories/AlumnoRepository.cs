using PS.Template.Domain.Commands;
using PS.Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Template.AccessData.Commands
{
    public class AlumnoRepository : GenericsRepository<Alumno> , IAlumnoRepository
    {
        public AlumnoRepository(BaseDbContext context) : base(context)
        {

        }

    }
}
