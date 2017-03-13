﻿using System;
using System.ComponentModel.DataAnnotations;

namespace InterfaceCustomer
{
    public class CustomerBase: ICustomer
    {
        private IValidation<ICustomer> _validation = null;
        public CustomerBase()
        {
            this.CustomerName = "";
            this.PhoneNumber = "";
            this.BillAmount = 0;
            this.BillDate = new DateTime();
            this.Address = "";
        }

        public CustomerBase(IValidation<ICustomer> validation)
        {
            _validation = validation;
        }

        [Key]
        public int Id { get; set; }

        public string CustomerType { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }

        public virtual void Validate()
        {
            _validation.Validate(this);
        }
    }
}