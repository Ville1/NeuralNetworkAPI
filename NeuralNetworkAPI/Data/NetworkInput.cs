using System;

namespace NeuralNetworkAPI.Data
{
    [Serializable]
    public class NetworkInput
    {
        public long NetworkId { get; set; }
        public string Input { get; set; }
        public string ExpectedOutput { get; set; }
        public int TeachRepeats { get; set; }
    }
}
