using System.Configuration;
using System.Data.SqlClient;
using InterfaceDal;

namespace AdoDotNetDAL
{
    public class AdoUow: IUow
    {
        public SqlConnection Connection { get; set; }
        public SqlTransaction Transaction { get; set; }

        public AdoUow()
        {
            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CustomerContext"].ToString());
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }

        public void Commit()
        {
            Transaction.Commit();
            Connection.Close();
        }

        public void RollBack() // Design pattern :- object Adapter pattern
        {
            Transaction.Dispose();
            Connection.Close();
        }
    }
}