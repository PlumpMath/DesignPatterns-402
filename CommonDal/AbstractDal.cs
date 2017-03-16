using System;
using System.Collections.Generic;
using InterfaceDal;

namespace CommonDal
{
    public abstract class AbstractDal<AnyType> : IRepository<AnyType>
    {
        protected List<AnyType> AnyTypes = new List<AnyType>();

        public virtual void SetUnitOfWork(IUow uow)
        {
            throw new NotImplementedException();
        }

        public virtual void Add(AnyType obj)
        {
            AnyTypes.Add(obj);
        }

        public IEnumerable<AnyType> GetData()
        {
            return AnyTypes;
        }

        public virtual void Save()
        {
            throw new NotImplementedException();
        }

        public virtual List<AnyType> Search()
        {
            throw new NotImplementedException();
        }

        public virtual void Update(AnyType obj)
        {
            throw new NotImplementedException();
        }
    }
}
