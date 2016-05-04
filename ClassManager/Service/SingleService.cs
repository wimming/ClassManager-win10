using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassManager.Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Windows.UI.Xaml.Media.Imaging;
using System.IO;
using Windows.UI.Xaml.Media;

namespace ClassManager.Service
{
    public class SingleService
    {
        private static SingleService _instatnce;
        public static SingleService Instance
        {
            get
            {
                if (_instatnce == null)
                {
                    _instatnce = new SingleService();
                }
                return _instatnce;
            }
        }

        private string _serverAddress;
        private User _user;

        private HttpClient httpClient;

        private SingleService()
        {
            _serverAddress = "http://119.29.65.204:3000/";
            _user = null;

            // 自动处理cookie
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();
            httpClient = new HttpClient(handler);
        }

        private async Task<JObject> _post(string sub_url, Dictionary<string, string> dict)
        {
            var content = new FormUrlEncodedContent(dict);
            var response = await httpClient.PostAsync(_serverAddress + sub_url, content);
            var responseByte = await response.Content.ReadAsByteArrayAsync();
            string responseString = System.Text.Encoding.UTF8.GetString(responseByte);
            JObject resultJson = JObject.Parse(responseString);
            return resultJson;
        }

        private async Task<JObject> _get(string sub_url)
        {
            var response = await httpClient.GetAsync(_serverAddress + sub_url);
            var responseByte = await response.Content.ReadAsByteArrayAsync();
            string responseString = System.Text.Encoding.UTF8.GetString(responseByte);
            JObject resultJson = JObject.Parse(responseString);
            return resultJson;
        }
        //method
        public User getUser()
        {
            return _user;
        }
        public string getServerAddress()
        {
            return _serverAddress;
        }
        public bool hasLogin()
        {
            return (_user != null) ? true : false;
        }

        public async Task<Result> login(string account, string password)
        {
            var values = new Dictionary<string, string>
            {
                {"account", account },
                {"password", password }
            };
            JObject resultJson = await _post("login/user", values);
            Result result = new Result();
            result.error = (bool)resultJson["error"];
            result.message = (string)resultJson["message"];
            if (result.error == false)
            {
                result.user_data.account = (string)resultJson["user_data"]["account"];
                result.user_data.password = (string)resultJson["user_data"]["password"];
                result.user_data.name = (string)resultJson["user_data"]["name"];
                result.user_data.student_id = (string)resultJson["user_data"]["student_id"];
                result.user_data.gender = (string)resultJson["user_data"]["gender"];
                result.user_data.image = (string)resultJson["user_data"]["image"];
                result.user_data.email = (string)resultJson["user_data"]["email"];
                result.user_data.phone = (string)resultJson["user_data"]["phone"];
                result.user_data.qq = (string)resultJson["user_data"]["qq"];
                result.user_data.wechat = (string)resultJson["user_data"]["wechat"];
                List<JToken> homeworkList = resultJson["user_data"]["homeworks"].Children().ToList();
                foreach (JToken token in homeworkList)
                {
                    result.user_data.homeworks.Add(JsonConvert.DeserializeObject<Homework>(token.ToString()));
                }
                _user = result.user_data;
            }
            return result;
        }

        public async Task<Result> register(string account, string password)
        {
            var values = new Dictionary<string, string>
            {
                {"account", account },
                {"password", password }
            };
            JObject resultJson = await _post("register/user", values);
            Result result = new Result();
            result.error = (bool)resultJson["error"];
            result.message = (string)resultJson["message"];
            return result;
        }

        public async Task<BitmapImage> getImage(string sub_image_url)
        {
            string full_image_url = _serverAddress + ((sub_image_url != null) ? sub_image_url : "null");
            var response = await httpClient.GetAsync(full_image_url);
            var responseStream = await response.Content.ReadAsStreamAsync();
            Windows.UI.Xaml.Media.Imaging.BitmapImage bmp = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            await bmp.SetSourceAsync(responseStream.AsRandomAccessStream());
            return bmp;
        }
    }
}
