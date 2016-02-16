using System.Collections.ObjectModel;

namespace FreakCat.MangaReader.Model.Entities 
{
    public class TileInfo : MangaIfoBase
    {
        public string UrlToInfo { get; set; }
        public int ChapterCount { get; set; }
        public bool IsEnded { get; set; }
        public bool IsSingle { get; set; }

        public TileInfo()
        {
            Tags = new ObservableCollection<Tag> {
                new Tag() { Name = "��������������"},
                new Tag() { Name = "�������"},
                new Tag() { Name = "���������"},
                new Tag() { Name = "���"},
                new Tag() { Name = "���������"}
            };
        }


    }
}