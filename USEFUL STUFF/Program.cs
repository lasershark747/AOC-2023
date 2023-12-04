using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USEFUL_STUFF
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }


        public static int BFS((int, int) start, (int, int) end, List<List<char>> grid)
        {
            int distance = 0;
            Queue<((int, int), int)> queue = new Queue<((int, int), int)>();
            List<(int, int)> visited = new List<(int, int)>();

            queue.Enqueue(((start.Item1, start.Item2), 0));
            grid[start.Item1][start.Item2] = 'a';

            while (true)
            {
                ((int, int), int) node = ((0, 0), 0);
                if (queue.Count == 0)
                {
                    break;
                }
                else
                {
                    node = queue.Dequeue();
                    distance = node.Item2;
                }

                if (node.Item1.Item1 + 1 < grid.Count)
                {
                    if (grid[node.Item1.Item1][node.Item1.Item2] + 1 >= grid[node.Item1.Item1 + 1][node.Item1.Item2] && !visited.Contains((node.Item1.Item1 + 1, node.Item1.Item2)))
                    {
                        if (grid[node.Item1.Item1 + 1][node.Item1.Item2] == '{')
                        {
                            return node.Item2 + 1;
                        }
                        else
                        {
                            queue.Enqueue(((node.Item1.Item1 + 1, node.Item1.Item2), node.Item2 + 1));
                            visited.Add((node.Item1.Item1 + 1, node.Item1.Item2));
                        }
                    }
                }

                if (node.Item1.Item1 - 1 >= 0)
                {
                    if (grid[node.Item1.Item1][node.Item1.Item2] + 1 >= grid[node.Item1.Item1 - 1][node.Item1.Item2] && !visited.Contains((node.Item1.Item1 - 1, node.Item1.Item2)))
                    {
                        if (grid[node.Item1.Item1 - 1][node.Item1.Item2] == '{')
                        {
                            return node.Item2 + 1;
                        }
                        else
                        {
                            queue.Enqueue(((node.Item1.Item1 - 1, node.Item1.Item2), node.Item2 + 1));
                            visited.Add((node.Item1.Item1 - 1, node.Item1.Item2));

                        }
                    }
                }
                
                if (node.Item1.Item2 + 1 < grid[0].Count)
                {
                    if (grid[node.Item1.Item1][node.Item1.Item2] + 1 >= grid[node.Item1.Item1][node.Item1.Item2 + 1] && !visited.Contains((node.Item1.Item1, node.Item1.Item2 + 1)))
                    {
                        if (grid[node.Item1.Item1][node.Item1.Item2 + 1] == '{')
                        {
                            return node.Item2 + 1;
                        }
                        else
                        {
                            queue.Enqueue(((node.Item1.Item1, node.Item1.Item2 + 1), node.Item2 + 1));
                            visited.Add((node.Item1.Item1, node.Item1.Item2 + 1));
                        }
                    }
                }

                if (node.Item1.Item2 - 1 >= 0)
                {
                    if (grid[node.Item1.Item1][node.Item1.Item2] + 1 >= grid[node.Item1.Item1][node.Item1.Item2 - 1] && !visited.Contains((node.Item1.Item1, node.Item1.Item2 - 1)))
                    {
                        if (grid[node.Item1.Item1][node.Item1.Item2 - 1] == '{')
                        {
                            return node.Item2 + 1;
                        }
                        else
                        {
                            queue.Enqueue(((node.Item1.Item1, node.Item1.Item2 - 1), node.Item2 + 1));
                            visited.Add((node.Item1.Item1, node.Item1.Item2 - 1));
                        }
                    }
                }
            }
            return 99999;
        }
    }
}
