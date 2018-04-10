using System;
using System.Collections.Generic;
using System.IO;

namespace GeneticNN_NS
{
    /// <summary>
    ///     Class representing a fully connected feedforward neural network.
    /// </summary>
    public class NeuralNetwork
    {
        #region Constructors

        /// <summary>
        ///     Initialises a new fully connected feedforward neural network with given topology.
        /// </summary>
        /// <param name="topology">
        ///     An array of unsigned integers representing the node count of each layer from input to output
        ///     layer.
        /// </param>
        public NeuralNetwork(ActivationFunctionType activationFunction, params uint[] topology)
        {
            Topology = topology;

            //Calculate overall weight count
            WeightCount = 0;
            for (var i = 0; i < topology.Length - 1; i++)
                WeightCount += (int) ((topology[i] + 1) * topology[i + 1]); // + 1 for bias node

            //Initialise layers
            Layers = new NeuralLayer[topology.Length - 1];
            for (var i = 0; i < Layers.Length; i++)
                Layers[i] = new NeuralLayer(topology[i], topology[i + 1])
                {
                    NeuronActivationFunctionType = activationFunction
                };
        }

        #endregion

        #region Members

        /// <summary>
        ///     The individual neural layers of this network.
        /// </summary>
        public NeuralLayer[] Layers { get; }

        /// <summary>
        ///     An array of unsigned integers representing the node count
        ///     of each layer of the network from input to output layer.
        /// </summary>
        public uint[] Topology { get; }

        /// <summary>
        ///     The amount of overall weights of the connections of this network.
        /// </summary>
        public int WeightCount { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Processes the given inputs using the current network's weights.
        /// </summary>
        /// <param name="inputs">The inputs to be processed.</param>
        /// <returns>The calculated outputs.</returns>
        public float[] ProcessInputs(float[] inputs)
        {
            //Check arguments
            if (inputs.Length != Layers[0].NeuronCount)
                throw new ArgumentException("Given inputs do not match network input amount.");

            //Process inputs by propagating values through all layers
            var outputs = inputs;
            foreach (var layer in Layers)
                outputs = layer.ProcessInputs(outputs);

            return outputs;
        }

        public void SetWeights(IList<float> weights)
        {
            //Check if topology is valid
            if (WeightCount != weights.Count)
                throw new ArgumentException(
                    "The given genotype's parameter count must match the neural network topology's weight count.");

            var iWeight = 0;
            foreach (var layer in Layers) //Loop over all layers
            {
                var c0 = layer.Weights.GetLength(0);
                var c1 = layer.Weights.GetLength(1);
                for (var i = 0; i < c0; i++) //Loop over all nodes of current layer
                for (var j = 0; j < c1; j++) //Loop over all nodes of next layer
                    layer.Weights[i, j] = weights[iWeight++];
            }
        }

        public float[] GetWeights()
        {
            var weights = new float[WeightCount];

            var iWeight = 0;
            foreach (var layer in Layers) //Loop over all layers
            {
                var c0 = layer.Weights.GetLength(0);
                var c1 = layer.Weights.GetLength(1);
                for (var i = 0; i < c0; i++) //Loop over all nodes of current layer
                for (var j = 0; j < c1; j++) //Loop over all nodes of next layer
                    weights[iWeight++] = layer.Weights[i, j];
            }

            return weights;
        }

        /// <summary>
        ///     Sets the weights of this network to random values in given range.
        /// </summary>
        /// <param name="minValue">The minimum value a weight may be set to.</param>
        /// <param name="maxValue">The maximum value a weight may be set to.</param>
        public void SetRandomWeights(float minValue, float maxValue)
        {
            if (Layers != null)
                foreach (var layer in Layers)
                    layer.SetRandomWeights(minValue, maxValue);
        }

        /// <summary>
        ///     Returns a new NeuralNetwork instance with the same topology and
        ///     activation functions, but the weights set to their default value.
        /// </summary>
        public NeuralNetwork GetTopologyCopy()
        {
            var copy = new NeuralNetwork(ActivationFunctionType.Identity, Topology);

            for (var i = 0; i < Layers.Length; i++)
                copy.Layers[i].NeuronActivationFunctionType = Layers[i].NeuronActivationFunctionType;

            return copy;
        }

        /// <summary>
        ///     Copies this NeuralNetwork including its topology and weights.
        /// </summary>
        /// <returns>A deep copy of this NeuralNetwork</returns>
        public NeuralNetwork DeepCopy()
        {
            var newNet = new NeuralNetwork(ActivationFunctionType.Identity, Topology);

            for (var i = 0; i < Layers.Length; i++)
                newNet.Layers[i] = Layers[i].DeepCopy();

            return newNet;
        }

        /// <summary>
        ///     Returns a string representing this network in layer order.
        /// </summary>
        public override string ToString()
        {
            var output = "";

            for (var i = 0; i < Layers.Length; i++)
                output += "Layer " + i + ":\n" + Layers[i];

            return output;
        }

        public void SaveWeights(Stream stream)
        {
            var bw = new BinaryWriter(stream);
            //version
            bw.Write((byte) 0);
            //number of layers
            bw.Write(Layers.Length);
            //topology
            for (var i = 0; i < Topology.Length; i++)
                bw.Write(Topology[i]);
            //weights
            foreach (var w in GetWeights())
                bw.Write(w);
            //
            bw.Flush();
        }

        public void LoadWeightsSafe(Stream stream)
        {
            var bw = new BinaryReader(stream);
            //version
            bw.ReadByte();
            //number of layers
            var layerCount = bw.ReadUInt32();
            //topology
            var topology = new uint[layerCount + 1];
            for (var i = 0; i < topology.Length; i++)
                topology[i] = bw.ReadUInt32();
            //weight count
            var weightCount = 0u;
            for (var i = 0; i < topology.Length - 1; i++)
                weightCount += (topology[i] + 1) * topology[i + 1];

            //read weights
            var counter = 0;
            for (var iLayer = 0; iLayer < layerCount; iLayer++)
            {
                var c0 = topology[iLayer] + 1;
                var c1 = topology[iLayer + 1];
                for (var i = 0; i < c0; i++) //Loop over all nodes of current layer
                for (var j = 0; j < c1; j++) //Loop over all nodes of next layer
                {
                    var w = bw.ReadSingle();
                    counter++;

                    if (iLayer < Layers.Length)
                    {
                        var layer = Layers[iLayer];
                        if (i < layer.Weights.GetLength(0) && j < layer.Weights.GetLength(1))
                            if (i == c0 - 1) //bias ?
                                layer.Weights[layer.Weights.GetLength(0) - 1, j] = w;
                            else
                                layer.Weights[i, j] = w;
                    }
                }
            }
        }

        #endregion
    }
}