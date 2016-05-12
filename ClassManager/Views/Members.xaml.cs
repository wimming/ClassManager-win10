using ClassManager.Controls;
using ClassManager.Model;
using ClassManager.Service;
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
	public sealed partial class Members : Page
	{
		private OrganizationViewModel OVM;
		private UserViewModel UVM;
		public Members ()
		{
			this.InitializeComponent();

			OVM = OrganizationViewModel.Instance;
			UVM = UserViewModel.Instance;
		}

		protected override void OnNavigatedTo (NavigationEventArgs e)
		{
			OVM.initialOVM((string)e.Parameter);
		}

		private async void OnItemClick (object sender, ItemClickEventArgs e)
		{
			var clickUser = (User)(e.ClickedItem);
			bool hasPowful = false;
			string pos = clickUser.Position;
			foreach (var item in UVM.User.Relationships) {
				if (item.account == OVM.Organization.Account && item.position == "founder") {
					hasPowful = true;
				}
			}
			if (clickUser.Account == UVM.User.Account) {
				hasPowful = false;
			}
			if (hasPowful) {
				var dialog = new ContentDialog() {
					Title = "What do you want to do?",
					Content = new UserControlContent(pos),
					PrimaryButtonText = "确定",
					SecondaryButtonText = "取消",
					FullSizeDesired = false,
				};
				dialog.PrimaryButtonClick += (_s, _e) => {
					ContentDialog x = dialog;
					Dictionary<string, string> voteData = new Dictionary<string, string>();
					int what = ((UserControlContent)dialog.Content).getwhatControls();
					if (what == 0) {
						OVM.deleteMember(OVM.Organization.account, clickUser.Account);
					} else if (what == 1) {
						OVM.UpMember(OVM.Organization.account, clickUser.ID);
					} else if (what == 2) {
						OVM.downMember(OVM.Organization.account, clickUser.ID);
					}
				};
				await dialog.ShowAsync();
			}
		}
	}
}
