using System.Data.Entity;
using InterfaceCustomer;

namespace EFDal
{
    public class EFCustomerDal: EFDalAbstract<CustomerBase>
    {
        //mapping
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Mapping code
            modelBuilder.Entity<CustomerBase>().ToTable("Customer");
        }
    }
}