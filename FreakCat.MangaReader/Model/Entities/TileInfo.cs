using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;

namespace FreakCat.MangaReader.Model.Entities 
{
    public class TileInfo
    {
        public ImageSource Image { get; set; }
        public string Name { get; set; }
        public string RusName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }


        public ObservableCollection<Tag> Tags { get; set; }

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