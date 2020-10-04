using System;
using System.Collections.Generic;

namespace PS.Template.Domain.Entities 
{ 
    public partial class Estado
    {
        public Estado()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        public int IdEstado { get; set; }
        public string DescEstado { get; set; }
        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
