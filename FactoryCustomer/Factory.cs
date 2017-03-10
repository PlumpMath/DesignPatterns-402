using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleLayer;
using Microsoft.Practices.Unity;
using InterfaceCustomer;
using ValidationAlgorithms;

namespace FactoryCustomer
{
    public static class Factory // Design pattern :- Simple Factory Pattern
    {
        private static IUnityContainer custs = null;
        
        public static ICustomer Create(string typeCust)
        {
            // Design pattern :- Lazy loading != Eager loading
            if (custs == null)
            {
                custs = new UnityContainer();
                custs.RegisterType<ICustomer, Customer>("Customer", new InjectionConstructor(new CustomerValidationAll()));
                custs.RegisterType<ICustomer, Lead>("Lead", new InjectionConstructor(new LeadValidation()));
            }
            // Design pattern :- RIP Replace If with Poly
            return custs.Resolve<ICustomer>(typeCust);
        }
    }
}
