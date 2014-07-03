using System;
using System.Runtime.InteropServices;

namespace MccTomskHelpers.Wpf
{
    public class DpiHelper
    {
        private const int DefaultWpfDpi = 96;

        private const int LOGPIXELSX = 88;
        private const int LOGPIXELSY = 90;

        [DllImport("User32.dll")]
        private static extern IntPtr GetDC(HandleRef hWnd);

        [DllImport("User32.dll")]
        private static extern int ReleaseDC(HandleRef hWnd, HandleRef hDC);

        [DllImport("GDI32.dll")]
        private static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

        private static int _dpiX;
        private static int _dpiY;

        public static int DpiX
        {
            get
            {
                ReadDpi();
                return _dpiX;
            }
        }

        public static int DpiY
        {
            get
            {
                ReadDpi();
                return _dpiY;
            }
        }

        private static void ReadDpi()
        {
            if (_dpiX == 0)
            {
                var desktopHwnd = new HandleRef(null, IntPtr.Zero);
                var desktopDc = new HandleRef(null, GetDC(desktopHwnd));
                _dpiX = GetDeviceCaps(desktopDc, LOGPIXELSX);
                _dpiY = GetDeviceCaps(desktopDc, LOGPIXELSY);
                ReleaseDC(desktopHwnd, desktopDc);
            }
        }

        public static double GetPhysicalXPixels(double width)
        {
            return width * DpiX / DefaultWpfDpi;
        }

        public static double GetPhysicalYPixels(double height)
        {
            return height * DpiX / DefaultWpfDpi;
        } 
    }
}