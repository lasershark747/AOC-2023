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
            List<(int,int)> locations = new List<(int,int)>(); 
            List<int> vert = new List<int>();
            List<int> hori = new List<int>();

            for (int i = 0; i < input.Length; i++)
            {
                bool expand = true;
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '#')
                    {
                        expand = false;
                    }
                }

                if(expand)
                {
                    hori.Add(i);
                }
            }

            for (int i = 0; i < input[0].Length; i++)
            {
                bool expand = true;
                for (int j = 0; j < input.Length; j++)
                {
                    if (input[j][i] == '#')
                    {
                        expand = false;
                    }
                }
                if (expand)
                {
                    vert.Add(i);
                }
            }




            for (int i = 0; i < input.Length; i++)
            {
                for(int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] =='#')
                    {
                        locations.Add((i,j));
                    }
                }
            }
            
            for(int i = 0;i < locations.Count; i++)
            {
                for(int j = 0;j<locations.Count; j++) 
                {
                    int distance = 0;
                    if (i==j)
                    { }
                    else
                    {
                        distance += Math.Abs(locations[i].Item1 - locations[j].Item1);
                        distance += Math.Abs(locations[i].Item2 - locations[j].Item2);
                        
                        for(int a  = 0; a < hori.Count; a++)
                        {
                            if ((locations[i].Item1 > hori[a] && locations[j].Item1 < hori[a]) || (locations[i].Item1 < hori[a] && locations[j].Item1 > hori[a]))
                            {
                                distance += 1000000-1;
                            }
                        }

                        for (int a = 0; a < vert.Count; a++)
                        {
                            if ((locations[i].Item2 > vert[a] && locations[j].Item2 < vert[a]) || (locations[i].Item2 < vert[a] && locations[j].Item2 > vert[a]))
                            {
                                distance += 1000000-1;
                            }
                        }
                    }
                    
                    count += distance;
                }
            }

            Console.WriteLine(count/2);
            Console.ReadKey();
        }
    }
}
