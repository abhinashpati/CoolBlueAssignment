using CoolBleSearchAssignment.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoolBleSearchAssignment.Contracts.Services.General
{
    public interface INavigationService
    {
        BaseViewModel PreviousPageViewModel { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();

        Task PopStackAsync();

        Task PushPageAsync(Page page);
    }
}
