using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KoharuYomiageApp.Infrastructures.GUI.Views
{
    public partial class AccountList
    {
        public AccountList()
        {
            InitializeComponent();
        }

        void HandlePreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Handled)
            {
                return;
            }

            e.Handled = true;
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = MouseWheelEvent;
            eventArg.Source = sender;
            var parent = ((Control)sender).Parent as UIElement;
            parent?.RaiseEvent(eventArg);
        }
    }
}
