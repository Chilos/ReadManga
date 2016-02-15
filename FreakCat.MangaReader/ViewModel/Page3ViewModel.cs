using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using FreakCat.MangaReader.Model.Entities;
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
        public ObservableCollection<MangaImage> ImageCollection { get; set; }

        public Page3ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ImageCollection = new ObservableCollection<MangaImage>()
            {
                new MangaImage() {Title = "#shin5 - Kekkonshite mo Koishiteru", Image = new BitmapImage(new Uri(@"http://img2.mangachan.ru/manganew_webp/-9new/f/1455435592_fuuka_v10_ch95/02._.webp")), Chapter = "Том 10 Глава 95", PageNamber = "1/4"},
                new MangaImage() {Title = "#shin5 - Kekkonshite mo Koishiteru", Image = new BitmapImage(new Uri(@"http://img2.mangachan.ru/manganew_webp/-9new/f/1455435592_fuuka_v10_ch95/03._.webp")), Chapter = "Том 10 Глава 95", PageNamber = "2/4"},
                new MangaImage() {Title = "#shin5 - Kekkonshite mo Koishiteru", Image = new BitmapImage(new Uri(@"http://img2.mangachan.ru/manganew_webp/-9new/f/1455435592_fuuka_v10_ch95/04._.webp")), Chapter = "Том 10 Глава 95", PageNamber = "3/4"},
                new MangaImage() {Title = "#shin5 - Kekkonshite mo Koishiteru", Image = new BitmapImage(new Uri(@"http://img2.mangachan.ru/manganew_webp/-9new/f/1455435592_fuuka_v10_ch95/05._.webp")), Chapter = "Том 10 Глава 95", PageNamber = "4/4"},
            };


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
