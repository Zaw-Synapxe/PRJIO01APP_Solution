using log4net;
using log4net.Config;
using Log4net.ConsoleApp;
using System.Reflection;

var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4netconfig_3.config"));

var demo = new Logger();
Console.WriteLine("Start Here ...");

demo.Info($"Starting the console application at : {DateTimeOffset.Now}");

try
{
    demo.Debug($"Starting {MethodBase.GetCurrentMethod()?.DeclaringType}");

    throw new Exception("Sample Error inside the try catch block code");
}
catch (Exception ex)
{

    demo.Error(ex.Message, ex.InnerException);
}

demo.Debug("Waiting for user input");

Console.WriteLine("Press any key to End ...");
Console.ReadLine();

demo.Info("Ending application");