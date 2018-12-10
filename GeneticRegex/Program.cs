using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace GeneticRegex
{
    class Program
    {
        static void Main()
        {
            string[] destination = { "123@gmail.com", "me@yahoo.com", "abc.de@live.com", "abcfsdf.de@live.com" };
            List<string> dic = getCommon(destination);
            foreach (string s in dic)
            {
                Console.WriteLine(s);
            }
            dic.AddRange(new string[] { "\\d", "\\w", "\\s" });
            Vocab.vocabs = dic.ToArray();
            GeneticPopulation geneticPopulation = new GeneticPopulation(destination);
            while (true)
            {
                geneticPopulation.NextGeneration();
                Thread.Sleep(500);
            }
        }

        static List<string> getCommon(string[] source)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < source[0].Length - 1; i++)
            {
                for (int j = 1; j < source[0].Length - i; j++)
                {
                    string reference = source[0].Substring(i, j);
                    if (result.Contains(reference)) continue;
                    bool valid = true;
                    for (int k = 1; k < source.Length; k++)
                    {
                        if (!source[k].Contains(reference))
                        {
                            valid = false;
                            break;
                        }
                    }

                    if (valid) result.Add(reference.Replace(".", "\\."));
                }
            }
            return result;
        }
    }



}
