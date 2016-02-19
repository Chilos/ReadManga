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
        private readonly TileInfo _tileInfo;
        private MangaInfo _pagesInfo;
        private ICommand _toReadCommand;
        private ICommand _onLoadCommand;
        public ObservableCollection<Chapter> Chapters { get; set; }
        private bool _processingRingVisible;
        private bool _mainViewVisible;
        public Page2ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            if (_navigationService.NavigatedParametr is TileInfo)
            {
                _tileInfo = (TileInfo)_navigationService.NavigatedParametr;
                PagesInfo = new MangaInfo {Chapters = new ObservableCollection<Chapter>()};
                MainViewVisible = false;
            }
        }

        public bool ProcessingRingVisible
        {
            get { return _processingRingVisible; }
            set
            {
                _processingRingVisible = value;
                RaisePropertyChanged(() => ProcessingRingVisible);
            }
        }
        public bool MainViewVisible
        {
            get { return _mainViewVisible; }
            set
            {
                _mainViewVisible = value;
                RaisePropertyChanged(() => MainViewVisible);
            }
        }

        public ICommand OnLoadCommand
        {
            get
            {
                return _onLoadCommand ?? (_onLoadCommand = new RelayCommand(async () =>
                {
                    ProcessingRingVisible = true;
                    var parser =new MangachanInfoParser(_tileInfo.UrlToInfo);
                    var nInf =await parser.GetInfoAsync();
                    nInf.Image = _tileInfo.Image;
                    nInf.EnName = _tileInfo.EnName;
                    nInf.RusName = _tileInfo.RusName;
                    PagesInfo = nInf;

                }));
            }
        }

        public ICommand ToReadCommand
        {
            get
            {
                return _toReadCommand ?? (_toReadCommand = new RelayCommand(()=>
                {
                    _navigationService.Navigate(typeof(Page3), PagesInfo.ReadUrl);

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
                ProcessingRingVisible = false;
                MainViewVisible = true;
            }
        }

    }
}
