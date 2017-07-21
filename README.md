# NightMode
night mode on Windows 在Windows上的夜间模式

## NightModeCore
夜间模式遮罩层，通过以下方式连接

```
ChannelServices.RegisterChannel(new IpcClientChannel(), false);
NightModeService service = (NightModeService)Activator.GetObject(typeof(NightModeService), "ipc://NightModeServerChannel/NightModeService");
```

接口如下
```
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
```

在代码中已禁止将遮罩的不透明的修改到0.75以上。

## NightModeMaterialDesignUI
夜间模式设置界面

![夜间模式设置界面](https://raw.githubusercontent.com/chen3/NightMode/master/Screenshot/1.png)

使用了[MaterialDesignInXamlToolkit](https://github.com/ButchersBoy/MaterialDesignInXamlToolkit)
