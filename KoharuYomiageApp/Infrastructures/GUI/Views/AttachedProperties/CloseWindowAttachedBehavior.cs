using System.Diagnostics;
using System.Windows;

namespace KoharuYomiageApp.Infrastructures.GUI.Views.AttachedProperties
{
    public class CloseWindowAttachedBehavior
    {
        public static bool GetClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(CloseProperty);
        }

        public static void SetClose(DependencyObject obj, bool value)
        {
            obj.SetValue(CloseProperty, value);
        }

        public static readonly DependencyProperty CloseProperty =
            DependencyProperty.RegisterAttached("Close", typeof(bool), typeof(CloseWindowAttachedBehavior),
                new PropertyMetadata(false, OnCloseChanged));

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
