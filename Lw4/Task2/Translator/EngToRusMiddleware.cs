using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Translator
{
    public class EngToRusMiddleware
    {
        private readonly RequestDelegate _next;
        public EngToRusMiddleware( RequestDelegate next )
        {
            _next = next;
        }

        public async Task InvokeAsync( HttpContext context )
        {
            context.Response.ContentType = "text/plain; charset=utf-8";
            bool looking = false;
            if ( string.IsNullOrEmpty( context.Request.Query[ "word" ] ) )
                context.Response.StatusCode = 400;
            else
            {
                string word = context.Request.Query[ "word" ];
                word = word.ToLower();
                looking = true;
                const string TrPath = @"translatorData\data.txt";
                using StreamReader fIn = new StreamReader( TrPath );
                while ( fIn.Peek() != -1 && looking )
                {
                    string[] trPair = fIn.ReadLine().Split( "=" );
                    if ( word == trPair[ 0 ] )
                    {
                        word = trPair[ 1 ];
                        await context.Response.WriteAsync( word );
                        looking = false;
                    }
                }
            }
            if ( looking )
                context.Response.StatusCode = 404;
        }
    }
}
