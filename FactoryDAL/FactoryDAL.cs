using System.Configuration;
using AdoDotNetDAL;
using EFDal;
using InterfaceCustomer;
using InterfaceDal;
using Microsoft.Practices.Unity;

namespace FactoryDAL
{
    public static class FactoryDAL<AnyType> // Design pattern :- Simple Factory Pattern
    {
        private static IUnityContainer ObjectsOfOurProjects = null;

        public static AnyType Create(string type)
        {
            // Design pattern :- Lazy loading != Eager loading
            if (ObjectsOfOurProjects == null)
            {
                ObjectsOfOurProjects = new UnityContainer();

                ObjectsOfOurProjects.RegisterType<IDal<CustomerBase>, CustomerDAL>("ADODal");
                ObjectsOfOurProjects.RegisterType<IDal<CustomerBase>, EFCustomerDal>("EFDal");
            }
            // Design pattern :- RIP Replace If with Poly
            return ObjectsOfOurProjects.Resolve<AnyType>(type, new ResolverOverride[]
            {
                new ParameterOverride("connectionString", ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString),
            });
        }
    }
}
