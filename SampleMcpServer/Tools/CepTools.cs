using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

/// <summary>
/// Ferramentas para consulta de informações de CEP.
/// </summary>
internal class CepTools
{
    private static readonly HttpClient httpClient = new HttpClient();

    [McpServerTool]
    [Description("Busca informações de endereço através do CEP brasileiro.")]
    public string BuscarCep(
        [Description("CEP no formato XXXXX-XXX ou XXXXXXXX")] string cep)
    {
        try
        {
            // Remove caracteres especiais do CEP
            string cepLimpo = cep.Replace("-", "").Replace(".", "").Replace(" ", "");
            
            // Valida se o CEP tem 8 dígitos
            if (cepLimpo.Length != 8 || !cepLimpo.All(char.IsDigit))
            {
                return "❌ CEP inválido. Deve conter exatamente 8 dígitos.";
            }

            // Chama a API ViaCEP de forma síncrona
            string url = $"https://viacep.com.br/ws/{cepLimpo}/json/";
            using HttpResponseMessage response = httpClient.GetAsync(url).GetAwaiter().GetResult();
            
            if (!response.IsSuccessStatusCode)
            {
                return "❌ Erro ao consultar a API de CEP.";
            }

            string jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            
            using JsonDocument document = JsonDocument.Parse(jsonResponse);
            JsonElement root = document.RootElement;

            // Verifica se o CEP foi encontrado
            if (root.TryGetProperty("erro", out _))
            {
                return "❌ CEP não encontrado.";
            }

            // Extrai as informações
            string logradouro = root.TryGetProperty("logradouro", out var logProp) ? logProp.GetString() ?? "" : "";
            string complemento = root.TryGetProperty("complemento", out var compProp) ? compProp.GetString() ?? "" : "";
            string bairro = root.TryGetProperty("bairro", out var bairroProp) ? bairroProp.GetString() ?? "" : "";
            string localidade = root.TryGetProperty("localidade", out var locProp) ? locProp.GetString() ?? "" : "";
            string uf = root.TryGetProperty("uf", out var ufProp) ? ufProp.GetString() ?? "" : "";
            string ddd = root.TryGetProperty("ddd", out var dddProp) ? dddProp.GetString() ?? "" : "";

            // Formata o resultado
            var resultado = $@"📍 Informações do CEP: {cep}

🏠 Logradouro: {logradouro}
{(string.IsNullOrWhiteSpace(complemento) ? "" : $"📝 Complemento: {complemento}\n")}🏘️ Bairro: {bairro}
🏙️ Cidade: {localidade}
🗺️ Estado: {uf}
📞 DDD: {ddd}

✅ Consulta realizada com sucesso!";

            return resultado;
        }
        catch (Exception ex)
        {
            return $"❌ Erro ao buscar informações do CEP: {ex.Message}";
        }
    }

    [McpServerTool]
    [Description("Valida se um CEP possui formato válido (8 dígitos).")]
    public string ValidarCep(
        [Description("CEP para validar")] string cep)
    {
        try
        {
            // Remove caracteres especiais do CEP
            string cepLimpo = cep.Replace("-", "").Replace(".", "").Replace(" ", "");
            
            if (cepLimpo.Length != 8)
            {
                return $"❌ CEP inválido: '{cep}' deve conter exatamente 8 dígitos.";
            }

            if (!cepLimpo.All(char.IsDigit))
            {
                return $"❌ CEP inválido: '{cep}' deve conter apenas números.";
            }

            // Formata o CEP
            string cepFormatado = $"{cepLimpo.Substring(0, 5)}-{cepLimpo.Substring(5, 3)}";
            
            return $"✅ CEP válido: {cepFormatado}";
        }
        catch (Exception ex)
        {
            return $"❌ Erro ao validar CEP: {ex.Message}";
        }
    }
}