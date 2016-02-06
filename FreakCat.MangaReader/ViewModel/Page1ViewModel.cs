using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using FreakCat.MangaReader.UI;
using FreakCat.MangaReader.UI.Navigate;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FreakCat.MangaReader.ViewModel
{
    public class Page1ViewModel : ViewModelBase
    {
        private ICommand _goBack;
        private readonly INavigationService _navigationService;
        public Page1ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public ICommand GoBack
        {
            get
            {
                return _goBack ?? (_goBack = new RelayCommand(() =>
                {
                    _navigationService.GoBack();

                }));
            }
        }
    }
}
