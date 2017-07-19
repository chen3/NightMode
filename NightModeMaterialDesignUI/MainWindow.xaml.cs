using MaterialDesignThemes.Wpf;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Windows;
using NightModeCore.Service;
using System.Runtime.Remoting;
using System.Threading;
using NightModeMaterialDesignUI.Properties;

namespace NightModeMaterialDesignUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string NightModeCoreFileName = "NightModeCore.exe";


        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (o, e) =>
            {
                //Register the channel with ChannelServices.
                ChannelServices.RegisterChannel(new IpcClientChannel(), false);
                if (UpdateValue())
                {
                    NightModeClickBlock = true;
                    this.NightMode.IsChecked = true;
                    NightModeClickBlock = false;
                    return;
                }
                if (!File.Exists(NightModeCoreFileName))
                {
                    NightMode.IsEnabled = false;
                    MessageBox.Show("无法找到夜间模式内核");
                }
                NightTheme.IsChecked = Settings.Default.IsDarkTheme;
            };
            this.Closed += (o, e) =>
            {
                service?.SaveSetting();
                Settings.Default.IsDarkTheme = NightTheme.IsChecked.GetValueOrDefault();
                Settings.Default.Save();
            };
        }

        private Lazy<PaletteHelper> paletteHelper = new Lazy<PaletteHelper>();
        private void NightTheme_Changed(object sender, RoutedEventArgs e)
        {
            paletteHelper.Value.SetLightDark(NightTheme.IsChecked.GetValueOrDefault());
        }
        
        private bool NightModeClickBlock = false;
        private void NightMode_Changed(object sender, RoutedEventArgs e)
        {
            if(NightModeClickBlock)
            {
                return;
            }
            if(NightMode.IsChecked.GetValueOrDefault())
            {
                Process.Start(NightModeCoreFileName).WaitForInputIdle();
                Thread.Sleep(200);
                UpdateValue();
            }
            else
            {
                service?.SaveSetting();
                service?.Exit();
                service = null;
            }
        }

        private bool UpdateValue()
        {
            NightModeService service = (NightModeService)Activator.GetObject(typeof(NightModeService),
                                                        "ipc://NightModeServerChannel/NightModeService");
            try
            {
                nightModeOpacityValueChangedBlock = true;
                this.NightModeOpacity.Value = NightModeOpacity.Maximum - service.Opacity * 100;
                this.service = service;
            }
            catch (RemotingException)
            {
                return false;
            }
            finally
            {
                nightModeOpacityValueChangedBlock = false;
            }
            return true;
        }

        private NightModeService service;

        private bool nightModeOpacityValueChangedBlock = false;
        private void NightModeOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (nightModeOpacityValueChangedBlock || service == null)
            {
                return;
            }
            try
            {
                service.Opacity = (NightModeOpacity.Maximum - e.NewValue) / 100;
            }
            catch (RemotingException)
            {
                NightMode.IsChecked = false;
            }
        }
    }
}
