using MaterialDesignThemes.Wpf;
using System;
using System.Windows;

namespace NightModeMaterialDesignUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Lazy<PaletteHelper> paletteHelper = new Lazy<PaletteHelper>();
        private void NightTheme_Click(object sender, RoutedEventArgs e)
        {
            paletteHelper.Value.SetLightDark(NightTheme.IsChecked.GetValueOrDefault());
        }
    }
}
