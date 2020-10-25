using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CoolBleSearchAssignment.Views;

namespace CoolBleSearchAssignment
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ProductListView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
