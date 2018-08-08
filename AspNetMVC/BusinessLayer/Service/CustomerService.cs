using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repository;
using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    //  inherit interface [ICustomerService].
    public class CustomerService : ICustomerService
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

        public Task<IQueryable<Customers>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(Customers customer)
        {
            throw new NotImplementedException();
        }

        public Task<Customers> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync(Customers customer)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomerResitoryService : ICustomerService
    {
        string apiUrl = "https://dotnetcoredemo-208409.appspot.com/api/Customers";

        private IRepository<Customers> _db;
        public CustomerResitoryService()
        {
            if (_db == null)
            {
                _db = new GenericRepository<Customers>();
            }
        }
        public CustomerResitoryService(IRepository<Customers> customerDB) : this()
        {
            if (customerDB != null)
            {
                this._db = customerDB;
            }
        }
        public IQueryable<Customers> Get()
        {
            var result = _db.Get();

            return result.AsQueryable();
        }
        public Customers Get(string id)
        {
            var data = from c in _db.Get()
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
            _db.Insert(customer);
        }

        public void DeleteCustomer(string id)
        {
            var Customer = _db.Get(id);
            _db.Delete(Customer);
        }

        public void SaveCustomer(Customers customer)
        {
            _db.Update(customer);
        }

        public async Task<IQueryable<Customers>> GetAsync()
        {
            var customers = new List<Customers>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    customers = JsonConvert.DeserializeObject<List<Customers>>(responseData);
                }
            }

            return customers.AsQueryable();
        }

        public async Task<bool> AddAsync(Customers customer)
        {
            var result = false;
            using (HttpClient client = new HttpClient())
            {
                var stringContent = new System.Net.Http.StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    //var returnUrl = response.Headers.Location;
                    //Console.WriteLine(returnUrl);
                    result = true;
                }
            }

            return result;
        }

        public async Task<Customers> GetAsync(string id)
        {
            var customers = await this.GetAsync();

            //var customers = task.Result;

            var data = from c in customers
                       where c.CustomerID == id
                       select c;

            if (data.Count() > 0)
            {
                return data.First();
            }
            return null;
        }

        public async Task<bool> SaveAsync(Customers customer)
        {
            var result = false;

            using (HttpClient client = new HttpClient())
            {
                var stringContent = new System.Net.Http.StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(apiUrl, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    //var returnUrl = response.Headers.Location;
                    //Console.WriteLine(returnUrl);
                    result = true;
                }
            }

            return result;
        }
    }
}
