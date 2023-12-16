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
            
            char[,] grid = new char[input.Length, input[0].Length];
            List<int> values = new List<int>();

            for (int i = 0; i < input.Length; i++)
            {
                for(int j =0; j < input[i].Length;j++)
                {
                    grid[i, j] = input[i][j];
                }
            }
            
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                List<(int, int, char)> seen = new List<(int, int, char)>();
                char[,] energised = new char[input.Length, input[0].Length];
                int count = 0;

                energise(-1, i, 'd', grid, ref energised, ref seen);

                foreach (char a in energised)
                {
                    if (a == '#')
                        count++;
                }
                values.Add(count);
            }
            values.Sort();
            Console.WriteLine(values[values.Count - 1]);
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                List<(int, int, char)> seen = new List<(int, int, char)>();
                char[,] energised = new char[input.Length, input[0].Length];
                int count = 0;

                energise(grid.GetLength(1), i, 'u', grid, ref energised, ref seen);

                foreach (char a in energised)
                {
                    if (a == '#')
                        count++;
                }
                values.Add(count);
            }
            values.Sort();
            Console.WriteLine(values[values.Count - 1]);
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                List<(int, int, char)> seen = new List<(int, int, char)>();
                char[,] energised = new char[input.Length, input[0].Length];
                int count = 0;

                energise(i, -1, 'r', grid, ref energised, ref seen);

                foreach (char a in energised)
                {
                    if (a == '#')
                        count++;
                }
                values.Add(count);
            }
            values.Sort();
            Console.WriteLine(values[values.Count - 1]);
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                List<(int, int, char)> seen = new List<(int, int, char)>();
                char[,] energised = new char[input.Length, input[0].Length];
                int count = 0;

                energise(i, grid.GetLength(0), 'l', grid, ref energised, ref seen);

                foreach (char a in energised)
                {
                    if (a == '#')
                        count++;
                }
                values.Add(count);
            }

            values.Sort();
            Console.WriteLine(values[values.Count-1]);
            Console.ReadKey();
        }

        static void energise(int y,int x,char direction, char[,] grid, ref char[,] energised, ref List<(int,int,char)> seen)
        {
            (int, int) current = (y, x);
            (int, int) next = (y, x);
            if (x != -1 && y!=-1 && x != grid.GetLength(0) && y != grid.GetLength(1))
            {
                energised[current.Item1, current.Item2] = '#';
            }
            if (!seen.Contains((current.Item1, current.Item2, direction)))
            {
                seen.Add((current.Item1, current.Item2, direction));
                if (direction == 'u')
                {
                    next.Item1--;
                }
                else if (direction == 'd')
                {
                    next.Item1++;
                }
                else if (direction == 'l')
                {
                    next.Item2--;
                }
                else if (direction == 'r')
                {
                    next.Item2++;
                }

                if (next.Item1 < 0 || next.Item2 < 0 || next.Item1 >= grid.GetLength(0) || next.Item2 >= grid.GetLength(1))
                {

                }
                else if (grid[next.Item1, next.Item2] == '.')
                {
                    energise(next.Item1, next.Item2, direction, grid, ref energised, ref seen);
                }
                else if (grid[next.Item1, next.Item2] == '\\')
                {
                    if (direction == 'u')
                    {
                        energise(next.Item1, next.Item2, 'l', grid, ref energised, ref seen);
                    }
                    else if (direction == 'd')
                    {
                        energise(next.Item1, next.Item2, 'r', grid, ref energised, ref seen);
                    }
                    else if (direction == 'l')
                    {
                        energise(next.Item1, next.Item2, 'u', grid, ref energised, ref seen);
                    }
                    else if (direction == 'r')
                    {
                        energise(next.Item1, next.Item2, 'd', grid, ref energised, ref seen);
                    }
                }
                else if (grid[next.Item1, next.Item2] == '/')
                {
                    if (direction == 'u')
                    {
                        energise(next.Item1, next.Item2, 'r', grid, ref energised, ref seen);
                    }
                    else if (direction == 'd')
                    {
                        energise(next.Item1, next.Item2, 'l', grid, ref energised, ref seen);
                    }
                    else if (direction == 'l')
                    {
                        energise(next.Item1, next.Item2, 'd', grid, ref energised, ref seen);
                    }
                    else if (direction == 'r')
                    {
                        energise(next.Item1, next.Item2, 'u', grid, ref energised, ref seen);
                    }
                }
                else if (grid[next.Item1, next.Item2] == '-')
                {
                    if (direction == 'r' || direction == 'l')
                    {
                        energise(next.Item1, next.Item2, direction, grid, ref energised, ref seen);
                    }
                    else
                    {
                        energise(next.Item1, next.Item2, 'l', grid, ref energised, ref seen);
                        energise(next.Item1, next.Item2, 'r', grid, ref energised, ref seen);
                    }
                }
                else if (grid[next.Item1, next.Item2] == '|')
                {
                    if (direction == 'u' || direction == 'd')
                    {
                        energise(next.Item1, next.Item2, direction, grid, ref energised, ref seen);
                    }
                    else
                    {
                        energise(next.Item1, next.Item2, 'u', grid, ref energised, ref seen);
                        energise(next.Item1, next.Item2, 'd', grid, ref energised, ref seen);
                    }
                }
            }   
        }
    }
}
