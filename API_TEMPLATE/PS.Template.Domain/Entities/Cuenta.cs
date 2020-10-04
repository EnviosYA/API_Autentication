using System;
using System.Collections.Generic;

namespace PS.Template.Domain.Entities
{
    public partial class Cuenta
    {
        public int IdCuenta { get; set; }
        public string Mail { get; set; }
        public string Contraseña { get; set; }
        public int IdEstado { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoCuenta { get; set; }
        public DateTime? FecAlta { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; }
        public virtual TipoCuenta IdTipoCuentaNavigation { get; set; }
    }
}
