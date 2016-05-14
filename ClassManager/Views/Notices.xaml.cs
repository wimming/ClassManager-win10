using ClassManager.Controls;
using ClassManager.Model;
using ClassManager.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
		private Notice clickNotice;
		public Notices ()
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
		async void OnShareDataRequested (DataTransferManager sender, DataRequestedEventArgs args)
		{
			var dp = args.Request.Data;
			var deferral = args.Request.GetDeferral();
			dp.Properties.Title = "共享班级公告：" + clickNotice.name;
			dp.Properties.Description = "来自ClassManager的共享";
			dp.SetText("开始日期： " + clickNotice.join_on + "\n截止日期： " + clickNotice.deadline + "\n\n公告内容：" + clickNotice.content + "\n");
			dp.SetHtmlFormat(HtmlFormatHelper.CreateHtmlFormat(
				"<p>" + "开始日期： " + Convert.ToDateTime(clickNotice.join_on).ToLocalTime() + "</p>" +
				"<p>" + "截止日期： " + Convert.ToDateTime(clickNotice.deadline).ToLocalTime() + "</p>" +
				"<p>" + "公告内容：" + clickNotice.content + "</p>" +
				"<p>" + "公告配图：" + "</p>" +
				"<img src=" + clickNotice.image + " />"));
			deferral.Complete();
		}

		private async void AddButton_Click (object sender, RoutedEventArgs e)
		{
			var dialog = new ContentDialog() {
				Title = "新建通知",
				Content = new CreateNoticeContent(),
				PrimaryButtonText = "确定",
				SecondaryButtonText = "取消",
				FullSizeDesired = false,
			};

			dialog.PrimaryButtonClick += (_s, _e) =>
            {
                ContentDialog x = dialog;

                //Stream image_stream = await ((CreateNoticeContent)dialog.Content).getImageStream();
                //StreamContent sc = new StreamContent(image_stream);
                //sc.Headers.Add("Content-Type", "image/* ");

                MultipartFormDataContent multipartContent = new MultipartFormDataContent();
                multipartContent.Add(new StringContent(((CreateNoticeContent)dialog.Content).getName()), "name");
                multipartContent.Add(new StringContent(((CreateNoticeContent)dialog.Content).getContent()), "content");
                multipartContent.Add(new StringContent(((CreateNoticeContent)dialog.Content).getDeadline()+""), "deadline");
                //multipartContent.Add(sc, "image");

                OVM.createNotice(multipartContent);
            };
			await dialog.ShowAsync();
		}

		private async void OnItemClick (object sender, ItemClickEventArgs e)
		{
			clickNotice = (Notice)(e.ClickedItem);
			bool hasPowful = false;
			bool unlook = false;
			Dictionary<string, string> type = new Dictionary<string, string>();
			foreach (var item in UVM.User.Relationships) {
				if (item.account == OVM.Organization.Account && (item.position == "founder" || item.position == "manager")) {
					hasPowful = true;
				}
			}
			foreach (var item in OVM.Organization.Notices) {
				if (item._id == clickNotice._id) {
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
					OVM.deleteNotice(OVM.Organization.account, clickNotice._id);
				} else if (what == 2) {
					OVM.lookNotice(OVM.Organization.account, clickNotice._id);
				} else if (what == 5) {
					DataTransferManager.ShowShareUI();
				}
			};
			await dialog.ShowAsync();
		}

	}
}
