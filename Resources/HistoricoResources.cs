using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using ModelContextProtocol.Server;
using SampleMcpServer.Services;

namespace SampleMcpServer.Resources;

/// <summary>
/// Resources para expor dados de histórico e estatísticas do servidor.
/// </summary>
internal class HistoricoResources
{
    private readonly ConsultaHistoryService _historyService;
    private readonly TimeZoneService _timeZoneService;
    private static readonly DateTime _iniciadoEm = DateTime.UtcNow;

    public HistoricoResources(ConsultaHistoryService historyService, TimeZoneService timeZoneService)
    {
        _historyService = historyService;
        _timeZoneService = timeZoneService;
    }

    [McpServerResource]
    [Description("Retorna o histórico completo de consultas de CEP realizadas")]
    public string ObterHistoricoCep()
    {
        var historico = _historyService.ObterHistoricoCep(20).ToList();
        var total = _historyService.TotalConsultasCep;

        var consultas = historico.Select(c => new
        {
            cep = c.Cep,
            data_consulta = c.DataConsulta.ToString("yyyy-MM-dd HH:mm:ss"),
            sucesso = c.Sucesso,
            resultado = c.Sucesso ? c.Resultado : null,
            erro = !c.Sucesso ? c.Resultado : null
        });

        var resultado = new
        {
            total_consultas = total,
            consultas_exibidas = consultas.Count(),
            consultas = consultas
        };

        return JsonSerializer.Serialize(resultado, new JsonSerializerOptions 
        { 
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
    }

    [McpServerResource]
    [Description("Retorna o histórico completo de cálculos matemáticos realizados")]
    public string ObterHistoricoCalculos()
    {
        var historico = _historyService.ObterHistoricoCalculos(20).ToList();
        var total = _historyService.TotalCalculos;

        var calculos = historico.Select(c => new
        {
            operacao = c.Operacao,
            expressao = c.Expressao,
            resultado = c.Resultado,
            data_calculo = c.DataCalculo.ToString("yyyy-MM-dd HH:mm:ss")
        });

        var resultado = new
        {
            total_calculos = total,
            calculos_exibidos = calculos.Count(),
            calculos = calculos
        };

        return JsonSerializer.Serialize(resultado, new JsonSerializerOptions 
        { 
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
    }

    [McpServerResource]
    [Description("Retorna estatísticas gerais de uso do servidor MCP")]
    public string ObterEstatisticas()
    {
        var totalCeps = _historyService.TotalConsultasCep;
        var totalCalculos = _historyService.TotalCalculos;
        var cepsSucesso = _historyService.ObterHistoricoCep(int.MaxValue).Count(c => c.Sucesso);
        var cepsFalha = totalCeps - cepsSucesso;
        
        var uptime = DateTime.UtcNow - _iniciadoEm;
        var process = Process.GetCurrentProcess();

        var calculosPorOperacao = _historyService.ObterHistoricoCalculos(int.MaxValue)
            .GroupBy(c => c.Operacao)
            .Select(g => new { operacao = g.Key, total = g.Count() })
            .OrderByDescending(x => x.total)
            .ToList();

        var estatisticas = new
        {
            servidor = new
            {
                nome = "SampleMcpServer",
                versao = "0.1.0",
                uptime = new
                {
                    segundos = uptime.TotalSeconds,
                    formatado = $"{uptime.Days}d {uptime.Hours}h {uptime.Minutes}m"
                },
                iniciado_em = _iniciadoEm.ToString("yyyy-MM-dd HH:mm:ss"),
                timestamp_atual = _timeZoneService.Agora.ToString("yyyy-MM-dd HH:mm:ss")
            },
            consultas_cep = new
            {
                total = totalCeps,
                sucessos = cepsSucesso,
                falhas = cepsFalha,
                taxa_sucesso = totalCeps > 0 ? Math.Round(cepsSucesso * 100.0 / totalCeps, 2) : 0
            },
            calculos = new
            {
                total = totalCalculos,
                por_operacao = calculosPorOperacao
            },
            sistema = new
            {
                memoria_mb = Math.Round(process.WorkingSet64 / 1024.0 / 1024.0, 2),
                threads = process.Threads.Count,
                tempo_processador_ms = process.TotalProcessorTime.TotalMilliseconds
            },
            total_operacoes = totalCeps + totalCalculos
        };

        return JsonSerializer.Serialize(estatisticas, new JsonSerializerOptions 
        { 
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
    }

    [McpServerResource]
    [Description("Busca todas as consultas de um CEP específico no histórico")]
    public string BuscarCepNoHistorico(
        [Description("CEP a ser buscado no histórico (com ou sem hífen)")] string cep)
    {
        // Normalizar CEP para comparação
        var cepNormalizado = cep.Replace("-", "").Trim();

        var consultas = _historyService.ObterHistoricoCep(int.MaxValue)
            .Where(c => c.Cep.Replace("-", "") == cepNormalizado)
            .OrderByDescending(c => c.DataConsulta)
            .Select(c => new
            {
                cep = c.Cep,
                data_consulta = c.DataConsulta.ToString("yyyy-MM-dd HH:mm:ss"),
                sucesso = c.Sucesso,
                resultado = c.Sucesso ? c.Resultado : null,
                erro = !c.Sucesso ? c.Resultado : null
            })
            .ToList();

        var resultado = new
        {
            cep_buscado = cep,
            total_encontrado = consultas.Count,
            consultas = consultas
        };

        return JsonSerializer.Serialize(resultado, new JsonSerializerOptions 
        { 
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
    }

    [McpServerResource]
    [Description("Retorna informações detalhadas sobre o servidor MCP")]
    public string InformacoesServidor()
    {
        var uptime = DateTime.UtcNow - _iniciadoEm;
        var process = Process.GetCurrentProcess();

        var informacoes = new
        {
            servidor = new
            {
                nome = "SampleMcpServer",
                descricao = "Servidor MCP de exemplo com CEP, calculadora e gerador de números aleatórios",
                versao = "0.1.0",
                framework = ".NET 9.0",
                cultura = "pt-BR"
            },
            runtime = new
            {
                uptime_segundos = uptime.TotalSeconds,
                uptime_formatado = $"{uptime.Days}d {uptime.Hours}h {uptime.Minutes}m {uptime.Seconds}s",
                iniciado_em = _iniciadoEm.ToString("yyyy-MM-dd HH:mm:ss UTC"),
                timezone_local = "E. South America Standard Time (Brasília)",
                horario_atual = _timeZoneService.Agora.ToString("yyyy-MM-dd HH:mm:ss")
            },
            recursos = new
            {
                memoria_mb = Math.Round(process.WorkingSet64 / 1024.0 / 1024.0, 2),
                memoria_privada_mb = Math.Round(process.PrivateMemorySize64 / 1024.0 / 1024.0, 2),
                threads = process.Threads.Count,
                handles = process.HandleCount,
                tempo_processador = process.TotalProcessorTime.ToString(@"hh\:mm\:ss")
            },
            capacidades = new
            {
                tools = new[] { "CEP", "Calculadora", "Número Aleatório" },
                resources = new[] { "Histórico", "Estatísticas", "Documentação", "Sistema" },
                prompts = new[] { "Relatórios", "Análises", "Auditoria", "Validação" }
            }
        };

        return JsonSerializer.Serialize(informacoes, new JsonSerializerOptions 
        { 
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
    }

    [McpServerResource]
    [Description("Verifica o status de saúde (health check) do servidor")]
    public string HealthCheck()
    {
        var process = Process.GetCurrentProcess();
        var memoriaUsadaMb = process.WorkingSet64 / 1024.0 / 1024.0;
        var uptime = DateTime.UtcNow - _iniciadoEm;

        // Determinar status com base em métricas
        string status;
        var problemas = new List<string>();

        if (memoriaUsadaMb > 500)
        {
            problemas.Add("Memória acima de 500 MB");
            status = "degraded";
        }
        else
        {
            status = "healthy";
        }

        var health = new
        {
            status = status,
            timestamp = _timeZoneService.Agora.ToString("yyyy-MM-dd HH:mm:ss"),
            uptime = new
            {
                segundos = uptime.TotalSeconds,
                formatado = $"{uptime.Days}d {uptime.Hours}h {uptime.Minutes}m"
            },
            recursos = new
            {
                memoria = new
                {
                    usado_mb = Math.Round(memoriaUsadaMb, 2),
                    status = memoriaUsadaMb < 500 ? "healthy" : "degraded"
                },
                threads = new
                {
                    count = process.Threads.Count,
                    status = process.Threads.Count < 100 ? "healthy" : "degraded"
                },
                processador = new
                {
                    tempo_total_ms = process.TotalProcessorTime.TotalMilliseconds,
                    status = "healthy"
                }
            },
            servicos = new
            {
                historico_cep = new
                {
                    total_registros = _historyService.TotalConsultasCep,
                    status = "healthy"
                },
                historico_calculos = new
                {
                    total_registros = _historyService.TotalCalculos,
                    status = "healthy"
                }
            },
            problemas = problemas.Any() ? problemas : new List<string> { "Nenhum problema detectado" }
        };

        return JsonSerializer.Serialize(health, new JsonSerializerOptions 
        { 
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
    }
}
