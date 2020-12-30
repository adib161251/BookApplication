using BookApplication.Models;
using BookApplication.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookApplication.Service.Business
{
    public class SecurityService
    {
        public bool Authentication(Customer customermodel)
        {
            SecurityDAO dao = new SecurityDAO();

            return dao.FindByUser(customermodel);
        }
    }
}