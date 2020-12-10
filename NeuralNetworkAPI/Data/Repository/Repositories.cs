namespace NeuralNetworkAPI.Data.Repository
{
    public class Repositories
    {
        private static Repository<User> users;

        public static Repository<User> Users
        {
            get {
                if(users == null) {
                    users = new Repository<User>();
                }
                return users;
            }
        }

        private static Repository<NetworkMetadata> networks;

        public static Repository<NetworkMetadata> Networks
        {
            get {
                if (networks == null) {
                    networks = new Repository<NetworkMetadata>();
                }
                return networks;
            }
        }
    }
}
