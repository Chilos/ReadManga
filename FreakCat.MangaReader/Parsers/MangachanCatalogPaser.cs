using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using FreakCat.MangaReader.Model.Entities;

namespace FreakCat.MangaReader.Parsers
{
    class MangachanCatalogPaser
    {
        private const string CATALOG_URL = @"http://mangachan.ru/mostfavorites";
        private const string SITE_URL = @"http://mangachan.ru";


        public async void GetCatalogAsync(ObservableCollection<TileInfo> collection)
        {
            var str = await GetHtmlPage();
            var list = ParseHtmlContent(str);
            foreach (var element in list)
            {
                var ct = await GetCatalogTile(element);
                collection.Add(ct);
            }
        }

        private async Task<string> GetHtmlPage()
        {
            return await new HttpClient().GetStringAsync(CATALOG_URL);
        }

        private List<string> ParseHtmlContent(string htmlPage)
        {            
            var tilesList = new List<string>();
            string patrn = "<div class=\"content_row\"(.*?)>(?<val>.*?)<div class=\"line_break\">";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            foreach (Match mat in r.Matches(htmlPage))
            {
                tilesList.Add(mat.Groups["val"].Value);
            }
            return tilesList.Count == 0 ? null : tilesList;
        }

        private async Task<TileInfo> GetCatalogTile(string htmlTile)
        {
            return new TileInfo()
            {
                EnName = GetTileName(ParseTileBigName(htmlTile)),
                RusName = GetTileRusName(ParseTileBigName(htmlTile)),
                IsEnded = IsEndedStatus(GetStatusesHtml(htmlTile)),
                IsSingle = IsSingleStatus(GetStatusesHtml(htmlTile)),
                ChapterCount = Convert.ToInt32(GetChapterCount(GetStatusesHtml(htmlTile))),
                Image = await GetImage(ParseImageUrl(htmlTile)),
                UrlToInfo = ParseUrl(htmlTile)
            };
        }

        private Match ParsNames(string bigName)
        {
            string patrn = "(?<nam>.*?)\\((?<rus>.*?)\\)";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            return r.Match(bigName);
        }
        private string GetTileName(string bigName)
        {
            return ParsNames(bigName)?.Groups["nam"]?.Value;
        }

        private string GetTileRusName(string bigName)
        {
            return ParsNames(bigName)?.Groups["rus"]?.Value;
        }

        private string ParseTileBigName(string htmlTile)
        {
            var tilesList = new List<string>();
            string patrn = "class=\"title_link\">(?<val>.*?)</a>";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            foreach (Match mat in r.Matches(htmlTile))
            {
                tilesList.Add(mat.Groups["val"].Value);
            }
            return tilesList.Count == 0 ? null : tilesList[0];
        }

        private string GetStatusesHtml(string htmlTile)
        {
            string patrn = "<div class=\"manga_row3\">(?<sta>.*?)</div>(\\W*)</div>";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            return r.Match(htmlTile).Groups["sta"].Value;
        }
        private string ParseImageUrl(string htmlTile)
        {
            string patrn = "<img(\\W*)src=\"(.*?)\"";
            Regex r = new Regex(patrn, RegexOptions.Multiline);
            return SITE_URL+r.Match(htmlTile).Groups[2].Value;
        }

        private string ParseUrl(string htmlTile)
        {
            //<a href="/manga/17625--okazaki-mari.html">
            string patrn = "<a href=\"(.*?)\">";
            Regex r = new Regex(patrn, RegexOptions.Multiline);
            return SITE_URL + r.Match(htmlTile).Groups[1].Value;
        }
        private async Task<BitmapImage> GetImage(string url)
        {
            var b = await GetImageByteAsync(url);
            var s = await ConvertTo(b);
            var ii = new BitmapImage();
            ii.SetSource(s);
            return ii;
        }

        private async Task<InMemoryRandomAccessStream> ConvertTo(byte[] arr)
        {
            InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
            await randomAccessStream.WriteAsync(arr.AsBuffer());
            randomAccessStream.Seek(0);
            return randomAccessStream;
        }

        private static async Task<byte[]> GetImageByteAsync(string url)
        {
            return await new HttpClient().GetByteArrayAsync(url);
        }
        private bool IsEndedStatus(string htmlStatus)
        {
            string patrn = "перевод завершен";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            return r.IsMatch(htmlStatus);
        }

        private bool IsSingleStatus(string htmlStatus)
        {
            string patrn = "Сингл";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            return r.IsMatch(htmlStatus);
        }

        private string GetChapterCount(string htmlStatus)
        {
            string patrn = "<b>(?<ch>\\d*)(.*?)</b>";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            return r.Match(htmlStatus).Groups["ch"].Value;
        }
    }
}
