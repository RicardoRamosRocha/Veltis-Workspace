# Padrões de Repositório

Foram criados contratos genéricos para acesso a dados:

- `IReadRepository<T>`
- `IRepository<T>`
- `IUnitOfWork`

## Implementação

`Repository<T>` e `UnitOfWork` ficam em Infrastructure, pois dependem de Entity Framework Core.

## Diretrizes

- Usar repositórios genéricos para operações basicas.
- Evitar repositories especificos antes de existir regra real que justifique.
- Manter queries complexas em casos de uso ou específicações futuras.

