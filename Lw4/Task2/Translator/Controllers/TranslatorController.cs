using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Translator.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class TranslatorController : Controller
    {
        [HttpGet]
        public async Task Translate()
        {
            HttpContext.Response.ContentType = "text/plain; charset=utf-8";
            bool looking = false;
            if ( string.IsNullOrEmpty( HttpContext.Request.Query[ "word" ] ) )
                HttpContext.Response.StatusCode = 400;
            else
            {
                string word = HttpContext.Request.Query[ "word" ];
                word = word.ToLower();
                looking = true;
                const string TrPath = @"translatorData\data.txt";
                using StreamReader fIn = new StreamReader( TrPath );
                while ( fIn.Peek() != -1 && looking )
                {
                    string[] trPair = fIn.ReadLine().Split( "=" );
                    if ( word == trPair[ 0 ] )
                    {
                        await HttpContext.Response.WriteAsync( trPair[ 1 ] );
                        looking = false;
                    }
                }
            }
            if ( looking )
                HttpContext.Response.StatusCode = 404;
        }
    }
}
