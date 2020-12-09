using NeuralNetwork;
using NeuralNetworkAPI.Utils;

namespace NeuralNetworkAPI.Data.Networks
{
    public class NetworkManager
    {
        private static readonly string FILE_PREFIX = "network";

        public static void InitializeNetwork(NetworkMetadata data)
        {
            Network network = new Network(data.InputCount, data.HiddenWidth, data.Layers, data.OutputCount, data.LearningRate, ActivationFunctionType.Sigmoid);

            Network.Save(network, string.Format("{0}/{1}{2}.json", Settings.SaveLocation, FILE_PREFIX, data.Id));
        }
    }
}
