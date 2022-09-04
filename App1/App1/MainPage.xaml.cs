using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void OpenUsersView(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsersPage());
        }
        private async void OpenResultsView(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResultsPage());
        }
        private async void OpenLanguagesView(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LanguagesPage());
        }
    }
}
