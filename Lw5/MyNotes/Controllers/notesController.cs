using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Collections.Generic;
using MyNotes.Models;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace MyNotes.Controllers
{
    [ApiController]
    [Route( "[controller]/[action]" )]
    public class NotesController : Controller
    {
        readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create( UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic )
        };
        const string PathDb = "Data/db.json";


        [HttpPost]
        [ActionName( "Add" )]
        public async Task<ActionResult<Note>> Add( Note note )
        {
            if ( note == null )
            {
                return BadRequest();
            }
            List<Note> db = new List<Note>();
            using ( FileStream fs = new FileStream( PathDb, FileMode.OpenOrCreate ) )
            {
                try
                {
                    db = await JsonSerializer.DeserializeAsync<List<Note>>( fs );
                    fs.Seek( 0, SeekOrigin.Begin );
                    db.Add( note );
                    await JsonSerializer.SerializeAsync<List<Note>>( fs, db, _options );
                }
                catch
                {
                    db.Add( note );
                    await JsonSerializer.SerializeAsync<List<Note>>( fs, db, _options );
                }
            }
            return Ok( note );
        }

        [HttpGet]
        [ActionName( "list" )]
        public async Task<ActionResult<Note>> Get()
        {
            List<Note> note = new List<Note>();
            using ( FileStream fs = new FileStream( PathDb, FileMode.Open ) )
            {
                note = await JsonSerializer.DeserializeAsync<List<Note>>( fs );
            }
            return Ok( note );
        }
    }
}
