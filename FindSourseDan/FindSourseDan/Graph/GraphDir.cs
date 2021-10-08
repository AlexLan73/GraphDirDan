using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindSourseDan.Graph
{
    //  Вершина
    internal class Vertex
    {
        public ConcurrentDictionary<string, bool> Vertexs { get; set; }
        public string PathSourse { get; set; }            
        public bool Is { get; set; }
        public Vertex(string path)
        {
            Is = false;
            PathSourse = path;
            Vertexs = new ConcurrentDictionary<string, bool>();
            Vertexs.AddOrUpdate(PathSourse, false, (x, y) => false);

            while (Vertexs.Count > 0)
            {
                var _dirx= Vertexs.Keys.ToList();
                foreach (var v in _dirx)
                {
                    var _dir = Directory.GetDirectories(v);
                    Vertexs.TryRemove(v, out _);

                    if (_dir.Count() > 0)
                    {
                        var _dirD = _dir.Count(x => x.ToLower().Contains("!d"));
                        if (_dirD > 0)
                        {
                            SaveNameFile.Add(v);
                            continue;
                        }

                        var _dirClf = _dir.FirstOrDefault(x => x.ToLower().Contains("clf"));
                        if (_dirClf != null)
                            continue;

                        foreach (var v2 in _dir)
                            Vertexs.AddOrUpdate(v2, false, (x, y) => false);
                    }
                }
            }

//            Run();
        }
        public void Run()
        {

        }

    }

    // ребро
    internal class Edge
    {
    }


    internal class GraphDir
    {
        private string _path;
        private ConcurrentDictionary<string, Vertex> _root;

        public GraphDir(string path)
        {
            _path = path;
            _root = new ConcurrentDictionary<string, Vertex>();
        }

        public void Run()
        {
            if (!Directory.Exists(_path))
                return;

            string [] notDir = new []{"#common", "dll", "_bin", "_gsdata_" };
            Func<string[], string, bool> predicate0 = (sm, s) =>
            {
                var x0 = sm.FirstOrDefault(z=> s.ToLower().Contains(z));
                return x0 == null ? true: x0.Length <= 0;
            };

            var _dir = Directory.GetDirectories(_path)
                .Where(x => predicate0(notDir, x)==true)
                .ToList();
            
            List<Task> _tastPaths = new List<Task>();

            foreach (var dirxx in _dir)
            {
                _tastPaths.Add(Task.Factory.StartNew(() => { new Vertex(dirxx); }));
                var _status = _tastPaths.ElementAt(_tastPaths.Count - 1);
                Console.WriteLine($" ! find !D... in dit-> {dirxx} ");
                while (_status.Status != TaskStatus.Running) { }
            }

            Console.WriteLine(" WAIT...... ");

            while (_tastPaths.Count>0)
            {
                Console.WriteLine($" count Task->  {_tastPaths.Count}");
                _tastPaths.ElementAt(0).Wait();
                try
                {
                    _tastPaths.RemoveAt(0);
                }
                catch (Exception)
                {
                }
            }
            Console.WriteLine($" Save  !!!sourse.txt in  {_path} ");

            SaveNameFile.WriteToFile(_path);

        }

    }
}
