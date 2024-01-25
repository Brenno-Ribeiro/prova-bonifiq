using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Repository.intefaces;

namespace ProvaPub.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;

        }

        public async Task<ProductList> GetAllAsync(int page, int pageSize)
        {
            var products = await _productRepository.GetAllAsync(page, pageSize);
            var totalResult = await _productRepository.GetTotaItems();

            var productsList = new ProductList
            {
                CurrentPage = page,
                TotalCount = pageSize,
                TotalResult = totalResult,
                Result = products
            };

            productsList.CountTotalPages();
            productsList.IsNextPage();

            return productsList;
        }
    }
}
