using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPage : ContentPage
    {
        List<UserModel> list = new List<UserModel>();
        public UsersPage()
        {
            InitializeComponent();
            DownloadUserData();
        }

        public async Task DownloadUserData()
        {
            var uri = new Uri("https://www.idt.mdh.se/personal/plt01/languide/?get=users");
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                string jsonString = json.ToString();
                var jObject = JObject.Parse(jsonString);
                var jsonDataArray = JArray.Parse(jObject["data"].ToString());

                foreach(var userObject in jsonDataArray)
                {
                    UserModel model = new UserModel();
                    model.userID = userObject["id_user"].ToString();
                    model.email = userObject["email"].ToString();
                    model.time = userObject["create_time"].ToString();
                    list.Add(model);
                }
            }
            userList.ItemsSource = list;
        }
        public void SortByID(object sender, System.EventArgs e)
        {
            userList.ItemsSource = list;
        }

        public void SortByTime(object sender, System.EventArgs e)
        {
            List<UserModel> sortedListTime = list.OrderByDescending(x => x.time).ToList();
            userList.ItemsSource = sortedListTime;
        }

        public void SortByEMail(object sender, System.EventArgs e)
        {
            List<UserModel> sortedListEMail = list.OrderBy(x => x.email).ToList();
            userList.ItemsSource = sortedListEMail;
        }
    }
}