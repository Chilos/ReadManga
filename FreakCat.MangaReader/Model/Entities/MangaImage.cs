using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FreakCat.MangaReader.Model.Entities
{
    public class MangaImage
    {
        public BitmapImage Image { get; set; }
        public string Title { get; set; }
        public string PageNamber { get; set; }
        public string Chapter { get; set; }
    }
}
