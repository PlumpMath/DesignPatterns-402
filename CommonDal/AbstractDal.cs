using System;
using System.Collections.Generic;
using InterfaceDal;

namespace CommonDal
{
    public abstract class AbstractDal<AnyType> : IDal<AnyType>
    {
        protected string ConnectionString = "";
        protected List<AnyType> AnyTypes = new List<AnyType>();

        public AbstractDal(string connectionString)
        {
            ConnectionString = connectionString;
        }
        
        public virtual void Add(AnyType obj)
        {
            AnyTypes.Add(obj);
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
