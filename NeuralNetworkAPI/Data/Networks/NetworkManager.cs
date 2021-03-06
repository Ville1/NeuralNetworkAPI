﻿using NeuralNetwork;
using NeuralNetwork.Data;
using NeuralNetworkAPI.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NeuralNetworkAPI.Data.Networks
{
    public class NetworkManager
    {
        private static readonly string FILE_PREFIX = "network";

        public static void InitializeNetwork(NetworkMetadata metadata)
        {
            Network network = new Network(metadata.InputCount, metadata.HiddenWidth, metadata.Layers, metadata.OutputCount, metadata.LearningRate, ActivationFunctionType.Sigmoid);
            Network.Save(network, ParseSaveFile(metadata));
        }

        public static bool DeleteNetwork(NetworkMetadata metadata)
        {
            if (!File.Exists(ParseSaveFile(metadata))) {
                return false;
            }
            File.Delete(ParseSaveFile(metadata));
            return true;
        }

        public static string Process(NetworkMetadata metadata, NetworkInput input)
        {
            Network network = Network.Load(ParseSaveFile(metadata));
            return network.Process(new NetworkData(new Bits(input.Input))).BitValues.ToString();
        }

        public static string Teach(NetworkMetadata metadata, NetworkInput input)
        {
            Network network = Network.Load(ParseSaveFile(metadata));
            input.TeachRepeats = input.TeachRepeats > 0 ? input.TeachRepeats : 1;
            List<LearningData> learningData = new List<LearningData>() { new LearningData(new NetworkData(new Bits(input.Input)), new NetworkData(new Bits(input.ExpectedOutput))) };
            network.Teach(learningData, input.TeachRepeats);
            Network.Save(network, ParseSaveFile(metadata));
            return network.Process(new NetworkData(new Bits(input.Input))).BitValues.ToString();
        }

        public static float Teach(NetworkMetadata metadata, LearningDataInput input)
        {
            Network network = Network.Load(ParseSaveFile(metadata));
            input.Repeats = input.Repeats > 0 ? input.Repeats : 1;
            List<LearningData> learningData = input.Cases.Select(x => new LearningData(new NetworkData(new Bits(x.Input)), new NetworkData(new Bits(x.ExpectedOutput)))).ToList();
            network.Teach(learningData, input.Repeats);
            Network.Save(network, ParseSaveFile(metadata));
            return NeuralNetwork.Analytics.Test(network, learningData).SuccessRate;
        }

        private static string ParseSaveFile(NetworkMetadata metadata)
        {
            return string.Format("{0}/{1}{2}.json", Settings.SaveLocation, FILE_PREFIX, metadata.Id.Value);
        }
    }
}
