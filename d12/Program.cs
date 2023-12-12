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
            List<string> gears = new List<string>();
            List<string> combos = new List<string>();

            for (int i = 0; i < input.Length; i++)
            {
                string[] split = input[i].Split(' ');
                gears.Add(split[0] + "?" + split[0] + "?" + split[0] + "?" + split[0] + "?" + split[0]);
                combos.Add(split[1] + "," + split[1] + "," + split[1] + "," + split[1] + "," + split[1]);                
            }
            Dictionary<(string, string), long> done = new Dictionary<(string, string), long>();
            for (int i = 0; i < gears.Count; i++)
            {
                count += possibilities(gears[i], combos[i], done);
            }

            
            Console.WriteLine(count);
            Console.ReadKey();
        }
        static long possibilities(string s, string nums, Dictionary<(string, string), long> done)
        {
            if (done.ContainsKey((s, nums)))
                return done[(s, nums)];

            string[] strings = nums.Split(',');
            
            if(s.Length==0)
            {
                if (nums.Length == 0)
                    return 1;
                else
                    return 0;
            }

            if(nums.Length == 0) 
            {
                if (s.Contains("#"))
                    return 0;
                else
                    return 1;
                
            }
            

            List<int> lengths = new List<int>();
            foreach (string st in strings)
            {
                lengths.Add(int.Parse(st));
            }

            long permitations = 0;

            if (s[0] == '.' || s[0] == '?') 
            {
                permitations += possibilities(s.Substring(1), nums, done);
            }
              
            if (s[0] == '#' || s[0] == '?')
            {
                if (lengths[0] <= s.Length && !s.Substring(0, lengths[0]).Contains('.') && (lengths[0] == s.Length|| s[lengths[0]] == '.' || s[lengths[0]] == '?'))
                {
                    string buffer = "";
                    for(int i = 1; i < lengths.Count; i++) 
                    {
                        buffer += lengths[i];
                        if(i != lengths.Count - 1)
                        {
                            buffer += ",";
                        }
                    }
                    if (s.Length > lengths[0] + 1)
                    {
                        permitations += possibilities(s.Substring(lengths[0] + 1), buffer, done);
                    }
                    else
                    {
                        permitations += possibilities("", buffer, done);
                    }
                }
            }

            done.Add((s, nums), permitations);
            return permitations;
        }
    }
}
 