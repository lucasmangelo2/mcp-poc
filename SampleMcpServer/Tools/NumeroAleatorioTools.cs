using System.ComponentModel;
using ModelContextProtocol.Server;

/// <summary>
/// Sample MCP tools for demonstration purposes.
/// These tools can be invoked by MCP clients to perform various operations.
/// </summary>
internal class NumeroAleatorioTools
{
    [McpServerTool]
    [Description("Gera um número aleatório entre os valores mínimo e máximo especificados.")]
    public int RetornaNumeroAleatorio(
        [Description("Número mínimo")] int min = 0,
        [Description("Número máximo")] int max = 100)
    {
        return Random.Shared.Next(min, max);
    }
}
