using BookApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookApplication.Service.Data
{
    public class SecurityDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Book;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        internal bool FindByUser(Customer customermodel)
        {
            bool result = false;

            //Need to write sql
            //@- prepared statement symbol is used for security.... Protection from sql injection attack
            string query = "SELECT * FROM dbo.Customer WHERE Name=@name AND Password=@password ";

            //Create and Open connection of db in using block

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Create the command and parameter obj
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 20).Value = customermodel.Name;
                command.Parameters.Add("@password", System.Data.SqlDbType.Int).Value = customermodel.Password;

                //OPEN database and run the command
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }

                    reader.Close();

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Exception was created");
                }
            }


            return result;
        }

        public void CreateUser(Customer customer)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "insert into dbo.Customer(Name,Age,CellNumber,Password) values (@name,@age,@cellnumber,@password)";
                SqlCommand command = new SqlCommand(query,connection);

                command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 20).Value = customer.Name;
                command.Parameters.Add("@age", System.Data.SqlDbType.Int).Value = customer.Age;
                command.Parameters.Add("@cellnumber", System.Data.SqlDbType.Int).Value = customer.CellNumber;
                command.Parameters.Add("@password", System.Data.SqlDbType.Int).Value = customer.Password;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();


            }
        }

        public bool Ifexist(Customer customer)
        {
            bool result = false;

            //Need to write sql
            //@- prepared statement symbol is used for security.... Protection from sql injection attack
            string query = "SELECT * FROM dbo.Customer WHERE Name=@name";

            //Create and Open connection of db in using block

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Create the command and parameter obj
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 20).Value = customer.Name;

                //OPEN database and run the command
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }

                    reader.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Exception was created");
                }
            }


            return result;
        }

       
    }
}