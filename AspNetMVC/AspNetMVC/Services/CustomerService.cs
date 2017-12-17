﻿using AspNetMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Services
{
    public class CustomerService
    {
        private NORTHWNDEntities db;
        public CustomerService()
        {
            db = new NORTHWNDEntities();
        }

        public IQueryable<Customers> Get()
        {
            var data = db.Customers.OrderBy(c => c.CustomerID);
            return data;
        }

        public Customers Get(string id)
        {
            var data = from c in db.Customers
                       where c.CustomerID == id
                       select c;

            if (data.Count() > 0)
            {
                return data.First();
            }
            return null;
        }
    }
}