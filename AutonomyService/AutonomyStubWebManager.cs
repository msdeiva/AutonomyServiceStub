using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Flight.V3;

namespace AutonomyServiceStub
{
    public class AutonomyStubWebManager
    {
        private WebApplication _app;

        public AutonomyStubWebManager()
        {
            try
            {
                WebApplicationBuilder builder = WebApplication.CreateBuilder();

                builder.Services.AddGrpc();
                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(policy =>
                    {
                        policy.WithOrigins(
                                "http://localhost:5173",
                                "http://localhost:4001",
                                "http://localhost:5001",
                                "http://localhost:2008",
                                "http://10.200.11.12:5001",
                                "http://10.10.182.92:5173", // <- the other PC's IP, port the dev server uses
                                "http://10.10.182.92:5001", // <- the other PC's IP, port the dev server uses
                                "http://10.10.182.92:4001",
                                "http://10.10.182.105:5001")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
                    });
                });

                _app = builder.Build();
                _app.UseCors();
                _app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
                _app.MapGet("/",
                    () => "AutonomyService gRPC server is running. Use a gRPC/gRPC-Web client to call it.");
                _app.Urls.Add("https://0.0.0.0:5000");
            }
            catch (Exception exception)
            {
                string logFilePath = Program.GetLogFilePath();
                using (StreamWriter sw = new StreamWriter(logFilePath, append: true))
                {
                    sw.WriteLine($"[{DateTime.Now}] {exception}");
                }
            }
        }

        public void AddService<T>() where T : AutonomyService.AutonomyServiceBase
        {
            if (_app == null) return;

            _app.MapGrpcService<T>().EnableGrpcWeb().RequireCors();
        }

        public void Start()
        {
            if (_app == null) return;

            _app.Run();
        }
    }
}
