using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;

namespace FreakCat.MangaReader.Model.Entities 
{
    public class TileInfo : MangaIfoBase
    {
        public string UrlToInfo { get; set; }

        public TileInfo()
        {
            Tags = new ObservableCollection<Tag> {
                new Tag() { Name = "повседневность"},
                new Tag() { Name = "комедия"},
                new Tag() { Name = "романтика"},
                new Tag() { Name = "сёдзё"},
                new Tag() { Name = "романтика"}
            };
        }


    }
}