using PS.Template.Domain.Commands;
using PS.Template.Domain.Service.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Template.Application.Services.Base
{
    public class BaseService<E> : IBaseService<E> where E : class
    {
        protected IGenericsRepository<E> Repository;

        public BaseService(IGenericsRepository<E> repository)
        {
            Repository = repository;
        }

        public virtual void Add(E entity)
        {
            Repository.Add(entity);
        }

        public virtual void Delete(E entity)
        {
            Repository.Delete(entity);
        }

        public virtual void Delete(int id)
        {
            Repository.Delete(id);
        }
        public virtual void Update(E entity)
        {
            Repository.Edit(entity);
        }
        public virtual void Edit(E entity)
        {
            Repository.Edit(entity);
        }
    }
}
