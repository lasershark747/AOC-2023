using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace d5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string[] input = File.ReadAllLines("input.txt");
            char[,] grid = new char[input.Length, input[0].Length];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i, j] = input[i][j];
                }
            }

            var start = (0, 1);
            var end = (input.Length - 1, input[0].Length - 2);
            Console.WriteLine(end);
            (int, int)[] directions = { (0, -1), (0, 1), (-1, 0), (1, 0) };

            List<(int, int)> crossroads = new List<(int, int)>
            {
                start, end
            };

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    int neighbours = 0;
                    if (grid[i, j] != '#')
                    {
                        foreach (var d in directions)
                        {
                            if (i + d.Item1 >= 0 && i + d.Item1 < grid.GetLength(0) && j + d.Item2 >= 0 && j + d.Item2 < grid.GetLength(1) && grid[i + d.Item1, j + d.Item2] != '#')
                            {
                                neighbours++;
                            }

                        }
                    }
                    if (neighbours >= 3)
                    {
                        crossroads.Add((i, j));
                    }
                }
            }

            Dictionary<(int, int), List<(int, int, int)>> graph = new Dictionary<(int, int), List<(int, int, int)>>(); // (node), List<(neighbour, distance)> --> adjacency list 


            foreach (var node in crossroads)
            {
                Queue<(int, int, int)> queue = new Queue<(int, int, int)>();
                List<(int, int)> seen = new List<(int, int)>
                {
                    node
                };
                List<(int, int, int)> neighbours = new List<(int, int, int)>();
                queue.Enqueue((node.Item1, node.Item2, 0));

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    int i = current.Item1;
                    int j = current.Item2;
                    foreach (var d in directions)
                    {
                        if (i + d.Item1 >= 0 && i + d.Item1 < grid.GetLength(0) && j + d.Item2 >= 0 && j + d.Item2 < grid.GetLength(1) && grid[i + d.Item1, j + d.Item2] != '#' && !seen.Contains((i + d.Item1, j + d.Item2)))
                        {
                            if (crossroads.Contains((i + d.Item1, j + d.Item2)))
                            {
                                neighbours.Add((i + d.Item1, j + d.Item2, current.Item3 + 1));
                                seen.Add((i + d.Item1, j + d.Item2));
                            }
                            else 
                            {
                                queue.Enqueue((i + d.Item1, j + d.Item2, current.Item3 + 1));
                                seen.Add((i + d.Item1, j + d.Item2));
                            }
                        }

                    }
                }
                graph.Add(node, neighbours);
            }


            List<(int,int)> list = new List<(int, int)>
            {
                start
            };
            Console.WriteLine(DFS(start, 0, list, graph));
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            Console.ReadKey();
        }

        static int DFS((int,int) node,int distance, List<(int,int)> seen, Dictionary<(int, int), List<(int, int, int)>> graph)
        {
            if(node == (140,139))
            {
                return distance;
            }

            List<int> distances = new List<int>();
            distances.Add(0);
            seen.Add(node);
            foreach(var n in graph[node])
            {
                if (!seen.Contains((n.Item1, n.Item2)))
                {
                    distances.Add(DFS((n.Item1, n.Item2),distance+n.Item3,seen,graph));
                }
            }
            seen.Remove(node);


            distances.Sort();
            return distances[distances.Count-1];
        }
    }
}
