namespace NeuralNetworkAPI.Data
{
    public class User : IHasId
    {
        public long? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
