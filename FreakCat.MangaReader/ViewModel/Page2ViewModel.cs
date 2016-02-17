using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FreakCat.MangaReader.Model.Entities;
using FreakCat.MangaReader.Parsers;
using FreakCat.MangaReader.UI;
using FreakCat.MangaReader.UI.Navigate;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FreakCat.MangaReader.ViewModel
{
    public class Page2ViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private TileInfo _tileInfo;
        private MangaInfo _pagesInfo;
        private ICommand _toReadCommand;
        private ICommand _onLoadCommand;
        public ObservableCollection<Chapter> Chapters { get; set; } 
        public Page2ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            if (_navigationService.NavigatedParametr is TileInfo)
            {
                _tileInfo = (TileInfo)_navigationService.NavigatedParametr;
                PagesInfo = new MangaInfo()
                {
                    Name = _tileInfo.Name,
                    RusName = _tileInfo.RusName,
                    AnotherNames = "My love for you is always growing.",
                    Status = _tileInfo.Status,
                    DownloadingStatus = "2 главы, перевод продолжается",
                    Tags = _tileInfo.Tags,
                    Author = "Shirako, Shin5",
                    Description = _tileInfo.Description,
                    Translater = "Alen Greed",
                    Image = _tileInfo.Image
                };
                Chapters = new ObservableCollection<Chapter>();
                Chapters.Add(new Chapter() {Date = DateTime.Today, Name = "#shin5 - Kekkonshite mo Koishiteru   v1 - 1" });
                Chapters.Add(new Chapter() { Date = DateTime.Today, Name = "#shin5 - Kekkonshite mo Koishiteru   v1 - 2" });
            }
        }

        public ICommand OnLoadCommand
        {
            get
            {
                return _onLoadCommand ?? (_onLoadCommand = new RelayCommand(async () =>
                {
                    var parser =new MangachanInfoParser(_tileInfo.UrlToInfo);
                    var nInf =await parser.GetInfoAsync();
                }));
            }
        }

        public ICommand ToReadCommand
        {
            get
            {
                return _toReadCommand ?? (_toReadCommand = new RelayCommand(()=>
                {
                    _navigationService.Navigate(typeof(Page3));

                }));
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
