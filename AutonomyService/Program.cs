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
            AutonomyStubWebManager autonomyStubWebManager = new AutonomyStubWebManager();
            autonomyStubWebManager.AddService<AutonomyStubHandler>();
            autonomyStubWebManager.Start();

            const int Sleep_ms = 1000;

            while (true)
            {
                Thread.Sleep(Sleep_ms);
            }
        }
    }
}
