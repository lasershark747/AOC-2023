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

            string[] strings = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            for (int i = 0; i < input.Length; i++)
            {
                string num = "";
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] >= 48 && input[i][j] <= 57)
                    {
                        num += input[i][j];
                        break;
                    }
                    if (j <= input[i].Length - 3)
                    {
                        if (input[i].Substring(j, 3) == "one")
                        {
                            num += 1;
                            break;

                        }
                        else if (input[i].Substring(j, 3) == "two")
                        {
                            num += 2;
                            break;
                        }

                        else if (input[i].Substring(j, 3) == "six")
                        {
                            num += 6; break;

                        }

                    }
                    if (j <= input[i].Length - 4)
                    {
                        if (input[i].Substring(j, 4) == "four")
                        {
                            num += 4;
                            break;
                        }
                        else if (input[i].Substring(j, 4) == "five")
                        {
                            num += 5;
                            break;

                        }
                        else if (input[i].Substring(j, 4) == "nine")
                        {
                            num += 9; break;

                        }
                    }
                    if (j <= input[i].Length - 5)
                    {
                        if (input[i].Substring(j, 5) == "three")
                        {
                            num += 3;
                            break;
                        }
                        else if (input[i].Substring(j, 5) == "seven")
                        {
                            num += 7; break;

                        }
                        else if (input[i].Substring(j, 5) == "eight")
                        {
                            num += 8; break;

                        }
                    }
                }


                for (int j = input[i].Length - 1; j >= 0; j--)
                {
                    if (input[i][j] >= 48 && input[i][j] <= 57)
                    {
                        num += input[i][j];
                        break;
                    }
                    if (j <= input[i].Length - 3)
                    {
                        if (input[i].Substring(j, 3) == "one")
                        {
                            num += 1;
                            break;

                        }
                        else if (input[i].Substring(j, 3) == "two")
                        {
                            num += 2;
                            break;
                        }

                        else if (input[i].Substring(j, 3) == "six")
                        {
                            num += 6; break;

                        }

                    }
                    if (j <= input[i].Length - 4)
                    {
                        if (input[i].Substring(j, 4) == "four")
                        {
                            num += 4;
                            break;
                        }
                        else if (input[i].Substring(j, 4) == "five")
                        {
                            num += 5;
                            break;

                        }
                        else if (input[i].Substring(j, 4) == "nine")
                        {
                            num += 9; break;

                        }
                    }
                    if (j <= input[i].Length - 5)
                    {
                        if (input[i].Substring(j, 5) == "three")
                        {
                            num += 3;
                            break;
                        }
                        else if (input[i].Substring(j, 5) == "seven")
                        {
                            num += 7; break;

                        }
                        else if (input[i].Substring(j, 5) == "eight")
                        {
                            num += 8; break;

                        }
                    }
                }
                count += int.Parse(num);
            }
            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}

