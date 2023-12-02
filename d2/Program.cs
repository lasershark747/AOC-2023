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
            ulong count = 0;

            for (int i = 0; i < input.Length; i++)
            {
                string num = input[i];
                ulong red = 0;
                ulong blue = 0;
                ulong green = 0;
                ulong minr = 0;
                ulong minb = 0;
                ulong ming = 0;
                bool succes= true;

                string[] split = num.Split(' ');
                for(int j = 0; j < split.Length; j++)
                {
                    if (split[j][0] == 'b')
                    {
                        blue += ulong.Parse(split[j - 1]);
                    }
                    if (split[j][0] == 'r')
                    {
                        red += ulong.Parse(split[j - 1]);
                    }
                    if (split[j][0] == 'g')
                    {
                        green += ulong.Parse(split[j - 1]);
                    }
                    
                    
                    if (split[j][split[j].Length - 1] == ';' || j ==split.Length-1)
                    {
                        if(green > ming)
                        {
                            ming = green;
                        }
                        if(blue> minb )
                        {
                            minb= blue;
                        }
                        if (red > minr)
                        {
                            minr = red;
                        }

                        red = 0;blue = 0;green = 0;
                    }

                }

                count +=minr*minb*ming;
                }

            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}

