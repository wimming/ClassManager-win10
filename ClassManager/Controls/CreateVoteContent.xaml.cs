using ClassManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ClassManager.Controls
{
    public sealed partial class CreateVoteContent : UserControl
    {
        private ObservableCollection<OptionString> options = new ObservableCollection<OptionString>();

        public CreateVoteContent()
        {
            this.InitializeComponent();

            options.Add(new OptionString());
            options.Add(new OptionString());
        }

        private void addOptionBtn_Click(object sender, RoutedEventArgs e)
        {
            options.Add(new OptionString());
        }

        public string getName()
        {
            return name.Text;
        }
        public string getContent()
        {
            return content.Text;
        }
        public int getDeadline()
        {
            return (datePicker.Date + timePicker.Time).Millisecond;
        }
        public string getOptions()
        {
            string result = "[";
            for (int i = 0, flag = 0; i < listView.Items.Count; ++i)
            {
                if (flag == 0)
                {
                    flag = 1;
                } else
                {
                    result += ",";
                }
                result += "{\"name\":\"";
                result += options[i].Str;
                result += "\"}";
            }
            result += "]";

            return result;
        }
    }

    class OptionString : INotifyPropertyChanged
    {
        private string str;
        public String Str
        {
            get { return str; }
            set
            {
                str = value;
                NotifyPropertyChanged("Str");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // NotifyPropertyChanged will raise the PropertyChanged event passing the
        // source property that is being updated.
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
