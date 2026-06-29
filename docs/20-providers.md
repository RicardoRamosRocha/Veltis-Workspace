# Provedores

Provedores de IA são selecionados por chave e resolvidos por `IAgentFactory`.

## Contrato

`IAIProvider` define:

- `Key`
- `ExecuteAsync`

## Provedores planejados

- OpenAI
- Gemini
- Anthropic
- Azure OpenAI
- OpenRouter
- Ollama
- DeepSeek
- Groq
- Mistral

## Sprint 2.1

Apenas a arquitetura foi preparada. A implementação `ArchitectureOnlyAIProvider` simula uma resposta local para validar o pipeline sem consumir APIs externas.

