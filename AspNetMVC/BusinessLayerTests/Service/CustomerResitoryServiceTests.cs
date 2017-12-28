using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using DataAccessLayer.Interfaces;
using Domain.Models;

namespace BusinessLayer.Service.Tests
{
    [TestClass()]
    public class CustomerResitoryServiceTests
    {
        [TestMethod()]
        [TestCategory("Service.Repository")]
        public void Checkout_RespositoryService_for_Customers_Get_function_execute_1_time()
        {
            // arrange
            var target = Substitute.For<IRepository<Customers>>();
            var service = new CustomerResitoryService(target);
            var expected = 1;

            // act
            service.Get();

            // assert
            target.Received(expected).Get();
        }

        [TestMethod()]
        [TestCategory("Service.Repository")]
        public void Checkout_RespositoryService_for_Customers_Get_function_execute_1_times_with_argu_string_type()
        {
            // arrange
            var target = Substitute.For<IRepository<Customers>>();
            var service = new CustomerResitoryService(target);
            var expected = 1;

            // act
            service.Get("12");

            // assert
            target.Received(expected).Get();
        }
    }
}