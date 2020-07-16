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
            Password pass = new Password( args[ 0 ] );
            Console.WriteLine( "Password strenght: " + pass.getStrength() );
        }
    }

    class Password
    {
        protected int passStrength = 0;
        protected int digitNum = 0;
        protected int upperCaseChNum = 0;
        protected int lowerCaseChNum = 0;
        protected string passString = "";
        public Password( string passString )
        {
            Regex reg = new Regex( @"^[A-Za-z0-9]*$" );
            if ( reg.IsMatch( passString ) )
            {
                this.passString = passString;
            }
            else
            {
                Console.WriteLine( "Password contains invalid characters" );
                Environment.Exit( 0 );
            }
        }

        public int getStrength()
        {
            passStrength = 0;
            digitNum = new Regex( @"[0-9]" ).Matches( passString ).Count;
            upperCaseChNum = new Regex( @"[A-Z]" ).Matches( passString ).Count;
            lowerCaseChNum = new Regex( @"[a-z]" ).Matches( passString ).Count;
            int len = passString.Length;
            passStrength += ( 4 * len );
            passStrength += ( 4 * digitNum );
            if ( upperCaseChNum != 0 )
                passStrength += ( ( len - upperCaseChNum ) * 2 );
            if ( lowerCaseChNum != 0 )
                passStrength += ( ( len - lowerCaseChNum ) * 2 );
            if ( digitNum == 0 )
                passStrength -= len;
            if ( upperCaseChNum == 0 && lowerCaseChNum == 0 )
                passStrength -= len;
            passStrength -= duplicatesFine( passString );
            return passStrength;
        }

        private int duplicatesFine( string passString )
        {
            int index = 1;
            int fine = 0;
            while ( index < passString.Length )
            {
                string temp = passString.Remove( index );
                if ( getDuplicateCount( index - 1 ) > 1 )
                {
                    fine += getDuplicateCount( index - 1 );
                }
                passString = passString.Remove( 0, index );
                passString = passString.Replace( Convert.ToString( temp[ index - 1 ] ), "" );
                passString = temp + passString;
                index++;
            }
            return fine;
        }

        private int getDuplicateCount( int index )
        {
            return passString.Length - passString.Replace( Convert.ToString( passString[ index ] ), "" ).Length;
        }
    }
}
