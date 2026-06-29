# Modelagem de Agentes

## Entidades

- `AgentCategory`: agrupa agentes.
- `Agent`: configuração executavel universal.
- `PromptTemplate`: prompt base e instruções.
- `FormDefinition`: schema JSON e UI schema.
- `AIProvider`: provedor configurado.
- `AIModel`: modelo disponível.
- `AgentExecution`: histórico de execução.
- `GeneratédDocument`: documento inicial derivado da execução.

## FormDefinition

Formulários devem ser gerados dinamicamente a partir de `JsonSchema` e `UiSchema`. Não devem existir Razor Pages específicas por agente ou profissão.

## Histórico

`AgentExecution` guarda prompt, resposta, provedor, model, tokens, custo estimado e tempo de execução.

