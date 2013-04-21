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
            int v = task.RecognizeType("acfg..");
            Console.WriteLine(v);
            Console.ReadLine();

        }
    }
}
