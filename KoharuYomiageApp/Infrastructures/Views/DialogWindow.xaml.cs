using System.Windows.Input;
using Prism.Services.Dialogs;
using SourceChord.FluentWPF;

namespace KoharuYomiageApp.Infrastructures.Views
{
    public partial class DialogWindow : AcrylicWindow, IDialogWindow
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public IDialogResult? Result { get; set; }
    }
}
