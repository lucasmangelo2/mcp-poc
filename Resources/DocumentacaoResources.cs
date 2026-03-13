using System.ComponentModel;
using ModelContextProtocol.Server;

namespace SampleMcpServer.Resources;

/// <summary>
/// Resources para expor documentação sobre as funcionalidades do servidor.
/// </summary>
internal class DocumentacaoResources
{
    [McpServerResource]
    [Description("Documentação sobre o formato e validação de CEP brasileiro")]
    public string FormatoCep()
    {
        return @"📖 Documentação: Formato de CEP Brasileiro

🔢 Formato Válido:
   • XXXXX-XXX (com hífen)
   • XXXXXXXX (sem hífen)
   • Exatamente 8 dígitos numéricos

✅ Exemplos Válidos:
   • 01310-100
   • 01310100
   • 20040-020
   • 20040020

❌ Exemplos Inválidos:
   • 1234-567 (menos de 8 dígitos)
   • 12345-6789 (mais de 8 dígitos)
   • 01310-ABC (contém letras)
   • 01310 100 (com espaço)

🌐 API Utilizada:
   • ViaCEP (https://viacep.com.br)
   • Retorna: logradouro, bairro, cidade, estado, DDD

💡 Dicas:
   • O servidor aceita CEP com ou sem hífen
   • Espaços são automaticamente removidos
   • Consultas são salvas no histórico
";
    }

    [McpServerResource]
    [Description("Lista de operações matemáticas disponíveis na calculadora")]
    public string OperacoesCalculadora()
    {
        return @"📖 Documentação: Operações da Calculadora

➕ Soma
   • Método: Somar(a, b)
   • Descrição: Realiza a adição de dois números
   • Exemplo: Somar(5, 3) = 8
   • Parâmetros: a (número), b (número)

➖ Subtração
   • Método: Subtrair(a, b)
   • Descrição: Realiza a subtração de dois números
   • Exemplo: Subtrair(10, 4) = 6
   • Parâmetros: a (minuendo), b (subtraendo)

✖️ Multiplicação
   • Método: Multiplicar(a, b)
   • Descrição: Realiza a multiplicação de dois números
   • Exemplo: Multiplicar(6, 7) = 42
   • Parâmetros: a (número), b (número)

➗ Divisão
   • Método: Dividir(a, b)
   • Descrição: Realiza a divisão de dois números
   • Exemplo: Dividir(20, 4) = 5
   • Parâmetros: a (dividendo), b (divisor)
   • ⚠️ Atenção: Divisão por zero retorna erro

💡 Características:
   • Suporta números decimais
   • Precisão de double (64 bits)
   • Todos os cálculos são salvos no histórico
";
    }

    [McpServerResource]
    [Description("Exemplos práticos de uso das ferramentas disponíveis")]
    public string ExemplosUso()
    {
        return @"📖 Documentação: Exemplos de Uso

🔍 Consulta de CEP
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Tool: BuscarCepAsync
Entrada: ""01310-100""
Saída: Informações completas do endereço

🎲 Número Aleatório
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Tool: RetornaNumeroAleatorio
Entrada: min=1, max=100
Saída: Número entre 1 e 100

🔢 Calculadora
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Tool: Somar
Entrada: a=10, b=5
Saída: 15.0

Tool: Dividir
Entrada: a=100, b=4
Saída: 25.0

📊 Histórico e Estatísticas
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Resource: ObterHistoricoCep
Retorna: Últimas 20 consultas de CEP

Resource: ObterHistoricoCalculos
Retorna: Últimos 20 cálculos realizados

Resource: ObterEstatisticas
Retorna: Estatísticas gerais de uso

Resource: BuscarCepNoHistorico
Entrada: cep=""01310-100""
Retorna: Todas as consultas desse CEP

🔍 Documentação
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Resource: FormatoCep
Retorna: Documentação sobre formato de CEP

Resource: OperacoesCalculadora
Retorna: Lista de operações matemáticas

Resource: ExemplosUso (este resource)
Retorna: Exemplos práticos de uso

🖥️ Sistema
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Resource: InformacoesServidor
Retorna: Informações sobre o servidor

Resource: HealthCheck
Retorna: Status de saúde do servidor
";
    }

    [McpServerResource]
    [Description("Lista completa de todas as ferramentas (tools) disponíveis")]
    public string ListaFerramentas()
    {
        return @"📖 Documentação: Lista de Ferramentas

🔧 TOOLS (Ações)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

📍 CEP Tools
   • BuscarCepAsync(cep)
   • ValidarCep(cep)

🔢 Calculadora Tools
   • Somar(a, b)
   • Subtrair(a, b)
   • Multiplicar(a, b)
   • Dividir(a, b)

🎲 Número Aleatório Tools
   • RetornaNumeroAleatorio(min, max)

📚 RESOURCES (Dados)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

📊 Histórico Resources
   • ObterHistoricoCep()
   • ObterHistoricoCalculos()
   • ObterEstatisticas()
   • BuscarCepNoHistorico(cep)

📖 Documentação Resources
   • FormatoCep()
   • OperacoesCalculadora()
   • ExemplosUso()
   • ListaFerramentas()

🖥️ System Resources
   • InformacoesServidor()
   • HealthCheck()

💡 Diferença entre Tools e Resources:
   • Tools: Executam ações (consultar API, calcular)
   • Resources: Expõem dados (histórico, docs, status)
";
    }
}
