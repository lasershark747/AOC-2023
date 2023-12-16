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
            int done = 0;
            List<List<char>> list = new List<List<char>>();
            List<string> states = new List<string>();
            

            foreach (string line in input)
            {
                List<char> list2 = new List<char>();
                foreach (char c in line)
                {
                    list2.Add(c);
                }
                list.Add(list2);
            }
            states.Add(intoString(list));

            for (int a = 1; a <= 1000000000; a++)
            {

                while (true) //north
                {
                    int rolls = 0;
                    for (int i = 1; i < list.Count; i++)
                    {
                        for (int j = 0; j < list[i].Count; j++)
                        {
                            if (list[i][j] == 'O' && list[i - 1][j] == '.')
                            {
                                list[i - 1][j] = 'O';
                                list[i][j] = '.';
                                rolls++;
                            }
                        }
                    }
                    if (rolls == 0)
                        break;
                }

                while (true) //west
                {
                    int rolls = 0;
                    for (int i = 1; i < list[0].Count; i++)
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                            if (list[j][i] == 'O' && list[j][i - 1] == '.')
                            {
                                list[j][i - 1] = 'O';
                                list[j][i] = '.';
                                rolls++;
                            }
                        }
                    }
                    if (rolls == 0)
                        break;
                }

                while (true)//south
                {
                    int rolls = 0;
                    for (int i = list.Count - 2; i >= 0; i--)
                    {
                        for (int j = 0; j < list[i].Count; j++)
                        {
                            if (list[i][j] == 'O' && list[i + 1][j] == '.')
                            {
                                list[i + 1][j] = 'O';
                                list[i][j] = '.';
                                rolls++;
                            }
                        }
                    }
                    if (rolls == 0)
                        break;
                }

                while (true)//east
                {
                    int rolls = 0;
                    for (int i = list[0].Count - 2; i >= 0; i--)
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                            if (list[j][i] == 'O' && list[j][i + 1] == '.')
                            {
                                list[j][i + 1] = 'O';
                                list[j][i] = '.';
                                rolls++;
                            }
                        }
                    }
                    if (rolls == 0)
                        break;
                }

                /*
                foreach(List<char> line in  list)
                {
                    foreach(char c in line)
                        Console.Write(c);
                    Console.WriteLine();
                }
                Console.WriteLine();
                */
                ;

                if (states.IndexOf(intoString(list))>=0 && a != 0)
                {
                    done = a;
                    break;
                }

                states.Add(intoString(list));

                
            }

            Console.WriteLine(states.IndexOf(intoString(list)));
            Console.WriteLine(done);

            int index = (1000000000- states.IndexOf(intoString(list)))%(done-states.IndexOf(intoString(list))) + states.IndexOf(intoString(list));
            Console.WriteLine(index);

            list = intoList(states[index],list);




            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (list[i][j] == 'O')
                    {
                        count += list.Count - i;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine(count);
            Console.ReadKey();
        }

        static string intoString(List<List<char>> input)
        {
            string s = "";
            foreach (List<char> line in input)
            {
                foreach (char c in line)
                {
                    s += c;
                }
            }
            return s;
        }
        static List<List<char>> intoList (string input, List<List<char>> grid)
        {
            List<List<char>> output = new List<List<char>>();
            int count = 0;
            for(int i = 0; i < grid.Count;i++)
            {
                List<char> temp = new List<char>();
                for(int j = 0; j < grid[i].Count; j++)
                {
                    temp.Add(input[count]);
                    count++;
                }
                output.Add(temp);
            }

            return output;
        }
    }
}


