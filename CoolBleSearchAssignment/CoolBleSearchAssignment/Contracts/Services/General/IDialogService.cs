using Acr.UserDialogs;
using System.Threading.Tasks;

namespace CoolBleSearchAssignment.Contracts.Services.General
{
    public interface IDialogService
    {
        Task ShowDialog(string message, string title, string buttonLabel);

        void ShowToast(string message);

        void ShowLoadingDialog(string message, MaskType mask);

        void HideLoadingDialog();
    }
}
