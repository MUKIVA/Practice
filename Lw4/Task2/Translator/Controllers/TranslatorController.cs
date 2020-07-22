using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Translator.Controllers
{
    [ApiController]
    [Route( "[controller]" )]
    public class TranslatorController : Controller
    {
        [HttpGet]
        public ActionResult<string> Translate( [FromQuery] string word )
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
    }
}
