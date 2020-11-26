using PS.Template.AccessData.Commands;
using PS.Template.AccessData.Context;
using PS.Template.Domain.DTO;
using PS.Template.Domain.Entities;
using PS.Template.Domain.Interfaces.Repositories;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Template.AccessData.Repositories
{
    public class CuentaRepository : GenericsRepository<Cuenta>, ICuentaRepository
    {
        private readonly DbAutenticacionContext _context;
        public CuentaRepository(DbAutenticacionContext context) : base(context)
        {
            _context = context;
        }
        public virtual DatosCuentasDTO FindDataAccount(UserInfo userInfo)
        {
            IQueryable<DatosCuentasDTO> account = from cuenta in _context.Cuenta
                                      join estado in _context.Estado on cuenta.IdEstado equals estado.IdEstado
                                      join tipcuenta in _context.TipoCuenta on cuenta.IdTipoCuenta equals tipcuenta.IdTipoCuenta
                                      where cuenta.Mail == userInfo.Email
                                      where cuenta.Contraseña == userInfo.Password
                                      select new DatosCuentasDTO
                                      {
                                          Mail = cuenta.Mail,
                                          IdUsuario = cuenta.IdUsuario,
                                          CodEstado = estado.IdEstado,
                                          CodCuenta = tipcuenta.IdTipoCuenta
                                      };

            return account.FirstOrDefault();
        }
        
        public bool FindMail(string mail)
        {
            IQueryable<string> account = from cuenta in _context.Cuenta
                                         where cuenta.Mail == mail
                                         select cuenta.Mail;
            

            return account.Any();
        }
    }
}
