using System.Collections.Concurrent;

namespace SampleMcpServer.Services;

/// <summary>
/// Serviço para armazenar histórico de consultas realizadas.
/// </summary>
public class ConsultaHistoryService
{
    private readonly ConcurrentBag<CepConsulta> _cepHistory = new();
    private readonly ConcurrentBag<CalculoRealizado> _calculoHistory = new();

    public void AdicionarConsultaCep(string cep, string resultado, bool sucesso)
    {
        _cepHistory.Add(new CepConsulta
        {
            Cep = cep,
            Resultado = resultado,
            DataConsulta = DateTime.Now,
            Sucesso = sucesso
        });
    }

    public void AdicionarCalculo(string operacao, string expressao, double resultado)
    {
        _calculoHistory.Add(new CalculoRealizado
        {
            Operacao = operacao,
            Expressao = expressao,
            Resultado = resultado,
            DataCalculo = DateTime.Now
        });
    }

    public IEnumerable<CepConsulta> ObterHistoricoCep(int limite = 10)
    {
        return _cepHistory
            .OrderByDescending(c => c.DataConsulta)
            .Take(limite);
    }

    public IEnumerable<CalculoRealizado> ObterHistoricoCalculos(int limite = 10)
    {
        return _calculoHistory
            .OrderByDescending(c => c.DataCalculo)
            .Take(limite);
    }

    public int TotalConsultasCep => _cepHistory.Count;
    public int TotalCalculos => _calculoHistory.Count;
}

public record CepConsulta
{
    public required string Cep { get; init; }
    public required string Resultado { get; init; }
    public required DateTime DataConsulta { get; init; }
    public required bool Sucesso { get; init; }
}

public record CalculoRealizado
{
    public required string Operacao { get; init; }
    public required string Expressao { get; init; }
    public required double Resultado { get; init; }
    public required DateTime DataCalculo { get; init; }
}
