using ClassManager.Model;
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

namespace ClassManager.Controls
{
	/// <summary>
	/// 可用于自身或导航至 Frame 内部的空白页。
	/// </summary>
	public sealed partial class UserControlContent : Page
	{
		private int whatControls = -1;
		public UserControlContent (string pos)
		{
			this.InitializeComponent();
			if (pos == "member") {
				rb3.Visibility = Visibility.Collapsed;
			} else {
				rb2.Visibility = Visibility.Collapsed;
			}
		}
		public int getwhatControls ()
		{
			return whatControls;
		}
		void Controls (object sender, RoutedEventArgs e)
		{
			RadioButton li = (sender as RadioButton);
			if (li.Name == "rb1") {
				whatControls = 0;
			} else if (li.Name == "rb2") {
				whatControls = 1;
			} else {
				whatControls = 2;
			}
		}
	}
}
