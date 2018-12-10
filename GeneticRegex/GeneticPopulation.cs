using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticRegex
{
    class GeneticPopulation
    {
        private static Random random = new Random();

        int popSize = 500;
        List<Expression> population;
        string[] destination;
        int maxFitness;
        List<Expression> max;
        int generation;
        int totalFitness;
        public GeneticPopulation(string[] destination)
        {
            this.destination = destination;
            population = new List<Expression>();
            maxFitness = 0;
            max = new List<Expression>();
            generation = 0;
        }

        public void NextGeneration()
        {
            generation++;
            Console.Clear();
            Console.WriteLine("Generating new generation...");
            Selection();

            Console.Clear();
            Console.WriteLine("Evaluating...");
            EvaluatingFitness();

            PrintSummary();
        }

        void PrintSummary()
        {
            Console.Clear();
            int lengthMax = -99999;
            int lengthMin = 99999;

            for (int i = 0; i < popSize; i++)
            {
                Console.Write(population[i].CompileExp() + "\t\t\t");
                if (population[i].Exp.Count < lengthMin) lengthMin = population[i].Exp.Count;
                if (population[i].Exp.Count > lengthMax) lengthMax = population[i].Exp.Count;
            }
            Console.WriteLine();
            Console.WriteLine("Gen: " + generation + "\tTotalFitness: " + totalFitness + "\t\tFitnessMax: " + maxFitness
                + "\t\tLengthMin: " + lengthMin + "\t\tLengthMax: " + lengthMax);
            Console.Write("Best: ");
            foreach (Expression exp in max)
            {
                Console.Write(exp.CompileExp() + "\t\t\t");
            }
            Console.WriteLine();
        }

        void Selection()
        {
            // init population/selection and crossover
            List<Expression> newPop = new List<Expression>();
            if (population.Count == 0)
            {
                for (int i = 0; i < popSize; i++)
                {
                    Expression exp = new Expression();
                    exp.PutRandom();
                    newPop.Add(exp);
                }
            }
            else
            {
                population.Sort((a, b) => b.fitness - a.fitness);
                for (int i = 0; i < popSize; i++)
                {
                    //Crossover
                    Expression parent1 = SelectRandom();
                    Expression parent2 = SelectRandom();

                    Expression child = parent1.Crossover(parent2);
                    child.Mutate();
                    newPop.Add(child);
                }
            }
            population = newPop;
        }

        Expression SelectRandom()
        {
            int val = random.Next(totalFitness);
            int i = 0;
            while (val > 0 && i < population.Count)
            {
                if (val < population[i].fitness)
                {
                    return population[i];
                }
                else
                {
                    val -= population[i].fitness;
                }
                i++;
            }
            Expression fallback = new Expression();
            fallback.PutRandom();
            return fallback;
        }
        void EvaluatingFitness()
        {
            totalFitness = 0;
            for (int i = 0; i < popSize; i++)
            {
                population[i].CalculateFitness(destination);
                totalFitness += population[i].fitness;
                if (population[i].fitness == maxFitness)
                {
                    if (!max.Exists((x) => x.CompileExp().Equals(population[i].CompileExp())))
                        max.Add(population[i]);

                }
                else
                if (population[i].fitness > maxFitness)
                {
                    maxFitness = population[i].fitness;
                    max = new List<Expression>();
                    max.Add(population[i]);
                }
            }
        }
    }
}
