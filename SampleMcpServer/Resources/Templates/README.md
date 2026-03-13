# Templates de Resources

Esta pasta contém templates JSON Schema que definem a estrutura dos dados retornados pelos Resources do servidor MCP.

## 📁 Estrutura

```
Templates/
├── README.md                          # Este arquivo
├── historico-cep.json                # Schema para histórico de consultas CEP
├── estatisticas.json                 # Schema para estatísticas gerais
└── docs/                              # Templates de documentação
    ├── formato-cep.json              # Schema para documentação de CEP
    ├── operacoes-calculadora.json    # Schema para operações matemáticas
    └── health-check.json             # Schema para health check
```

## 🎯 Propósito

Estes templates servem como:

1. **Documentação**: Especificam exatamente quais dados são retornados
2. **Validação**: Podem ser usados para validar a estrutura dos dados
3. **Contratos**: Definem contratos claros entre servidor e cliente
4. **Exemplos**: Incluem exemplos de uso para cada estrutura

## 📋 Templates Disponíveis

### historico-cep.json
Define a estrutura do histórico de consultas de CEP.

**Resource associado**: `HistoricoResources.ObterHistoricoCep()`

**Campos principais**:
- `total_consultas`: Número total de consultas
- `consultas[]`: Array com detalhes de cada consulta
  - `cep`: CEP consultado
  - `data_consulta`: Timestamp da consulta
  - `sucesso`: Boolean indicando sucesso/falha
  - `resultado`: Dados retornados (quando sucesso)
  - `erro`: Mensagem de erro (quando falha)

### estatisticas.json
Define a estrutura das estatísticas gerais do servidor.

**Resource associado**: `HistoricoResources.ObterEstatisticas()`

**Campos principais**:
- `servidor`: Informações sobre o servidor (nome, versão, uptime)
- `consultas_cep`: Estatísticas de CEP (total, sucessos, falhas, taxa)
- `calculos`: Estatísticas de cálculos (total, por operação)
- `sistema`: Métricas do sistema (memória, threads, CPU)
- `ultima_atualizacao`: Timestamp da atualização

### docs/formato-cep.json
Define a estrutura da documentação sobre formato de CEP.

**Resource associado**: `DocumentacaoResources.FormatoCep()`

**Campos principais**:
- `formato`: Especificação do formato (dígitos, padrões aceitos)
- `validacao`: Regras de validação
- `exemplos`: Exemplos válidos e inválidos com motivos
- `api`: Informações sobre a API externa usada

### docs/operacoes-calculadora.json
Define a estrutura da documentação sobre operações matemáticas.

**Resource associado**: `DocumentacaoResources.OperacoesCalculadora()`

**Campos principais**:
- `operacoes[]`: Array com cada operação disponível
  - `tipo`: Tipo da operação (soma, subtração, etc.)
  - `simbolo`: Símbolo matemático
  - `metodo`: Nome do método/tool
  - `parametros[]`: Descrição dos parâmetros
  - `exemplos[]`: Exemplos de uso
  - `restricoes[]`: Limitações da operação
- `caracteristicas`: Informações técnicas (tipo numérico, precisão)

### docs/health-check.json
Define a estrutura do health check do servidor.

**Resource associado**: `SystemResources.HealthCheck()`

**Campos principais**:
- `status`: Status geral (healthy/degraded/unhealthy)
- `timestamp`: Momento da verificação
- `uptime`: Tempo de atividade do servidor
- `recursos`: Status de recursos (memória, threads)
- `servicos`: Status de serviços internos
- `conectividade`: Status de rede e DNS
- `endpoints[]`: Lista de endpoints e seus status

## 🔧 Como Usar

### Validação de Dados

Estes schemas podem ser usados com bibliotecas de validação JSON Schema:

```csharp
// Exemplo conceitual (não implementado)
var schema = JsonSchema.FromFile("Templates/estatisticas.json");
var data = GetEstatisticas();
var isValid = schema.Validate(data);
```

### Documentação API

Os schemas servem como documentação viva da API do servidor MCP. Clientes podem usar esses schemas para:

- Gerar código automaticamente (code generation)
- Validar respostas do servidor
- Criar interfaces de usuário dinâmicas
- Entender a estrutura dos dados sem chamar a API

### Versionamento

Quando a estrutura dos dados mudar:

1. Atualize o schema correspondente
2. Incremente a versão no schema se aplicável
3. Mantenha compatibilidade retroativa quando possível
4. Documente breaking changes

## 📚 JSON Schema

Todos os templates seguem a especificação **JSON Schema Draft 7**.

Documentação: https://json-schema.org/

## 💡 Notas

- Os templates incluem exemplos práticos no campo `example`
- Schemas podem ser estendidos conforme necessário
- Mantenha os schemas sincronizados com o código dos Resources
- Use validação automática em testes quando apropriado
