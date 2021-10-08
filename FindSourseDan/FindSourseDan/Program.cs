using FindSourseDan.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApp // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Find files !D...");
            if (args.Length == 0)
            {
                Console.WriteLine("Not arg!!!");
                return; 
            }
            string path = args[0];
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Not DIRECT!!!!");
                return;
            }
            GraphDir graphDir = new GraphDir(path);
            graphDir.Run();
        }
    }
}
