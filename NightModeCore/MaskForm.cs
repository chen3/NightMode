using NightModeCore.Service;
using System.Drawing;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Windows.Forms;

namespace NightModeCore.UI
{
    public partial class MaskForm : Form
    {
        public MaskForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            this.FormBorderStyle = FormBorderStyle.None;
            LoadSetting();
            this.EnableMousePierce();

            UpdateSizeAndLocation();
            Microsoft.Win32.SystemEvents.UserPreferenceChanged += (s,e) => UpdateSizeAndLocation();

            maskForm = this;
            StartService();
        }

        private static MaskForm maskForm;
        public static MaskForm GetMaskForm()
        {
            return maskForm;
        }

        private void LoadSetting()
        {
            this.Opacity = Properties.Settings.Default.Opacity;
        }

        private void UpdateSizeAndLocation()
        {
            //Rectangle rect = SystemInformation.VirtualScreen;
            Rectangle rect = Screen.PrimaryScreen.Bounds;
            this.Location = new Point(0, 0);
            this.Width = rect.Width;
            this.Height = rect.Height;
        }

        private void StartService()
        {
            IpcServerChannel channel = new IpcServerChannel("NightModeServerChannel");
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(NightModeService),
                                        "NightModeService", WellKnownObjectMode.SingleCall);
        }
        
    }
}
