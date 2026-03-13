using System.ComponentModel;
using ModelContextProtocol.Server;

namespace SampleMcpServer.Prompts;

/// <summary>
/// Prompts MCP para conteúdo educacional e explicações didáticas.
/// Templates parametrizados que referenciam recursos via URIs.
/// </summary>
internal class EducacionalPrompts
{
    [McpServerPrompt]
    [Description("Explica conceitos matemáticos baseados em um cálculo específico do histórico")]
    public string ExplicacaoMatematica(
        [Description("Tipo de operação a explicar (Soma, Subtração, Multiplicação, Divisão)")] string operacao)
    {
        return $@"🎓 Explicação Educacional - {operacao}

**Contexto:**
{{operacoes-calculadora://}}

**Histórico de Exemplos:**
{{historico-calculos://}}

**Suas tarefas:**
1. Explique o conceito matemático de '{operacao}' de forma simples
2. Use os exemplos do histórico para ilustrar
3. Forneça 3 aplicações práticas do dia a dia
4. Explique propriedades importantes (ex: comutativa, associativa)
5. Mostre casos especiais ou armadilhas comuns
6. Sugira exercícios baseados nos números do histórico
7. Inclua curiosidades históricas sobre a operação

**Público-alvo:**
- Estudantes do ensino fundamental/médio
- Profissionais que precisam revisar conceitos

**Formato da resposta:**
- Linguagem clara e acessível
- Use analogias do cotidiano
- Inclua emojis para tornar mais amigável
- Exemplos práticos e aplicáveis";
    }
}
