using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleMcpServer.Prompts;
using SampleMcpServer.Resources;
using SampleMcpServer.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Configurar cultura brasileira para toda a aplicação
var culturaBrasil = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = culturaBrasil;
CultureInfo.DefaultThreadCurrentUICulture = culturaBrasil;

// Configure all logs to go to stderr
builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

// Registrar serviços
builder.Services.AddSingleton<TimeZoneService>();
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
    .WithResources<DocumentacaoResources>()
    .WithResources<HistoricoResources>()
    .WithPrompts<AnaliticoPrompts>()
    .WithPrompts<EducacionalPrompts>()
    .WithPrompts<ValidacaoPrompts>();

var app = builder.Build();

// Map MCP endpoint
app.MapMcp();

await app.RunAsync();
