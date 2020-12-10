using Microsoft.Extensions.Configuration;

namespace NeuralNetworkAPI.Utils
{
    public class Settings
    {
        public static void Initialize(IConfiguration configuration)
        {
            SaveLocation = configuration.GetValue<string>("SaveLocation");
            RepositoryFileLocation = configuration.GetValue<string>("RepositoryFileLocation");
        }

        public static string SaveLocation { get; private set; }
        public static string RepositoryFileLocation { get; private set; }
    }
}
