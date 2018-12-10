using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticRegex
{
    public class Vocab
    {
        private static Random random = new Random();
        public static string[] quantifiers = {"", "+", "*", "?" };
        public static string[] vocabs = null;
        public string content;
        public int quantifier;
        
        public int Complexity
        {
            get
            {
                return (this.quantifier == 0 ? 20 : 0)
                    + ((this.content == "\\w" || this.content == "\\d" || this.content == "\\s") ? 0 : 10);
            }
        }

        public Vocab(string content, int quantifier)
        {
            this.content = content;
            this.quantifier = quantifier;
        }
        public static Vocab Random() {
            Vocab result = null;
            if (vocabs!=null)
                result = new Vocab(vocabs[random.Next(vocabs.Length)],random.Next(quantifiers.Length));
            
            return result;
        }
        public string Compile() {
            return content + quantifiers[quantifier];
        }
        public override bool Equals(object obj)
        {
            return obj.GetType().Equals(this.GetType()) && this.content.Equals(((Vocab)obj).content);
        }

    }
}
