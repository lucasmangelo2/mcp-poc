namespace SampleMcpServer.Services;

/// <summary>
/// Serviço centralizado para gerenciar timezone da aplicação.
/// </summary>
public class TimeZoneService
{
    private readonly TimeZoneInfo _timeZoneBrasil;

    public TimeZoneService()
    {
        // Timezone do Brasil (Horário de Brasília - UTC-3)
        _timeZoneBrasil = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
    }

    /// <summary>
    /// Retorna a data/hora atual no timezone do Brasil.
    /// </summary>
    public DateTime Agora => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZoneBrasil);

    /// <summary>
    /// Converte uma data UTC para o timezone do Brasil.
    /// </summary>
    public DateTime ConverterParaBrasil(DateTime dataUtc)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(dataUtc, _timeZoneBrasil);
    }

    /// <summary>
    /// Converte uma data local para o timezone do Brasil.
    /// </summary>
    public DateTime ConverterLocalParaBrasil(DateTime dataLocal)
    {
        var utc = dataLocal.ToUniversalTime();
        return TimeZoneInfo.ConvertTimeFromUtc(utc, _timeZoneBrasil);
    }

    /// <summary>
    /// Formata uma data no padrão brasileiro.
    /// </summary>
    public string FormatarData(DateTime data, string formato = "dd/MM/yyyy HH:mm:ss")
    {
        return data.ToString(formato);
    }
}
