using ClassManager.Controls;
using ClassManager.Model;
using ClassManager.ViewModels;
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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ClassManager.Views
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class Notices : Page
	{
		private OrganizationViewModel OVM;
		private UserViewModel UVM;
		Boolean? isOpenUnlooksList = false;
		public Notices ()
		{
			this.InitializeComponent();
			OVM = OrganizationViewModel.Instance;
			UVM = UserViewModel.Instance;
		}

		protected override void OnNavigatedTo (NavigationEventArgs e)
		{
			OVM.initialOVM((string)e.Parameter);
			bool hasPowful = false;
			foreach (var item in UVM.User.Relationships) {
				if (item.account == OVM.Organization.Account && (item.position == "founder" || item.position == "manager")) {
					hasPowful = true;
				}
			}
			if (!hasPowful) {
				add_btn.Visibility = Visibility.Collapsed;
			}
		}

		private async void AddButton_Click (object sender, RoutedEventArgs e)
		{
			var dialog = new ContentDialog() {
				Title = "新建作业",
				Content = new CreateNoticeContent(),
				PrimaryButtonText = "确定",
				SecondaryButtonText = "取消",
				FullSizeDesired = false,
			};

			dialog.PrimaryButtonClick += (_s, _e) => {
				ContentDialog x = dialog;
				Dictionary<string, string> NoticeData = new Dictionary<string, string>();
				NoticeData.Add("name", ((CreateNoticeContent)dialog.Content).getName());
				NoticeData.Add("content", ((CreateNoticeContent)dialog.Content).getContent());
				NoticeData.Add("deadline", ((CreateNoticeContent)dialog.Content).getDeadline() + "");

				OVM.createNotice(NoticeData);
			};
			await dialog.ShowAsync();
		}

		private void UnlooksList_btn(object sender, RoutedEventArgs e)
		{
			isOpenUnlooksList = !isOpenUnlooksList;
		}

		private async void OnItemClick (object sender, ItemClickEventArgs e)
		{
			var clickHome = (Notice)(e.ClickedItem);
			bool hasPowful = false;
			bool unlook = false;
			Dictionary<string, string> type = new Dictionary<string, string>();
			foreach (var item in UVM.User.Relationships) {
				if (item.account == OVM.Organization.Account && (item.position == "founder" || item.position == "manager")) {
					hasPowful = true;
				}
			}
			foreach (var item in OVM.Organization.Notices) {
				if (item._id == clickHome._id) {
					foreach (var unlookUser in item.unlooks) {
						if (unlookUser.account == UVM.User.Account) {
							unlook = true;
						}
					}
				}
			}
			type.Add("hasPowful", hasPowful.ToString());
			type.Add("unlook", unlook.ToString());
			type.Add("type", "Notice");
			var dialog = new ContentDialog() {
				Title = "What do you want to do?",
				Content = new homeWorkAndNoticesCtl(type),
				PrimaryButtonText = "确定",
				SecondaryButtonText = "取消",
				FullSizeDesired = false,
			};
			dialog.PrimaryButtonClick += (_s, _e) => {
				ContentDialog x = dialog;
				int what = ((homeWorkAndNoticesCtl)dialog.Content).getwhatControls();
				if (what == 0) {
					OVM.deleteNotice(OVM.Organization.account, clickHome._id);
				} else if (what == 2) {
					OVM.lookNotice(OVM.Organization.account, clickHome._id);
				}
			};
			await dialog.ShowAsync();
		}

	}
}
