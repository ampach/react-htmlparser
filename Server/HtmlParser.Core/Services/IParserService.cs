using System.Threading.Tasks;
using HtmlParser.Core.Models;

namespace HtmlParser.Core.Services
{
    public interface IParserService
    {
        Task<Result> ProcessUrlAsync(string url);
    }
}