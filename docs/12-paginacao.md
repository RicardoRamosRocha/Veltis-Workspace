# Paginacao

A paginacao usa `PagedRequest`, `PagedResult<T>` e `PaginationMetadata`.

## Regras

- `PageNumber` minimo: 1.
- `PageSize` padrao: 10.
- `PageSize` maximo: 100.

## Metadados

O resultado informa total de itens, total de paginas, pagina atual, tamanho da pagina e indicadores de pagina anterior/proxima.
