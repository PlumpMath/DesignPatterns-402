using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceCustomer;

namespace MiddleLayer
{
    public class Customer: CustomerBase
    {
        public Customer(IValidation<ICustomer> validation) : base(validation)
        {
            this.CustomerType = "Customer";
        }
    }
}
