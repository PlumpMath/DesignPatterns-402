using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using InterfaceCustomer;
using FactoryCustomer;

namespace AdoDotNetDAL
{
    public class CustomerDAL: TemplateADO<CustomerBase>
    {
        public override void Add(CustomerBase obj)
        {
            obj.Validate();
            base.Add(obj);
        }

        protected override void ExecuteCommand(CustomerBase customer)
        {
            if (customer.Id == 0)
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
            else
            {
                //Update
            }
        }

        protected override List<CustomerBase> ExecuteCommand()
        {
            objCommand.CommandText = "select * from Customer";
            SqlDataReader dr = null;
            dr = objCommand.ExecuteReader();
            while (dr.Read())
            {
                CustomerBase icust = Factory<CustomerBase>.Create(dr["CustomerType"].ToString());
                icust.Id = Convert.ToInt32(dr["Id"]);
                icust.CustomerType = dr["CustomerType"].ToString();
                icust.CustomerName = dr["CustomerName"].ToString();
                icust.BillAmount = Convert.ToDecimal(dr["BillAmount"]);
                icust.BillDate = Convert.ToDateTime(dr["BillDate"]);
                icust.PhoneNumber = dr["PhoneNumber"].ToString();
                icust.Address = dr["Address"].ToString();
                AnyTypes.Add(icust);
            }
            return AnyTypes;
        }
    }
}