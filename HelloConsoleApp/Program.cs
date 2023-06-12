using HelloConsoleApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

public class MyLogger
{
    private readonly ILogger logger;

    public MyLogger()
    {
        logger = ConfigureLogger();
    }

    public void Run()
    {

        for (int i = 0; i < 10000; i++)
        {
            logger.LogInformation($"Hello, world{i}!");
        }



        // Rest of your application code
    }

    private ILogger ConfigureLogger()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole(); // Add console logging provider
        });

        return loggerFactory.CreateLogger<MyLogger>();
    }
}

public static class Program
{
    public static void Main()
    {
        var myLogger = new MyLogger();
        //myLogger.Run();

        var builder = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
.AddJsonFile("appsettings.json", optional: false);

        var configuration = builder.Build();

        var services = new ServiceCollection();
        services.Configure<Person>(configuration.GetSection("person"));
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        //Person person = serviceProvider.GetRequiredService<Person>();

        IOptions<Person> personOptions = serviceProvider.GetRequiredService<IOptions<Person>>();



        Console.WriteLine(JsonConvert.SerializeObject(personOptions.Value));




        Thread.Sleep(100000);
    }
}
