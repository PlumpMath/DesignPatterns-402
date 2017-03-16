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

                //Lead
                IValidation<ICustomer> leadValidation = new PhoneValidation(new CustomerBasicValidation());
                ObjectsOfOurProjects.RegisterType<CustomerBase, Customer>("Lead", new InjectionConstructor(leadValidation, "Lead"));

                //SelfService
                IValidation<ICustomer> selfServiceValidation = new CustomerBasicValidation();
                ObjectsOfOurProjects.RegisterType<CustomerBase, Customer>("SelfService",
                    new InjectionConstructor(selfServiceValidation, "SelfService"));

                //HomeDelivery
                IValidation<ICustomer> homeDeliveryValidation = new AddressValidation(new CustomerBasicValidation());
                ObjectsOfOurProjects.RegisterType<CustomerBase, Customer>("HomeDelivery",
                    new InjectionConstructor(homeDeliveryValidation, "HomeDelivery"));

                //Customer
                IValidation<ICustomer> customerValidation = new AddressValidation(new BillDateValidation(new BillAmountValidation(new PhoneValidation(new CustomerBasicValidation()))));
                ObjectsOfOurProjects.RegisterType<CustomerBase, Customer>("Customer", new InjectionConstructor(customerValidation, "Customer"));
                
            }
            // Design pattern :- RIP Replace If with Poly
            return ObjectsOfOurProjects.Resolve<AnyType>(type);
        }
    }
}
