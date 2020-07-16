using System;
using System.IO;

namespace RemoveExtraBlanks
{
    class Program
    {
        static void Main( string[] args )
        {
            if ( args.Length != 2 )
            {
                Console.WriteLine( "Incorrect number of arguments!" );
                Environment.Exit(0);
            }
            string pathInput = @"data/" + args[ 0 ] + ".txt";
            string pathOutput = @"data/" + args[ 1 ] + ".txt";
            if ( File.Exists( pathInput ) && File.Exists( pathOutput ) )
            {
                using StreamReader fIn = new StreamReader( pathInput );
                using StreamWriter fOut = new StreamWriter( pathOutput, false );
                string temp = "";
                while ( fIn.Peek() != -1 )
                {
                    temp = fIn.ReadLine();
                    temp = System.Text.RegularExpressions.Regex.Replace( temp, @"\s+", " " ).Trim();
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
