using System;

namespace remove_duplicates
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Incorrect number of arguments!\n" + "Usage remove_duplicates.exe <input string>");
            }
            else
            {
                string arg_string = args[0];
                int index = 1;
                while (index < arg_string.Length)
                {
                    string temp = arg_string.Remove(index);
                    arg_string = arg_string.Remove(0, index);
                    arg_string = arg_string.Replace(Convert.ToString(temp[index - 1]), "");
                    arg_string = temp + arg_string;
                    index++;
                }
                Console.WriteLine(arg_string);
            }
        }
    }
}
