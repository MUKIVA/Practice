using System;

namespace print_args
{
    class Program
    {
        static void Main(string[] args)
        {
            int count_args = args.Length;
            if (count_args == 0)
            {
                Console.WriteLine("No parameters were specified!");
            }
            else
            {
                Console.WriteLine("Number of arguments: " + count_args);
                Console.Write("Arguments: ");
                foreach (var arg in args)
                {
                    if (arg != null)
                    {
                        Console.Write(arg + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
