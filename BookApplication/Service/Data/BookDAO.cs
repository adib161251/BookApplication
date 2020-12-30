using BookApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookApplication.Service.Data
{
    public class BookDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Book;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Book> GetAll()
        {
            List<Book> returnList = new List<Book>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "select * from dbo.Book";

                SqlCommand command = new SqlCommand(query,connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if(reader.HasRows)
                    {
                        //Create a new Book object, Add all obj inside the list
                        while(reader.Read())
                        {
                            Book bookData = new Book();
                            bookData.Id = reader.GetInt32(0);
                            bookData.BookName = reader.GetString(1);
                            bookData.Price = reader.GetInt32(2);
                            bookData.AuthorName = reader.GetString(3);
                            bookData.Description = reader.GetString(4);

                            returnList.Add(bookData);


                        }

                    }

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            return returnList;
        }
        
        public Book FetchOne(int id)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM dbo.Book WHERE Id=@id";

                SqlCommand command = new SqlCommand(query , connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Book bookData = new Book();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        bookData.Id = reader.GetInt32(0);
                        bookData.BookName = reader.GetString(1);
                        bookData.Price = reader.GetInt32(2);
                        bookData.AuthorName = reader.GetString(3);
                        bookData.Description = reader.GetString(4);
                    }
                }

                return bookData;
            }
        }

        public int CreateOrUpdate(Book book)
        {
            //if id has -1 then we have to create an entry
            //if id has a positive value then we will update the existed entry


              
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "";
                if(book.Id  <= 0)
                {
                    query = "INSERT INTO dbo.Book(BookName,Price,AuthorName,Description) VALUES (@bookname,@price,@author,@descrip)";
                }
                else
                {
                    query = "update dbo.Book SET BookName=@bookname,Price=@price,AuthorName=@author,Description=@descrip WHERE Id=@id";
                }
                
                SqlCommand command = new SqlCommand(query,connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = book.Id;

                command.Parameters.Add("@bookname", System.Data.SqlDbType.VarChar, 37).Value = book.BookName;
                command.Parameters.Add("@price", System.Data.SqlDbType.Int).Value = book.Price;

                command.Parameters.Add("@author", System.Data.SqlDbType.VarChar, 23).Value = book.AuthorName;
                command.Parameters.Add("@descrip", System.Data.SqlDbType.VarChar,39).Value = book.Description;

                connection.Open();

                int returnId = command.ExecuteNonQuery();

                connection.Close();
                return returnId;
            }

        }

        public void Delete(int id)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM dbo.Book WHERE Id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


    }
}