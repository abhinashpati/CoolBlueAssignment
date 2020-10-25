using CoolBleSearchAssignment.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoolBleSearchAssignment.Contracts.Services.Data
{
    public interface IProductApiService
    {
        Task<ProductList> GetProductList(string searchText = "", int pageNo = 1);
        Task<ProductList> GetMockProductList(string searchText = "", int pageNo = 1);
    }
}
