using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlParser.Core;
using HtmlParser.Core.Models;
using HtmlParser.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HtmlParser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HtmlParserController : ControllerBase
    {

        private readonly ILogger<HtmlParserController> _logger;
        private readonly IParserService _parserService;

        public HtmlParserController(ILogger<HtmlParserController> logger, IParserService parserService)
        {
            _logger = logger;
            _parserService = parserService;
        }

        /// <summary>
        /// GET Student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("LoadUrl")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<Result> LoadUrl(string url)
        {
            try
            {
                var results = _parserService.ProcessUrlAsync(url);
                return results;

            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                return null;
            }
            
        }
    }
}
