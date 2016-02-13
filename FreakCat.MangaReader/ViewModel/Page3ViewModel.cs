using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FreakCat.MangaReader.UI;
using FreakCat.MangaReader.UI.Navigate;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FreakCat.MangaReader.ViewModel
{
    public class Page3ViewModel : ViewModelBase
    {
        private ICommand _goBack;
        private ICommand _onLoadCommand;
        private readonly INavigationService _navigationService;
        public Page3ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            
        }
        public ICommand GoBack
        {
            get
            {
                return _goBack ?? (_goBack = new RelayCommand(() =>
                {
                    //_navigationService.Navigate(typeof(MainPageView), (Frame)Window.Current.Content);

                }));
            }
        }

        public ICommand OnLoadCommand
        {
            get
            {
                return _onLoadCommand ?? (_onLoadCommand = new RelayCommand(() =>
                {
                    var tmp = _navigationService.CurrentFrame;
                    var view = ApplicationView.GetForCurrentView();
                    view.TryEnterFullScreenMode();

                }));
            }
        }

    }
}
