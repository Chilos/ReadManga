using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using FreakCat.MangaReader.Model.Entities;
using FreakCat.MangaReader.Parsers;
using FreakCat.MangaReader.UI;
using FreakCat.MangaReader.UI.Navigate;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Universal.WebP;
using WinRTXamlToolkit.Imaging;

namespace FreakCat.MangaReader.ViewModel
{
    public class Page3ViewModel : ViewModelBase
    {
        private ICommand _goBack;
        private ICommand _onLoadCommand;
        private bool _processingRingVisible;
        private MangaImage _selectedImage;
        private readonly INavigationService _navigationService;
        public ObservableCollection<MangaImage> ImageCollection { get; set; }
        private string _pagesUrl;


        public Page3ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ImageCollection = new ObservableCollection<MangaImage>();
            if (_navigationService.NavigatedParametr is string)
            {
                _pagesUrl = (string) _navigationService.NavigatedParametr;
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
        public MangaImage SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
                RaisePropertyChanged(() => SelectedImage);
            }
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
                    //var tmp = _navigationService.CurrentFrame;
                    //var view = ApplicationView.GetForCurrentView();
                    //view.TryEnterFullScreenMode();
                    ProcessingRingVisible = true;
                    var parser = new MangachanPageParser(_pagesUrl);
                    ImageCollection.CollectionChanged += ImageCollection_CollectionChanged;
                     parser.GetMangaImagesAsync(ImageCollection);


                }));
            }
        }

        private void ImageCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ProcessingRingVisible = false;
        }
    }
}
