using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsPage : ContentPage
    {
        List<ResultModel> list = new List<ResultModel>();
        public ResultsPage()
        {
            InitializeComponent();
            DownloadResultData();
        }

        public async Task DownloadResultData()
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

                foreach (var userObject in jsonDataArray)
                {
                    ResultModel model = new ResultModel();
                    model.userID = userObject["id_user"].ToString();
                    model.exerciseID = userObject["id_exercise"].ToString();
                    model.percent = userObject["result_percent"].ToString();
                    model.score = userObject["result_score"].ToString();
                    model.skill = userObject["skill"].ToString();
                    model.maxScore = userObject["result_max"].ToString();
                    list.Add(model);
                }
            }
            resultList.ItemsSource = list;
        }
        public void SortByUserID(object sender, System.EventArgs e)
        {
            resultList.ItemsSource = list;
        }
        public void SortByExerciseID(object sender, System.EventArgs e)
        {
            List<ResultModel> sortedListExerciseID = list.OrderByDescending(x => x.exerciseID).ToList();
            resultList.ItemsSource = sortedListExerciseID;
        }
        public void SortByPercent(object sender, System.EventArgs e)
        {
            List<ResultModel> sortedListPercent = list.OrderByDescending(x => x.percent).ToList();
            resultList.ItemsSource = sortedListPercent;
        }
        public void SortByScore(object sender, System.EventArgs e)
        {
            List<ResultModel> sortedListScore = list.OrderByDescending(x => x.score).ToList();
            resultList.ItemsSource = sortedListScore;
        }
        public void SortBySkill(object sender, System.EventArgs e)
        {
            List<ResultModel> sortedListSkill = list.OrderByDescending(x => x.skill).ToList();
            resultList.ItemsSource = sortedListSkill;
        }
        public void SortByMaxScore(object sender, System.EventArgs e)
        {
            List<ResultModel> sortedListMaxScore = list.OrderByDescending(x => x.maxScore).ToList();
            resultList.ItemsSource = sortedListMaxScore;
        }
    }
}