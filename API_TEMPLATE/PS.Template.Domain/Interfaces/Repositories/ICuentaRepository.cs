using PS.Template.Domain.Commands;
using PS.Template.Domain.DTO;
using PS.Template.Domain.Entities;

namespace PS.Template.Domain.Interfaces.Repositories
{
    public interface ICuentaRepository : IGenericsRepository<Cuenta>
    {
        DatosCuentasDTO FindDataAccount(UserInfo userInfo);
    }
}
