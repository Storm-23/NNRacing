using System;
using System.Collections.Generic;
using System.IO;
using GeneticNN_NS;

namespace NNRacingCore
{
    /// <summary>
    /// Trainer of GeneticNN for car racing
    /// </summary>
    public class Trainer
    {
        public static Trainer Instance;

        public Track Track;
        public List<Car> Cars = new List<Car>();
        public byte[] DefaultNNWeights;
        public int Generation = 0;
        public int MaxIterations = 5000;
        public GeneticNN GeneticNN;
        public int Population = 60;

        private int iteration;

        static Trainer()
        {
            if (Instance == null)
                Instance = new Trainer();
        }

        public bool IsAlive
        {
            get
            {
                foreach (var car in Cars)
                    if (car.IsAlive)
                        return true;

                return false;
            }
        }

        public void BuildFirstPopulation()
        {
            Cars.Clear();
            Generation = 0;

            //GeneticNN = new GeneticNN(new uint[] { 6, 5, 4, 3, 3 }, Population);
            GeneticNN = new GeneticNN(new uint[] {6, 6, 6, 3}, Population); // - best topology !
            GeneticNN.CrossSwapProb = 0.2f;
            GeneticNN.MutationProb = 0.3f;
            GeneticNN.MutationAmount = 2f;
            GeneticNN.MutationPerc = 0.8f;

            var pathPos = 0;

            for (var i = 0; i < GeneticNN.PopulationCount; i++)
            {
                var car = new Car(i);
                //load default weights
                if (DefaultNNWeights != null)
                    using (var str = new MemoryStream(DefaultNNWeights))
                    {
                        GeneticNN.GetNN(i).LoadWeightsSafe(str);
                    }
                Cars.Add(car);
                car.Reset(Track, false);
            }
        }

        public void Update(float dt)
        {
            iteration++;

            for (var i = 0; i < GeneticNN.PopulationCount; i++)
            {
                var car = Cars[i];
                UpdateCar(car, dt);
                //
                if (car.IsOutOfTrack)
                    car.IsAlive = false;
                if (iteration > MaxIterations || iteration > 200 && car.TotalPassedLength / iteration < 0.1)//0.03
                    car.IsAlive = false;
            }
        }

        public void UpdateCar(Car car, float dt)
        {
            var sensors = new float[GeneticNN.GetNN(car.Index).Layers[0].NeuronCount];
            Sensors.GetDistances(car, Track, sensors, 0);
            sensors[5] = car.Velocity.Length();
            var output = GeneticNN.GetNN(car.Index).ProcessInputs(sensors);
            var steering = output[1];

            car.Update(Track, 1, steering, output[2] > 0, dt);
        }

        public void BuildNextGeneration()
        {
            //SetEvaluation
            for (var i = 0; i < GeneticNN.PopulationCount; i++)
                GeneticNN.SetEvaluation(i, Cars[i].TotalPassedLength - Cars[i].Penalty);

            //BuildNextGeneration
            GeneticNN.BuildNextGeneration();

            //set random Adhesion
            Track.Adhesion = 1;

            //reset cars
            for (var i = 0; i < Cars.Count; i++)
                Cars[i].Reset(Track, false);

            iteration = 0;
            Generation++;
        }

        public void Run(int generationCount)
        {
            BuildFirstPopulation();
            BuildNextGeneration();

            while (true)
            {
                if (!IsAlive)
                {
                    if (Generation >= generationCount)
                        break;
                    BuildNextGeneration();
                }

                var dt = 0.02f;

                //update cars
                Update(dt);
            }
        }
    }
}