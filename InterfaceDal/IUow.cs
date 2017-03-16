namespace InterfaceDal
{
    // Design pattern :- Unit of Work pattern
    public interface IUow
    {
        void Commit();
        void RollBack();
    }
}