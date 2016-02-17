using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FreakCat.MangaReader.Model.Entities;

namespace FreakCat.MangaReader.Parsers
{
    public class MangachanInfoParser
    {
        private readonly string _infoUrl;
        public MangachanInfoParser(string infoUrl)
        {
            _infoUrl = infoUrl;
        }

        public async Task<MangaInfo> GetInfoAsync()
        {
            var pageHtml = await GetHtmlPage();
            var match = ParseTableInfo(pageHtml);
            var inf = new MangaInfo()
            { //<font style='color:green;'>перевод завершен</font>
                AnotherNames = match.Groups["anname"].Value,
                Author = ParseAuthors(match.Groups["author"].Value).Replace("<font style=\'color:green;\'>","").Replace("</font>",""),
                DownloadingStatus = match.Groups["dchapt"].Value + match.Groups["dstatus"].Value,
                Status = match.Groups["status"].Value,
                Translater = ParseAuthors(match.Groups["transl"].Value)
            };
            return inf;
        }

        private async Task<string> GetHtmlPage()
        {
            return await new HttpClient().GetStringAsync(_infoUrl);
        }

        private Match ParseTableInfo(string htmlPage)
        {
            string patrn1 = "<tr>(\\W*)<td class=\"item\">Другие названия</td>(\\W*)<td class=\"item2\">(\\W*)<h2>(?<anname>.*?)</h2>(\\W*)</td>(\\W*)</tr>(\\W*)<tr>(\\W*)<td class=\"item\">Тип</td>(\\W*)<td class=\"item2\">(\\W*)<h2>(\\W*)<span class=\"translation\">(\\W*)<a href=\"(.*?)\">(?<type>.*?)</a>(\\W*)</span>(\\W*)</h2>(\\W*)</td>(\\W*)</tr>(\\W*)<tr>(\\W*)<td class=\"item\">Автор</td>(\\W*)<td class=\"item2\">(\\W*)<span class=\"translation\">(?<author>.*?)</span>(\\W*)</td>(\\W*)</tr>(\\W*)<tr>(\\W*)<td class=\"item\">Статус \\(Томов\\)</td>(\\W*)<td class=\"item2\">(\\W*)<h2>(?<status>.*?)</h2>(\\W*)</td>(\\W*)</tr>(\\W*)<tr>(\\W*)<td class=\"item\">Загружено</td>(\\W*)<td class=\"item2\">(\\W*)<h2><b>(?<dchapt>.*?)</b>(?<dstatus>.*?)</h2>(\\W*)</td>(.*?)<td class=\"item\">Переводчики</td>(\\W*)<td class=\"item2\">(\\W*)<span class=\"translation\">(?<transl>.*?)</span>(\\W*)</td>(\\W*)</tr>";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r1 = new Regex(patrn1, options);
            return r1.Match(htmlPage);
        }

        private void GetAll(Match match)
        {
            var another = match.Groups["anname"].Value;
            var type = match.Groups["type"].Value;
            var author = ParseAuthors(match.Groups["author"].Value);
            var status = match.Groups["status"].Value;
            var dstatus = match.Groups["dchapt"].Value + match.Groups["dstatus"].Value;
            var transl = ParseAuthors(match.Groups["transl"].Value);
        }

        private string ParseAuthors(string authorsHtml)
        {
            string patrn = "<a(.*?)>(?<sta>.*?)</a>";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            var res = "";
            foreach (Match math in r.Matches(authorsHtml))
            {
                res += string.Format("{0}, ", math.Groups["sta"].Value);
            }
            
            return string.IsNullOrEmpty(res)?"": res.Remove((res.Length - 4),4);
        }
    }
}
