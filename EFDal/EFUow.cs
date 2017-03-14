using System.Data.Entity;
using InterfaceCustomer;
using InterfaceDal;

namespace EFDal
{
    public class EFUow: DbContext, IUow
    {
        public EFUow(): base("name=CustomerContext")
        {
        }

        //mapping
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Mapping code
            modelBuilder.Entity<CustomerBase>().ToTable("Customer");
        }

        public void Commit()
        {
            SaveChanges();
        }

        public void Rollback() // Adapter
        {
            Dispose();
        }
    }
}