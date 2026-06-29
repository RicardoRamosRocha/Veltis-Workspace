# Configuração da API Key

## Appsettings Development

Em desenvolvimento, a seção esperada é:

```json
{
  "AI": {
    "DefaultProvider": "OpenAI",
    "OpenAI": {
      "ApiKey": "",
      "Model": "gpt-4o-mini",
      "BaseUrl": "https://api.openai.com/v1"
    }
  }
}
```

Não coloque chave real no repositório.

## Variável de Ambiente

Preferencialmente configure a chave por variável de ambiente:

```powershell
$env:AI__OpenAI__ApiKey="SUA_CHAVE_LOCAL"
```

Depois reinicie a aplicação.

## Teste

1. Acesse `/AgentTest`.
2. Preencha título e descrição.
3. Execute o pipeline.
4. Verifique provider, modelo, resposta, tokens e custo estimado.

## Fallback

Quando `AI__OpenAI__ApiKey` ou `AI:OpenAI:ApiKey` estiver vazia, o sistema não chama a API externa. Ele retorna resposta simulada com mensagem clara de fallback.

