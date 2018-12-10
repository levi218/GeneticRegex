using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GeneticRegex
{
    class Expression
    {
        private static Random random = new Random();

        private List<Vocab> _exp;
        public string content;
        public List<Vocab> Exp
        {
            get
            {
                return _exp;
            }
            set
            {
                _exp = value;
            }
        }
        public int fitness;

        public Expression()
        {
            this.Exp = new List<Vocab>();
        }

        public void PutRandom()
        {
            if (Vocab.vocabs != null)
                Exp.Add(Vocab.Random());
        }

        public string CompileExp()
        {
            string result = "";
            for (int i = Exp.Count - 1; i >= 0; i--)
            {
                if (i == Exp.Count - 1 || !Exp[i].Equals(Exp[i + 1]))
                    result += Exp[i].Compile();
                else
                {
                    Exp.RemoveAt(i);
                }
            }
            return result;
        }
        public void CalculateFitness(string[] tests)
        {
            int fitness = 0;
            foreach (string test in tests)
            {
                if (test.Length < Exp.Count)
                {
                    this.fitness = 0;
                    return;
                }
                try
                {
                    Match m = Regex.Match(test, CompileExp());
                    fitness += m.Length;
                }
                catch (Exception ex) { }
            }

            foreach (Vocab v in Exp)
            {
                this.fitness += v.Complexity;
            }
            this.fitness -= Exp.Count * Exp.Count * Exp.Count;
            if (this.fitness < 0) this.fitness = 0;
            this.fitness = fitness;
        }
        public void Mutate()
        {
            int rand = random.Next(1000);
            int[] rates = { 10, 140, 15 };
            if (rand < rates[0])
            {
                Exp.Add(Vocab.Random());
            }
            else
            if (rand < rates[0] + rates[1])
            {
                if (Exp.Count > 1)
                    Exp.RemoveAt(random.Next(0, Exp.Count));
            }
            else
            if (rand < rates[0] + rates[1] + rates[2])
            {
                int index = random.Next(0, Exp.Count);
                Exp.RemoveAt(index);
                Exp.Insert(index, Vocab.Random());
            }

        }
        public Expression Crossover(Expression other)
        {
            Expression child = new Expression();
            for (int i = 0; i < this.Exp.Count; i++)
            {
                int rand = random.Next(5);
                if (rand <2)
                {
                    child.Exp.Add(this.Exp[i]);
                }
                else if (rand < 4)
                {
                    if (i < other.Exp.Count) child.Exp.Add(other.Exp[i]);
                }
            }
            child.Exp.Add(Vocab.Random());
            return child;
        }


    }
}
