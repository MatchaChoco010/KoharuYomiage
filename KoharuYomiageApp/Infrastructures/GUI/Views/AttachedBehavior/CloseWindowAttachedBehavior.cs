using System.Diagnostics;
using System.Windows;

namespace KoharuYomiageApp.Infrastructures.GUI.Views.AttachedBehavior
{
    public class CloseWindowAttachedBehavior
    {
        public static readonly DependencyProperty CloseProperty =
            DependencyProperty.RegisterAttached("Close", typeof(bool), typeof(CloseWindowAttachedBehavior),
                new PropertyMetadata(false, OnCloseChanged));

        public static bool GetClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(CloseProperty);
        }

        public static void SetClose(DependencyObject obj, bool value)
        {
            obj.SetValue(CloseProperty, value);
        }

        static void OnCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var win = d as Window ?? Window.GetWindow(d);

            Debug.WriteLine("Close");
            Debug.WriteLine(d);

            if (GetClose(d))
            {
                win?.Close();
            }
        }
    }
}
