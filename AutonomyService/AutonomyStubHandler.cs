using Flight.V3;
using Grpc.Core;

namespace AutonomyServiceStub
{
    public class AutonomyStubHandler : AutonomyService.AutonomyServiceBase
    {
        private void PrintGreen(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public override Task<RequestAck> StartMission(StartMissionRequest request, ServerCallContext context)
        {
            PrintGreen($"{nameof(StartMission)} called");

            if (request.MissionId != 0) return Task.FromResult(new RequestAck() { ErrorMessage = $"Cannot find system with id: {request.MissionId}", Success = false });

            RequestAck ack = new RequestAck();
            ack.Success = true;

            return Task.FromResult(ack);
        }
    }
}
