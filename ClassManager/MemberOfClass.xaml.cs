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
			_organization = res.organization_data;
			_organization.image = SingleService.Instance.getServerAddress() + (_organization.image == null ? "null" : _organization.image);
			org_account.Text = "Account: " + (_organization.account == null ? "" : _organization.account);
			org_name.Text = "Name: " + (_organization.name == null ? "" : _organization.name);
			//Debug.WriteLine(JsonConvert.SerializeObject(_organization));
			foreach (var item in _organization.members) {
				item.image = SingleService.Instance.getServerAddress() + (item.image == null ? "null" : item.image);
			}
		}
		private Organization _organization { get; set; }
	}
}
