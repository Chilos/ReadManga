using System.Collections.ObjectModel;

namespace FreakCat.MangaReader.Model.Entities
{
    public class MangaInfo : MangaIfoBase
    {
        public string AnotherNames { get; set; }
        public string Author { get; set; }
        public string DownloadingStatus { get; set; }
        public string Translater { get; set; }
        public string ReadUrl { get; set; }
        public ObservableCollection<Chapter> Chapters { get; set; }

    }
}
