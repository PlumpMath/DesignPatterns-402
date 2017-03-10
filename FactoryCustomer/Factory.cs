using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleLayer;

namespace FactoryCustomer
{
    public static class Factory // Design pattern :- Simple Factory Pattern
    {
        private static Dictionary<string, CustomerBase> custs = new Dictionary<string, CustomerBase>();
        
        public static CustomerBase Create(string typeCust)
        {
            // Design pattern :- Lazy loading != Eager loading
            if (custs.Count == 0)
            {
                custs.Add("Customer", new Customer());
                custs.Add("Lead", new Lead());
            }
            // Design pattern :- RIP Replace If with Poly
            return custs[typeCust];
        }
    }
}
