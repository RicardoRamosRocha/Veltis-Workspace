# Padroes de Repositorio

Foram criados contratos genericos para acesso a dados:

- `IReadRepository<T>`
- `IRepository<T>`
- `IUnitOfWork`

## Implementacao

`Repository<T>` e `UnitOfWork` ficam em Infrastructure, pois dependem de Entity Framework Core.

## Diretrizes

- Usar repositorios genericos para operacoes basicas.
- Evitar repositories especificos antes de existir regra real que justifique.
- Manter queries complexas em casos de uso ou especificacoes futuras.
