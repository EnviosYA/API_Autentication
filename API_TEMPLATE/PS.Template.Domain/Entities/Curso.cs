using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Template.Domain.Entities
{
    public class Curso
    {
        public Guid CursoId { get; set; }
        public string Materia { get; set; }
        public Guid ProfesorId { get; set; }
        public Profesor ProfesorNavigator { get; set; }
        public ICollection<Alumno> AlumnosNavigator { get; set; }
    }
}
