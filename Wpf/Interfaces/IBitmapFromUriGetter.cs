using System;
using System.Drawing;

namespace WpfInfrastructure.Interfaces
{
    public interface IBitmapFromUriGetter
    {
        Image GetImage(Uri uri);
        Image GetDefaultWeinmannLogo();
    }
}