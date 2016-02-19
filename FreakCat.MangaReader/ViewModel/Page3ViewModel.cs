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
        private MangaImage _selectedImage;
        private readonly INavigationService _navigationService;
        public ObservableCollection<MangaImage> ImageCollection { get; set; }
        private string _pagesUrl;

        public async Task<WriteableBitmap> qqqq(string url)
        {
            var bytes =  new HttpClient().GetByteArrayAsync(url).Result;

            // Create an instance of the decoder
            var webp = new WebPDecoder();

            // Decode to BGRA (Bitmaps use this format)
            var pixelData1 = await webp.DecodeBgraAsync(bytes);
            var pixelData = pixelData1.ToArray();
            // Get the size
            var size = await webp.GetSizeAsync(bytes);

            // With the size of the webp, create a WriteableBitmap
            var bitmap = new WriteableBitmap((int)size.Width, (int)size.Height);

            // Write the pixel data to the buffer
            var stream = bitmap.PixelBuffer.AsStream();
            await stream.WriteAsync(pixelData, 0, pixelData.Length);
            return bitmap;
        }

        public Page3ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ImageCollection = new ObservableCollection<MangaImage>();
            if (_navigationService.NavigatedParametr is string)
            {
                _pagesUrl = (string) _navigationService.NavigatedParametr;
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
                return _onLoadCommand ?? (_onLoadCommand = new RelayCommand( async () =>
                {
                    //var tmp = _navigationService.CurrentFrame;
                    //var view = ApplicationView.GetForCurrentView();
                    //view.TryEnterFullScreenMode();
                    var parser = new MangachanPageParser(_pagesUrl);
                    parser.GetMangaImagesAsync(ImageCollection);
                    ImageCollection.Add(new MangaImage()
                    {
                        Title = "#shin5 - Kekkonshite mo Koishiteru",
                        Image =
                            await
                                qqqq(
                                    @"http://img2.mangachan.ru/manganew_webp/-9new/f/1455435592_fuuka_v10_ch95/01._.webp"),
                        Chapter = "Том 10 Глава 95",
                        PageNamber = "1/4"
                    });
                    ImageCollection.Add(new MangaImage()
                    {
                        Title = "#shin5 - Kekkonshite mo Koishiteru",
                        Image =
                            await
                                qqqq(
                                    @"http://img2.mangachan.ru/manganew_webp/-9new/f/1455435592_fuuka_v10_ch95/02._.webp"),
                        Chapter = "Том 10 Глава 95",
                        PageNamber = "2/4"
                    });

                }));
            }
        }

    }
}
