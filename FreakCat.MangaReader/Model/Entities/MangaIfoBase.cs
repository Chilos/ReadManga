using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace FreakCat.MangaReader.Model.Entities
{
    public class MangaIfoBase
    {
        public ImageSource Image { get; set; }
        public string EnName { get; set; }
        public string RusName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }
    }
}
