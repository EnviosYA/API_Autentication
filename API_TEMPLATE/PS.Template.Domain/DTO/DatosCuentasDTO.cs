using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Template.Domain.DTO
{
    public class DatosCuentasDTO
    {
        public string Mail { get; set; }
        public int IdUsuario { get; set; }
        public string NameUser { get; set; }
        public string LastNameUser { get; set; }
        public int CodCuenta { get; set; }
        public int CodEstado { get; set; }
    }
}
