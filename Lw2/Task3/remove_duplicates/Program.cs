using System;

namespace remove_duplicates
{
    class Program
    {
        static void Main( string[] args )
        {
            if ( args.Length != 1 )
            {
                Console.WriteLine( "Incorrect number of arguments!\n" + "Usage remove_duplicates.exe <input string>" );
            }
            else
            {
                string argString = args[ 0 ];
                int index = 1;
                while ( index < argString.Length )
                {
                    string temp = argString.Remove( index );
                    argString = argString.Remove( 0, index );
                    argString = argString.Replace( Convert.ToString( temp[ index - 1 ] ), "" );
                    argString = temp + argString;
                    index++;
                }
                Console.WriteLine( argString );
            }
        }
    }
}
