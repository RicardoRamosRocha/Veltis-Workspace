# OpenAI Provider

## Objetivo

O OpenAI Provider é a primeira implementação real de `IAiChatProvider`. Ele usa `HttpClientFactory` e HTTP direto, sem SDK externo nesta sprint.

## Endpoint

```text
POST /chat/completions
```

## Payload

```json
{
  "model": "gpt-4o-mini",
  "messages": [
    { "role": "system", "content": "..." },
    { "role": "user", "content": "..." }
  ],
  "temperature": 0.2,
  "max_tokens": 2000
}
```

## Segurança

- A API Key não é exibida na tela.
- A API Key não é gravada em log.
- A API Key não é salva no banco.
- Nenhuma chave real deve ser commitada.

## Evolução

Novos providers devem implementar `IAiChatProvider` e ser registrados por DI. A arquitetura já está preparada para Gemini, Anthropic, OpenRouter, Groq, DeepSeek, Ollama e Azure OpenAI.

