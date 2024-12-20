using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Results;
using WebApiAssignment1.Models.EntityLayer;

namespace WebApiAssignment1.BusinessLayer
{
    public class CustomerService
    {
        string ConnStr = ConfigurationManager.ConnectionStrings["CustomerDB"].ConnectionString;

        public string InsertCust(Customer customer)
        {
            string result = string.Empty;

            SqlConnection sqlConnection = new SqlConnection(ConnStr);

            SqlCommand sqlCommand = new SqlCommand("sp_InsertCust", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@username",customer.UserName),
                new SqlParameter("@gender",customer.Gender),
                new SqlParameter("@address",customer.Address),
                new SqlParameter("@phone",customer.Phone),
                new SqlParameter{
                    ParameterName = "@message",
                    Size = int.MaxValue,
                    Direction = System.Data.ParameterDirection.Output
                }

            };

            sqlCommand.Parameters.AddRange(parameters);

            try
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                result = parameters[parameters.Length - 1].Value.ToString();
                sqlConnection.Close();
                return result;

            }
            catch (Exception exc)
            {
                return exc.Message;
            }

        }

        public List<Customer> GetAllCust()
        {

            SqlConnection sqlConnection = new SqlConnection(ConnStr);

            SqlCommand sqlCommand = new SqlCommand("sp_FetchCust", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            List<Customer> list = new List<Customer>();


            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();


                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Customer customer = new Customer();

                        customer.Id = Convert.ToInt32(reader["ID"]);
                        customer.UserName = reader["UserName"].ToString();
                        customer.Gender = reader["Gender"].ToString();
                        customer.Address = reader["Address"].ToString();
                        customer.Phone = reader["Phone"].ToString();

                        list.Add(customer);

                    }
                }
                
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message,exc.InnerException);
            }

            return list;

        }

        public string UpdateCustInfo(Customer customer)
        {

            string result = string.Empty;

            SqlConnection sqlConnection = new SqlConnection(ConnStr);

            SqlCommand sqlCommand = new SqlCommand("sp_UpdateCust", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter[] parameters = new SqlParameter[]
           {
               new SqlParameter("id",customer.Id),
                new SqlParameter("@username",customer.UserName),
                new SqlParameter("@gender",customer.Gender),
                new SqlParameter("@address",customer.Address),
                new SqlParameter("@phone",customer.Phone),
                new SqlParameter{
                    ParameterName = "@message",
                    Size = int.MaxValue,
                    Direction = System.Data.ParameterDirection.Output
                }

           };

            sqlCommand.Parameters.AddRange(parameters);

            try
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                result = parameters[parameters.Length - 1].Value.ToString();
                sqlConnection.Close();
                return result;

            }
            catch (Exception exc)
            {
                return exc.Message;
            }

        }
        public string DeleteCust(int id)
        {

            string result = string.Empty;

            SqlConnection sqlConnection = new SqlConnection(ConnStr);

            SqlCommand sqlCommand = new SqlCommand("sp_DeleteCust", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter[] parameters = new SqlParameter[]
           {
               new SqlParameter("id",id),
                new SqlParameter{
                    ParameterName = "@message",
                    Size = int.MaxValue,
                    Direction = System.Data.ParameterDirection.Output
                }

           };

            sqlCommand.Parameters.AddRange(parameters);

            try
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                result = parameters[parameters.Length - 1].Value.ToString();
                sqlConnection.Close();
                return result;

            }
            catch (Exception exc)
            {
                return exc.Message;
            }

        }

        public Customer GetCustomerByID(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);

            SqlCommand sqlCommand = new SqlCommand("sp_FetchCustByID", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("id",id),
            };

            sqlCommand.Parameters.AddRange(parameters);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();

                        customer.Id = Convert.ToInt32(reader["ID"]);
                        customer.UserName = reader["UserName"].ToString();
                        customer.Gender = reader["Gender"].ToString();
                        customer.Address = reader["Address"].ToString();
                        customer.Phone = reader["Phone"].ToString();

                        return customer;

                    }
                }

            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message, exc.InnerException);
            }

            return null;

        }

    }
}