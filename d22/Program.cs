using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Data;

namespace d5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            string[] input = File.ReadAllLines("input.txt");
            long count = 0;
            List<int[]> blocks = new List<int[]>();

            for (int i = 0; i < input.Length; i++)
            {
                int[] temp = new int[6];
                string[] split1 = input[i].Split('~');
                string[] split2 = split1[0].Split(',');
                string[] split3 = split1[1].Split(',');
                for (int j = 0; j < 3; j++)
                {
                    temp[j] = int.Parse(split2[j]);
                    temp[j + 3] = int.Parse(split3[j]);
                }
                blocks.Add(temp);
            }

            int swaps = 1;
            while (swaps > 0)
            {
                swaps = 0;
                for (int i = 0; i < blocks.Count - 1; i++)
                {
                    if (blocks[i][2] > blocks[i + 1][2])
                    {
                        (blocks[i], blocks[i + 1]) = (blocks[i + 1], blocks[i]);
                        swaps++;
                    }
                }
            }

            for (int i = 0; i < blocks.Count; i++)
            {
                int maxZ = 1;
                int[] block = blocks[i];
                for (int j = 0; j < i; j++)
                {
                    var block2 = blocks[j];
                    if (intersect(block, block2))
                    {
                        maxZ = Math.Max(maxZ, block2[5] + 1);
                    }
                }
                block[5] -= block[2] - maxZ;
                block[2] = maxZ;
                blocks[i] = block;
            }

            swaps = 1;
            while (swaps > 0)
            {
                swaps = 0;
                for (int i = 0; i < blocks.Count - 1; i++)
                {
                    if (blocks[i][2] > blocks[i + 1][2])
                    {
                        (blocks[i], blocks[i + 1]) = (blocks[i + 1], blocks[i]);
                        swaps++;
                    }
                }
            }

            Dictionary<int, List<int>> supportsMe = new Dictionary<int, List<int>>();
            Dictionary<int, List<int>> iSupport = new Dictionary<int, List<int>>();

            for (int i = 0; i < blocks.Count; i++)
            {
                List<int> ints = new List<int>();
                List<int> ints2 = new List<int>();
                supportsMe.Add(i, ints);
                iSupport.Add(i, ints2);
            }


            for (int j = 0; j < blocks.Count; j++)
            {
                var upper = blocks[j];
                for (int i = 0; i < j; i++)
                {
                    var lower = blocks[i];
                    if (intersect(lower, upper) && upper[2] == lower[5] + 1)
                    {
                        iSupport[i].Add(j);
                        supportsMe[j].Add(i);
                    }
                }
            }

            for (int i = 0; i < blocks.Count; i++)
            {

                if (iSupport[i].Count == 0)
                {

                }
                else
                {
                    Queue<int> queue = new Queue<int>();
                    List<int> seen = new List<int>();
                    foreach (int j in iSupport[i])
                    {
                        if (supportsMe[j].Count == 1)
                        {
                            queue.Enqueue(j);
                            seen.Add(j);
                        }
                    }
                    seen.Add(i);
                    while (queue.Count > 0)
                    {
                        int current = queue.Dequeue();
                        foreach (int j in iSupport[current])
                        {
                            if (seen.Contains(j))
                            {

                            }
                            else
                            {
                                bool inFalling = true;
                                foreach (var item in supportsMe[j])
                                {
                                    if (!seen.Contains(item))
                                    {
                                        inFalling = false;
                                    }
                                }
                                if (inFalling)
                                {
                                    queue.Enqueue(j);
                                    seen.Add(j);
                                }
                            }

                        }
                    }
                    count += seen.Count - 1;

                }
            }
            Console.WriteLine(count);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            Console.ReadKey();
        }

        static bool intersect(int[] b1, int[] b2)
        {
            if (Math.Max(b1[0], b2[0]) <= Math.Min(b1[3], b2[3]) && Math.Max(b1[1], b2[1]) <= Math.Min(b1[4], b2[4]))
            {
                return true;
            }
            return false;
        }
    }
}
