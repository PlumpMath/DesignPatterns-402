using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceDal
{
    // Design pattern :- Generic Repository pattern
    public interface IDal<AnyType>
    {
        void Add(AnyType obj); // In memory addtion
        void Update(AnyType obj); //In memory updation
        List<AnyType> Search();
        void Save();// Physical commit
    }
}
