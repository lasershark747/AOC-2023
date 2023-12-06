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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string[] input = File.ReadAllLines("input.txt");
            int count = -1;
            string[] strings = input[0].Split(':');
            string[] seeds = strings[1].Split(' ');
            List<(long, long, long)>[] convertors = new List<(long, long, long)>[7];
            List<(long, long, long)> temp = new List<(long, long, long)>();
            List<(long, long, long)> temp2 = new List<(long, long, long)>();
            List<(long, long, long)> temp3 = new List<(long, long, long)>();
            List<(long, long, long)> temp4 = new List<(long, long, long)>();
            List<(long, long, long)> temp5 = new List<(long, long, long)>();
            List<(long, long, long)> temp6 = new List<(long, long, long)>();
            List<(long, long, long)> temp7 = new List<(long, long, long)>();
            convertors[0] = temp;
            convertors[1] = temp2;
            convertors[2] = temp3;
            convertors[3] = temp4;
            convertors[4] = temp5;
            convertors[5] = temp6;
            convertors[6] = temp7;

            Console.WriteLine(convertors.Length);

            for (int i = 1; i < input.Length; i++)
            {

                if (input[i].Length > 0)
                {
                    if (char.IsLetter(input[i][0]))
                    {
                        count++;
                    }
                    else if (char.IsDigit(input[i][0]))
                    {
                        string[] ints = input[i].Split(' ');
                        (long, long, long) buffer = (0, 0, 0);
                        buffer.Item1 = long.Parse(ints[1]);
                        buffer.Item2 = long.Parse(ints[1]) + long.Parse(ints[2]);
                        buffer.Item3 = long.Parse(ints[0]) - long.Parse(ints[1]);
                        convertors[count].Add(buffer);
                    }
                }
            }

            List<long> allSeeds = new List<long>();

            foreach (string s in seeds)
            {
                if(s.Length>0)
                {
                    allSeeds.Add(long.Parse(s));
                }
            }
            
            long num = 0;
            List<long> list = new List<long>();
            long best = long.MaxValue;

            for (int i = 0; i < allSeeds.Count; i +=2)
            {
                long seed = allSeeds[i];
                List<long> all = new List<long>();

                for (long x = allSeeds[i]; x < allSeeds[i] + allSeeds[i+1];x++ )
                {
                    num++;
                    long see = x;
                    for (int k = 0; k < convertors.Length; k++)
                    {
                        for (int j = 0; j < convertors[k].Count; j++)
                        {
                            if (see >= convertors[k][j].Item1 && see <= convertors[k][j].Item2)
                            {
                                see += convertors[k][j].Item3;
                                j = 10000;
                            }
                        }
                    }
                    if(see < best)
                    {
                        best = see;
                    }
                    if (num % 100000 == 0)
                    {
                        Console.WriteLine(num);
                    }
                    if(num%10000000 == 0)
                    {
                        Console.Clear();
                    }
                }           
            }
            

            Console.WriteLine();
            Console.WriteLine(best);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            Console.ReadKey();
        }
    }
}

