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
    public class EFDalAbstract<AnyType>: DbContext, IDal<AnyType> where AnyType:class 
    {
        public EFDalAbstract(): base("name=CustomerContext")
        {
            
        }
        public void Add(AnyType obj)
        {
            Set<AnyType>().Add(obj);// in memory commit
        }

        public void Update(AnyType obj)
        {
            throw new NotImplementedException();
        }

        public List<AnyType> Search()
        {
            return Set<AnyType>().AsQueryable<AnyType>().ToList<AnyType>();
        }

        public void Save()
        {
            SaveChanges();//physical commit
        }
    }
}
