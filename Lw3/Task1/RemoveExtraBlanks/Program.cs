using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RemoveExtraBlanks
{
    class Program
    {
        static void Main( string[] args )
        {
            if ( args.Length != 2 )
            {
                Console.WriteLine( "Incorrect number of arguments!" );
                Environment.Exit( 0 );
            }
            if ( !( new Regex( @".+(\.txt)$" ).IsMatch( args[ 0 ] ) ) || !( new Regex( @".+(\.txt)$" ).IsMatch( args[ 1 ] ) ) )
            {
                Console.WriteLine( "One of the specified files is not in the correct format\nPlease use files with \".txt\" extension" );
                Environment.Exit( 0 );
            }
            string pathInput = @"data/" + args[ 0 ];
            string pathOutput = @"data/" + args[ 1 ];
            if ( File.Exists( pathInput ) && File.Exists( pathOutput ) )
            {
                using StreamReader fIn = new StreamReader( pathInput );
                using StreamWriter fOut = new StreamWriter( pathOutput, false );
                string temp = "";
                while ( fIn.Peek() != -1 )
                {
                    temp = fIn.ReadLine();
                    temp = new Regex( @" {2,}" ).Replace( temp, " " ).Trim();
                    temp = new Regex( @"	{2,}" ).Replace( temp, "	" ).Trim();
                    fOut.WriteLine( temp );
                }
                Console.WriteLine( "Success" );
            }
            else
            {
                Console.WriteLine( "One of the specified files is missing\n" + "Please create a text file with this name in the data directory" );
            }
        }
    }
}
