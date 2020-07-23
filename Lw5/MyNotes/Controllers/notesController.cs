using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
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
    public class notesController : Controller
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
            using ( FileStream fs = new FileStream( PathDb, FileMode.Open ) )
            {
                try
                {
                    db = await JsonSerializer.DeserializeAsync<List<Note>>( fs );
                }
                catch ( Exception e )
                {
                    Console.WriteLine( e );
                }
            }
            using ( FileStream fs = new FileStream( PathDb, FileMode.Open ) )
            {
                db.Add( note );
                await JsonSerializer.SerializeAsync<List<Note>>( fs, db, _options );
            }
            return Ok( note );
        }

        [HttpGet]
        [ActionName( "list HTTP/1.1" )]
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
