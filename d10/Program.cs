using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace d5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            string[] input2 = File.ReadAllLines("p2.txt");
            long count = 0;
            int area = 0;
            char[,] grid = new char[input.Length,input[0].Length];
            for (int i = 0; i < input.Length; i++)
            {
                for(int j = 0; j < input[i].Length; j++)
                {
                    grid[i,j] = input[i][j];
                }
            }

            char[,] grid2 = new char[input.Length, input[0].Length];
            for (int i = 0; i < input2.Length; i++)
            {
                for (int j = 0; j < input2[i].Length; j++)
                {
                    grid2[i, j] = input2[i][j];

                }
            }

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 'S')
                    {
                        count = traceLoop(i, j, grid);
                    }
                }
            }


            area = trace(0, 0, grid2);
            Console.WriteLine("p1: " + count);
            Console.WriteLine("p2: " + area);
            
            Console.ReadKey();
        }


        static int traceLoop(int i, int j, char[,] grid)
        {
            
            List<(int, int)> visited = new List<(int, int)>
            {
                (i, j),
            };

            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((i, j));

            while (true)
            {
                (int, int) node = queue.Dequeue();
                findNext(node.Item1, node.Item2, grid, ref visited, ref queue);
                if (queue.Count == 0) break;
            }

            char[,] grid2 = new char[grid.GetLength(0), grid.GetLength(1)];
            for (int a = 0; a < grid.GetLength(0); a++)
            {
                for (int b = 0; b < grid.GetLength(1); b++)
                {
                    if (!visited.Contains((a, b)))
                    {
                        grid2[a, b] = '.';
                    }
                    else
                    {
                        grid2[a, b] = grid[a,b];
                    }
                   
                }
            }

            return visited.Count / 2;
        }
        static int trace(int i, int j, char[,] grid)
        {
            int area = 0;

            List<char> pipeParts = new List<char>
            {
                '|', 'J', 'L','-', 'F', '7'
            };

            for (int a = 0; a < grid.GetLength(0); a++)
            {
                bool inside = false;
                char pipeDirection = 'b';

                for (int b = 0; b < grid.GetLength(1); b++)
                {
                    if (grid[a, b] == '|')
                    {
                        inside = !inside;
                    }
                    else if (grid[a, b] == '-')
                    {

                    }
                    else if (grid[a, b] == 'L')
                    {
                        pipeDirection = 'd';
                    }
                    else if (grid[a, b] == 'F')
                    {
                        pipeDirection = 'u';

                    }
                    else if (grid[a, b] == 'J')
                    {
                        if (pipeDirection == 'u')
                        {
                            inside = !inside;
                        }
                    }
                    else if (grid[a, b] == '7')
                    {
                        if (pipeDirection == 'd')
                        {
                            inside = !inside;
                        }

                    }
                    if (grid[a, b] == '.' && !inside)
                    {
                        grid[a, b] = '@';
                    }
                }
            }


            for (int a = 0; a < grid.GetLength(0); a++)
            {
                for (int b = 0; b < grid.GetLength(1); b++)
                {
                    if (grid[a, b] == '.')
                    {
                        area++;
                    }
                }
            }


            return area;
        }
        static void findNext(int x, int y, char[,] grid, ref List<(int, int)> visited, ref Queue<(int, int)> queue)
        {
            List<char> possibleS = new List<char>
            {
                '|', 'J', 'L','-', 'F', '7'
            };
            char[] goU = { '|', 'J', 'L', 'S' };
            char[] fromU = { '|', 'F', '7' };
            char[] goD = { '|', 'F', '7', 'S' };
            char[] fromD = { '|', 'J', 'L' };
            char[] goL = { '-', 'J', '7', 'S' };
            char[] fromL = { '-', 'F', 'L' };
            char[] goR = { '-', 'F', 'L', 'S' };
            char[] fromR = { '-', 'J', '7' };
            

            if (!visited.Contains((x,y)))
            {
                visited.Add((x, y));
            }

            if (goU.Contains(grid[x, y]) && fromU.Contains(grid[x - 1, y]) && !visited.Contains((x - 1, y)))
            {
                queue.Enqueue((x - 1, y));
                if (grid[x,y] == 'S')
                {
                    possibleS = possibleS.Intersect(goU).ToList();
                }
            }
            if (goD.Contains(grid[x, y]) && fromD.Contains(grid[x + 1, y]) && !visited.Contains((x + 1, y)))
            {
                queue.Enqueue((x + 1, y));
                if (grid[x, y] == 'S')
                {
                    possibleS = possibleS.Intersect(goD).ToList();

                }
            }
            if (goL.Contains(grid[x, y]) && fromL.Contains(grid[x, y - 1]) && !visited.Contains((x, y-1)))
            {
                queue.Enqueue((x, y - 1));
                if (grid[x, y] == 'S')
                {
                    possibleS = possibleS.Intersect(goL).ToList();
                }
            }
            if (goR.Contains(grid[x, y]) && fromR.Contains(grid[x, y + 1]) && !visited.Contains((x, y + 1)))
            {
                queue.Enqueue((x, y + 1));
                if (grid[x, y] == 'S')
                {
                    possibleS = possibleS.Intersect(goR).ToList();
                }
            }
            if (possibleS.Count == 1)
            {
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[i, j] == 'S')
                        {
                            grid[i, j] = possibleS[0];
                            possibleS.RemoveAt(0);
                        }

                    }
                }
            }
        }
    }
}
