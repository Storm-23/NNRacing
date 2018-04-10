using System;

namespace GeneticNN_NS
{
    /// <summary>
    ///     Class representing a single layer of a fully connected feedforward neural network.
    /// </summary>
    public class NeuralLayer
    {
        #region Constructors

        /// <summary>
        ///     Initialises a new neural layer for a fully connected feedforward neural network with given
        ///     amount of node and with connections to the given amount of nodes of the next layer.
        /// </summary>
        /// <param name="nodeCount">The amount of nodes in this layer.</param>
        /// <param name="outputCount">The amount of nodes in the next layer.</param>
        /// <remarks>All weights of the connections from this layer to the next are initialised with the default double value.</remarks>
        public NeuralLayer(uint nodeCount, uint outputCount)
        {
            NeuronCount = nodeCount;
            OutputCount = outputCount;

            Weights = new float[nodeCount + 1, outputCount]; // + 1 for bias node
        }

        #endregion

        #region Members

        private static readonly Random randomizer = new Random();

        /// <summary>
        ///     The activation function used by the neurons of this layer.
        /// </summary>
        /// <remarks>The default activation function is the sigmoid function (see <see cref="SigmoidFunction" />).</remarks>
        public ActivationFunctionType NeuronActivationFunctionType = ActivationFunctionType.SigmoidFunction;

        /// <summary>
        ///     The amount of neurons in this layer.
        /// </summary>
        public uint NeuronCount { get; }

        /// <summary>
        ///     The amount of neurons this layer is connected to, i.e., the amount of neurons of the next layer.
        /// </summary>
        public uint OutputCount { get; }

        /// <summary>
        ///     The weights of the connections of this layer to the next layer.
        ///     E.g., weight [i, j] is the weight of the connection from the i-th weight
        ///     of this layer to the j-th weight of the next layer.
        /// </summary>
        public float[,] Weights { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Processes the given inputs using the current weights to the next layer.
        /// </summary>
        /// <param name="inputs">The inputs to be processed.</param>
        /// <returns>The calculated outputs.</returns>
        public float[] ProcessInputs(float[] inputs)
        {
            //Check arguments
            if (inputs.Length != NeuronCount)
                throw new ArgumentException("Given xValues do not match layer input count.");

            //Calculate sum for each neuron from weighted inputs and bias
            var sums = new float[OutputCount];

            for (var j = 0; j < OutputCount; j++)
            {
                float sum = 0;
                for (var i = 0; i < inputs.Length; i++)
                    sum += inputs[i] * Weights[i, j];

                //Add bias (always on) neuron to inputs
                sum += 1 * Weights[NeuronCount, j];

                sums[j] = sum;
            }

            //Apply activation function to sum
            switch (NeuronActivationFunctionType)
            {
                case ActivationFunctionType.SigmoidFunction:
                    for (var i = 0; i < sums.Length; i++)
                        sums[i] = SigmoidFunction(sums[i]);
                    break;

                case ActivationFunctionType.TanHFunction:
                    for (var i = 0; i < sums.Length; i++)
                        sums[i] = TanHFunction(sums[i]);
                    break;

                case ActivationFunctionType.SoftSignFunction:
                    for (var i = 0; i < sums.Length; i++)
                        sums[i] = SoftSignFunction(sums[i]);
                    break;

                case ActivationFunctionType.Identity:
                    break;
            }

            return sums;
        }

        /// <summary>
        ///     Copies this NeuralLayer including its weights.
        /// </summary>
        /// <returns>A deep copy of this NeuralLayer</returns>
        public NeuralLayer DeepCopy()
        {
            //Copy weights
            var copiedWeights = new float[Weights.GetLength(0), Weights.GetLength(1)];

            for (var x = 0; x < Weights.GetLength(0); x++)
            for (var y = 0; y < Weights.GetLength(1); y++)
                copiedWeights[x, y] = Weights[x, y];

            //Create copy
            var newLayer = new NeuralLayer(NeuronCount, OutputCount);
            newLayer.Weights = copiedWeights;
            newLayer.NeuronActivationFunctionType = NeuronActivationFunctionType;

            return newLayer;
        }

        /// <summary>
        ///     Sets the weights of the connection from this layer to the next to random values in given range.
        /// </summary>
        /// <param name="minValue">The minimum value a weight may be set to.</param>
        /// <param name="maxValue">The maximum value a weight may be set to.</param>
        public void SetRandomWeights(float minValue, float maxValue)
        {
            double range = Math.Abs(minValue - maxValue);
            for (var i = 0; i < Weights.GetLength(0); i++)
            for (var j = 0; j < Weights.GetLength(1); j++)
                Weights[i, j] = minValue + (float) (randomizer.NextDouble() * range);
                    //random double between minValue and maxValue
        }

        /// <summary>
        ///     Returns a string representing this layer's connection weights.
        /// </summary>
        public override string ToString()
        {
            var output = "";

            for (var x = 0; x < Weights.GetLength(0); x++)
            {
                for (var y = 0; y < Weights.GetLength(1); y++)
                    output += "[" + x + "," + y + "]: " + Weights[x, y];

                output += "\n";
            }

            return output;
        }

        #endregion

        #region Activation Functions

        /// <summary>
        ///     The standard sigmoid function.
        /// </summary>
        /// <param name="xValue">The input value.</param>
        /// <returns>The calculated output.</returns>
        public static float SigmoidFunction(float xValue)
        {
            if (xValue > 10) return 1.0f;
            if (xValue < -10) return 0.0f;
            return 1.0f / (1.0f + (float) Math.Exp(-xValue));
        }

        /// <summary>
        ///     The standard TanH function.
        /// </summary>
        /// <param name="xValue">The input value.</param>
        /// <returns>The calculated output.</returns>
        public static float TanHFunction(float xValue)
        {
            if (xValue > 10) return 1.0f;
            if (xValue < -10) return -1.0f;
            return (float) Math.Tanh(xValue);
        }

        /// <summary>
        ///     The SoftSign function as proposed by Xavier Glorot and Yoshua Bengio (2010):
        ///     "Understanding the difficulty of training deep feedforward neural networks".
        /// </summary>
        /// <param name="xValue">The input value.</param>
        /// <returns>The calculated output.</returns>
        public static float SoftSignFunction(float xValue)
        {
            return xValue / (1 + Math.Abs(xValue));
        }

        #endregion
    }

    [Serializable]
    public enum ActivationFunctionType
    {
        Identity,
        SigmoidFunction,
        TanHFunction,
        SoftSignFunction
    }
}