using PS.Template.Application.Services.Base;
using PS.Template.Domain.Entities;
using PS.Template.Domain.Interfaces.Repositories;
using PS.Template.Domain.Interfaces.Service;

namespace PS.Template.Application.Services
{
    public class TipoCuentaService : BaseService<TipoCuenta>, ITipoCuentaService
    {
        public TipoCuentaService(ITipoCuentaRepository repisitory) : base(repisitory)
        {

        }
    }
}
