using System;
using System.Collections.Generic;

namespace GeneticNN_NS
{
    /// <summary>
    ///     The class represents population of NeuralNetworks and supports genetic algoritms to train NN
    /// </summary>
    public class GeneticNN
    {
        private static readonly Random randomizer = new Random();
        private readonly float[] Evaluations;
        private readonly NeuralNetwork[] NNs;

        /// <summary>
        ///     Create Genetic NN with given topology
        /// </summary>
        /// <param name="topology">Topology</param>
        /// <param name="populationCount">Population amount</param>
        /// <param name="activationFunctionType">Neuron activation function</param>
        /// <param name="initGenotype">Initial genotype (can be null)</param>
        public GeneticNN(uint[] topology, int populationCount,
            ActivationFunctionType activationFunctionType = ActivationFunctionType.SoftSignFunction,
            float[] initGenotype = null)
        {
            PopulationCount = populationCount;
            Topology = topology;
            ActivationFunctionType = activationFunctionType;
            NNs = new NeuralNetwork[populationCount];
            Evaluations = new float[populationCount];

            //create NN, init by random values
            for (var i = 0; i < populationCount; i++)
            {
                NNs[i] = new NeuralNetwork(activationFunctionType, topology);
                if (initGenotype != null)
                    NNs[i].SetWeights(initGenotype); //init by default weights (if presented)
                else
                    NNs[i].SetRandomWeights(InitParamMin, InitParamMax); //init by random weights
            }

            //build next generation based on default weights
            if (initGenotype != null)
            {
                for (var i = 0; i < populationCount; i++)
                    Evaluations[i] = 1;
                BuildNextGeneration();
            }
        }

        public int PopulationCount { get; }
        public uint[] Topology { get; private set; }

        /// <summary>
        ///     Default Activation function type
        /// </summary>
        public ActivationFunctionType ActivationFunctionType { get; private set; }

        #region Calc Fitness

        /// <summary>
        ///     Calculates the fitness of each genotype by the formula: fitness = evaluation / averageEvaluation.
        /// </summary>
        /// <param name="currentPopulation">The current population.</param>
        public static void DefaultFitnessCalculation(IEnumerable<Genotype> currentPopulation)
        {
            //First calculate average evaluation of whole population
            uint populationSize = 0;
            float overallEvaluation = 0;
            foreach (var genotype in currentPopulation)
            {
                overallEvaluation += genotype.Evaluation;
                populationSize++;
            }

            var averageEvaluation = overallEvaluation / populationSize;

            //Now assign fitness with formula fitness = evaluation / averageEvaluation
            foreach (var genotype in currentPopulation)
                if (Math.Abs(averageEvaluation) > float.Epsilon)
                    genotype.Fitness = genotype.Evaluation / averageEvaluation;
                else
                    genotype.Fitness = 1;
        }

        #endregion

        #region Default Parameters

        /// <summary>
        ///     Default min value of inital population parameters.
        /// </summary>
        public float InitParamMin = -1.0f;

        /// <summary>
        ///     Default max value of initial population parameters.
        /// </summary>
        public float InitParamMax = 1.0f;

        /// <summary>
        ///     Default probability of a parameter being swapped during crossover.
        /// </summary>
        public float CrossSwapProb = 0.6f;

        /// <summary>
        ///     Default probability of a parameter being mutated.
        /// </summary>
        public float MutationProb = 0.3f;

        /// <summary>
        ///     Default amount by which parameters may be mutated.
        /// </summary>
        public float MutationAmount = 2.0f;

        /// <summary>
        ///     Default percent of genotypes in a new population that are mutated.
        /// </summary>
        public float MutationPerc = 1.0f;

        #endregion

        #region Public methods

        /// <summary>
        ///     Return NeuarlNetwork by index of population member
        /// </summary>
        public NeuralNetwork GetNN(int index)
        {
            return NNs[index];
        }

        /// <summary>
        ///     Set evaluation by index of memeber
        /// </summary>
        public void SetEvaluation(int index, float evaluation)
        {
            Evaluations[index] = evaluation;
        }

        /// <summary>
        ///     Build next generation of NN
        /// </summary>
        public void BuildNextGeneration()
        {
            //build genotypes
            var genotypes = new List<Genotype>(PopulationCount);
            for (var i = 0; i < PopulationCount; i++)
                genotypes.Add(new Genotype(NNs[i].GetWeights()) {Evaluation = Evaluations[i]});

            //calc fitness
            DefaultFitnessCalculation(genotypes);

            //sort population
            genotypes.Sort();

            //Apply Selection
            var intermediatePopulation = RemainderStochasticSampling(genotypes);

            //Apply Recombination
            var newPopulation = RandomRecombination(intermediatePopulation, PopulationCount);

            //Apply Mutation
            MutateAllButBestTwo(newPopulation);

            //set weights to NN
            for (var i = 0; i < PopulationCount; i++)
                NNs[i].SetWeights(newPopulation[i]);
        }

        #endregion

        #region Selection Operators

        // Selection operator for the genetic algorithm, using a method called remainder stochastic sampling.
        private List<Genotype> RemainderStochasticSampling(List<Genotype> currentPopulation)
        {
            var intermediatePopulation = new List<Genotype>();
            //Put integer portion of genotypes into intermediatePopulation
            //Assumes that currentPopulation is already sorted
            foreach (var genotype in currentPopulation)
            {
                if (genotype.Fitness < 1)
                    break;
                for (var i = 0; i < (int) genotype.Fitness; i++)
                    intermediatePopulation.Add(new Genotype(genotype.GetParameterCopy()));
            }

            //Put remainder portion of genotypes into intermediatePopulation
            foreach (var genotype in currentPopulation)
            {
                var remainder = genotype.Fitness - (int) genotype.Fitness;
                if (randomizer.NextDouble() < remainder)
                    intermediatePopulation.Add(new Genotype(genotype.GetParameterCopy()));
            }

            return intermediatePopulation;
        }

        /// <summary>
        ///     Only selects the best three genotypes of the current population and copies them to the intermediate population.
        /// </summary>
        /// <param name="currentPopulation">The current population.</param>
        /// <returns>The intermediate population.</returns>
        public static List<Genotype> DefaultSelectionOperator(List<Genotype> currentPopulation)
        {
            var intermediatePopulation = new List<Genotype>();
            intermediatePopulation.Add(currentPopulation[0]);
            intermediatePopulation.Add(currentPopulation[1]);
            intermediatePopulation.Add(currentPopulation[2]);

            return intermediatePopulation;
        }

        #endregion

        #region Recombination Operators

        // Recombination operator for the genetic algorithm, recombining random genotypes of the intermediate population
        private List<Genotype> RandomRecombination(List<Genotype> intermediatePopulation, int newPopulationSize)
        {
            if (intermediatePopulation.Count < 2)
                return new List<Genotype>(intermediatePopulation);

            var newPopulation = new List<Genotype>();
            //Always add best two (unmodified)
            newPopulation.Add(intermediatePopulation[0]);
            newPopulation.Add(intermediatePopulation[1]);


            while (newPopulation.Count < newPopulationSize)
            {
                //Get two random indices that are not the same
                int randomIndex1 = randomizer.Next(0, intermediatePopulation.Count), randomIndex2;
                do
                {
                    randomIndex2 = randomizer.Next(0, intermediatePopulation.Count);
                } while (randomIndex2 == randomIndex1);

                Genotype offspring1, offspring2;
                CompleteCrossover(intermediatePopulation[randomIndex1], intermediatePopulation[randomIndex2],
                    CrossSwapProb, out offspring1, out offspring2);

                newPopulation.Add(offspring1);
                if (newPopulation.Count < newPopulationSize)
                    newPopulation.Add(offspring2);
            }

            return newPopulation;
        }

        /// <summary>
        ///     Simply crosses the first with the second genotype of the intermediate population until the new
        ///     population is of desired size.
        /// </summary>
        /// <param name="intermediatePopulation">The intermediate population that was created from the selection process.</param>
        /// <returns>The new population.</returns>
        private List<Genotype> DefaultRecombinationOperator(List<Genotype> intermediatePopulation, int newPopulationSize)
        {
            if (intermediatePopulation.Count < 2)
                return new List<Genotype>(intermediatePopulation);

            var newPopulation = new List<Genotype>();
            while (newPopulation.Count < newPopulationSize)
            {
                Genotype offspring1, offspring2;
                CompleteCrossover(intermediatePopulation[0], intermediatePopulation[1], CrossSwapProb, out offspring1,
                    out offspring2);

                newPopulation.Add(offspring1);
                if (newPopulation.Count < newPopulationSize)
                    newPopulation.Add(offspring2);
            }

            return newPopulation;
        }

        private static void CompleteCrossover(Genotype parent1, Genotype parent2, float swapChance,
            out Genotype offspring1, out Genotype offspring2)
        {
            //Initialise new parameter vectors
            var parameterCount = parent1.Count;
            float[] off1Parameters = new float[parameterCount], off2Parameters = new float[parameterCount];

            //Iterate over all parameters randomly swapping
            for (var i = 0; i < parameterCount; i++)
                if (randomizer.Next() < swapChance)
                {
                    //Swap parameters
                    off1Parameters[i] = parent2[i];
                    off2Parameters[i] = parent1[i];
                }
                else
                {
                    //Don't swap parameters
                    off1Parameters[i] = parent1[i];
                    off2Parameters[i] = parent2[i];
                }

            offspring1 = new Genotype(off1Parameters);
            offspring2 = new Genotype(off2Parameters);
        }

        #endregion

        #region Mutation Operators

        // Mutates all members of the new population with the default probability, while leaving the first 2 genotypes in the list untouched.
        private void MutateAllButBestTwo(List<Genotype> newPopulation)
        {
            for (var i = 2; i < newPopulation.Count; i++)
                if (randomizer.NextDouble() < MutationPerc)
                    MutateGenotype(newPopulation[i], MutationProb, MutationAmount);
        }

        /// <summary>
        ///     Simply mutates each genotype with the default mutation probability and amount.
        /// </summary>
        /// <param name="newPopulation">The mutated new population.</param>
        private void DefaultMutationOperator(List<Genotype> newPopulation)
        {
            foreach (var genotype in newPopulation)
                if (randomizer.NextDouble() < MutationPerc)
                    MutateGenotype(genotype, MutationProb, MutationAmount);
        }

        /// <summary>
        ///     Mutates the given genotype by adding a random value in range [-mutationAmount, mutationAmount] to each parameter
        ///     with a probability of mutationProb.
        /// </summary>
        /// <param name="genotype">The genotype to be mutated.</param>
        /// <param name="mutationProb">The probability of a parameter being mutated.</param>
        /// <param name="mutationAmount">A parameter may be mutated by an amount in range [-mutationAmount, mutationAmount].</param>
        private static void MutateGenotype(Genotype genotype, float mutationProb, float mutationAmount)
        {
            for (var i = 0; i < genotype.Count; i++)
                if (randomizer.NextDouble() < mutationProb)
                    genotype[i] += (float) (randomizer.NextDouble() * (mutationAmount * 2) - mutationAmount);
        }

        #endregion
    }
}