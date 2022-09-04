using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public string FirebaseAPIKey = "AIzaSyDxMRd2suAzFcWwzMBg7Uhj9GdPNzMn3rU";

        public LoginPage()
        {
            InitializeComponent();
        }

        async private void login(object sender, EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(FirebaseAPIKey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(usernameEntry.Text, passwordEntry.Text);
                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedContent);
                await Navigation.PushAsync(new MainPage());
                string getToken = auth.FirebaseToken;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Incorrect log in credentials.", "OK");
            }
        }

        private void openRegisterView(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}