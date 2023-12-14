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
            string[] input = File.ReadAllLines("input.txt");
            long count = 0;
            int moves = 0;
            List<List<char>> list = new List<List<char>>();
            List<List<char>> start = new List<List<char>>();

            foreach (string line in input)
            {
                List<char> list2 = new List<char>();
                foreach (char c in line)
                {
                    list2.Add(c);
                }
                list.Add(list2);
            }
            List<string> dict = new List<string>();

            for (int a = 0; a < 3; a++)
            {
                if (start == list && a != 0)
                {
                    moves = a;
                    break;
                }
                else
                {
                    for (int i = 1; i < list.Count; i++)
                    {
                        for (int j = 0; j < list[i].Count; j++)
                        {
                            if (list[i][j] == 'O')
                            {
                                if (list[i - 1][j] == '.')
                                {
                                    moves++;
                                    list[i - 1][j] = 'O';
                                    list[i][j] = '.';
                                }
                            }
                        }
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        for (int j = 1; j < list[i].Count; j++)
                        {
                            if (list[i][j] == 'O')
                            {
                                if (list[i][j - 1] == '.')
                                {
                                    moves++;
                                    list[i][j - 1] = 'O';
                                    list[i][j] = '.';
                                }
                            }
                        }
                    }

                    for (int i = 0; i < list.Count - 1; i++)
                    {
                        for (int j = 0; j < list[i].Count; j++)
                        {
                            if (list[i][j] == 'O')
                            {
                                if (list[i + 1][j] == '.')
                                {
                                    moves++;
                                    list[i + 1][j] = 'O';
                                    list[i][j] = '.';
                                }
                            }
                        }
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        for (int j = 0; j < list[i].Count - 1; j++)
                        {
                            if (list[i][j] == 'O')
                            {
                                if (list[i][j + 1] == '.')
                                {
                                    moves++;
                                    list[i][j + 1] = 'O';
                                    list[i][j] = '.';
                                }
                            }
                        }
                    }
                    string st = "";
                    foreach (List<char> l in list)
                    {
                        string t = "";
                        foreach (char c in l)
                        {
                            t += c;
                        }
                        Console.WriteLine(t);
                    }
                    Console.WriteLine();
                }

                if (a % 100000 == 0)
                {
                    Console.WriteLine(a);
                }

            }

            /*
            string s = dict[1000000000 % moves];
            List<List<char>> hope = new List<List<char>>();
            int num = 0;
            for(int i = 0; i < input.Length; i++)
            {
                List<char> temp = new List<char>();
                for(int j = 0; j < input[i].Length; j++)
                {
                    temp.Add(s[num]);
                    num++;
                }
            }

            for (int i = 0; i < hope.Count; i++)
            {
                for (int j = 0; j < hope[i].Count; j++)
                {
                    if (hope[i][j] == 'O')
                    {
                        count += hope.Count - i;
                    }
                }
            }
            */
            Console.WriteLine();
            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}

