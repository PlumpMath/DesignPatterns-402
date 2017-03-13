using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDal;
using System.Data;
using System.Data.SqlClient;

namespace AdoDotNetDAL
{
    public abstract class TemplateADO<AnyType>: AbstractDal<AnyType>
    {
        protected SqlConnection objConn = null;
        protected SqlCommand objCommand = null;

        public TemplateADO(string connectionString) : base(connectionString)
        {
        }

        private void OpenConnection()
        {
            objConn = new SqlConnection(ConnectionString);
            objConn.Open();
            objCommand = new SqlCommand();
            objCommand.Connection = objConn;
        }

        protected abstract void ExecuteCommand(AnyType obj);
        protected abstract List<AnyType> ExecuteCommand();

        private void CloseConnection()
        {
            objConn.Close();
        }

        public void Execute(AnyType obj)
        {
            OpenConnection();
            ExecuteCommand(obj);
            CloseConnection();
        }

        public List<AnyType> Execute()
        {
            OpenConnection();
            var objectTypes = ExecuteCommand();
            CloseConnection();
            return objectTypes;
        }

        public override void Save()
        {
            foreach (var obj in AnyTypes)
            {
                Execute(obj);
            }
        }

        public override List<AnyType> Search()
        {
            return Execute();
        }
    }
}
