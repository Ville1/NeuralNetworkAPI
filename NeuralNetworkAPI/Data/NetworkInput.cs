using System;

namespace NeuralNetworkAPI.Data
{
    [Serializable]
    public class NetworkInput
    {
        public long NetworkId { get; set; }
        public string Inputs { get; set; }
        public string ExpectedOutputs { get; set; }
        public int TeachRepeats { get; set; }
    }
}
