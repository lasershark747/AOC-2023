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

            for (int i = 0; i < input.Length; i++)
            {
                string[] array = input[i].Split(' ');
                List<long> sequence = new List<long>();
                for (int j = array.Length-1; j >= 0; j--)
                {
                    sequence.Add(long.Parse(array[j]));
                }
                count += findNext(sequence);
            }

            Console.WriteLine(count);
            Console.ReadKey();
        }


        static long findNext(List<long> sequence)
        {
            List<long> second = new List<long>();
            long difference = 0;
            for(int i = 1; i < sequence.Count; i++)
            {
                second.Add(sequence[i] - sequence[i-1]);
            }
            if(second.Distinct().Count()==1)
            {
                difference = second[0];   
            }
            else
            {
                difference = findNext(second);
                 
            }

            return difference + sequence[sequence.Count - 1];
        }
    }
}
