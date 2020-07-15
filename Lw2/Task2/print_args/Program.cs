using System;

namespace print_args
{
    class Program
    {
        static void Main( string[] args )
        {
            int countArgs = args.Length;
            if ( countArgs == 0 )
            {
                Console.WriteLine( "No parameters were specified!" );
            }
            else
            {
                Console.WriteLine( "Number of arguments: " + countArgs );
                Console.Write( "Arguments: " );
                foreach ( string arg in args )
                {
                    Console.Write( arg + " " );
                }
                Console.WriteLine();
            }
        }
    }
}
