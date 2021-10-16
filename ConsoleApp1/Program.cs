using MarkovEncode;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new MarkovCrypter().Decrypt("25b"));
        }
    }
}
