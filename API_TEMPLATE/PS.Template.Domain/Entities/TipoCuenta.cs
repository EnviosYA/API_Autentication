using System;
using System.Collections.Generic;

namespace PS.Template.Domain.Entities
{ 
    public partial class TipoCuenta
    {
        public TipoCuenta()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        public int IdTipoCuenta { get; set; }
        public string DescTipCuenta { get; set; }

        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
