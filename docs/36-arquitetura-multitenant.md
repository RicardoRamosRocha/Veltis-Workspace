# Arquitetura Multi-Tenant

O multi-tenant foi preparado para crescimento gradual.

## Componentes

- `Tenant`: representa o cliente/organização.
- `TenantId`: campo opcional em entidades de cliente.
- `ITenantProvedor`: contrato para resolver o tenant atual.
- `CurrentTenantService`: implementação inicial.
- `TenantResolutionMiddleware`: resolve tenant por headers.

## Filtros EF Core

Entidades que implementam `ITenantEntity` recebem filtro preparado por tenant. Registros globais continuam permitidos com `TenantId` nulo.

## Headers

- `X-Tenant-Id`
- `X-Tenant-Slug`

