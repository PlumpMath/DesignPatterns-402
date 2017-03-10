using System;

namespace MiddleLayer
{
    public class CustomerBase
    {
        public CustomerBase()
        {
            this.CustomerName = "";
            this.PhoneNumber = "";
            this.BillAmount = 0;
            this.BillDate = new DateTime();
            this.Address = "";
        }

        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }

        public virtual void Validate()
        {
            throw new Exception("Not Implemented");
        }
    }
}