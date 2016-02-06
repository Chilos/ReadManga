using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;
using FreakCat.MangaReader.Model.Entities;
using FreakCat.MangaReader.UI;
using FreakCat.MangaReader.UI.Navigate;
using GalaSoft.MvvmLight.Command;

namespace FreakCat.MangaReader.ViewModel
{
    public class Page0ViewModel
    {
        private readonly INavigationService _navigationService;
        private RelayCommand<TileInfo> _showMangaInfo;
        public ObservableCollection<TileInfo> Tiles { get; set; }

        public Page0ViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Tiles = new ObservableCollection<TileInfo>
            {
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" },
                new TileInfo() {Image = new BitmapImage(new Uri(@"http://mangachan.ru/showfull_retina/uploads/posts/2015-11/thumbs/1446880739_cny0gnquwaaskwh.jpg")),Name = "#shin5 - Kekkonshite mo Koishiteru", RusName = "Любовь после свадьбы", Status = "1 том, выпуск продолжается 2 главы, перевод продолжается", Description =@"Милая и забавная манга о жизни пары, вступившей в брак.(с) Alen Greed" }
            };

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
