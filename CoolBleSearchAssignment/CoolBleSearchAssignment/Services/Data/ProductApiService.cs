using CoolBleSearchAssignment.Constants;
using CoolBleSearchAssignment.Contracts.Repository;
using CoolBleSearchAssignment.Contracts.Services.Data;
using CoolBleSearchAssignment.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoolBleSearchAssignment.Services.Data
{
    public class ProductApiService : IProductApiService
    {
        private readonly IGenericRepository _genericRepository;
        public ProductApiService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<ProductList> GetProductList(string searchText = "", int pageNo = 1)
        {
            UriBuilder uri = new UriBuilder()
            {
                Scheme = ApiConstants.HttpsUrlScheme,
                Host = ApiConstants.MobileAssignmentEndpoint,
                Path = ApiConstants.SeacrhEndpoint,
                Query = $"query={searchText}&page={pageNo}"

            };
            return await _genericRepository.GetAsync<ProductList>(uri.ToString());
        }


        public async Task<ProductList> GetMockProductList(string searchText = "", int pageNo = 1)
        {
            UriBuilder uri = new UriBuilder()
            {
                Scheme = ApiConstants.HttpsUrlScheme,
                Host = ApiConstants.MobileAssignmentEndpoint,
                Path = ApiConstants.SeacrhEndpoint,
                Query = $"query={searchText}&page={pageNo}"

            };
            return await _genericRepository.GetAsync<ProductList>(uri.ToString());
        }
    }
}
