using ClassManager.Controls;
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
		public Notices ()
		{
			this.InitializeComponent();
			OVM = OrganizationViewModel.Instance;
		}

		protected override void OnNavigatedTo (NavigationEventArgs e)
		{
			OVM.initialOVM((string)e.Parameter);
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
	}
}
