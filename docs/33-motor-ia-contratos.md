# Motor de IA - Contratos

A Sprint 0.8 não implementa IA. Apenas cria contratos e provedores vazios para preservar o desenho futuro.

## Contratos

- `IAiProvedor`
- `IAiModel`
- `IAgentExecutor`
- `IAgentContext`
- `IAgentPromptBuilder`
- `IAgentMemory`

## Provedores preparados

- OpenAI
- Anthropic
- Google Gemini
- Azure OpenAI
- OpenRouter
- Ollama
- DeepSeek
- Groq
- Mistral

Implementações reais devem respeitar os contratos e ficar desacopladas da interface web.

