using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleLayer;
using Microsoft.Practices.Unity;
using InterfaceCustomer;
using ValidationAlgorithms;

namespace FactoryCustomer
{
    public static class Factory<AnyType> // Design pattern :- Simple Factory Pattern
    {
        private static IUnityContainer ObjectsOfOurProjects = null;
        
        public static AnyType Create(string type)
        {
            // Design pattern :- Lazy loading != Eager loading
            if (ObjectsOfOurProjects == null)
            {
                ObjectsOfOurProjects = new UnityContainer();
                ObjectsOfOurProjects.RegisterType<ICustomer, Customer>("Customer", new InjectionConstructor(new CustomerValidationAll()));
                ObjectsOfOurProjects.RegisterType<ICustomer, Lead>("Lead", new InjectionConstructor(new LeadValidation()));
            }
            // Design pattern :- RIP Replace If with Poly
            return ObjectsOfOurProjects.Resolve<AnyType>(type);
        }
    }
}
