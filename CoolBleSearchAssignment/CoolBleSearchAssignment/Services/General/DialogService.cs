using Acr.UserDialogs;
using CoolBleSearchAssignment.Contracts.Services.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoolBleSearchAssignment.Services.General
{
    public class DialogService : IDialogService
    {
        public Task ShowDialog(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public void ShowToast(string message)
        {
            UserDialogs.Instance.Toast(message);
        }

        public void ShowLoadingDialog(string message, MaskType mask)
        {
            UserDialogs.Instance.ShowLoading(message, mask);
        }
        public void HideLoadingDialog()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}
