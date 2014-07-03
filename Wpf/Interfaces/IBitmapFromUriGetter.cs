using System;
using System.Drawing;

namespace MccTomskHelpers.Wpf.Interfaces
{
    public interface IBitmapFromUriGetter
    {
        Image GetImage(Uri uri);
        Image GetDefaultWeinmannLogo();
    }
}