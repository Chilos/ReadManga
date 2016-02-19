using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;
using FreakCat.MangaReader.Model.Entities;
using FreakCat.MangaReader.Parsers;
using FreakCat.MangaReader.UI;
using FreakCat.MangaReader.UI.Navigate;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FreakCat.MangaReader.ViewModel
{
    public class Page0ViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ICommand _onLoadCommand;
        private bool _processingRingVisible;
        private RelayCommand<TileInfo> _showMangaInfo;
        public ObservableCollection<TileInfo> Tiles { get; set; }

        public Page0ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Tiles = new ObservableCollection<TileInfo>();


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

        public ICommand OnLoadCommand
        {
            get
            {
                return _onLoadCommand ?? (_onLoadCommand = new RelayCommand(() =>
                {
                    
                    var parser = new MangachanCatalogPaser();
                    if(Tiles.Count!=0)
                        return;
                    ProcessingRingVisible = true;
                    Tiles.CollectionChanged += Tiles_CollectionChanged;
                    parser.GetCatalogAsync(Tiles);
                }));
            }
        }

        private void Tiles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ProcessingRingVisible = false;
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
