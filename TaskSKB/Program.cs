using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskSKB
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = new Task();

            var str = Console.ReadLine();

            Console.WriteLine(str);

            int a;

            if (Int32.TryParse(str, out a))
                Console.WriteLine(a.ToString());
            else
                Console.WriteLine("fail parse");
        }
    }
}
