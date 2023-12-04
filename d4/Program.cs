using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace d4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            double count = 0;
            int[] ints = new int[input.Length];
            
            for(int i  = 0; i < ints.Length;i++)
            {
                ints[i] = 1;
            }


            for (int i = 0; i < input.Length; i++)
            {                
                    string[] array = input[i].Split(':');
                    string[] array2 = array[1].Split('|');
                    string[] array3 = array2[0].Split(' ');
                    string[] array4 = array2[1].Split(' ');
                    List<string> list = new List<string>();
                    foreach (string s in array4)
                    {
                        if (s.Length > 0)
                        {
                            list.Add(s);
                        }
                    }
                    foreach (string s in array3)
                    {
                        if (s.Length > 0)
                        {
                            list.Add(s);
                        }
                    }
                    List<string> strings = list.Distinct().ToList();

                if ((list.Count - strings.Count) > 0)
                {
                    for (int j = i + 1; j < i + (list.Count - strings.Count) + 1 && j < input.Length; j++)
                    {
                        
                        ints[j] += ints[i];
                    }
                }
            }
            foreach(int i in ints)
            {
                Console.WriteLine(i);
                count += i;
            }
            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}

