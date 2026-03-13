using System.ComponentModel;
using ModelContextProtocol.Server;

namespace SampleMcpServer.Prompts;

/// <summary>
/// Prompts MCP para análise de dados e geração de relatórios.
/// Templates parametrizados que referenciam recursos via URIs.
/// </summary>
internal class AnaliticoPrompts
{
    [McpServerPrompt]
    [Description("Gera um relatório detalhado das consultas de CEP realizadas")]
    public string RelatorioConsultasCEP()
    {
        return @"📊 Análise de Consultas de CEP

**Dados Disponíveis:**
{{historico-cep://}}

**Suas tarefas:**
1. Analise os dados do histórico de CEPs consultados
2. Identifique os estados mais consultados (baseado nos CEPs)
3. Liste os horários de maior atividade
4. Identifique padrões nas consultas (sequenciais, repetidas, etc)
5. Calcule métricas de qualidade (taxa de erro, tempo médio)
6. Forneça 3-5 insights acionáveis baseados nos dados
7. Sugira melhorias para o serviço

**Formato da resposta:**
- Use seções claras com emojis
- Inclua números e percentuais
- Seja objetivo e direto
- Destaque insights importantes em negrito";
    }

    [McpServerPrompt]
    [Description("Analisa o histórico de cálculos e identifica padrões matemáticos")]
    public string AnaliseHistoricoCalculos()
    {
        return @"🔢 Análise Matemática do Histórico

**Dados Disponíveis:**
{{historico-calculos://}}

**Suas tarefas:**
1. Identifique qual operação é mais utilizada e por quê
2. Analise os números mais frequentes nas operações
3. Detecte padrões (ex: multiplicações por 2, divisões por 10)
4. Identifique se há sequências matemáticas (fibonacci, primos, etc)
5. Calcule médias e medianas dos resultados
6. Sugira operações adicionais que seriam úteis
7. Identifique possíveis casos de uso (financeiro, científico, etc)

**Formato da resposta:**
- Separe por tipo de operação
- Use exemplos concretos do histórico
- Inclua visualizações textuais se apropriado (gráficos ASCII)
- Destaque descobertas interessantes";
    }

    [McpServerPrompt]
    [Description("Cria um sumário executivo das estatísticas gerais do servidor")]
    public string SumarioEstatisticas()
    {
        return @"📈 Sumário Executivo do Servidor MCP

**Métricas Atuais:**
{{estatisticas://}}

**Suas tarefas:**
1. Crie um sumário executivo em formato de apresentação
2. Destaque as métricas mais importantes
3. Compare desempenho entre os diferentes serviços
4. Identifique o serviço mais utilizado e por quê
5. Avalie a saúde geral do sistema (baseado na taxa de erro)
6. Forneça 3 KPIs principais
7. Sugira 3 melhorias de curto prazo
8. Propor 2 melhorias de longo prazo

**Formato da resposta:**
- Estilo executivo/gerencial
- Máximo 500 palavras
- Use bullet points
- Inclua uma conclusão com próximos passos";
    }

    [McpServerPrompt]
    [Description("Compara múltiplos CEPs e fornece análise geográfica comparativa")]
    public string ComparacaoRegioes()
    {
        return @"🗺️ Comparação Geográfica de Regiões

**CEPs disponíveis no histórico:**
{{historico-cep://}}

**Suas tarefas:**
1. Para os CEPs consultados, identifique:
   - Estados e regiões do Brasil
   - Características econômicas de cada região
   - Distâncias aproximadas entre eles
   - Diferenças de fuso horário

2. Análise comparativa:
   - Compare desenvolvimento econômico
   - Densidades populacionais
   - Características culturais
   - Custo de vida relativo

3. Insights:
   - Padrões geográficos nos CEPs consultados
   - Possíveis motivos para consultas (negócios, mudança, etc)
   - Regiões sub-representadas

**Formato da resposta:**
- Use mapas textuais se apropriado
- Tabela comparativa com principais métricas
- Gráficos ASCII para visualização
- Destaques com emojis regionais";
    }
}
