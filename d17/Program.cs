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
            List<List<int>> grid = new List<List<int>>();
            int count = 0;

            for (int i = 0; i < input.Length; i++)
            {
                List<int> list = new List<int>();
                for (int j = 0; j < input[i].Length; j++)
                {
                    list.Add(int.Parse(input[i][j].ToString()));
                }
                grid.Add(list);
            }
            count += pathFinding(grid, (0, 0));

            Console.WriteLine(count);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            Console.ReadKey();
        }

        static int pathFinding(List<List<int>> grid, (int, int) start)
        {
            int output = 0;
            List<(int, int, int, int, (int, int))> seen = new List<(int, int, int, int, (int, int))>();
            List<(int, int, int, (int, int))> seen2 = new List<(int, int, int, (int, int))>();
            List<(int, int, int, int, (int, int))> queue = new List<(int, int, int, int, (int, int))>
            {
                (0,start.Item1, start.Item1, 0, (0,0))
            }; // heat loss, y, x, moves in a row, direction
            int distance = 0;

            while (queue.Count > 0)
            {
                (int, int, int, int, (int, int)) current = queue[0];
                queue.RemoveAt(0);
                int y = current.Item2;
                int x = current.Item3;
                int heatLoss = current.Item1;
                int movesInARow = current.Item4;
                (int, int) direction = current.Item5;

                if (y == grid.Count - 1 && x == grid[0].Count - 1 && movesInARow >= 4 )
                {
                    output = heatLoss;
                    break;
                }
                (int, int, int, (int, int)) temp = (y, x, movesInARow, direction);

                if (!seen2.Contains(temp))
                {
                    seen.Add(current);
                    seen2.Add(temp);

                    if(heatLoss > distance)
                    {
                        Console.WriteLine(heatLoss);
                        distance= heatLoss;
                    }
                    //Console.WriteLine(current);

                    int xd = direction.Item2;
                    int yd = direction.Item1;

                    if (movesInARow < 10 && direction != (0, 0))
                    {
                        if (x + xd >= 0 && x + xd < grid[0].Count && y + yd >= 0 && y + yd < grid.Count)
                        {
                            queue.Add((heatLoss + grid[y + direction.Item1][x + direction.Item2], y + yd, x + xd, movesInARow + 1, direction));
                        }
                    }

                    (int, int)[] moves = { (0, 1), (0, -1), (1, 0), (-1, 0) };
                    if (movesInARow >= 4 || movesInARow==0)
                    {
                        foreach (var move in moves)
                        {
                            if (x + move.Item2 >= 0 && x + move.Item2 < grid[0].Count && y + move.Item1 >= 0 && y + move.Item1 < grid.Count)
                            {
                                if ((yd, xd) != move && (-yd, -xd) != move)
                                {
                                    queue.Add((heatLoss + grid[y + move.Item1][x + move.Item2], y + move.Item1, x + move.Item2, 1, move));
                                }
                            }
                        }
                    }

                    for (int i = queue.Count - 1; i > 0; i--)
                    {
                        if (queue[i].Item1 < queue[i - 1].Item1)
                        {
                            (queue[i], queue[i - 1]) = (queue[i - 1], queue[i]);
                        }
                    }
                }
            }

            Console.WriteLine(output);
            return output;
        }

    }
}

