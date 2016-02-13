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
using FreakCat.MangaReader.UI;
using FreakCat.MangaReader.UI.Navigate;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FreakCat.MangaReader.ViewModel
{
    public class Page2ViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private MangaInfo _pagesInfo;
        private ICommand _toReadCommand;
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

        public ICommand ToReadCommand
        {
            get
            {
                return _toReadCommand ?? (_toReadCommand = new RelayCommand(()=>
                {
                    _navigationService.Navigate(typeof(Page3), (Frame)Window.Current.Content);

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
