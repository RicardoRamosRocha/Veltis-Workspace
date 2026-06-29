# Pipeline de Execução

O pipeline de execução segue etapas substituíveis:

```mermaid
flowchart TD
  A["Selecionar agente"] --> B["Validar formulario"]
  B --> C["Montar prompt"]
  C --> D["Selecionar provedor"]
  D --> E["Selecionar modelo"]
  E --> F["Executar"]
  F --> G["Persistir histórico"]
  G --> H["Gerar documento"]
```

## Contratos

- `IAgentService`
- `IAgentExecutor`
- `IFormRenderer`
- `IAIModelSelector`
- `IAgentFactory`
- `IAgentHistoryService`
- `IDocumentGenerator`

## Estratégias

- Factory Pattern para provedor.
- Stratégy Pattern para execução.
- Builder Pattern para prompt.
- Dependency Injection para substituicao de componentes.

