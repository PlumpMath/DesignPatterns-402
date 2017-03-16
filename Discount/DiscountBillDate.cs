using System;
using InterfaceCustomer;

namespace Discount
{
    public class DiscountBillDate : IDiscount
    {
        public decimal Calculate(ICustomer obj)
        {
            if (obj.BillDate.DayOfWeek == DayOfWeek.Saturday || 
                obj.BillDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return (obj.BillAmount * Convert.ToDecimal(0.01));
            }
            return (obj.BillAmount * Convert.ToDecimal(0.005));
        }
    }
}