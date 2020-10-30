using PS.Template.AccessData.Commands;
using PS.Template.AccessData.Context;
using PS.Template.Domain.Entities;
using PS.Template.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Template.AccessData.Repositories
{
    public class EstadoRepository : GenericsRepository<Estado>, IEstadoRepository
    {
        public EstadoRepository(DbAutenticacionContext context) : base(context)
        {

        }
    }
}
