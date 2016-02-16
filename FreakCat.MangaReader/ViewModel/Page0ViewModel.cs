using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;
using FreakCat.MangaReader.Model.Entities;
using FreakCat.MangaReader.Parsers;
using FreakCat.MangaReader.UI;
using FreakCat.MangaReader.UI.Navigate;
using GalaSoft.MvvmLight.Command;

namespace FreakCat.MangaReader.ViewModel
{
    public class Page0ViewModel
    {
        private readonly INavigationService _navigationService;
        private ICommand _onLoadCommand;
        private RelayCommand<TileInfo> _showMangaInfo;
        public ObservableCollection<TileInfo> Tiles { get; set; }

        public Page0ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Tiles = new ObservableCollection<TileInfo>();


        }

        public ICommand OnLoadCommand
        {
            get
            {
                return _onLoadCommand ?? (_onLoadCommand = new RelayCommand(() =>
                {
                    var parser = new MangachanCatalogPaser();
                    Tiles.Clear();
                    parser.GetCatalogAsync(Tiles);
                }));
            }
        }

        public RelayCommand<TileInfo> ShowMangaInfo
        {
            get
            {
                return _showMangaInfo ?? (_showMangaInfo = new RelayCommand<TileInfo>( o =>
                {
                    _navigationService.Navigate(typeof(Page2),o);

                }));
            }
        }
    }
}
