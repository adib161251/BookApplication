using BookApplication.Models;
using BookApplication.Service.Business;
using BookApplication.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookApplication.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult LoggedIn(Customer customer)
        {
            SecurityService authenticate = new SecurityService();
            bool result = authenticate.Authentication(customer);
            if (result)
            {
                return View("LoginSuccess",customer);

            }
            else
            {
                return Content("LOGIN FAILED"+ result);
            }
        }

        public ActionResult SignUp()
        {
            return View("SignUp");
        }

        [HttpPost]
        public ActionResult ProcessSignUp(Customer customer)
        {
            SecurityDAO security = new SecurityDAO();
            bool exists = security.Ifexist(customer);

            if(exists)
            {
                ViewData["exists"] = customer.Name;
                return View("Login");
            }

            else
            {
                security.CreateUser(customer);
                return View("LoginSuccess", customer);
            }


        }

        public ActionResult Home()
        {
            return Redirect("/Home");
        }
    }
}