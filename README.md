# SampleMcpServer

Servidor MCP em C# (.NET 9) com foco em três grupos de funcionalidades:

- consulta e validacao de CEP brasileiro (ViaCEP)
- operacoes matematicas basicas
- historico, estatisticas, documentacao e prompts prontos para analise

O projeto sobe via HTTP na porta `5000` e expoe endpoint MCP com `MapMcp()`.

## O que este servidor entrega hoje

### Tools
- `RetornaNumeroAleatorio(min, max)`
- `Somar(a, b)`
- `Subtrair(a, b)`
- `Multiplicar(a, b)`
- `Dividir(a, b)`
- `BuscarCepAsync(cep)`
- `ValidarCep(cep)`

### Resources
- `ObterHistoricoCep()`
- `ObterHistoricoCalculos()`
- `ObterEstatisticas()`
- `BuscarCepNoHistorico(cep)`
- `InformacoesServidor()`
- `HealthCheck()`
- `FormatoCep()`
- `OperacoesCalculadora()`
- `ExemplosUso()`
- `ListaFerramentas()`

### Prompts
- `RelatorioConsultasCEP()`
- `AnaliseHistoricoCalculos()`
- `SumarioEstatisticas()`
- `ComparacaoRegioes()`
- `ExplicacaoMatematica(operacao)`
- `ValidacaoDados(ceps)`

## Subindo com Docker (caminho recomendado)

Com Docker e Docker Compose instalados, execute na raiz do projeto:

```bash
docker-compose -f docker-compose.yaml up --build
```

O servico sera publicado em:

- `http://localhost:5000`

Para parar:

```bash
docker-compose -f docker-compose.yaml down
```

## Como testar rápido

Depois de subir o container, conecte um cliente MCP apontando para o servidor HTTP e teste chamadas como:

- `ValidarCep("01310-100")`
- `BuscarCepAsync("01310100")`
- `Somar(25, 4)`
- `ObterEstatisticas()`
- `HealthCheck()`

## Execucao local sem Docker (opcional)

Se preferir rodar localmente:

```bash
dotnet run
```

## Estrutura resumida

- `Tools/`: acoes executaveis pelo cliente MCP
- `Resources/`: dados e consultas de apoio (historico, status, docs)
- `Prompts/`: templates para analise, validacao e explicacoes
- `Services/`: servicos internos (historico e fuso horario)
- `Models/`: modelos de dados

## Detalhes tecnicos

- .NET 9 com `ModelContextProtocol`
- transporte MCP em HTTP (`WithHttpTransport`)
- `HttpClient` via DI para consumo do ViaCEP
- cultura padrao definida para `pt-BR`
- historico de consultas e calculos centralizado em servico singleton

## Publicacao de pacote (quando fizer sentido)

Para empacotar:

```bash
dotnet pack -c Release
```

O `.nupkg` sera gerado em `bin/Release`.

## Referencias

- [Documentacao oficial do MCP](https://modelcontextprotocol.io/)
- [Especificacao do protocolo](https://spec.modelcontextprotocol.io/)
- [SDK C# ModelContextProtocol no NuGet](https://www.nuget.org/packages/ModelContextProtocol)
