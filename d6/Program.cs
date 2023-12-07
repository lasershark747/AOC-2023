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
            //edit the text file to have one space between races
            string[] input = File.ReadAllLines("input.txt");
            int count = 1;
            string[] buffer = input[0].Split(':');
            string[] time = buffer[1].Split(' ');
            string[] buffer2 = input[1].Split(':');
            string[] distance = buffer2[1].Split(' ');

            for (int i = 0; i < time.Length; i++)
            {
                int wins = 0;
                
                long t = long.Parse(time[i]);
                long d = long.Parse(distance[i]);
                for(int j = 1; j <= t; j++)
                {
                    if((t-j)*j >= d)
                    {
                        wins++;
                    }
                }
                count *= wins;
            }

            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}
