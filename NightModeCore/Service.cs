using NightModeCore.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NightModeCore.Service
{
    public class NightModeService : MarshalByRefObject
    {
        public double Opacity
        {
            get
            {
                return MaskForm.GetMaskForm().Opacity;
            }
            set
            {
                if(value > 0.75)
                {
                    return;
                }
                MaskForm.GetMaskForm().Opacity = value;
            }
        }

        public void SaveSetting()
        {
            Properties.Settings.Default.Opacity = Opacity;
            Properties.Settings.Default.Save();
        }

    }
}
