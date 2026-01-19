using System.ComponentModel;
using ModelContextProtocol.Server;
using SampleMcpServer.Services;

/// <summary>
/// Sample MCP tools for demonstration purposes.
/// These tools can be invoked by MCP clients to perform various operations.
/// </summary>
internal class CalculadoraTools
{
    private readonly ConsultaHistoryService _historyService;

    public CalculadoraTools(ConsultaHistoryService historyService)
    {
        _historyService = historyService;
    }

    [McpServerTool]
    [Description("Realiza a soma de dois números.")]
    public double Somar(
        [Description("Primeiro número")] double a,
        [Description("Segundo número")] double b)
    {
        var resultado = a + b;
        _historyService.AdicionarCalculo("Soma", $"{a} + {b}", resultado);
        return resultado;
    }

    [McpServerTool]
    [Description("Realiza a subtração de dois números.")]
    public double Subtrair(
        [Description("Primeiro número (minuendo)")] double a,
        [Description("Segundo número (subtraendo)")] double b)
    {
        var resultado = a - b;
        _historyService.AdicionarCalculo("Subtração", $"{a} - {b}", resultado);
        return resultado;
    }

    [McpServerTool]
    [Description("Realiza a multiplicação de dois números.")]
    public double Multiplicar(
        [Description("Primeiro número")] double a,
        [Description("Segundo número")] double b)
    {
        var resultado = a * b;
        _historyService.AdicionarCalculo("Multiplicação", $"{a} × {b}", resultado);
        return resultado;
    }

    [McpServerTool]
    [Description("Realiza a divisão de dois números.")]
    public double Dividir(
        [Description("Dividendo")] double a,
        [Description("Divisor")] double b)
    {
        if (b == 0)
            throw new ArgumentException("Divisão por zero não é permitida.");
        
        var resultado = a / b;
        _historyService.AdicionarCalculo("Divisão", $"{a} ÷ {b}", resultado);
        return resultado;
    }
}
