using System.ComponentModel;
using ModelContextProtocol.Server;
using SampleMcpServer.Services;

/// <summary>
/// Tools para expor histórico de consultas e cálculos.
/// </summary>
internal class HistoricoTools
{
    private readonly ConsultaHistoryService _historyService;

    public HistoricoTools(ConsultaHistoryService historyService)
    {
        _historyService = historyService;
    }

    [McpServerTool]
    [Description("Retorna o histórico das últimas consultas de CEP realizadas")]
    public string ObterHistoricoCep()
    {
        var historico = _historyService.ObterHistoricoCep(20);
        
        if (!historico.Any())
        {
            return "📋 Nenhuma consulta de CEP foi realizada ainda.";
        }

        var resultado = $"📋 Histórico de Consultas de CEP ({_historyService.TotalConsultasCep} total)\n\n";
        
        foreach (var consulta in historico)
        {
            var status = consulta.Sucesso ? "✅" : "❌";
            resultado += $"{status} {consulta.Cep} - {consulta.DataConsulta:dd/MM/yyyy HH:mm:ss}\n";
            
            if (consulta.Sucesso)
            {
                // Extrai apenas informações básicas do resultado
                var linhas = consulta.Resultado.Split('\n');
                var cidadeEstado = linhas.FirstOrDefault(l => l.Contains("🏙️") || l.Contains("🗺️"));
                if (!string.IsNullOrEmpty(cidadeEstado))
                {
                    resultado += $"   {cidadeEstado.Trim()}\n";
                }
            }
            resultado += "\n";
        }

        return resultado;
    }

    [McpServerTool]
    [Description("Retorna o histórico dos últimos cálculos realizados")]
    public string ObterHistoricoCalculos()
    {
        var historico = _historyService.ObterHistoricoCalculos(20);
        
        if (!historico.Any())
        {
            return "📊 Nenhum cálculo foi realizado ainda.";
        }

        var resultado = $"📊 Histórico de Cálculos ({_historyService.TotalCalculos} total)\n\n";
        
        foreach (var calculo in historico)
        {
            resultado += $"🔢 {calculo.Operacao}: {calculo.Expressao} = {calculo.Resultado}\n";
            resultado += $"   ⏰ {calculo.DataCalculo:dd/MM/yyyy HH:mm:ss}\n\n";
        }

        return resultado;
    }

    [McpServerTool]
    [Description("Retorna estatísticas gerais de uso do servidor MCP")]
    public string ObterEstatisticas()
    {
        var totalCeps = _historyService.TotalConsultasCep;
        var totalCalculos = _historyService.TotalCalculos;
        var cepsSucesso = _historyService.ObterHistoricoCep(int.MaxValue).Count(c => c.Sucesso);
        var cepsFalha = totalCeps - cepsSucesso;

        var resultado = $@"📈 Estatísticas do Servidor MCP

🔍 Consultas de CEP:
   ✅ Sucessos: {cepsSucesso}
   ❌ Falhas: {cepsFalha}
   📊 Total: {totalCeps}

🔢 Cálculos Realizados:
   📊 Total: {totalCalculos}

⏰ Última Atualização: {DateTime.Now:dd/MM/yyyy HH:mm:ss}
";

        return resultado;
    }

    [McpServerTool]
    [Description("Busca no histórico se um CEP específico já foi consultado")]
    public string BuscarCepNoHistorico(
        [Description("CEP a ser buscado no histórico")] string cep)
    {
        var cepLimpo = cep.Replace("-", "").Replace(".", "").Replace(" ", "");
        var consultas = _historyService.ObterHistoricoCep(int.MaxValue)
            .Where(c => c.Cep.Replace("-", "").Replace(".", "").Replace(" ", "") == cepLimpo)
            .ToList();

        if (!consultas.Any())
        {
            return $"🔍 O CEP {cep} não foi encontrado no histórico.";
        }

        var resultado = $"📋 Histórico do CEP {cep} ({consultas.Count} consulta(s))\n\n";
        
        foreach (var consulta in consultas.OrderByDescending(c => c.DataConsulta))
        {
            var status = consulta.Sucesso ? "✅ Sucesso" : "❌ Falha";
            resultado += $"{status} - {consulta.DataConsulta:dd/MM/yyyy HH:mm:ss}\n";
            resultado += $"{consulta.Resultado}\n\n";
        }

        return resultado;
    }
}
