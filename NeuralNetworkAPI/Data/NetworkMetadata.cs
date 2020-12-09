using System;

namespace NeuralNetworkAPI.Data
{
    [Serializable]
    public class NetworkMetadata : IHasId
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OwnerId { get; set; }
        public int InputCount { get; set; }
        public int HiddenWidth { get; set; }
        public int Layers { get; set; }
        public int OutputCount { get; set; }
        public float LearningRate { get; set; }
    }
}
