using CoolBleSearchAssignment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoolBleSearchAssignment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListView : ContentPage
    {
        public ProductListView()
        {
            InitializeComponent();
        }
        private void Searchtext_Completed(object sender, System.EventArgs e)
        {
            ((ProductListViewModel)this.BindingContext).PerformSearchCommand.Execute((sender as Entry).Text.Trim());
        }
    }
}