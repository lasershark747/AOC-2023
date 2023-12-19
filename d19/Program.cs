using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;

namespace d5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            Dictionary<string, List<(int, char, int, string)>> workflow = new Dictionary<string, List<(int, char, int, string)>>();
            List<(int, int, int, int)> parts = new List<(int, int, int, int)>();

            int line = 0;
            while (true)
            {
                if (input[line] == "")
                {
                    break;
                }
                else
                {
                    List<(int, char, int, string)> list1 = new List<(int, char, int, string)>();
                    string[] split1 = input[line].Split('{');
                    string lable = split1[0];
                    string[] split2 = split1[1].Substring(0, split1[1].Length - 1).Split(',');

                    for (int i = 0; i < split2.Length - 1; i++)
                    {
                        string[] split3 = split2[i].Split(':');
                        int type = 0;
                        if (split3[0][0] == 'm') type = 1;
                        else if (split3[0][0] == 'a') type = 2;
                        else if (split3[0][0] == 's') type = 3;

                        list1.Add((type, split3[0][1], int.Parse(split3[0].Substring(2)), split3[1]));
                    }
                    list1.Add((0, '>', 0, split2[split2.Length - 1]));

                    workflow.Add(lable, list1);
                    line++;
                }
            }

            Console.WriteLine("P2: " + possibilities(workflow));
            Console.ReadKey();
        }

        static long possibilities(Dictionary<string, List<(int, char, int, string)>> workflow)
        {
            long count = 0;
            List<(long, long)> list = new List<(long, long)>
            {
                (1,4000),
                (1,4000),
                (1,4000),
                (1,4000)
            };

            count = range(list, workflow, "in");

            return count;
        }

        static long range(List<(long, long)> ranges, Dictionary<string, List<(int, char, int, string)>> workflow, string lable)
        {
            long total = 0;
            if(lable == "A")
            {
                total = 1;
                foreach(var item in ranges)
                {
                    total *= item.Item2 - item.Item1 + 1;
                }
                return total;
            }
            else if(lable == "R")
            {
                return total;
            }
            List<(long, long)> list1 = new List<(long, long)>
            {
                ranges[0],
                ranges[1],
                ranges[2],
                ranges[3]
            };
            var changes = workflow[lable];
            foreach (var l in changes)
            {
                List<(long, long)> list2 = new List<(long, long)>
                { 
                list1[0],
                list1[1],
                list1[2],
                list1[3]
                };

                if (l.Item3 == 0)
                {
                    total += range(list1, workflow, l.Item4);
                }
                else
                {
                    var scope = list1[l.Item1];
                    (long, long) new1, new2;

                    if(l.Item2 == '>')
                    {
                        new1 = (l.Item3+1,scope.Item2);
                        new2 = (scope.Item1,l.Item3);
                    }
                    else
                    {
                        new1 = (scope.Item1,l.Item3-1);
                        new2 = (l.Item3,scope.Item2);
                    }

                    if(new1.Item1<=new1.Item2)
                    {
                        list2[l.Item1] = new1;
                        total += range(list2, workflow, l.Item4);
                    }

                    if(new2.Item1<=new2.Item2)
                    {
                        list1[l.Item1] = new2;
                    }
                }
            }
            return total;
        }
    }
}
