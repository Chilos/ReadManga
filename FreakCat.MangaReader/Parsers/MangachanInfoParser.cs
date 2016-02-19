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
                Translater = ParseAuthors(match.Groups["transl"].Value),
                Tags = ParseTags(pageHtml),
                Description = ParseDescription(pageHtml),
                Chapters = ParseChapters(pageHtml),
                ReadUrl = ParseReadUrl(pageHtml)
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

        private ObservableCollection<Tag> ParseTags(string pageHtml)
        {
            string patrn = "<li class='sidetag'>(?<name>.*?)</li>";
            string patrn1 = "<a href='(?<url>.*?)'>(?<name>.*?)</a>";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            var res = new ObservableCollection<Tag>();
            foreach (Match math in r.Matches(pageHtml))
            {
                Regex r1 = new Regex(patrn1, options);
                foreach (Match st in r1.Matches(math.Groups["name"].Value))
                {
                    if(st.Groups["name"].Value =="+"|| st.Groups["name"].Value == "-")
                        continue;
                    res.Add(new Tag() { Name = st.Groups["name"].Value, Url = st.Groups["url"].Value });
                }
            }
            return res;
        }

        private string ParseDescription(string pageHtml)
        {
            string patrn = "<div id=\"description\" style=\"(.*?)\">(?<name>.*?)<div";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            return r.Match(pageHtml).Groups["name"].Value;
        }

        private ObservableCollection<Chapter> ParseChapters(string pageHtml)
        {
            string patrn = "<td>(\\W*)<div class='manga'>(\\W*)<a href='(?<url>.*?)'(.*?)>(?<name>.*?)</a>(\\W*)</div>(\\W*)</td>(\\W*)<td>(\\W*)<div class='date'>(?<date>.*?)</div>(\\W*)</td>(\\W*)</tr>";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            var res = new ObservableCollection<Chapter>();
            foreach (Match math in r.Matches(pageHtml))
            {
                //Girl the Wild's &nbsp;&nbsp;v2 - 219 &nbsp;&nbsp;</span>
                var st = math.Groups["name"].Value;
                st = st.Replace("&nbsp;&nbsp;", " ");
                st = st.Replace("</span>", " ");
                res.Add(new Chapter() {Name = st, Date = math.Groups["date"].Value, Url = math.Groups["url"].Value});
            }
            return res;
        }
        //<a class="" href="/online/146402-red-storm_v1_ch0.html" title="Читать Red Storm онлайн">Читать онлайн</a>
        private string ParseReadUrl(string pageHtml)
        {
            string patrn = "<a class=\"\" href=\"(?<url>.*?)\" title=\"Читать(.*?)онлайн\">";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            return r.Match(pageHtml).Groups["url"].Value;
        }
    }
}
