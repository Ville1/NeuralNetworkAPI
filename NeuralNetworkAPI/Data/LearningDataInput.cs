using System;
using System.Collections.Generic;

namespace NeuralNetworkAPI.Data
{
    [Serializable]
    public class LearningDataInput
    {
        public long NetworkId { get; set; }
        public List<LearningDataCase> Cases { get; set; }
        public int Repeats { get; set; }
    }

    [Serializable]
    public class LearningDataCase
    {
        public string Inputs { get; set; }
        public string ExpectedOutputs { get; set; }
    }
}
