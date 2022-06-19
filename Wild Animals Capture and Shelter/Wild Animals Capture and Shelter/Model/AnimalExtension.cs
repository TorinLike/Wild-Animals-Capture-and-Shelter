using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Wild_Animals_Capture_and_Shelter.Model
{
    public partial class Animal
    {
        public BitmapImage PhotoImage
        {
            get
            {
                if (Photo is null || Photo.Length == 0)
                {
                    return new BitmapImage(new Uri("/Images/null_image.jpg", UriKind.Relative));
                }
                var image = new BitmapImage();
                using (var mem = new MemoryStream(Photo))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();
                return image;
            }
        }
    }
}
