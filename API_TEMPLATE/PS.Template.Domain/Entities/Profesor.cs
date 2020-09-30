using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Template.Domain.Entities
{
    public class Profesor
    {
        public Guid ProfesorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public ICollection<Curso> CursosNavigator { get; set; }
    }   
}
