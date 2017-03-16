﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            foreach (var temp in AnyTypes)
            {
                if (obj.Equals(temp))
                {
                    return;
                }
            }
            AnyTypes.Add(obj);
        }

        public IEnumerable<AnyType> GetData()
        {
            return AnyTypes;
        }

        public AnyType GetData(int index)
        {
            return AnyTypes[index];
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
