using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Principal;

namespace d5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            long count = 0;
            int startRow = 0;
            int endRow = 0;
            int grids = 0;
            List<List<string>> list = new List<List<string>>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Length == 0)
                {
                    endRow = i - 1;
                    List<string> temp = new List<string>();
                    for (int j = startRow; j <= endRow; j++)
                    {
                        temp.Add(input[j]);
                    }
                    list.Add(temp);
                    startRow = i + 1;
                    grids++;
                }
            }

            foreach (List<string> s in list)
            {
                long temp = findSymmetry(s, 0);
                count += temp;
            }

            Console.WriteLine("P1: " + count);
            count = 0;
            foreach (List<string> s in list)
            {
                long temp = findSymmetry(s, 1);
                count += temp;
            }

            Console.WriteLine("P2: " + count);
            Console.ReadKey();
        }
        static long findSymmetry(List<string> s, int diff)
        {
            long count = 0;
            List<string> grid = s;
            List<string> result = Transpose(grid);

            bool done = false;
            for (int i = 0; i < grid.Count - 1; i++)
            {
                int differences = 0;

                int num = 0;

                while (num + i + 1 < grid.Count && i - num >= 0)
                {
                    for (int a = 0; a < grid[i - num].Length; a++)
                    {
                        if (grid[i - num][a] != grid[i + num + 1][a])
                        {
                            differences++;
                        }
                    }
                    num++;
                }
                if (differences == diff)
                {
                    count += (i + 1) * 100;
                    done = true;
                    break;
                }
            }

            if (!done)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    int differences = 0;

                    int num = 0;

                    while (num + i + 1 < result.Count && i - num >= 0)
                    {
                        for (int a = 0; a < result[i - num].Length; a++)
                        {
                            if (result[i - num][a] != result[i + num + 1][a])
                            {
                                differences++;
                            }
                        }
                        num++;
                    }
                    if (differences == diff)
                    {
                        count += (i + 1);
                        done = true;
                        break;
                    }
                }
            }
            return count;
        }

        public static List<string> Transpose(List<string> matrix)
        {
            List<string> transposed = new List<string>();
            for (int i = 0; i < matrix[0].Length; i++)
            {
                string buffer = "";
                for (int j = 0; j < matrix.Count; j++)
                {
                    buffer += matrix[j][i];
                }
                transposed.Add(buffer);
            }

            return transposed;
        }

    }
}
