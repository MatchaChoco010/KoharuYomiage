using System.Windows.Input;
using Prism.Services.Dialogs;

namespace KoharuYomiageApp.Infrastructures.GUI.Views.Dialogs
{
    public partial class DialogWindow : IDialogWindow
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        public IDialogResult? Result { get; set; }

        void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
