using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using ClassManager.Model;
using ClassManager.ViewModels;
using Windows.UI.Xaml;
using System.Collections.Generic;
using ClassManager.Controls;
using Windows.ApplicationModel.DataTransfer;
// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ClassManager.Views
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class Homeworks : Page
	{
		private OrganizationViewModel OVM;
		private UserViewModel UVM;
		private Homework clickHome;
		public Homeworks ()
		{
			this.InitializeComponent();
			OVM = OrganizationViewModel.Instance;
			UVM = UserViewModel.Instance;
		}

		protected override void OnNavigatedTo (NavigationEventArgs e)
		{
			OVM.initialOVM((string)e.Parameter);
			DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;
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

		protected override void OnNavigatedFrom (NavigationEventArgs e)
		{
			DataTransferManager.GetForCurrentView().DataRequested += OnShareDataRequested;
		}

		//数据打包进行分享
		/*async*/ void OnShareDataRequested (DataTransferManager sender, DataRequestedEventArgs args)
		{
			var dp = args.Request.Data;
			dp.Properties.Title = "共享作业：" + clickHome.name;
			dp.Properties.Description = "来自ClassManager的共享";
			dp.SetText("开始日期： " + Convert.ToDateTime(clickHome.join_on).ToLocalTime() + "\n截止日期： " + Convert.ToDateTime(clickHome.deadline).ToLocalTime()  + "\n\n作业内容：" + clickHome.content + "\n");
		}

		private async void AddButton_Click (object sender, RoutedEventArgs e)
		{
			var dialog = new ContentDialog() {
				Title = "新建作业",
				Content = new CreateHomeworkContent(),
				PrimaryButtonText = "确定",
				SecondaryButtonText = "取消",
				FullSizeDesired = false,
			};

			dialog.PrimaryButtonClick += (_s, _e) => {
				ContentDialog x = dialog;
				Dictionary<string, string> voteData = new Dictionary<string, string>();
				voteData.Add("name", ((CreateHomeworkContent)dialog.Content).getName());
				voteData.Add("content", ((CreateHomeworkContent)dialog.Content).getContent());
				voteData.Add("deadline", ((CreateHomeworkContent)dialog.Content).getDeadline() + "");
				OVM.createHomeWork(voteData);
			};
			await dialog.ShowAsync();
		}

		private async void OnItemClick (object sender, ItemClickEventArgs e)
		{
			clickHome = (Homework)(e.ClickedItem);
			bool hasPowful = false;
			bool unlook = false;
			bool uncomplete = false;
			Dictionary<string, string> type = new Dictionary<string, string>();
			foreach (var item in UVM.User.Relationships) {
				if (item.account == OVM.Organization.Account && (item.position == "founder" || item.position == "manager")) {
					hasPowful = true;
				}
			}
			foreach (var item in UVM.User.homeworks) {
				if (item._id == clickHome._id) {
					if (item.unlook) {
						unlook = item.unlook;
					}
					if (item.uncomplish) {
						uncomplete = item.uncomplish;
					}
				}
			}
			type.Add("hasPowful", hasPowful.ToString());
			type.Add("unlook", unlook.ToString());
			type.Add("uncomplete", uncomplete.ToString());
			type.Add("type", "homework");
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
					OVM.deleteHomework(OVM.Organization.account, clickHome._id);
				} else if (what == 1) {
					OVM.lookHomework(OVM.Organization.account, clickHome._id);
				} else if (what == 3) {
					UVM.complishHomework(clickHome._id, !uncomplete);
				} else if (what == 4) {
					UVM.complishHomework(clickHome._id, !uncomplete);
				} else if (what == 5) {
					DataTransferManager.ShowShareUI();
				}
			};
			await dialog.ShowAsync();
		}
	}
}
