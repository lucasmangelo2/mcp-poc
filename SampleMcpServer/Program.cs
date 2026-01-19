using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleMcpServer.Services;

var builder = Host.CreateApplicationBuilder(args);

// Configure all logs to go to stderr (stdout is used for the MCP protocol messages).
builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

// Registrar serviços
builder.Services.AddSingleton<ConsultaHistoryService>();

// Configurar HttpClient para CepTools
builder.Services.AddHttpClient<CepTools>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);
    client.DefaultRequestHeaders.Add("User-Agent", "SampleMcpServer/0.1.0");
});

// Add the MCP services: the transport to use (stdio) and the tools to register.
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<NumeroAleatorioTools>()
    .WithTools<CalculadoraTools>()
    .WithTools<CepTools>()
    .WithTools<HistoricoTools>();

await builder.Build().RunAsync();
