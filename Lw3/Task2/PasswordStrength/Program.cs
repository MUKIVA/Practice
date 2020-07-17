using System;
using System.Text.RegularExpressions;

namespace PasswordStrength
{
    class Program
    {
        static void Main( string[] args )
        {
            if ( args.Length != 1 )
            {
                Console.WriteLine( "Incorrect number of arguments!" );
                Environment.Exit( 0 );
            }
            Regex reg = new Regex( @"^[A-Za-z0-9]*$" );
            if ( reg.IsMatch( args[ 0 ] ) )
            {
                Password pass = new Password( args[ 0 ] );
                Console.WriteLine( "Password strenght: " + pass.getStrength() );
            }
            else
            {
                Console.WriteLine( "Password contains invalid characters" );
                Environment.Exit( 0 );
            }
        }
    }
}
