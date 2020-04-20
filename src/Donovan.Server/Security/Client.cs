namespace Donovan.Server.Security
{
    public class Client
    {
        public Client()
        {
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Secret { get; set; }

        public bool IsEnabled { get; set; }
    }
}
