using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FreakCat.MangaReader.Model.Entities;

namespace FreakCat.MangaReader.Parsers
{
    class MangachanPageParser
    {
        private const string SITE_URL = @"http://mangachan.ru";
        private string _mangaImageUrl;

        public MangachanPageParser(string mangaUrl)
        {
            _mangaImageUrl = SITE_URL + mangaUrl +"#page=1";
        }

        public async void GetMangaImagesAsync(ObservableCollection<MangaImage> mangaImages)
        {
            string previewPageHtml = await GetHtmlPage();
            int count = ParsePagesCount(previewPageHtml);

        }

        private async Task<string> GetHtmlPage()
        {
            return await new HttpClient().GetStringAsync(_mangaImageUrl);
        }

        private int ParsePagesCount(string previewPageHtml)
        {
            var tilesList = new List<string>();
            string patrn = "webp";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            foreach (Match mat in r.Matches(previewPageHtml))
            {
                tilesList.Add(mat.Groups["val"].Value);
            }
            return tilesList.Count;
        }
    }
}
