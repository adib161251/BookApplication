using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookApplication.Models
{
    public class Book
    {
        public int Id { get; set; }
        
        [Required]
        public string BookName { get; set; }
        
        [Required]
        public int Price { get; set; }
        
        [Required]
        public string AuthorName { get; set; }

        [Required]
        public string Description { get; set; }

        public Book()
        {
            this.Id = 0;
            this.BookName = "";
            this.Price = 0;
            this.AuthorName = "";
            this.Description = "";
        }

        public Book(int id, string bookName, int price, string authorName, string description)
        {
            Id = id;
            BookName = bookName;
            Price = price;
            AuthorName = authorName;
            Description = description;
        }
    }
}