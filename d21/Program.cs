using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Collections;

namespace d5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string[] input = File.ReadAllLines("input.txt");
            char[,] grid = new char[input.Length,input[0].Length];            
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i, j] = input[i][j];
                }
            }



            //P2



            
            int size = grid.GetLength(0);
            int steps = 26501365;
            int numOfGrids = steps / size;

            long total = 0;
            

            Queue<(int, int, int)> queue = new Queue<(int, int, int)>();
            List<(int, int, int)> seen = new List<(int, int, int)>();
            List<(int,int,int)> seen2 = new List<(int, int, int)>(); 
            var start = (65, 65, 0);
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                int x = node.Item2;
                int y = node.Item1;
                int distance = node.Item3;


                if (x - 1 >= 0 && grid[y, x - 1] != '#' && !seen.Contains((y, x - 1, (distance + 1) % 2)))
                {
                    queue.Enqueue((y, x - 1, distance + 1));
                    seen.Add((y, x - 1, (distance + 1) % 2));
                    seen2.Add((y, x - 1, distance + 1));
                }
                if (y - 1 >= 0 && grid[y - 1, x] != '#' && !seen.Contains((y - 1, x, (distance + 1) % 2)))
                {
                    queue.Enqueue((y - 1, x, distance + 1));
                    seen.Add((y - 1, x, (distance + 1) % 2));
                    seen2.Add((y, x - 1, distance + 1));
                }
                if (x + 1 < grid.GetLength(0) && grid[y, x + 1] != '#' && !seen.Contains((y, x + 1, (distance + 1) % 2)))
                {
                    queue.Enqueue((y, x + 1, distance + 1));
                    seen.Add((y, x + 1, (distance + 1) % 2));
                    seen2.Add((y, x - 1, distance + 1));
                }
                if (y + 1 < grid.GetLength(1) && grid[y + 1, x] != '#' && !seen.Contains((y + 1, x, (distance + 1) % 2)))
                {
                    queue.Enqueue((y + 1, x, distance + 1));
                    seen.Add((y + 1, x, (distance + 1) % 2));
                    seen2.Add((y, x - 1, distance + 1));
                }

            }

            total = 0;
            long odd = 0;
            long even = 0;
            long cornerOdd = 0;
            long cornerEven = 0;
            foreach(var item in seen2)
            {
                if(item.Item3%2==0)
                {
                    even++;
                }
                else
                {
                    odd++;
                }
                if (item.Item3 % 2 == 0 && item.Item3>65)
                {
                    cornerEven++;
                }
                if (item.Item3 % 2 == 1 && item.Item3 > 65)
                {
                    cornerOdd++;
                }
            }

            total += odd * (long)Math.Pow(numOfGrids + 1, 2);
            total += even * (long)Math.Pow(numOfGrids, 2);
            total -= cornerOdd*(long)(numOfGrids + 1);
            total += cornerEven * (long)(numOfGrids);
            Console.WriteLine("Total: " + total);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            Console.ReadKey();
        }
    }
}
