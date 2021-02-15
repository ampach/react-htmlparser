using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using HtmlParser.Core.Models;

namespace HtmlParser.Core.Services
{
    public class ParserService : IParserService
    {
        public async Task<Result> ProcessUrlAsync(string url)
        {
            try
            {
                var result = new Result();
                var pageUrl = HttpUtility.UrlDecode(url);
                var htmlString = await GetHtmlByUrl(pageUrl);

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlString);

                result.Imagaes = GetImagesFromHtml(htmlDoc, pageUrl);

                var allWords = GetAllWords(htmlDoc);

                if (allWords != null && allWords.Any())
                {
                    result.NumberOfWords = allWords.Count;
                    result.Occurrences = GetOccurrences(allWords);
                }
                

                return result;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to parse HTML", e);
            }
        }

        private List<string> GetAllWords(HtmlDocument doc)
        {
            char[] delimiter = { ' ', ',', '.', '!', '?', ':', ';', '\'', '"' };

            var allWords = new List<string>();

            var nodes = doc.DocumentNode.SelectNodes("//body//text()[not(parent::script)]");
            if (nodes == null) return allWords;

            foreach (var text in nodes.Select(node => node.InnerText))
            {
                var words = text.Split(delimiter, StringSplitOptions.RemoveEmptyEntries)
                    .Where(s => Char.IsLetter(s[0])).ToList();

                if (words.Count > 0)
                {
                    allWords.AddRange(words);
                }
            }

            return allWords;

        }

        private Occurrency[] GetOccurrences(List<string> allWords)
        {
            return allWords
                .GroupBy(g => g.ToLower())
                .OrderByDescending(q => q.Count())
                .Take(10)
                .Select(q => new Occurrency { Key = q.Key, Count = q.Count() })
                .ToArray();
        }

        private List<ImageModel> GetImagesFromHtml(HtmlDocument doc, string pageUrl)
        {
            var uri = new Uri(pageUrl);
            var host = uri.Scheme + Uri.SchemeDelimiter + uri.Host;

            var images = doc.DocumentNode.SelectNodes("//img");

            if (images == null)
                return new List<ImageModel>();

            return images.Where(q => q != null && q.Attributes["src"] != null).Select(q => new ImageModel
                {
                    Src = EnsureSrc(q.GetAttributeValue("src",""), host), 
                    Alt = q.GetAttributeValue("alt", "")
                }).ToList();
        }

        private async Task<string> GetHtmlByUrl(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                var pageContents = await response.Content.ReadAsStringAsync();

                return pageContents;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to load page by URL", e);
            }

        }

        string EnsureSrc(string url, string host)
        {
            if (string.IsNullOrWhiteSpace(url) || IsAbsoluteUrl(url))
                return url;

            return host + url;
        }

        bool IsAbsoluteUrl(string url)
        {
            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute, out result);
        }

    }
}
