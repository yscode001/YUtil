using System;
using YCSharp;

namespace YConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BigNumber a = new BigNumber(0, 1);
            BigNumber b = new BigNumber(1, 1);

            var c = b - a;
            Console.WriteLine(c);
        }
    }
}