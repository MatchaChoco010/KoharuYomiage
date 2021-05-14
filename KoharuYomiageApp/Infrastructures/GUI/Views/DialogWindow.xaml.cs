using System.Windows.Input;
using Prism.Services.Dialogs;
using SourceChord.FluentWPF;

namespace KoharuYomiageApp.Infrastructures.GUI.Views
{
    public partial class DialogWindow : AcrylicWindow, IDialogWindow
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
