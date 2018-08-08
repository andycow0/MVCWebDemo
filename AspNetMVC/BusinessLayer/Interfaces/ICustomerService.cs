using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICustomerService
    {
        IQueryable<Customers> Get();


        Customers Get(string id);

        void AddCustomer(Customers customer);

        void SaveCustomer(Customers customer);
        void DeleteCustomer(string id);


        Task<IQueryable<Customers>> GetAsync();
        Task<Customers> GetAsync(string id);
        Task<bool> AddAsync(Customers customer);
        Task<bool> SaveAsync(Customers customer);
    }

}
