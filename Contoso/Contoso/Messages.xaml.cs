using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Contoso
{
    public sealed partial class Messages : UserControl
    {
        public Messages()
        {
            this.InitializeComponent();
        }

        public ObservableCollection<string> MessagesCollection { get; set; } = new ObservableCollection<string>
        {
            "Hey", "I'm loving this build tour!", "this messages window is great for showing the KeepLastItemInView", "Go ahead and try typing in text box"
        };

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            this.MessagesCollection.Add(this.MessageTextBox.Text);
        }
    }
}
