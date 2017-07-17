using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NightModeCore
{
    public static class MousePierce
    {
        /// <summary>
        /// 使窗体有鼠标穿透功能
        /// </summary>
        public static void EnableMousePierce(this Form form)
        {
            EnableMousePierce(form.Handle);
        }

        /// <summary>
        /// 使指定句柄窗体有鼠标穿透功能
        /// </summary>
        private static void EnableMousePierce(IntPtr intPtr)
        {
            uint intExTemp = GetWindowLong(intPtr, GWL_EXSTYLE);
            uint oldGWLEx = SetWindowLong(intPtr, GWL_EXSTYLE, WS_EX_TRANSPARENT | WS_EX_LAYERED);
            SetLayeredWindowAttributes(intPtr, 0, 100, LWA_ALPHA);
        }

        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        private static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, int bAlpha, int dwFlags);

        private const uint WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_STYLE = -16;
        private const int GWL_EXSTYLE = -20;
        private const int LWA_ALPHA = 0x2;

    }
}
