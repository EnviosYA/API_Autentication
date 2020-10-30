using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Template.Domain.DTO
{
    public class CuentaDTO
    {
        public string Mail { get; set; }
        public string Contraseña { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoCuenta { get; set; }
    }
}
