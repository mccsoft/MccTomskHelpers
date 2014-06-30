using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using WpfInfrastructure.Converters;
using WpfInfrastructure.Interfaces;

namespace WpfInfrastructure
{
    public class BitmapFromUriGetter : IBitmapFromUriGetter
    {
        private readonly Uri _defaultWeinmannLogoUri = new Uri("pack://application:,,,/MCC.WTS.ResourceDictionaries;component/Images/WM_Logo_SW_RGB-01.png", UriKind.Absolute);

        public Image GetImage(Uri uri)
        {
            var extention = Path.GetExtension(uri.ToString());
            var bitmapImage = new BitmapImage(uri);

            using (var outStream = new MemoryStream())
            {
                Bitmap bitmap;
                switch (extention)
                {
                    case ".png":
                    {
                        var enc = new PngBitmapEncoder();
                        enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                        enc.Save(outStream);

                        var bitmapTransparent = new Bitmap(outStream);
                        bitmap = (Bitmap)new TransparentToColorConverter().Convert(bitmapTransparent, null, null, null);
                    }
                        break;
                    default:
                    {
                        var enc = new BmpBitmapEncoder();
                        enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                        enc.Save(outStream);

                        bitmap = new Bitmap(outStream);
                    }
                        break;
                }
                // return bitmap; <-- leads to problems, stream is closed/closing: http://stackoverflow.com/questions/6484357/converting-bitmapimage-to-bitmap-and-vice-versa
                return new Bitmap(bitmap);
            }
        }

        public Image GetDefaultWeinmannLogo()
        {
            return GetImage(_defaultWeinmannLogoUri);
        }
    }
}