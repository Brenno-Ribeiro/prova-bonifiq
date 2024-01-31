using Bogus.DataSets;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Repository.intefaces;
using ProvaPub.Services.Exeptions;
using ProvaPub.Services.Interfaces;
using System;

namespace ProvaPub.Services
{
    public class CustomerService : ICustomerService
    {
       
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;


        public CustomerService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public async Task<CustomerList> GetAllAsync(int page, int pageSize)
        {
            var customers = await _customerRepository.GetAllAsync(page, pageSize);
            var totalResult = await _customerRepository.GetTotaItems();

            var customerList = new CustomerList
            {
                CurrentPage = page,
                TotalCount = pageSize,
                TotalResult = totalResult,
                Result = customers
            };

            customerList.CountTotalPages();
            customerList.IsNextPage();

            return customerList;
        }

        
        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {

            if (!VerifyValue(customerId))
                throw new ValueLessThanOrEqualToZeroException("Value cannot be less than or equal to zero");

            if (!VerifyValue(purchaseValue))
                throw new ValueLessThanOrEqualToZeroException("Value cannot be less than or equal to zero");


            //Business Rule: Non registered Customers cannot purchase
            if (await GetById(customerId) == null)
                throw new ItemDoesNotExistException($"Customer id {customerId} does not exists");


            //Business Rule: A customer can purchase only a single time per month
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            if(!await VerifyPurchaseInMonth(customerId, baseDate)) return false;


            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            if(!await VerifyHaveAlreadyPurchasedBefore(customerId, purchaseValue)) return false;


            return true;
        }


        public async Task<Customer> GetById(int id)
        {
            return await _customerRepository.GetById(id);
        }

        public bool VerifyValue(decimal value)
        {
            return value > 0;
        }

        public bool VerifyValue(int value)
        {
            return value > 0;            
        }

        public async Task<bool> VerifyPurchaseInMonth(int customerId, DateTime referenceDate)
        {
            var startDate = new DateTime(referenceDate.Year, referenceDate.Month, 1);
            var endDate   = new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day);

            return await _orderRepository.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= startDate && s.OrderDate <= endDate) == 0;
        }

        public async Task<bool> VerifyHaveAlreadyPurchasedBefore(int customerId, decimal purchaseValue)
        {
            var haveBoughtBefore = await _orderRepository.CountAsync(s => s.CustomerId == customerId);
            return haveBoughtBefore == 0 && purchaseValue >= 1 && purchaseValue <= 100;
        }


    }
}
