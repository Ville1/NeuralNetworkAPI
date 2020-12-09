using Microsoft.Extensions.Configuration;

namespace NeuralNetworkAPI.Utils
{
    public class Settings
    {
        public static void Initialize(IConfiguration configuration)
        {
            SaveLocation = configuration.GetValue<string>("SaveLocation");
        }

        public static string SaveLocation { get; private set; }
    }
}
