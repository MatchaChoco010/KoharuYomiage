using System.Collections.Generic;
using System.Windows.Media;
using KoharuYomiageApp.Infrastructures.GUI.ViewModels;
using SourceChord.FluentWPF;

namespace KoharuYomiageApp.Infrastructures.GUI.Views
{
    public partial class Setting
    {
        public Setting()
        {
            InitializeComponent();
            SetIconColor();
            SystemTheme.ThemeChanged += (_, _) => SetIconColor();
        }

        void SetIconColor()
        {
            if (DataContext is SettingViewModel viewModel)
            {
                viewModel.IconBrushes.Value = new Dictionary<string, Brush>
                {
                    {"license-logo-stroke", (Brush)FindResource("SystemBaseHighColorBrush")}
                };
            }
        }
    }
}
