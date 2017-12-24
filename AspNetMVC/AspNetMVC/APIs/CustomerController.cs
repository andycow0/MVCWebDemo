//using AspNetMVC.Models;
//using AspNetMVC.Services;
using BusinessLayer.Service;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetMVC.APIs
{
    public class CustomerController : ApiController
    {
        private CustomerService service;
        public CustomerController()
        {
            service = new CustomerService();
        }
        // GET: api/Customer
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                //var pageSize = 10;
                //var totalCount = 0;
                //var currentPage = 1;
                var data = service.Get();

                //totalCount = customers.Count();

                //var data = customers.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(int currentPage, int pageSize)
        {
            try
            {
                //var pageSize = 10;
                var totalCount = 0;
                //var currentPage = 1;
                var customers = service.Get();

                totalCount = customers.Count();

                var data = customers.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                var result = new { Total = totalCount, Data = data };
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/Customer/5
        public HttpResponseMessage Get(string id)
        {
            try
            {
                var data = service.Get(id);

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // POST: api/Customer
        public HttpResponseMessage Post(Customers customer)
        {
            try
            {
                service.AddCustomer(customer);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // PUT: api/Customer/5
        public HttpResponseMessage Put(Customers customer)
        {
            try
            {
                service.SaveCustomer(customer);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // DELETE: api/Customer/5
        public HttpResponseMessage Delete(string id)
        {
            try
            {
                service.DeleteCustomer(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }
    }
}
