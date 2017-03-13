using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InterfaceCustomer;
using FactoryCustomer;

namespace AdoDotNetDAL
{
    public class CustomerDAL: TemplateADO<ICustomer>
    {
        public CustomerDAL(string connectionString) : base(connectionString)
        {
        }

        protected override void ExecuteCommand(ICustomer customer)
        {
            objCommand.CommandText = $@"INSERT INTO [dbo].[Customer]
                                       ([CustomerType]
                                       ,[CustomerName]
                                       ,[BillAmount]
                                       ,[BillDate]
                                       ,[PhoneNumber]
                                       ,[Address])
                                 VALUES
                                       ('{customer.CustomerType}'
                                       ,'{customer.CustomerName}'
                                       ,{customer.BillAmount}
                                       ,'{customer.BillDate}'
                                       ,'{customer.PhoneNumber}'
                                       ,'{customer.Address}')";
            objCommand.ExecuteNonQuery();
        }

        protected override List<ICustomer> ExecuteCommand()
        {
            objCommand.CommandText = "select * from Customer";
            SqlDataReader dr = null;
            dr = objCommand.ExecuteReader();
            List<ICustomer> custs = new List<ICustomer>();
            while (dr.Read())
            {
                ICustomer icust = Factory<ICustomer>.Create(dr["CustomerType"].ToString());
                icust.CustomerType = dr["CustomerType"].ToString();
                icust.CustomerName = dr["CustomerName"].ToString();
                icust.BillAmount = Convert.ToDecimal(dr["BillAmount"]);
                icust.BillDate = Convert.ToDateTime(dr["BillDate"]);
                icust.PhoneNumber = dr["PhoneNumber"].ToString();
                icust.Address = dr["Address"].ToString();
                custs.Add(icust);
            }
            return custs;
        }
    }
}