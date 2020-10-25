using Acr.UserDialogs;
using CoolBleSearchAssignment.Constants;
using CoolBleSearchAssignment.Contracts.Services.Data;
using CoolBleSearchAssignment.Contracts.Services.General;
using CoolBleSearchAssignment.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BaseViewModel = CoolBleSearchAssignment.ViewModels.Base.BaseViewModel;

namespace CoolBleSearchAssignment.ViewModels
{
    public class ProductListViewModel : BaseViewModel
    {
        private readonly IProductApiService _productApiService;
        private ObservableRangeCollection<Product> _productItemList;
        private string _searchText = string.Empty;
        private int _itemTreshold = 4;
        private Command _itemTresholdReachedCommand;
        public ProductListViewModel(IConnectionService connectionService,
            INavigationService navigationService, IDialogService dialogService,
            IProductApiService productApiService) : base(connectionService, navigationService, dialogService)
        {
            _productApiService = productApiService;
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        public async Task LoadData()
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
            catch (Exception ex)
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

        public ObservableRangeCollection<Product> ProductItemList
        {
            get { return _productItemList; }
            set { SetProperty(ref _productItemList, value); }
        }

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
        public int ItemTreshold
        {
            get { return _itemTreshold; }
            set { SetProperty(ref _itemTreshold, value); }
        }
        public Command ItemTresholdReachedCommand
        {
            get
            {
                return _itemTresholdReachedCommand = new Command(async () =>
                {
                    if (_connectionService.IsConnected)
                    {
                        await ItemsTresholdReached();
                    }
                    else
                    {
                        await _dialogService.ShowDialog(AppConstant.NO_INTERNET, AppConstant.TITLE, AppConstant.OKMESSAGE);
                    }
                });
            }
        }

        public int CurrentPage { get; set; } = AppConstant.PageNumber;
        public int TotalPages { get; set; } = AppConstant.PageNumber;

        private async Task ItemsTresholdReached()
        {
            if (IsBusy)
                return;
            if (CurrentPage >= TotalPages)
                return;
            if (ProductItemList == null || ProductItemList.Count == 0)
                return;
            IsBusy = true;
            try
            {
                CurrentPage++;
                var items = await LoadProductList(SearchText, CurrentPage);

                var previousLastItem = ProductItemList.Last();
                foreach (var item in items.Products)
                {
                    ProductItemList.Add(item);
                }
                Debug.WriteLine($"{items.Products.Count()} {ProductItemList.Count} ");
                if (items.Products.Count() == 0)
                {
                    ItemTreshold = -1;
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
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
    }
}