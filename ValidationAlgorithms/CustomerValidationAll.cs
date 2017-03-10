using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceCustomer;

namespace ValidationAlgorithms
{
    public class CustomerValidationAll: IValidation<ICustomer>
    {
        public void Validate(ICustomer obj)
        {
            if (obj.CustomerName.Length == 0)
            {
                throw new Exception("Customer Name is required");
            }

            if (obj.PhoneNumber.Length == 0)
            {
                throw new Exception("Phone Number is required");
            }

            if (obj.BillAmount == 0)
            {
                throw new Exception("Bill Amount is required");
            }

            if (obj.BillDate >= DateTime.Now)
            {
                throw new Exception("Bill Date is not proper");
            }

            if (obj.Address.Length == 0)
            {
                throw new Exception("Address is required");
            }
        }
    }
}
