using System;
using System.Collections.Generic;

namespace NeuralNetworkAPI.Data
{
    [Serializable]
    public class NetworkInput
    {
        public long NetworkId { get; set; }
        public List<bool> Inputs { get; set; }
        public List<bool> ExpectedOutputs { get; set; }
        public int TeachRepeats { get; set; }
    }
}
