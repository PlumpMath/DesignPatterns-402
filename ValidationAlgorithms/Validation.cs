using System;
using InterfaceCustomer;

namespace ValidationAlgorithms
{
    public class CustomerBasicValidation: IValidation<ICustomer>
    {
        public void Validate(ICustomer obj)
        {
            if (obj.CustomerName.Length == 0)
            {
                throw new Exception("Customer Name is required");
            }
        }
    }

    // Design pattern :- Decorator pattern
    public class ValidationLinker: IValidation<ICustomer>
    {
        private readonly IValidation<ICustomer> _nextValidator;

        public ValidationLinker(IValidation<ICustomer> nextValidator)
        {
            _nextValidator = nextValidator;
        }
        public virtual void Validate(ICustomer obj)
        {
            _nextValidator.Validate(obj);
        }
    }

    public class PhoneValidation : ValidationLinker
    {
        public PhoneValidation(IValidation<ICustomer> nextValidator) : base(nextValidator)
        {
        }

        public override void Validate(ICustomer obj)
        {
            base.Validate(obj); //This will call the top of the cake
            if (obj.PhoneNumber.Length == 0)
            {
                throw new Exception("Phone Number is required");
            }
        }
    }

    public class BillAmountValidation : ValidationLinker
    {
        public BillAmountValidation(IValidation<ICustomer> nextValidator) : base(nextValidator)
        {
        }

        public override void Validate(ICustomer obj)
        {
            base.Validate(obj); //This will call the top of the cake
            if (obj.BillAmount == 0)
            {
                throw new Exception("Bill Amount is required");
            }
        }
    }

    public class BillDateValidation: ValidationLinker
    {
        public BillDateValidation(IValidation<ICustomer> nextValidator) : base(nextValidator)
        {
        }

        public override void Validate(ICustomer obj)
        {
            base.Validate(obj); //This will call the top of the cake
            if (obj.BillDate >= DateTime.Now)
            {
                throw new Exception("Bill Date is not proper");
            }

        }
    }

    public class AddressValidation: ValidationLinker
    {
        public AddressValidation(IValidation<ICustomer> nextValidator) : base(nextValidator)
        {
        }

        public override void Validate(ICustomer obj)
        {
            base.Validate(obj); // This will call the top of the cake
            if (obj.Address.Length == 0)
            {
                throw new Exception("Address is required");
            }
        }
    }
}