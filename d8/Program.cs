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
            string instructions = input[0];
            Dictionary<string, (string, string)> nodes = new Dictionary<string, (string, string)>();

            for (int i = 2; i < input.Length; i++)
            {
                nodes.Add(input[i].Substring(0, 3), ((input[i].Substring(7, 3), (input[i].Substring(12, 3)))));
            }

            List<string> endsInA = new List<string>();

            foreach(string key in nodes.Keys)
            {
                if (key[2]== 'A')
                {
                    endsInA.Add(key);
                }
            }

            List<int> distance = new List<int>();
            foreach (string key in endsInA)
            {
                int count = 0;
                string currrent = key;
                while (true)
                {
                    if (currrent[2] == 'Z')
                    {
                        break;
                    }

                    if (instructions[count % instructions.Length] == 'L')
                    {
                        currrent = nodes[currrent].Item1;
                    }
                    if (instructions[count % instructions.Length] == 'R')
                    {
                        currrent = nodes[currrent].Item2;
                    }
                    count += 1;
                }
                distance.Add(count);
            }


            foreach (int i in distance)
            {
                Console.WriteLine(i);
            }

            //find lcm of these numbers 


            Console.ReadKey();
        }
    }
}
