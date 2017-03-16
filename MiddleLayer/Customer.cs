﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceCustomer;

namespace MiddleLayer
{
    public class Customer: CustomerBase
    {
        public Customer(IValidation<ICustomer> validation, string custType) : base(validation)
        {
            this.CustomerType = custType;
        }
    }
}
