﻿using System;
using System.ComponentModel.DataAnnotations;

namespace InterfaceCustomer
{
    public class CustomerBase: ICustomer
    {
        private IValidation<ICustomer> _validation = null;
        private IDiscount _discount = null;
        private IExtraCharge _extraCharge = null;
        //Desgin pattern :- memento pattern (Revert old state)
        private ICustomer _oldCopy = null;

        public CustomerBase(IValidation<ICustomer> validation, IDiscount discount, IExtraCharge extraCharge)
        {
            _validation = validation;
            _discount = discount;
            _extraCharge = extraCharge;
        }

        [Key]
        public int Id { get; set; }

        public string CustomerType { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }

        public override bool Equals(object obj)
        {
            var customer = (ICustomer) obj;
            return customer != null && Id == customer.Id;
        }

        public virtual void Validate()
        {
            _validation.Validate(this);
        }

        public void Clone()
        {
            _oldCopy = (ICustomer)this.MemberwiseClone();
        }

        public void Revert()
        {
            this.CustomerName = _oldCopy.CustomerName;
            this.Address = _oldCopy.Address;
            this.BillAmount = _oldCopy.BillAmount;
            this.BillDate = _oldCopy.BillDate;
            this.CustomerType = _oldCopy.CustomerType;
            this.PhoneNumber = _oldCopy.PhoneNumber;
        }

        public decimal ActualCost()
        {
            return (BillAmount - _discount.Calculate(this)) + _extraCharge.Calculate(this);
        }
    }
}