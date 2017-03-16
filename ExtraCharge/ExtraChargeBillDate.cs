using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceCustomer;

namespace ExtraCharge
{
    public class ExtraChargeBillDate: IExtraCharge
    {
        public decimal Calculate(ICustomer obj)
        {
            if (obj.BillDate.DayOfWeek == DayOfWeek.Saturday ||
                obj.BillDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return (obj.BillAmount*Convert.ToDecimal(0.01));
            }
            return 0;
        }
    }
}
