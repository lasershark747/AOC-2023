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
            string[] hashes = input[0].Split(',');
            List<(string, int)>[] boxes = new List<(string, int)>[256];
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i] = new List<(string, int)>();
            }

            for (int i = 0; i < hashes.Length; i++)
            {
                long num = 0;
                foreach (char c in hashes[i])
                {
                    if (char.IsLetter(c))
                    {
                        num += c;
                        num *= 17;
                        num = num % 256;
                    }
                }
                if (hashes[i].Contains('='))
                {
                    string[] buffer = hashes[i].Split('=');
                    bool add = true;
                    int rep = 0;
                    if (boxes[num] == null)
                    {
                        rep = 0;
                    }
                    else
                    {
                        rep = boxes[num].Count;
                    }
                    for (int a = 0; a < boxes[num].Count; a++)
                    {
                        (string, int) t = boxes[num][a];
                        if (t.Item1 == buffer[0])
                        {
                            boxes[num][a] = (buffer[0], int.Parse(buffer[1]));
                            add = false;
                            break;
                        }
                    }
                    if (add)
                    {
                        boxes[num].Add((buffer[0], int.Parse(buffer[1])));
                    }
                }
                else
                {
                    string label = hashes[i].Remove(hashes[i].Length - 1);

                    List<(string, int)> l = boxes[num];
                    for (int b = 0; b < l.Count; b++)
                    {
                        (string, int) t = l[b];
                        if (t.Item1 == label)
                        {
                            boxes[num].RemoveAt(b);
                        }

                    }
                }
            }

            for (int i = 0; i < boxes.Length; i++)
            {
                for (int j = 0; j < boxes[i].Count; j++)
                {
                    count += (i + 1) * (j + 1) * boxes[i][j].Item2;
                }
            }
            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}
