using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight;

namespace FreakCat.MangaReader.Model.Entities
{
    public class MangaImage
    {
        public WriteableBitmap Image { get; set; }
        public string Title { get; set; }
        public string PageNamber { get; set; }
        public string Chapter { get; set; }

    }
}
