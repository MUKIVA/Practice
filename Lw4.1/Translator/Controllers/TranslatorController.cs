using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Translator.Controllers
{
    [ApiController]
    [Route( "[controller]/[action]" )]
    public class TranslatorController : Controller
    {
        [HttpGet]
        [ActionName( "en-ru" )]
        public ActionResult<string> EnRu( [FromQuery] string word )
        {
            if ( string.IsNullOrEmpty( word ) )
                return BadRequest();
            else
            {
                word = word.ToLower();
                const string TrPath = @"translatorData\data.txt";
                using StreamReader fIn = new StreamReader( TrPath );
                while ( fIn.Peek() != -1 )
                {
                    string[] trPair = fIn.ReadLine().Split( "=" );
                    if ( word == trPair[ 0 ] )
                    {
                        return Ok( trPair[ 1 ] );
                    }
                }
            }
            return NotFound();
        }

        [HttpGet]
        [ActionName( "ru-en" )]
        public ActionResult<string> RuEn( [FromQuery] string word )
        {
            if ( string.IsNullOrEmpty( word ) )
                return BadRequest();
            else
            {
                word = word.ToLower();
                const string TrPath = @"translatorData\data.txt";
                using StreamReader fIn = new StreamReader( TrPath );
                while ( fIn.Peek() != -1 )
                {
                    string[] trPair = fIn.ReadLine().Split( "=" );
                    if ( word == trPair[ 1 ] )
                    {
                        return Ok( trPair[ 0 ] );
                    }
                }
            }
            return NotFound();
        }
    }
}
