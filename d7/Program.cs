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
            ulong count = 0;
            char[] buffer = { 'J','2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };
            List<char> cards = buffer.ToList();
            List<(string,int,int)> list = new List<(string,int, int)>();
            string[] array = input[0].Split(' ');
            list.Add((array[0], handValue(array[0], cards), int.Parse(array[1])));

            for (int i = 1; i < input.Length; i++)
            {
                string[] array1 = input[i].Split(' ');
                string hand = array1[0];
                int handPoints = handValue(array1[0], cards);
                (string, int, int) card = (hand, handPoints, int.Parse(array1[1]));
                bool added = false;

                for (int j = 0; j < list.Count; j++) 
                {
                    if (handPoints > list[j].Item2)
                    {
                        if (j == list.Count - 1)
                        {
                            list.Add(card);
                            j = 10000;
                            added = true;
                        }
                    }
                    else if (handPoints == list[j].Item2)
                    {
                        for (int x = 0; x < 5; x++)
                        {
                            if (cards.IndexOf(hand[x]) > cards.IndexOf(list[j].Item1[x]))
                            {
                                x = 5;
                            }
                            else if (cards.IndexOf(hand[x]) < cards.IndexOf(list[j].Item1[x]))
                            {

                                list.Insert(j, card);
                                added = true;
                                j = 10000;
                                x = 5;
                            }
                        } 
                    }
                    else if (handPoints < list[j].Item2)
                    {

                        list.Insert(j, card);
                        added = true;
                        j = 10000;                        
                    }
                    else if (j == list.Count - 1)
                    {
                        list.Add(card);
                        added = true;
                        j = 10000;
                    }
                }
                if(!added)
                {
                    list.Add(card);
                }
            }

            for(int i = 0; i < list.Count; i++)
            {
                count += (ulong)((i + 1) *list[i].Item3);
            }

            Console.WriteLine(count);
            Console.ReadKey();
        }

        static int handValue(string hand,List<char> cards)
        {
            int bestvalue = 0;
            for (int i = 0; i < cards.Count; i++)
            {
                string temp  =hand.Replace('J', cards[i]);
                int value = 0;

                char[] smaller = temp.Distinct().ToArray();


                if (smaller.Length == 1)
                {
                    return 6;
                }
                else if (smaller.Length == 4)
                {
                    value =  1;
                }
                else if (smaller.Length == 3)
                {
                    int same = 0;
                    foreach (char a in smaller)
                    {
                        char first = a;
                        int buffer = 0;
                        foreach (char c in temp)
                        {
                            if (c == first)
                                buffer++;
                        }
                        if (same < buffer)
                        {
                            same = buffer;
                        }
                    }
                    if (same == 3)
                    {
                        value = 3;
                    }
                    if (same == 2)
                    {
                        value = 2;
                    }
                }
                else if (smaller.Length == 2)
                {
                    int same = 0;
                    foreach (char a in smaller)
                    {
                        char first = a;
                        int buffer = 0;
                        foreach (char c in temp)
                        {
                            if (c == first)
                                buffer++;
                        }
                        if (same < buffer)
                        {
                            same = buffer;
                        }
                    }
                    if (same == 4)
                    {
                        value = 5;
                    }
                    if (same == 3)
                    {
                        value = 4;
                    }
                }
                if(value>bestvalue)
                {
                    bestvalue = value;
                }
            }
            
            return bestvalue;
        }
    }
}
