using Phase10Library;

namespace ViewRun
{
    class Program
    {
        static void Main(string[] args)
        {

            var elasticClient = new MyElasticClient();

            var view = new View();
            view.Run();
        }
    }
}