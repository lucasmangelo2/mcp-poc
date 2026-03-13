using System.ComponentModel;
using ModelContextProtocol.Server;

namespace SampleMcpServer.Prompts;

/// <summary>
/// Prompts MCP para validação de dados e auditoria de operações.
/// Templates parametrizados que referenciam recursos via URIs.
/// </summary>
internal class ValidacaoPrompts
{
    [McpServerPrompt]
    [Description("Valida múltiplos CEPs e fornece análise de qualidade dos dados")]
    public string ValidacaoDados(
        [Description("Lista de CEPs separados por vírgula para validar")] string ceps)
    {
        return $@"✅ Validação e Análise de Dados

**CEPs para validar:**
{ceps}

**Documentação de Referência:**
{{formato-cep://}}

**Histórico para Comparação:**
{{historico-cep://}}

**Suas tarefas:**
1. Para cada CEP fornecido:
   - Valide o formato (8 dígitos)
   - Identifique o estado (pelos 2 primeiros dígitos)
   - Verifique se já foi consultado anteriormente
   - Compare com padrões conhecidos

2. Análise agregada:
   - Quantos CEPs válidos vs inválidos
   - Distribuição geográfica (estados)
   - Padrões ou sequências
   - Possíveis duplicatas

3. Recomendações:
   - CEPs que precisam correção
   - Sugestões de formatação
   - CEPs suspeitos ou incomuns

**Formato da resposta:**
- Tabela com status de cada CEP
- Resumo estatístico
- Lista de ações recomendadas
- Use ✅ para válidos, ❌ para inválidos, ⚠️ para suspeitos";
    }
}
