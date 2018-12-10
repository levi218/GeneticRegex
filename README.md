# GeneticRegex
An attempt to generate a regex (regular expression) that matches a set of examples with genetic algorithm

For that purpose, I used the set of examples as the "destination", or "references" to calculate the fitness values
(The fitness function itself needs much improvement)
And then perform selection and mutation, etc.. according to the genetic algorithm's description
The results after a few generations is a set of regexs, each of which matches the input set (some regex might look silly, ridiculous long or somewhat, not well-constrained, but serves the purposes)

Possible improvement (feel free to make a pull requests or put an idea as an issues topic)
- Fitness function
- Create a scale of "complexity" or degree of constrainment to filter out/sort the set of results
- Allow another set of examples to exclude matches

This project is a test, a trial project I wanted to make after reading the book Nature of Code from Daniel Shiffman (https://natureofcode.com/)

Language: C#
