using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDal;
using System.Data;
using System.Data.SqlClient;
using InterfaceDal;

namespace AdoDotNetDAL
{
    public abstract class TemplateADO<AnyType>: AbstractDal<AnyType>
    {
        private IUow _uow = null;
        protected SqlConnection objConn = null;
        protected SqlCommand objCommand = null;

        public override void SetUnitOfWork(IUow uow)
        {
            _uow = uow;
            objConn = ((AdoUow) uow).Connection;
            objCommand = new SqlCommand();
            objCommand.Connection = objConn;

            objCommand.Transaction = ((AdoUow) uow).Transaction;
        }

        private void OpenConnection()
        {
            if (objConn == null)
            {
                objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["CustomerContext"].ToString());
                objConn.Open();
                objCommand = new SqlCommand();
                objCommand.Connection = objConn;
            }
        }

        protected abstract void ExecuteCommand(AnyType obj);
        protected abstract List<AnyType> ExecuteCommand();

        private void CloseConnection()
        {
            if (_uow == null)
            {
                objConn.Close();
                objConn = null;
            }
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
