using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleMcpServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure all logs to go to stderr
builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

// Registrar serviços
builder.Services.AddSingleton<ConsultaHistoryService>();

// Configurar HttpClient para CepTools
builder.Services.AddHttpClient<CepTools>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
    client.DefaultRequestHeaders.Add("User-Agent", "SampleMcpServer/0.1.0");
});

// Add the MCP services: the transport to use (HTTP) and the tools to register.
builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithTools<NumeroAleatorioTools>()
    .WithTools<CalculadoraTools>()
    .WithTools<CepTools>()
    .WithTools<HistoricoTools>();

var app = builder.Build();

// Map MCP endpoint
app.MapMcp();

await app.RunAsync();
