using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceDal;

namespace EFDal
{
    // Design pattern: Adapter pattern
    public class EFDalAbstract<AnyType>: IRepository<AnyType> where AnyType:class
    {
        private DbContext dbContext = null;
        public EFDalAbstract()
        {
            dbContext = new EFUow();//Self contained transaction
        }

        public void SetUnitOfWork(IUow uow)
        {
            dbContext = (EFUow)uow;// Global transaction UOW
        }

        public void Add(AnyType obj)
        {
            dbContext.Set<AnyType>().Add(obj);// in memory commit
        }

        public void Update(AnyType obj)
        {
            throw new NotImplementedException();
        }

        public List<AnyType> Search()
        {
            return dbContext.Set<AnyType>().AsQueryable<AnyType>().ToList<AnyType>();
        }

        public void Save()
        {
            dbContext.SaveChanges();//physical commit
        }
    }
}
