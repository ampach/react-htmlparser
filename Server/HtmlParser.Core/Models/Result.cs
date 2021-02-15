using System.Collections.Generic;

namespace HtmlParser.Core.Models
{
    public class Result
    {
        public List<ImageModel> Imagaes { get; set; }
        public int NumberOfWords { get; set; } = 0;
        public Occurrency[] Occurrences { get; set; }
    }
}