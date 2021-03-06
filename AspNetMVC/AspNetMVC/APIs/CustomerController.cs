﻿//using AspNetMVC.Models;
//using AspNetMVC.Services;
using BusinessLayer.Interfaces;
using BusinessLayer.Service;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AspNetMVC.APIs
{
    public class CustomerController : ApiController
    {
        private ICustomerService service;
        public CustomerController()
        {
            //service = new CustomerService();
            service = new CustomerResitoryService();
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
        public async Task<HttpResponseMessage> Get(int currentPage, int pageSize)
        {
            try
            {
                var totalCount = 0;
                //var customers = service.Get();
                var customers = await service.GetAsync();

                totalCount = customers.Count();

                var data = customers.ToList().Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                var result = new { Total = totalCount, Data = data };
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // GET: api/Customer/5
        public async Task<HttpResponseMessage> Get(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("id", "Is null or empty!");

                //var data = service.Get(id);

                var data = await service.GetAsync(id);

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // POST: api/Customer
        public async Task<HttpResponseMessage> Post(Customers customer)
        {
            try
            {
                //service.AddCustomer(customer);
                var result = await service.AddAsync(customer);

                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        // PUT: api/Customer/5
        public async Task<HttpResponseMessage> Put(Customers customer)
        {
            try
            {
                //service.SaveCustomer(customer);
                var result = await service.SaveAsync(customer);

                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
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
