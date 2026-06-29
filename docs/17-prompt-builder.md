# Prompt Builder

O Prompt Builder compõe o prompt final em etapas substituíveis.

```mermaid
flowchart TD
  A["Prompt do Sistema"] --> B["Prompt da Profissão"]
  B --> C["Prompt do Agente"]
  C --> D["Dados do Formulário"]
  D --> E["Preferências do Usuário"]
  E --> F["Regras"]
  F --> G["Prompt Final"]
```

## Contratos

- `IPromptBuilder`
- `IPromptRenderer`

## Implementação inicial

`PromptBuilder` concatena seções de contexto e usa `PromptRenderer` para substituir variáveis no formato `{{variável}}`.

