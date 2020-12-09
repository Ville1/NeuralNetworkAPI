using System;

namespace NeuralNetworkAPI.Data
{
    [Serializable]
    public class NetworkMetadata : IHasId
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OwnerId { get; set; }
    }
}
