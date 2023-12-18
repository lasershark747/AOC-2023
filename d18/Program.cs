using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.ComponentModel;

namespace d5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            p1();

            string[] input = File.ReadAllLines("input.txt");
            long area = 0;
            long inside = 0;
            long outside= 0;
            
            (long, long) location = (0, 0);
            List<(long, long)> nodes = new List<(long, long)>();
            nodes.Add(location);

            for (int i = 0; i < input.Length; i++)
            {
                string[] l = input[i].Split(' ');
                outside += long.Parse(l[2].Substring(2, 5), System.Globalization.NumberStyles.HexNumber);
                if (l[2][7] == '2')
                {
                    location.Item2 -= long.Parse(l[2].Substring(2,5), System.Globalization.NumberStyles.HexNumber);
                }
                else if (l[2][7] == '0')
                {
                    location.Item2 += long.Parse(l[2].Substring(2, 5), System.Globalization.NumberStyles.HexNumber);
                }
                else if (l[2][7] == '3')
                {
                    location.Item1 -= long.Parse(l[2].Substring(2, 5), System.Globalization.NumberStyles.HexNumber);
                }
                else if (l[2][7] == '1')
                {
                    location.Item1 += long.Parse(l[2].Substring(2, 5), System.Globalization.NumberStyles.HexNumber);
                }
                nodes.Add(location);
            }

            for (int i = 0; i < nodes.Count; ++i)
            {
                if (i == 0)
                {
                    inside += nodes[i].Item1 * (nodes[nodes.Count - 1].Item2 - nodes[i + 1].Item2);
                }
                else
                {
                    inside += nodes[i].Item1 * (nodes[i - 1].Item2 - nodes[(i + 1) % nodes.Count].Item2);
                }
            }
            inside = Math.Abs(inside)/2;

            area = inside + outside / 2 + 1;
            Console.WriteLine("P2: " + area);

            Console.ReadKey();
        }


        static void p1()
        {
            string[] input = File.ReadAllLines("input.txt");
            long area = 0;
            (int, int) location = (0, 0);
            List<(int, int)> nodes = new List<(int, int)>();
            nodes.Add(location);

            long outside = 0;
            long inside = 0;

            for (int i = 0; i < input.Length; i++)
            {
                string[] l = input[i].Split(' ');
                outside += int.Parse(l[1]);
                if (l[0] == "L")
                {
                    location.Item1 -= int.Parse(l[1]);
                }
                else if (l[0] == "R")
                {
                    location.Item1 += int.Parse(l[1]);
                }
                else if (l[0] == "U")
                {
                    location.Item2 -= int.Parse(l[1]);
                }
                else if (l[0] == "D")
                {
                    location.Item2 += int.Parse(l[1]);
                }
                nodes.Add(location);
            }         


            for(int i  = 0; i < nodes.Count; ++i)
            {
                if(i==0)
                {
                    inside += nodes[i].Item1 * (nodes[nodes.Count - 1].Item2 - nodes[i + 1].Item2);
                }
                else
                {
                    inside += nodes[i].Item1 * (nodes[i - 1].Item2 - nodes[(i + 1) % nodes.Count].Item2);
                }
            }

            inside = Math.Abs(inside)/2;

            area = inside + outside / 2 + 1;
            Console.WriteLine("P1: " + area);

        }
    }
}
