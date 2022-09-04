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
    public partial class LanguagesPage : ContentPage
    {
        List<LanguageModel> list = new List<LanguageModel>();
        List<LanguageModel> listDistinct = new List<LanguageModel>();
        public LanguagesPage()
        {
            InitializeComponent();
            DownloadLanguageData();
        }

        public async Task DownloadLanguageData()
        {
            var uri = new Uri("https://www.idt.mdh.se/personal/plt01/languide/?get=results");
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                string jsonString = json.ToString();
                var jObject = JObject.Parse(jsonString);
                var jsonDataArray = JArray.Parse(jObject["data"].ToString());

                foreach (var resultObject in jsonDataArray)
                {
                    LanguageModel model = new LanguageModel();
                    model.language = resultObject["language"].ToString();
                    list.Add(model);
                }
            }

            languagesList.ItemsSource = list.GroupBy(x => x.language).Select (x => x.First());      //da ne implementiram svoj comparer, koristim ovo umjesto distinct
        }
    }
}