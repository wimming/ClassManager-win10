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
// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace ClassManager.Views
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class Homeworks : Page
	{
		private OrganizationViewModel OVM;
		public Homeworks ()
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
	}

	//public class SelectItemOfHomeworks
	//{
	//	public SelectItemOfHomeworks ()
	//	{
	//		Users = new ObservableCollection<User>();
	//	}
	//	private ObservableCollection<User> users;
	//	public ObservableCollection<User> Users {
	//		get {
	//			return users;
	//		}
	//		set {
	//			foreach (User item in value) {
	//				users.Add(item);
	//			}
	//		}
	//	}
	//}
}
