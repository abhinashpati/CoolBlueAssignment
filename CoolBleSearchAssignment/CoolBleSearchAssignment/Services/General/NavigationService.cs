using CoolBleSearchAssignment.Contracts.Services.General;
using CoolBleSearchAssignment.ViewModels;
using CoolBleSearchAssignment.ViewModels.Base;
using CoolBleSearchAssignment.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoolBleSearchAssignment.Services.General
{
    public class NavigationService : INavigationService
    {

        public BaseViewModel PreviousPageViewModel
        {
            get
            {
                var mainPage = Application.Current.MainPage as CustomNavigationView;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as BaseViewModel;
            }
        }
        public Task InitializeAsync()
        {
            return NavigateToAsync<ProductListViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveLastFromBackStackAsync()
        {

            if (Application.Current.MainPage is CustomNavigationView mainPage)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {

            if (Application.Current.MainPage is CustomNavigationView mainPage)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        public Task PopStackAsync()
        {
            if (Application.Current.MainPage is CustomNavigationView mainPage)
            {
                mainPage.Navigation.PopAsync();
            }

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);

            if (page is ProductListView)
            {
                Application.Current.MainPage = new CustomNavigationView(page);
            }
            else
            {
                var navigationPage = Application.Current.MainPage as CustomNavigationView;
                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    Application.Current.MainPage = new CustomNavigationView(page);
                }
            }

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

        public async Task PushPageAsync(Page page)
        {
            if (page is ProductListView)
            {
                Application.Current.MainPage = new CustomNavigationView(page);
            }
            else
            {
                if (Application.Current.MainPage is CustomNavigationView navigationPage)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    Application.Current.MainPage = new CustomNavigationView(page);
                }
            }
        }
    }
}
