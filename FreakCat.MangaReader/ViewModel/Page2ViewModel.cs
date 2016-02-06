using System;
using System.Collections.Generic;
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
        private TileInfo _tileInfo;
        public Page2ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            if (_navigationService.NavigatedParametr is TileInfo)
            {
                TileInfo = (TileInfo)_navigationService.NavigatedParametr;              
            }
        }

        public TileInfo TileInfo
        {
            get { return _tileInfo; }
            set
            {
                _tileInfo = value;
                RaisePropertyChanged(() => TileInfo);
            }
        }

    }
}
