using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceCustomer;

namespace Discount
{
    public class DiscountBillAmount: IDiscount
    {
        public decimal Calculate(ICustomer obj)
        {
            if (obj.BillAmount > 10000)
            {
                return (obj.BillAmount*Convert.ToDecimal(0.02));
            }
            return (obj.BillAmount*Convert.ToDecimal(0.01));
        }
    }
}
