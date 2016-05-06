using ClassManager.Model;
using ClassManager.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ClassManager
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class MemberOfClass : Page
	{
		public MemberOfClass ()
		{
			this.InitializeComponent();
		}
		protected override async void OnNavigatedTo (NavigationEventArgs e)
		{
			if (!SingleService.Instance.hasLogin()) {
				Frame.Navigate(typeof(MainPage));
				return;
			}
			string temp = ((string)e.Parameter);
			Debug.WriteLine("member");
			//Debug.WriteLine(temp);
			Result res = await SingleService.Instance.searchOrganizationDetail(temp);
            //res.organization_data.Image = SingleService.Instance.getServerAddress() + (_organization.Image == "" ? "null" : _organization.Image);
            _organization.DeepCopy(res.organization_data);
			//_organization.Image = SingleService.Instance.getServerAddress() + (_organization.Image == null ? "null" : _organization.Image);
			//org_account.Text = "Account: " + (_organization.Account == null ? "" : _organization.Account);
			//org_name.Text = "Name: " + (_organization.Name == null ? "" : _organization.Name);
			//Debug.WriteLine(JsonConvert.SerializeObject(_organization));
			//foreach (var item in _organization.Members) {
			//	item.image = SingleService.Instance.getServerAddress() + (item.image == null ? "null" : item.image);
			//}
		}
        private Organization _organization = new Organization();
	}
}
