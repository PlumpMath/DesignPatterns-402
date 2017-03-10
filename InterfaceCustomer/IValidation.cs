namespace InterfaceCustomer
{
    // Design pattern :- Strategy pattern
    public interface IValidation<AnyTime> where AnyTime: class 
    {
        void Validate(AnyTime obj);
    }
}