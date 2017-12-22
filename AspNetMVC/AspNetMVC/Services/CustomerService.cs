using AspNetMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void AddCustomer(Customers customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public void SaveCustomer(Customers customer)
        {
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void DeleteCustomer(string id)
        {
            var itemToRemove = db.Customers.SingleOrDefault(x => x.CustomerID == id); //returns a single item.

            if (itemToRemove != null)
            {
                db.Customers.Remove(itemToRemove);
                db.SaveChanges();
            }           
           
        }
    }
}