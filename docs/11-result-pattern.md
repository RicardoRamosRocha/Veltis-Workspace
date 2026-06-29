# Result Pattern

O Result Pattern padroniza retornos de services e casos de uso.

## Tipos

- `Result`: sucesso ou falha sem dados.
- `Result<T>`: sucesso ou falha com dados.
- `Error`: erro padrão com código e mensagem.
- `ValidationError`: erro de validação.

## Uso

```csharp
Result.Success();
Result.Failure("Mensagem de erro");
Result<string>.Success("dados");
Result<string>.Failure("Mensagem de erro");
```

## Benefícios

- Evita retornos desorganizados.
- Reduz uso de excecoes para fluxo esperado.
- Facilita validações e mensagens amigáveis.

