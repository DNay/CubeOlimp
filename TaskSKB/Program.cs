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
            //int v = task.RecognizeType("acfg..");
            task.GetRotateMap("abcdef");
            
            for (int i = 0; i < task.RotateMap.Count; i++)
            {
                var l = task.RotateMap[i];
                Console.WriteLine(l);
            }
            Console.ReadLine();
        }
    }
}
