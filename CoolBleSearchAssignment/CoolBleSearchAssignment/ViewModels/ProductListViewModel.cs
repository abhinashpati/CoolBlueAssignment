using Acr.UserDialogs;
using CoolBleSearchAssignment.Constants;
using CoolBleSearchAssignment.Contracts.Services.Data;
using CoolBleSearchAssignment.Contracts.Services.General;
using CoolBleSearchAssignment.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BaseViewModel = CoolBleSearchAssignment.ViewModels.Base.BaseViewModel;

namespace CoolBleSearchAssignment.ViewModels
{
    class ProductListViewModel : BaseViewModel
    {
        private readonly IProductApiService _productApiService;
        private ObservableRangeCollection<Product> _productItemList;
        private string _searchText = string.Empty;
        public ProductListViewModel(IConnectionService connectionService, IDialogService dialogService, INavigationService navigationService, IProductApiService productApiService)
            :base(connectionService,navigationService,dialogService)
        {
            _productApiService = productApiService;
        }
        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }
        public ObservableRangeCollection<Product> ProductItemList
        {
            get { return _productItemList; }
            set { SetProperty(ref _productItemList, value); }
        }
        private int CurrentPage { get; set; } = AppConstant.PageNumber;
        private int TotalPages { get; set; } = AppConstant.PageNumber;
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                SetProperty(ref _searchText, value);
            }
        }

        public ICommand PerformSearchCommand => new Command<string>(async search =>
        {
            if (!string.IsNullOrEmpty(search))
            {
                ProductItemList = null;
                SearchText = search;
                await LoadData();
            }
        });

        private async Task LoadData()
        {
            try
            {
                if (_connectionService.IsConnected)
                {
                    _dialogService.ShowLoadingDialog("Loading", MaskType.Black);
                    var results = await LoadProductList(SearchText, AppConstant.PageNumber);
                    if (results.Products.Count != 0)
                    {
                        ProductItemList = new ObservableRangeCollection<Product>(results.Products);
                        CurrentPage = results.currentPage;
                        TotalPages = results.pageCount;
                    }
                    _dialogService.HideLoadingDialog();
                }
                else
                {
                    await _dialogService.ShowDialog(AppConstant.NO_INTERNET, AppConstant.TITLE, AppConstant.OKMESSAGE);
                }
            }
            catch (Exception)
            {
                _dialogService.HideLoadingDialog();
                return;
            }
        }

        private async Task<ProductList> LoadProductList(string searchText, int pageNumber)
        {
            var result = new ProductList();
            try
            {
                result = await _productApiService.GetProductList(searchText, pageNumber);
            }
            catch (Exception)
            {
                await _dialogService.ShowDialog(AppConstant.TRY_AGAIN, AppConstant.TITLE, AppConstant.OKMESSAGE);
            }
            finally
            {

            }
            return result;
        }
    }
}
