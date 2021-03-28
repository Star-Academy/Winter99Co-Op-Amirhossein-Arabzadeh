using Microsoft.Extensions.Configuration;
using Phase10Library;

namespace ViewRun
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var settings = configuration.Get<Settings>();

            var view = new View();
            view.Run(settings);
        }
    }
}