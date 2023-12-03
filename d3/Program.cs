using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace q1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            int count = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '*')
                    {
                        List<int> list = new List<int>();

                        for (int y = i - 1; y < i + 2; y++)
                        {
                            if (y >= 0)
                            {
                                bool previous = true;
                                for (int x = j - 1; x < j + 2; x++)
                                {
                                    if (char.IsDigit(input[y][x - 2]) && char.IsDigit(input[y][x - 1]) && char.IsDigit(input[y][x]) && previous)
                                    {
                                        x -= 2;
                                        previous = false;
                                        if (char.IsDigit(input[y][x]))
                                        {
                                            string num = input[y][x].ToString();

                                            if (char.IsDigit(input[y][x + 1]))
                                            {
                                                num += input[y][x + 1].ToString();
                                                x++;
                                                if (char.IsDigit(input[y][x + 1]))
                                                {
                                                    num += input[y][x + 1].ToString();
                                                    x++;
                                                }
                                            }
                                            list.Add(int.Parse(num));
                                        }
                                    }
                                    else if (char.IsDigit(input[y][x + 1]) && char.IsDigit(input[y][x - 1]) && char.IsDigit(input[y][x]) && previous)
                                    {
                                        x -= 1;
                                        previous = false;
                                        if (char.IsDigit(input[y][x]))
                                        {
                                            string num = input[y][x].ToString();

                                            if (char.IsDigit(input[y][x + 1]))
                                            {
                                                num += input[y][x + 1].ToString();
                                                x++;
                                                if (char.IsDigit(input[y][x + 1]))
                                                {
                                                    num += input[y][x + 1].ToString();
                                                    x++;
                                                }
                                            }
                                            list.Add(int.Parse(num));
                                        }
                                    }
                                    else if ( char.IsDigit(input[y][x - 1]) && char.IsDigit(input[y][x]) && previous)
                                    {
                                        x -= 1;
                                        previous = false;
                                        if (char.IsDigit(input[y][x]))
                                        {
                                            string num = input[y][x].ToString();

                                            if (char.IsDigit(input[y][x + 1]))
                                            {
                                                num += input[y][x + 1].ToString();
                                                x++;
                                            }
                                            list.Add(int.Parse(num));
                                        }
                                    }
                                    else if (char.IsDigit(input[y][x]))
                                    {
                                        string num = input[y][x].ToString();
                                        while (true)
                                        {
                                            if (x < input[0].Length - 1 && y < input.Length)
                                            {
                                                if (char.IsDigit(input[y][x + 1]))
                                                {
                                                    num += input[y][x + 1].ToString();
                                                    x++;
                                                }
                                                else break;
                                            }
                                            else break;
                                        }
                                        list.Add(int.Parse(num));
                                    }

                                }
                            }
                        }
                        if (list.Count == 2)
                        {
                            Console.WriteLine(i+","+j);
                            count += list[0] * list[1];
                        }
                    }
                }
            }

            Console.WriteLine(count);
            Console.ReadKey();
        }


        public static bool check(int x, int y, int legnth, string[] input)
        {
            bool special = false;
            for (int j = y - 1; j < y + 2; j++)
            {
                if (j >= 0 && j < input.Length)
                {
                    for (int i = x - 1; i < x + legnth + 1; i++)
                    {
                        if (i >= 0 && i < input[0].Length)
                        {
                            if (input[j][i] != '.' && !char.IsDigit(input[j][i]))
                            {
                                special = true;
                                return special;
                            }
                        }
                    }
                }
            }
            return special;
        }
    }
}


