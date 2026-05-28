using Flight.V3;
using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AutonomyServiceStub
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AutonomyStubWebManager autonomyStubWebManager = new AutonomyStubWebManager();
                autonomyStubWebManager.AddService<AutonomyStubHandler>();
                autonomyStubWebManager.Start();
            }
            catch (Exception exception)
            {
                string logFilePath = GetLogFilePath();
                using (StreamWriter sw = new StreamWriter(logFilePath, append: true))
                {
                    sw.WriteLine($"[{DateTime.Now}] {exception}");
                }
            }

            const int Sleep_ms = 1000;

            while (true)
            {
                Thread.Sleep(Sleep_ms);
            }
        }

        private static string GetProgramDataFolder()
        {
            string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string appFolder = Path.Combine(programData, "MissionServiceExploiter");
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }
            return appFolder;
        }

        public static string GetLogFilePath()
        {
            string programDataFolder = GetProgramDataFolder();
            return Path.Combine(programDataFolder, "error.log");
        }
    }
}
