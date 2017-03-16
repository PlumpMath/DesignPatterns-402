namespace InterfaceCustomer
{
    // Design pattern :- Strategy pattern
    public interface IValidation<AnyTime> where AnyTime: class 
    {
        void Validate(AnyTime obj);
    }

    public interface IDiscount
    {
        decimal Calculate(ICustomer obj);
    }

    public interface IExtraCharge
    {
        decimal Calculate(ICustomer obj);
    }
}