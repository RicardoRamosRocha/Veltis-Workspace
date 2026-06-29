# Paginação

A paginação usa `PagedRequest`, `PagedResult<T>` e `PaginationMetadata`.

## Regras

- `PageNumber` minimo: 1.
- `PageSize` padrão: 10.
- `PageSize` maximo: 100.

## Metadados

O resultado informa total de itens, total de páginas, pagina atual, tamanho da pagina e indicadores de pagina anterior/próxima.

