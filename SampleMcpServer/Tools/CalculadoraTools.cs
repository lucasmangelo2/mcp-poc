using System.ComponentModel;
using ModelContextProtocol.Server;

/// <summary>
/// Sample MCP tools for demonstration purposes.
/// These tools can be invoked by MCP clients to perform various operations.
/// </summary>
internal class CalculadoraTools
{
    [McpServerTool]
    [Description("Realiza a soma de dois números.")]
    public double Somar(
        [Description("Primeiro número")] double a,
        [Description("Segundo número")] double b)
    {
        return a + b;
    }

    [McpServerTool]
    [Description("Realiza a subtração de dois números.")]
    public double Subtrair(
        [Description("Primeiro número (minuendo)")] double a,
        [Description("Segundo número (subtraendo)")] double b)
    {
        return a - b;
    }

    [McpServerTool]
    [Description("Realiza a multiplicação de dois números.")]
    public double Multiplicar(
        [Description("Primeiro número")] double a,
        [Description("Segundo número")] double b)
    {
        return a * b;
    }

    [McpServerTool]
    [Description("Realiza a divisão de dois números.")]
    public double Dividir(
        [Description("Dividendo")] double a,
        [Description("Divisor")] double b)
    {
        if (b == 0)
            throw new ArgumentException("Divisão por zero não é permitida.");
        
        return a / b;
    }
}
