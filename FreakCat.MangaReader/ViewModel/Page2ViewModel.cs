using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreakCat.MangaReader.Model.Entities;
using FreakCat.MangaReader.UI.Navigate;
using GalaSoft.MvvmLight;

namespace FreakCat.MangaReader.ViewModel
{
    public class Page2ViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private MangaInfo _pagesInfo;
        public ObservableCollection<Chapter> Chapters { get; set; } 
        public Page2ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            if (_navigationService.NavigatedParametr is TileInfo)
            {
                var tileInfo = (TileInfo)_navigationService.NavigatedParametr;
                PagesInfo = new MangaInfo()
                {
                    Name = tileInfo.Name,
                    RusName = tileInfo.RusName,
                    AnotherNames = "My love for you is always growing.",
                    Status = tileInfo.Status,
                    DownloadingStatus = "2 главы, перевод продолжается",
                    Tags = tileInfo.Tags,
                    Author = "Shirako, Shin5",
                    Description = tileInfo.Description,
                    Translater = "Alen Greed",
                    Image = tileInfo.Image
                };
                Chapters = new ObservableCollection<Chapter>();
                Chapters.Add(new Chapter() {Date = DateTime.Today, Name = "#shin5 - Kekkonshite mo Koishiteru   v1 - 1" });
                Chapters.Add(new Chapter() { Date = DateTime.Today, Name = "#shin5 - Kekkonshite mo Koishiteru   v1 - 2" });
            }
        }

        public MangaInfo PagesInfo
        {
            get { return _pagesInfo; }
            set
            {
                _pagesInfo = value;
                RaisePropertyChanged(() => PagesInfo);
            }
        }

    }
}
