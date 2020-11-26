using PS.Template.Domain.DTO;
using PS.Template.Domain.Entities;
using PS.Template.Domain.Service.Base;

namespace PS.Template.Domain.Interfaces.Service
{
    public interface ICuentaService : IBaseService<Cuenta>
    {
        DatosCuentasDTO FindDataAccount(UserInfo userInfo);
        Cuenta AltaCuenta(CuentaDTO account);
        bool ValidarCuenta(string mail);
    }
}
