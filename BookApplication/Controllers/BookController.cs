using BookApplication.Models;
using BookApplication.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookApplication.Controllers
{
    public class BookController : Controller
    {
        // GET: Book

        public ActionResult Index()
        {
            /* List<Book> book = new List<Book>
             {
                 new Book(12,"soleman",21,"Adib Arman","samsuujoga"),
                 new Book(14,"JUDHOOO KORE SHOBAI",249,"Khondokar Rashid","This is just a dummy data"),
                 new Book(16,"Je jon horoshe moner batashe",2000,"Syed Fabliha Rahman","She is my love")
             };
            For Testing Purpose
            */

            BookDAO dataList = new BookDAO();
            List<Book> book = dataList.GetAll();

            return View("BookList",book);
        }

        public ActionResult Details(int id)
        {
            BookDAO bookDAO = new BookDAO();
            Book book = bookDAO.FetchOne(id);

            return View("Details",book);
        }

    
        public ActionResult Create()
        {
            Book book = new Book();
            return View("Create",book);
        }

        public ActionResult Edit(int id)
        {
            BookDAO bookDAO = new BookDAO();
            Book book = bookDAO.FetchOne(id);
            return View("Create",book);

        }

        public ActionResult Delete(int id)
        {
            BookDAO bookDAO = new BookDAO();
            bookDAO.Delete(id);
            List<Book> bookList = bookDAO.GetAll();

            return View("BookList", bookList);
        }

        [HttpPost]
        public ActionResult ProcessCreate(Book book)
        {
            BookDAO bookDAO = new BookDAO();

            int id=bookDAO.CreateOrUpdate(book);
            return View("Details",book);
            
        }
    }
}