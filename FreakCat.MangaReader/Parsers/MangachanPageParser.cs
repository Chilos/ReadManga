using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using FreakCat.MangaReader.Model.Entities;
using Universal.WebP;
using WinRTXamlToolkit.Controls;

namespace FreakCat.MangaReader.Parsers
{
    class MangachanPageParser
    {
        private const string SITE_URL = @"http://mangachan.ru";
        private string _mangaImageUrl;

        public MangachanPageParser(string mangaUrl)
        {
            _mangaImageUrl = SITE_URL + mangaUrl;
        }

        public async void GetMangaImagesAsync(ObservableCollection<MangaImage> mangaImages)
        {          
            string previewPageHtml = await GetHtmlPage(_mangaImageUrl);
            int count = ParsePagesCount(previewPageHtml);
            for(int i=0;i<count;i++)
                mangaImages.Add(new MangaImage());

            for (int i = 0; i < count; i++)
            {
                mangaImages[i] = await GetElement($"{_mangaImageUrl}#page={i + 1}", $"{i + 1}/{count}");
                //mangaImages.Add(el);    
            }

        }

        private async Task<MangaImage> GetElement(string url, string pnamber)
        {
            string previewPageHtmlPage = await GetHtmlPage(url);
            string imageUrl = ParseImageUrl(previewPageHtmlPage);
            var img = await GetImageFromUrl(imageUrl);
            return new MangaImage() {Image = img, PageNamber = pnamber};
        }

        private async Task<string> GetHtmlPage(string mangaImageUrl)
        {
            wv  = new WebView();
            //wv.NavigationCompleted += Wv_NavigationCompleted;
            wv.Navigate(new Uri(mangaImageUrl));
            var ss = await WaitForMessageSent();
            if (ss.IsSuccess)
            {
                _content = await wv.InvokeScriptAsync("eval",
                    new string[] { "document.documentElement.outerHTML;" });
            }
            return _content;
        }

        WebView wv;

        string _content;
        async Task<WebViewNavigationCompletedEventArgs> WaitForMessageSent()
        {
            TypedEventHandler<WebView, WebViewNavigationCompletedEventArgs> h = null;
            try
            {
                var tcs = new TaskCompletionSource<WebViewNavigationCompletedEventArgs>();
                h = (o, args) => tcs.TrySetResult(args);
                wv.NavigationCompleted += h;
                var winner = await Task.WhenAny(tcs.Task);
                return await tcs.Task;
            }
            finally
            {
                wv.NavigationCompleted -= h;
            }
        }

        private int ParsePagesCount(string previewPageHtml)
        {
            var tilesList = new List<string>();
            string patrn = "<div id=\"thumbs\">(?<val>.*?)</div>";
            string patrn1 = "<a(.*?)></a>";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            Regex r1 = new Regex(patrn1, options);
            Match m = r.Match(previewPageHtml);
            foreach (Match mat in r1.Matches(m.Groups["val"].Value))
            {
                tilesList.Add(mat.Groups[1].Value);
            }
            return tilesList.Count;
        }

        private string ParseImageUrl(string previewPageHtml)
        {
            string patrn = "<div id=\"image\">(?<val>.*?)</div>";
            string patrn2 = "<img src=\"(?<val>.*?)\" style =\"(/*?)\">";
            string patrn1 = "<a href=\"(.*?)\"><img style=\"(.*?)\" src=\"(?<val>.*?)\"></a>";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            Regex r1 = new Regex(patrn1, options);
            Match m = r.Match(previewPageHtml);
            string st = r1.Match(m.Groups["val"].Value).Groups["val"].Value;
            return st;
        }

        public async Task<WriteableBitmap> GetImageFromUrl(string url)
        {
            var bytes = new HttpClient().GetByteArrayAsync(url).Result;
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
    }
}
