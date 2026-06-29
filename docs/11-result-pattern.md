# Result Pattern

O Result Pattern padroniza retornos de services e casos de uso.

## Tipos

- `Result`: sucesso ou falha sem dados.
- `Result<T>`: sucesso ou falha com dados.
- `Error`: erro padrao com codigo e mensagem.
- `ValidationError`: erro de validacao.

## Uso

```csharp
Result.Success();
Result.Failure("Mensagem de erro");
Result<string>.Success("dados");
Result<string>.Failure("Mensagem de erro");
```

## Beneficios

- Evita retornos desorganizados.
- Reduz uso de excecoes para fluxo esperado.
- Facilita validacoes e mensagens amigaveis.
