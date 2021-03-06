﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MachineSharpLibrary
{
   public class LMMCNet : INetBase
    {
        public Net Net;
        protected override Activations ActivationsFunction { get; set; }
        public int NumberOfInputs { get; private set; }
        public int NumberOfHiddenLayers { get; private set; }
        public int[] NeuronsPerHiddenLayer { get; private set; }
        public int NumberOfOutputs { get; private set; }
        public Random _localRandom { get; set; }

        protected override void InitNet()
        {
           
        }

        public LMMCNet(int numberOfInputs, int numberOfHiddenLayers, int[] neuronsPerHiddenLayer, int numberOfOutputs, Random rnd = null)
        : this(numberOfInputs, numberOfHiddenLayers,  neuronsPerHiddenLayer,  numberOfOutputs, Activations.Sigmoid, 0.1, rnd)
        {
            //constructor with no activation function specified & default learning rate
        }

        public LMMCNet(int numberOfInputs, int numberOfHiddenLayers, int[] neuronsPerHiddenLayer, int numberOfOutputs, 
            Activations activations, double InitLearningRate, Random rnd = null)
        {
            NumberOfInputs = numberOfInputs;
            NumberOfHiddenLayers = numberOfHiddenLayers;
            NeuronsPerHiddenLayer = neuronsPerHiddenLayer;
            NumberOfOutputs = numberOfOutputs;
            Net = new Net();
            ActivationsFunction = activations;
            LearningRate = InitLearningRate;

            bool MakeRandom = (rnd != null);

            //TOOD:  Add exception handling for valid funcs

            if (MakeRandom)
            {
                _localRandom = rnd;
            }
            
            Exception_CheckValidState();

            
            if (numberOfHiddenLayers == 0)
            {
                Net.Add(MakeLayer(NumberOfInputs, numberOfInputs, MakeRandom));
            }
            else
            {
                //Input layer
                Net.Add(MakeLayer(NumberOfInputs, NeuronsPerHiddenLayer[0], MakeRandom));
                //HiddenLayers
                for (int i = 0; i < numberOfHiddenLayers - 1; i++)
                {
                    Net.Add(MakeLayer(neuronsPerHiddenLayer[i], neuronsPerHiddenLayer[i + 1], MakeRandom));
                }
                Net.Add(MakeLayer(neuronsPerHiddenLayer.Last(), numberOfOutputs, MakeRandom));
            }
                //OutputLayer
            Net.Add(MakeLayer(numberOfOutputs, 0, MakeRandom));
            
        }

        public List<Neuron> MakeLayer(int NeuronsInThisLayer, int NeuronsInNextLayer, bool MakeRandom)
        {
            var ReturnList = new List<Neuron>();
            if (!MakeRandom)
            {
                for (int i = 0; i < NeuronsInThisLayer; i++)
                {
                    ReturnList.Add(new Neuron(NeuronsInNextLayer));
                }
            }
            else
            {
                for (int i = 0; i < NeuronsInThisLayer; i++)
                {
                    ReturnList.Add(new Neuron(NeuronsInNextLayer, _localRandom));
                }
            }
            return ReturnList;
        }


        public override double[] Predict (double[] Inputs)
        {
            //check valid input count
            Exception_Predict(Inputs.Count(), this.NumberOfInputs);
            //Set inputs
            for (int i = 0; i < NumberOfInputs; i++)
            {
                Net[0][i].OutValue = Inputs[i];
            }

            for (int LayerNumber = 1; LayerNumber < Net.Count(); LayerNumber++)
            {

                //reset each neuron
                

                for (int Neuron = 0; Neuron < Net[LayerNumber].Count(); Neuron++)
                {
                    ResetNeuron(LayerNumber, Neuron);
                    double sum = 0;
                    foreach(Neuron N in Net[LayerNumber - 1])
                    {
                        sum += (N.OutValue * N.WeightsOut[Neuron]);
                    }
                    Net[LayerNumber][Neuron].OutValue = Activation(sum,Activations.Sigmoid);
                }
            }

            double[] Outputs = new double[NumberOfOutputs];
            for(int N = 0; N < NumberOfOutputs; N++)
            {
                Outputs[N] = Net[Net.Count() - 1][N].OutValue;
            }

            //Thread.Sleep(5000);

            return Outputs;
            
        }

        public void ResetNeuron(int LayerNumber, int NeuronNumber)
        {
            Net[LayerNumber][NeuronNumber].OutValue = Net[LayerNumber][NeuronNumber].Bias;
        }

        /// <summary>
        /// Adds a neuron to the end of a layer
        /// </summary>
        /// <param name="LayerNumber">Which layer to add the neuron to, 0 indexed, layer 0 = input layer, Net.Count() - 1 = output layer </param>  
        public void AddNeuron(int LayerNumber)
        {
            if(LayerNumber >= Net.Count() || LayerNumber < 0)
            {
                throw new InvalidOperationException(("Layer " + LayerNumber + " Does not exist, largest index of this net is " + (Net.Count() - 1)));
            }

            //Add to input layer
            if (LayerNumber == 0)
            {
                Net[0].Add(new Neuron(Net[1].Count));
                NumberOfInputs++;
            }

            else { 
            //Add to Output layer
                 if (LayerNumber == Net.Count() - 1)
                {
                    Net[LayerNumber].Add(new Neuron(0));
                    NumberOfOutputs++;
                }
                else
                {
                    Net[LayerNumber].Add(new Neuron(Net[LayerNumber + 1].Count));
                }

                //adjust weights of previous layer
                BackAdjustWeights(LayerNumber - 1, Net[LayerNumber].Count - 1);
            }

           
        }

        /// <summary>
        /// Remove a neuron from a layer
        /// </summary>
        /// <param name="LayerNumber">Which layer to remove the neuron from, 0 indexed </param>
        /// <param name="NeuronNumber"></param>
        public void RemoveNeuron(int LayerNumber, int NeuronNumber)
        {
            if (LayerNumber >= Net.Count() || LayerNumber < 0)
            {
                throw new InvalidOperationException(("Layer " + LayerNumber + " Does not exist, largest index of this net is " + (Net.Count() - 1)));
            }

            if (Net[LayerNumber].Count <= NeuronNumber || NeuronNumber < 0)
            {
                throw new InvalidOperationException("Layer has " + Net[LayerNumber].Count + " Neurons, you asked to remove " + NeuronNumber);
            }

            if (Net[LayerNumber].Count == 1)
            {
                throw new InvalidOperationException("Layer has " + Net[LayerNumber].Count + " Neuron, In order to remove the layer, please call RemoveLayer");
            }

            var newLayer = new List<Neuron>();
            int indexer = 0;
            foreach (Neuron N in Net[LayerNumber])
            {
                if (indexer != NeuronNumber)
                {
                    newLayer.Add(N);
                }
                indexer++;
            }
            Net[LayerNumber] = newLayer;

            //adjust weightsout array of previous layer
            if (LayerNumber != 0)
            {
                BackAdjustWeights(LayerNumber - 1, NeuronNumber);
            }

            if (LayerNumber == 0)
            {
                NumberOfInputs--;
            }

            if (LayerNumber == Net.Count() - 1)
            {
                NumberOfOutputs--;
            }
        }

       /// <summary>
       /// Add a layer to the net with a set number of Neurons
       /// </summary>
       /// <param name="LayerNumber">Index the new layer will occupy, e.g. "1" will mean the layer added will be the 
       /// first hidden layer, every other layer is moved 1 up, cannot be 0 or the same as the output layer
       /// </param>
       /// <param name="NeuronsInNewLayer">How many neurons the new layer should occupy</param>
        public void AddLayer(int LayerNumber, int NeuronsInNewLayer, bool backAdjustNeeded)
        {
            Exception_AddLayer(LayerNumber, NeuronsInNewLayer);
            List<Neuron> newLayer = MakeLayer(NeuronsInNewLayer, Net[LayerNumber].Count, true);
            Net.Insert(newLayer, LayerNumber, backAdjustNeeded);
        }

        /// <summary>
        /// Removes a layer from the net
        /// </summary>
        /// <param name="LayerNumber">Which layer to remove, cannot be 0 or the same as the output layer</param>
        public void RemoveLayer(int LayerNumber, bool backAdjustNeeded)
        {
            Exception_Remove(LayerNumber);
            Net.RemoveLayer(LayerNumber, backAdjustNeeded);
        }

        public override void Train(double[] Inputs, double[] ExpectedOutputs)
        {

            //Check input number of elements correct
            Exception_Train(Inputs, ExpectedOutputs);

            //calculate actual outputs
            double[] ActualOutputs = Predict(Inputs);
            double[,] CostArray = Cost(ActualOutputs, ExpectedOutputs, true);
            double[,] nextCostArray = new double[0, 0];
            double[] previousLayerOutputs = new double[0];

            for (int l = Net.Count() - 1; l > 0; l--)
            {
                int NeuronCount = Net[l].Count();
                
                nextCostArray = DotProduct(Transpose(GetWeights(l - 1)), CostArray);
                double[,] gradients = new double[Net[l].Count(), 1];
                for (int i = 0; i < NeuronCount; i++)
                {
                    gradients[i, 0] = (Activation(Activations.DSigmoid, (Net[l][i].OutValue)));
                }

                //cross multiply by errors (not that kind of cross multiply! (element wise))
                for (int i = 0; i < NeuronCount; i++)
                {
                    gradients[i, 0] *= CostArray[i, 0];
                }
                
                for (int i = 0; i < NeuronCount; i++)
                {
                    gradients[i, 0] *= LearningRate;
                }

                //add grads + biases
                for (int i = 0; i < NeuronCount; i++)
                {
                    Net[l][i].Bias += gradients[i, 0];
                }

                if(l != 0 )
                    previousLayerOutputs = Net[l - 1].Select(x => x.OutValue).ToArray();

                var layerInputs = new double[1, previousLayerOutputs.Count()];
                for(int i = 0; i < previousLayerOutputs.Count(); i++)
                {
                    layerInputs[0, i] = previousLayerOutputs[i];
                }

                var weightDeltas = DotProduct(gradients, layerInputs);

                for(int i = 0; i < weightDeltas.GetLength(0); i++)
                {
                    for(int j = 0; j < weightDeltas.GetLength(1); j++)
                    {
                        Net[l - 1][j].WeightsOut[i] += weightDeltas[i, j];
                    }
                }
                CostArray = nextCostArray;
            }
            
        }
        
        private double[,] GetWeights(int layerIndex)
        {
            double[,] weights = new double[Net[layerIndex + 1].Count(), Net[layerIndex].Count()];
            for (int i = 0; i < weights.GetLength(1); i++)
            {
                for (int j = 0; j < weights.GetLength(0); j++)
                {
                    weights[j, i] = Net[layerIndex][i].WeightsOut[j];
                }
            }
            return weights;
        }

        private double[,] DotProduct(double[,] a, double[,] b)
        {
            double[,] c = new double[a.GetLength(0), b.GetLength(1)];
            double tempSum = 0;
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int k = 0; k < b.GetLength(1); k++)
                {
                    tempSum = 0;
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        tempSum += a[i, j] * b[j, k];
                    }
                    c[i, k] = tempSum;
                }
            }
            return c;
        }

        public double[,] Transpose(double[,] a)
        {
            double[,] b = new double[a.GetLength(1), a.GetLength(0)];
            for(int i = 0; i < a.GetLength(0); i++)
            {
                for(int j = 0; j < a.GetLength(1); j++)
                {
                    b[j, i] = a[i, j];
                }
            }
            return b;
        }

        private void Exception_AddLayer(int LayerNumber, int NeuronsInNewLayer)
        {
            if (LayerNumber < 1 || LayerNumber > Net.Count() - 1)
            {
                throw new InvalidOperationException("Layer number has to be larger than 0 and less than the index of the output layer which" +
                    " will be incrimented when a layer is successfully added");
            }

            if (NeuronsInNewLayer <= 0)
            {
                throw new InvalidOperationException("New layer must have more than 0 neurons");
            }
        }

        private void Exception_Train(double[] SuppInputs, double[] ExpOutputs)
        {
            if (ExpOutputs.GetUpperBound(0) + 1 != this.NumberOfOutputs)
            {
                throw new Exception("Number of expected outputs not equal to number of actual outputs");
            }

            if (SuppInputs.GetUpperBound(0) + 1 != this.NumberOfInputs)
            {
                throw new Exception("Number of inputs supplied not equal to number of expected inputs");
            }
        }

        private void Exception_Remove(int LayerNumber)
        {
            if (LayerNumber < 1 || LayerNumber >= Net.Count() - 1)
            {
                throw new InvalidOperationException("Layer cannot be smaller than 1 or the same as or larger than the output index");
            }
        }

        public void Exception_CheckValidState()
        {
            //Number of hidden vs neurons per hidden 
            if (this.NumberOfHiddenLayers != this.NeuronsPerHiddenLayer.Count())
            {
                throw new InvalidOperationException("Number of neurons per hidden layer does not match number of hidden layers");
            }
        }

        private void Exception_Predict(int SuppliedInputs, int ExpectedInputs)
        {
            if (SuppliedInputs != ExpectedInputs)
            {
                throw new InvalidOperationException("Number of inputs supplied is not equal to the number the net can take (" + ExpectedInputs + ")");
            }
        }

       
        protected override double[] Cost(double [] ActualOutput, double [] ExpectedOutput)
        {
            double[] ReturnArray = new double[ActualOutput.Count()];
            for (int i = 0; i <= ActualOutput.GetUpperBound(0); i++)
            {
                ReturnArray[i] = (ExpectedOutput[i]- ActualOutput[i]);
            }
            return ReturnArray;

        }

        protected double[,] Cost(double[] ActualOutput, double[] ExpectedOutput, bool isTwoD)
        {
            double[,] ReturnArray = new double[ActualOutput.Count(), 1];
            for (int i = 0; i <= ActualOutput.GetUpperBound(0); i++)
            {
                ReturnArray[i, 0] = (ExpectedOutput[i] - ActualOutput[i]);
            }
            return ReturnArray;

        }


        //Activation function?
        protected override double Activation(double ValueIn, Activations activation)
        {
            switch (activation)
            {
                case (Activations.Sigmoid):
                    return (1 / (1 + Math.Exp(-ValueIn)));

                default:
                    return (1 / (1 + Math.Exp(-ValueIn)));
            }
        }
        

      
        private void BackAdjustWeights(int LayerToBeAdjusted, int NeuronNumber)
        {

            if (Net[LayerToBeAdjusted][0].WeightsOut.Count() > Net[LayerToBeAdjusted + 1].Count())
            {
                //Neuron removed so we must remove the relevant weightout
                foreach (Neuron N in Net[LayerToBeAdjusted])
                {
                    var NewWeightsOut = new double[Net[LayerToBeAdjusted + 1].Count];
                    int indexer = 0;
                    for (int i = 0; i < N.WeightsOut.Count(); i++)
                    {
                        if (i != NeuronNumber)
                        {
                            NewWeightsOut[indexer] = N.WeightsOut[i];
                            indexer++;
                        }
                    }
                    N.WeightsOut = NewWeightsOut;
                }
            }
            else
            {
                //Neuron added so we must add a weight out
                
                foreach (Neuron N in Net[LayerToBeAdjusted])
                {
                    var NewWeightsOut = new double[Net[LayerToBeAdjusted + 1].Count];

                    int indexor = 0;
                    foreach (double d in N.WeightsOut)
                    {
                        NewWeightsOut[indexor] = d;
                        indexor++;
                    }
                    N.WeightsOut = NewWeightsOut;
                }
            }
        }
    }
        

}
