using CoolBleSearchAssignment.Contracts.Services.General;
using CoolBleSearchAssignment.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoolBleSearchAssignment.ViewModels
{
    class ProductListViewModel : BaseViewModel
    {
        public ProductListViewModel(IConnectionService connectionService, IDialogService dialogService, INavigationService navigationService)
            :base(connectionService,navigationService,dialogService)
        {

        }
    }
}
