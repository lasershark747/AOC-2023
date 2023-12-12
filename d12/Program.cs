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
            long count = 0;
            List<List<char>> gears = new List<List<char>>();
            List<List<int>> combos = new List<List<int>>();

            for (int i = 0; i < input.Length; i++)
            {
                string[] split = input[i].Split(' ');
                List<char> list = new List<char>();
                foreach (char c in split[0])
                {
                    list.Add(c);
                }
                gears.Add(list);
                string[] split2 = split[1].Split(',');
                List<int> ints = new List<int>();
                foreach (string s in split2)
                {
                    ints.Add(int.Parse(s));
                }
                combos.Add(ints);
            }


            List<List<string>> brokenGrears = new List<List<string>>();

            for (int i = 0; i < gears.Count; i++)
            {
                List<string> list = new List<string>();
                string word = "";
                foreach (char c in gears[i])
                {
                    if (c != '.')
                    {
                        word += c;
                    }
                    else if (word.Length > 0)
                    {
                        list.Add(word);
                        word = "";
                    }
                }
                list.Add(word);
                brokenGrears.Add(list);
            }

            for (int i = 0; i < brokenGrears.Count; i++)
            {
                int permitations = 0;
                int size = 0;
                foreach (int num in combos[i])
                {
                    size += num;
                }

                int length = 0;
                foreach (string s in brokenGrears[i])
                {
                    length += s.Length;
                }


                if (length == size)
                {
                    permitations++;
                }
                else if (size < length)
                {
                    int start = 0;
                    while (true)
                    {
                        int currentCell = 0;
                        int currentSize = 0;
                        bool success = true;
                        for (int a = 0; a < combos[i].Count; a++)
                        {
                            if (combos[i][a] + currentSize == brokenGrears[i][currentCell].Length)
                            {
                                currentCell++;
                                if (a == combos.Count - 1)
                                {
                                    permitations++;
                                }
                            }
                            else if (combos[i][a] + currentSize < brokenGrears[i][currentCell].Length)
                            {
                                currentSize += combos[i][a] + 1;
                                if (a == combos.Count - 1)
                                {
                                    permitations++;
                                }
                            }
                            else if (combos[i][a] + currentSize > brokenGrears[i][currentCell].Length)
                            {
                                currentCell++;
                                a--;

                            }
                            if (brokenGrears[i].Count <= currentCell)
                            {
                                a = 1000;
                                success = false;
                            }
                        }

                        if (success == false)
                        {
                            start++;
                        }
                        if (start + size > length)
                        {
                            break;
                        }

                    }
                }
                else if (length > size)
                {

                }
                Console.WriteLine(permitations);
                count += permitations;
            }

            Console.WriteLine(count);
            Console.ReadKey();
        }



    }
}