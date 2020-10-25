using CoolBleSearchAssignment.Contracts.Services.Data;
using CoolBleSearchAssignment.Contracts.Services.General;
using CoolBleSearchAssignment.Services.Data;
using CoolBleSearchAssignment.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyIoC;
using Xunit;

namespace CoolBlueSearchAssignmentTest
{
    public class ProductListViewModelTest
    {
        private static TinyIoCContainer _container;
        private IProductApiService _productApiService;
        public ProductListViewModelTest()
        {
            _container = new TinyIoCContainer();
            //_container.Register<INavigationService, NavigationService>();
            //_container.Register<IProductApiService, ProductApiService>();
            //_container.Register<IDialogService, DialogService>();
            //_container.Register<IGenericRepository, GenericRepository>();
            _productApiService = _container.Resolve<ProductApiService>();
        }
        [Fact]
        public async Task ProductsList_Not_Null_After_InitializeAsync_Test()
        {
            var mockConnectionService = new Mock<IConnectionService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IDialogService>();

            var productListViewModel = new ProductListViewModel(
                mockConnectionService.Object,
                mockDialogService.Object,
                mockNavigationService.Object,
                _productApiService);

            await productListViewModel.InitializeAsync(null);

            Assert.NotNull(productListViewModel.ProductItemList);
        }
        [Fact]
        public async Task ProductList_All_Get_Loaded_After_InitializeAsync_Test()
        {
            int leastItemCount = 1;
            var mockConnectionService = new Mock<IConnectionService>();
            var result = await _productApiService.GetMockProductList();
            if (mockConnectionService.Object.IsConnected)
            {
                bool actualListContainAtleastOneProduct = true;
            }

            Assert.Equal(leastItemCount, result.Products.Count);
        }

        [Fact]
        public void ProductList_Search_Command_Not_Null_Test()
        {
            var mockConnectionService = new Mock<IConnectionService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IDialogService>();

            var productListViewModel = new ProductListViewModel(
                           mockConnectionService.Object,
                           mockDialogService.Object,
                           mockNavigationService.Object,
                           _productApiService);
            Assert.NotNull(productListViewModel.PerformSearchCommand);
        }
    }
}
