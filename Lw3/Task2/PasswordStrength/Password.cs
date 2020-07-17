using System;
using System.Text.RegularExpressions;

namespace PasswordStrength
{
    public class Password
    {

        private readonly string _passString;
        public Password( string passString )
        {
            _passString = passString;
        }

        public int getStrength()
        {
            int passStrength = 0;
            int digitNum = new Regex( @"[0-9]" ).Matches( _passString ).Count;
            int upperCaseChNum = new Regex( @"[A-Z]" ).Matches( _passString ).Count;
            int lowerCaseChNum = new Regex( @"[a-z]" ).Matches( _passString ).Count;
            int len = _passString.Length;
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
            passStrength -= duplicatesFine();
            return passStrength;
        }

        private int duplicatesFine()
        {
            string passString = _passString;
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
            return _passString.Length - _passString.Replace( Convert.ToString( _passString[ index ] ), "" ).Length;
        }
    }
}
