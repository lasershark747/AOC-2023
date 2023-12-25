using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace d5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            Dictionary<string,List<string>> graph = new Dictionary<string,List<string>>();
            Random r = new Random();
            List<string> nodes = new List<string>();

            for (int i = 0; i < input.Length; i++)
            {
                string[] split1 = input[i].Split(':');
                List<string> list = new List<string>();
                string[] split2 = split1[1].Split(' ');

                for(int j =1; j < split2.Length; j++)
                {
                    list.Add(split2[j]);
                    if (!nodes.Contains(split2[j])) 
                    {
                        nodes.Add(split2[j]);
                    }
                }
                if (!nodes.Contains(split1[0]))
                {
                    nodes.Add(split1[0]);
                }
                graph.Add(split1[0], list);
            }
            bool[,] matrix = new bool[nodes.Count,nodes.Count];
            int[,] usage = new int[nodes.Count, nodes.Count];

            foreach (string s in graph.Keys)
            {
                foreach (string s2 in graph[s])
                {
                    matrix[nodes.IndexOf(s), nodes.IndexOf(s2)] = true;
                    matrix[nodes.IndexOf(s2), nodes.IndexOf(s)] = true;
                    usage[nodes.IndexOf(s2), nodes.IndexOf(s)] = 0;
                }
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int num = r.Next(1,1001);
                    if (num == 1 && i!=j)
                    {
                        List<string> s = new List<string>();
                        Queue<string> qu = new Queue<string>();
                        qu.Enqueue(nodes[i]);
                        s.Add(nodes[i]);
                        string destionation = nodes[j];
                        bool endLoop = false;
                        while (qu.Count>0)
                        {
                            var node = qu.Dequeue();
                            int index = nodes.IndexOf(node);

                            for (int a = 0; a < matrix.GetLength(1); a++)
                            {
                                if (matrix[index, a] == true && nodes[a] == destionation)
                                {
                                    usage[index, a]++;
                                    endLoop = true;
                                    break;
                                }
                                else if (matrix[index, a] == true && a != index && !s.Contains(nodes[a]))
                                {
                                    s.Add(nodes[a]);
                                    qu.Enqueue(nodes[a]);
                                    usage[index, a]++;
                                }
                            }
                            if (endLoop)
                                break;
                        }
                    }
                }
            }


            (int, int, int) u1 = (0, 0, 0);
            (int, int, int) u2 = (0, 0, 0);
            (int, int, int) u3 = (0, 0, 0);
            
            for (int i = 0; i < usage.GetLength(0); i++)
            {
                for(int j = i ; j < usage.GetLength(1); j++)
                {
                    if (usage[i, j] + usage[j,i] > u1.Item3)
                    {
                        u3 = u2;
                        u2 = u1;
                        u1 = (i, j, usage[i, j] + usage[j, i]);
                    }
                    else if (usage[i, j] + usage[j, i] > u2.Item3)
                    {
                        u3 = u2;
                        u2 = (i, j, usage[i, j] + usage[j, i]);

                    }
                    else if (usage[i, j] + usage[j, i] > u3.Item3)
                    {
                        u3 = (i, j, usage[i, j] + usage[j, i]);
                    }
                }
            }
            
            matrix[u1.Item1, u1.Item2] = false;
            matrix[u2.Item1, u2.Item2] = false;
            matrix[u3.Item1, u3.Item2] = false;
            matrix[u1.Item2, u1.Item1] = false;
            matrix[u2.Item2, u2.Item1] = false;
            matrix[u3.Item2, u3.Item1] = false;

            List<string> seen = new List<string>();
            Queue<string> q = new Queue<string>();
            q.Enqueue(nodes[u1.Item1]);
            seen.Add(nodes[u1.Item1]);
            
            while (q.Count>0)
            {
                var node = q.Dequeue();
                int index = nodes.IndexOf(node);

                for (int a = 0; a < matrix.GetLength(1); a++)
                {
                    if (matrix[index, a] == true && a != index && !seen.Contains(nodes[a]))
                    {
                        seen.Add(nodes[a]);
                        q.Enqueue(nodes[a]);
                    }
                }
            }


            List<string> seen2 = new List<string>();
            Queue<string> q2 = new Queue<string>();
            q2.Enqueue(nodes[u1.Item2]);
            seen2.Add(nodes[u1.Item2]);
            
            while (q2.Count>0)
            {
                var node = q2.Dequeue();
                int index = nodes.IndexOf(node);

                for (int a = 0; a < matrix.GetLength(1); a++)
                {
                    if (matrix[index, a] == true && a != index && !seen2.Contains(nodes[a]))
                    {
                        seen2.Add(nodes[a]);
                        q2.Enqueue(nodes[a]);
                    }
                }
            }

            Console.WriteLine(seen.Count*seen2.Count);
            Console.ReadKey();
        }

       

    }
}
