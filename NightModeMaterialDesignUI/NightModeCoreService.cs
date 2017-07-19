namespace NightModeCore.Service
{
    public interface NightModeService
    {
        double Opacity
        {
            get;
            set;
        }

        void SaveSetting();

        void Exit();

    }
}
