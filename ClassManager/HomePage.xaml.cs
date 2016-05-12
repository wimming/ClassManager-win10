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
using ClassManager.ViewModels;
using Windows.UI.Core;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace ClassManager
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private string _serverAddress;

        private UserViewModel UVM;

        public HomePage()
        {
            this.InitializeComponent();

            UVM = UserViewModel.Instance;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Collapsed;

            if (!SingleService.Instance.hasLogin())
            {
                Frame.Navigate(typeof(MainPage));
                return;
            }

            UVM.initialUVM();
        }

        private void relationships_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 跳转至班级页面
            Frame.Navigate(typeof(OrganizationDetail), ((Relationship)e.ClickedItem).account);
        }

        private void setting_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingPage));
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CreateOrganizationPage));
        }

        private void join_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(JoinOrganizationPage));
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            UVM.logout();
            Frame.GoBack();
        }

        private async void delete_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog()
            {
                Content = "确定解散这个班级？",
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消",
                FullSizeDesired = false,
            };

            dialog.PrimaryButtonClick += (_s, _e) =>
            {
                UVM.deleteOrganization(((Relationship)((Button)sender).DataContext).account);
            };
            await dialog.ShowAsync();
        }
    }

    class DeletableCVT : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((string)value) == "founder";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
