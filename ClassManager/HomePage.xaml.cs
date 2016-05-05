using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ClassManager.Service;
using ClassManager.Model;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace ClassManager
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private string _serverAddress;

        private User _user { get; set; }

        public HomePage()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!SingleService.Instance.hasLogin())
            {
                Frame.Navigate(typeof(MainPage));
                return;
            }
            _user = SingleService.Instance.getUser();
            _user.image = SingleService.Instance.getServerAddress() + (_user.image == null ? "null" : _user.image);
            //user_image.Source = await SingleService.Instance.getImage(_user.image);

        }

        private void relationships_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 跳转至班级页面，merge后把注释去掉
            // Frame.Navigate(typeof(MemberOfClass), ((Relationship)e.ClickedItem).account);
        }
    }
}
