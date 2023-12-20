using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace d5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");
            long count = 0;
            List<(char, string, List<string>)> modules = new List<(char, string, List<string>)>(); // type,name,destination modules
            Dictionary<string,bool> FlipFlops = new Dictionary<string,bool>();
            Dictionary<string, List<(string,bool)>> Conjunctions = new Dictionary<string, List<(string, bool)>>();


            for (int i = 0; i < input.Length; i++)
            {
                List<string> list = new List<string>();
                (char, string, List<string>) module = ('a', "a", list);

                if (input[i][0] == 'b')
                {
                    module.Item1 = 'b';
                    module.Item2 = "broadcaster";
                }
                else
                {
                    string[] temp = input[i].Split(' ');
                    module.Item1 = temp[0][0];
                    module.Item2 = temp[0].Substring(1);
                }

                string[] split1 = input[i].Split('>');
                string buffer = "";
                for(int j = 0; j < split1[1].Length; j++)
                {
                    if (char.IsLetter(split1[1][j]))
                    {
                        buffer += split1[1][j];
                    }
                    else if(buffer.Length>0)
                    {
                        list.Add(buffer);
                        buffer = "";
                    }
                }
                list.Add(buffer);
                module.Item3 = list;
                modules.Add(module);

                if(module.Item1 == '%')
                {
                    FlipFlops.Add(module.Item2, false);
                }
                else if(module.Item1 =='&')
                {
                    List<(string, bool)> pulses = new List<(string,bool)>();
                    Conjunctions.Add(module.Item2, pulses);
                }
            }

            foreach(var m in modules)
            {
                foreach(string s in m.Item3)
                {
                    if(Conjunctions.ContainsKey(s))
                    {
                        Conjunctions[s].Add((m.Item2,false));
                    }
                }   
            }
            // hf --> rx
            // nd, pc, vd, tx

            List<(int, int)> pulsesSent = new List<(int, int)>();
            int press = 0;
            bool loop = true; 
            while (loop)
            {
                Queue<(bool, string, string)> pulses = new Queue<(bool, string, string)>();
                broadcaster(ref pulses, modules[0]);
                press++;
                while (pulses.Count > 0)
                {
                    var pulse = pulses.Dequeue();
                    if(pulse.Item2 == "nd" && pulse.Item1 == false)
                    {
                        Console.WriteLine("nd " + press);
                    }
                    if (pulse.Item2 == "pc" && pulse.Item1 == false)
                    {
                        Console.WriteLine("pc " + press);
                    }
                    if (pulse.Item2 == "vd" && pulse.Item1 == false)
                    {
                        Console.WriteLine("vd " + press);
                    }
                    if (pulse.Item2 == "tx" && pulse.Item1 == false)
                    {
                        Console.WriteLine("tx " + press);
                    }
                    List<string> list = new List<string>();
                    (char, string, List<string>) module = ('a', "a", list);
                    foreach (var item in modules)
                    {
                        if (item.Item2 == pulse.Item2)
                        {
                            module = item; break;
                        }
                    }
                    if (module.Item1 == '%')
                    {
                        FlipFlop(ref FlipFlops, pulse.Item1, ref pulses, module);
                    }
                    else if (module.Item1 =='&')
                    {
                        Conjuction(ref Conjunctions, pulse, ref pulses, module);
                    }
                }
            }

            Console.ReadKey();
        }

        static void broadcaster(ref Queue<(bool, string, string)> pulses, (char, string, List<string>) module)
        {

            foreach(string s in module.Item3)
            {
                pulses.Enqueue((false, s, module.Item2));
            }

        }

        static void FlipFlop(ref Dictionary<string, bool> FlipFlops, bool pulseType, ref Queue<(bool, string, string)> pulses, (char, string, List<string>) module)
        {
            if (!pulseType)
            {
                FlipFlops[module.Item2] = !FlipFlops[module.Item2];
                if (FlipFlops[module.Item2])
                {
                    foreach (string s in module.Item3)
                    {
                        pulses.Enqueue((true, s, module.Item2));
                    }
                }
                else
                {
                    foreach (string s in module.Item3)
                    {
                        pulses.Enqueue((false, s, module.Item2));
                    }
                }
            }
        }

        static void Conjuction(ref Dictionary<string, List<(string,bool)>> Conjunctions, (bool, string, string) pulseType, ref Queue<(bool, string, string)> pulses, (char, string, List<string>) module)
        {
            var list = Conjunctions[pulseType.Item2];
            for (int i = 0; i < list.Count; i++)
            {
                var item = Conjunctions[pulseType.Item2][i];
                if (item.Item1 == pulseType.Item3)
                {
                    var buffer = list[i];
                    buffer.Item2 = pulseType.Item1;
                    list[i] = buffer;
                }
            }
            bool allHigh = true;
            foreach(var item in Conjunctions[pulseType.Item2])
            {
                if (item.Item2 == false)
                {
                    allHigh = false;
                    break;
                }
            }

            foreach (string s in module.Item3)
            {
                if (allHigh)
                {
                    pulses.Enqueue((false, s, module.Item2));
                }
                else
                {
                    pulses.Enqueue((true, s, module.Item2));
                }
            }
        }
    }
}
