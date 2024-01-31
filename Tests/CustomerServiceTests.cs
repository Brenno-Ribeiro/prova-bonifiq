using Moq;
using ProvaPub.Models;
using ProvaPub.Repository.intefaces;
using ProvaPub.Services;
using ProvaPub.Services.Exeptions;
using System.Linq.Expressions;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {

        private readonly CustomerService _customerService;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;

        public CustomerServiceTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _customerService = new CustomerService(_customerRepositoryMock.Object, _orderRepositoryMock.Object);
        }


        [Theory(DisplayName = "Checks if the customer id is greater than zero")]
        [InlineData(5)]
        [InlineData(10)]
        public void CanPurchase_VerifyValue_Valid_CustomerId_ReturnTrue(int value)
        {
            var result = _customerService.VerifyValue(value);
            Assert.True(result);
        }


        [Theory(DisplayName = "Checks if the customer id is less than or equal to zero")]
        [InlineData(-1)]
        [InlineData(0)]
        public void CanPurchase_VerifyValue_Invalid_CustomerId_ReturnFalse(int value)
        {
            var result = _customerService.VerifyValue(value);
            Assert.False(result);
        }


        [Theory(DisplayName = "Checks if the purchase value is greater than zero")]
        [InlineData(5)]
        [InlineData(10)]
        public void CanPurchase_VerifyValue_Valid_PurchaseValue_ReturnTrue(decimal value)
        {
            var result = _customerService.VerifyValue(value);
            Assert.True(result);
        }


        [Theory(DisplayName = "Checks if the purchase value is less than or equal to zero")]
        [InlineData(-1)]
        [InlineData(0)]
        public void CanPurchase_VerifyValue_Invalid_PurchaseValue_ReturnFalse(decimal value)
        {
            var result = _customerService.VerifyValue(value);
            Assert.False(result);
        }


        [Theory(DisplayName = "")]
        [InlineData(0,-1)]
        [InlineData(-1, -2)]
        public async Task CanPurchase_VerifyValue_Invalid_CustomerId_And_PurchaseValue_ValueLessThanOrEqualToZeroException(int customerId, decimal purchaseValue)
        {
            await Assert.ThrowsAsync<ValueLessThanOrEqualToZeroException>(() => _customerService.CanPurchase(customerId, purchaseValue));
        }


        [Theory(DisplayName = "")]
        [InlineData(21,100)]
        [InlineData(22, 100)]
        public async Task CanPurchase_VerifyIfNotExistCustomer_Return_ItemDoesNotExistException(int customerId, decimal purchaseValue)
        {
            await Assert.ThrowsAsync<ItemDoesNotExistException>(() => _customerService.CanPurchase(customerId, purchaseValue));
        }



        [Theory (DisplayName = "Checks if the customer exist")]
        [InlineData(1)]
        public async Task CanPurchase_VerifyIfExistCustomer_Return_Customer(int id)
        {
            _customerRepositoryMock
                .Setup(repo => repo.GetById(It.IsAny<int>()))
                .ReturnsAsync(new Customer { Id = id, Name = "Elaine Fay", Orders = new List<Order>()});

            var result = await _customerService.GetById(id);

            Assert.NotNull(result);
        }

        [Theory(DisplayName = "Checks if the customer does not exist")]
        [InlineData(21)]
        public async Task CanPurchase_VerifyIfExistCustomer_Return_Customer_Null(int id)
        {
            _customerRepositoryMock
                .Setup(repo => repo.GetById(It.IsAny<int>()))
                .ReturnsAsync((Customer)null);

            var result = await _customerService.GetById(id);

            Assert.Null(result);
        }


        [Theory(DisplayName = "Checks if the customer has never made a purchase in the current month")]
        [InlineData(1, "2023-12-31")]
        public async Task CanPurchase_VerifyPurchaseInMonth_Return_True(int customerId, DateTime date)
        {
            _orderRepositoryMock
                .Setup(repo => repo.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync(0);

            var result = await _customerService.VerifyPurchaseInMonth(customerId, date);
            Assert.True(result);
        }


        [Theory(DisplayName = "Checks whether the customer has already made a purchase in the current month")]
        [InlineData(1, "2024-01-30")]
        public async Task CanPurchase_VerifyPurchaseInMonth_Return_False(int customerId, DateTime date)
        {

            _orderRepositoryMock
                .Setup(repo => repo.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync(1);

            var result = await _customerService.VerifyPurchaseInMonth(customerId, date);
            Assert.False(result);
        }



        [Theory(DisplayName = "Checks if the customer has never made a purchase")]
        [InlineData(1, 100)]
        [InlineData(2, 100)]
        public async Task CanPurchase_VerifyHaveAlreadyPurchasedBefore_Return_True(int customerId, decimal purchaseValue)
        {
            _orderRepositoryMock
                .Setup(repo => repo.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync(0);

            var result = await _customerService.VerifyHaveAlreadyPurchasedBefore(customerId, purchaseValue);
            Assert.True(result);
        }


        [Theory(DisplayName = "Checks if the customer has already made a purchase")]
        [InlineData(1, 150)]
        [InlineData(2, 200)]
        public async Task CanPurchase_VerifyHaveAlreadyPurchasedBefore_Return_False(int customerId, decimal purchaseValue)
        {
            _orderRepositoryMock
                .Setup(repo => repo.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync(2);

            var result = await _customerService.VerifyHaveAlreadyPurchasedBefore(customerId, purchaseValue);
            Assert.False(result);
        }

    }
}
