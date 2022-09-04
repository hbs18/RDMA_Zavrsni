using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public string FirebaseAPIKey = "AIzaSyDxMRd2suAzFcWwzMBg7Uhj9GdPNzMn3rU";
        public RegisterPage()
        {
            InitializeComponent();
        }

        async private void register(object sender, EventArgs e)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(FirebaseAPIKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(usernameEntry.Text, passwordEntry.Text);
                string getToken = auth.FirebaseToken;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }
    }
}