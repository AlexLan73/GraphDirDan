using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSourseDan.Graph
{
    internal static class SaveNameFile
    {
        static private ConcurrentQueue<string> QueueNameFiles = new ConcurrentQueue<string>();
        static public void Add(string path)=>QueueNameFiles.Enqueue(path);

        static public void WriteToFile(string fileName)
        {
            string [] s =QueueNameFiles.ToArray();
            QueueNameFiles.Clear();
            File.WriteAllLines(fileName+ "\\!!!sourse.txt", s);


        }

    }
}
