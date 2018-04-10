using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeneticNN_NS
{
    /// <summary>
    ///     Class representing one genotype of a population
    /// </summary>
    public class Genotype : List<float>, IComparable<Genotype>
    {
        #region Constructors

        /// <summary>
        ///     Instance of a new genotype with given parameter vector and initial fitness of 0.
        /// </summary>
        /// <param name="parameters">The parameter vector to initialise this genotype with.</param>
        public Genotype(float[] parameters) : base(parameters)
        {
            Fitness = 0;
            Evaluation = 0;
        }

        #endregion

        #region Members

        private static readonly Random randomizer = new Random();

        /// <summary>
        ///     The current evaluation of this genotype.
        /// </summary>
        public float Evaluation { get; set; }

        /// <summary>
        ///     The current fitness (e.g, the evaluation of this genotype relative
        ///     to the average evaluation of the whole population) of this genotype.
        /// </summary>
        public float Fitness { get; set; }

        #endregion

        #region Methods

        #region IComparable

        /// <summary>
        ///     Compares this genotype with another genotype depending on their fitness values.
        /// </summary>
        /// <param name="other">The genotype to compare this genotype with.</param>
        /// <returns>The result of comparing the two floating point values representing the genotypes fitness in reverse order.</returns>
        public int CompareTo(Genotype other)
        {
            return other.Fitness.CompareTo(Fitness); //in reverse order for larger fitness being first in list
        }

        #endregion

        /// <summary>
        ///     Sets the parameters of this genotype to random values in given range.
        /// </summary>
        /// <param name="minValue">The minimum inclusive value a parameter may be initialised with.</param>
        /// <param name="maxValue">The maximum exclusive value a parameter may be initialised with.</param>
        public void SetRandomParameters(float minValue, float maxValue)
        {
            //Check arguments
            if (minValue > maxValue) throw new ArgumentException("Minimum value may not exceed maximum value.");

            //Generate random parameter vector
            var range = maxValue - minValue;
            for (var i = 0; i < Count; i++)
                this[i] = (float) (randomizer.NextDouble() * range + minValue);
                    //Create a random float between minValue and maxValue
        }

        /// <summary>
        ///     Returns a copy of the parameter vector.
        /// </summary>
        public float[] GetParameterCopy()
        {
            var copy = new float[Count];
            CopyTo(copy, 0);
            return copy;
        }

        /// <summary>
        ///     Saves the parameters of this genotype to a file at given file path.
        /// </summary>
        /// <param name="filePath">The path of the file to save this genotype to.</param>
        /// <remarks>
        ///     This method will override existing files or attempt to create new files, if the file at given file path does
        ///     not exist.
        /// </remarks>
        public void SaveToFile(string filePath)
        {
            var builder = new StringBuilder();
            foreach (var param in this)
                builder.Append(param.ToString()).Append(";");

            builder.Remove(builder.Length - 1, 1);

            File.WriteAllText(filePath, builder.ToString());
        }

        #region Static Methods

        /// <summary>
        ///     Generates a random genotype with parameters in given range.
        /// </summary>
        /// <param name="parameterCount">The amount of parameters the genotype consists of.</param>
        /// <param name="minValue">The minimum inclusive value a parameter may be initialised with.</param>
        /// <param name="maxValue">The maximum exclusive value a parameter may be initialised with.</param>
        /// <returns>A genotype with random parameter values</returns>
        public static Genotype GenerateRandom(uint parameterCount, float minValue, float maxValue)
        {
            //Check arguments
            if (parameterCount == 0) return new Genotype(new float[0]);

            var randomGenotype = new Genotype(new float[parameterCount]);
            randomGenotype.SetRandomParameters(minValue, maxValue);

            return randomGenotype;
        }

        /// <summary>
        ///     Loads a genotype from a file with given file path.
        /// </summary>
        /// <param name="filePath">The path of the file to load the genotype from.</param>
        /// <returns>The genotype loaded from the file at given file path.</returns>
        public static Genotype LoadFromFile(string filePath)
        {
            var data = File.ReadAllText(filePath);

            var parameters = new List<float>();
            var paramStrings = data.Split(';');

            foreach (var parameter in paramStrings)
            {
                float parsed;
                if (!float.TryParse(parameter, out parsed))
                    throw new ArgumentException(
                        "The file at given file path does not contain a valid genotype serialisation.");
                parameters.Add(parsed);
            }

            return new Genotype(parameters.ToArray());
        }

        #endregion

        #endregion
    }
}