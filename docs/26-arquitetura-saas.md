# Arquitetura SaaS Enterprise

A Sprint 0.8 fortalece a fundação do Veltis Workspace para operação SaaS moderna sem alterar a arquitetura principal.

## Bases adicionadas

- Multi-tenant preparado com `Tenant`, `TenantId`, `ITenantProvedor` e filtros EF Core.
- Feature flags por tenant e recurso.
- Permissões reutilizáveis alem de roles.
- Billing base com planos, assinaturas, faturas, pagamentos, uso e créditos.
- Observabilidade com health checks, logs estruturáveis e auditoria.
- Abstrações para cache, storage, e-mail, fila, eventos e IA.

## Limite da sprint

Não há isolamento completo por tenant, integração real de pagamentos, provedores de IA reais, filas externas ou storage em nuvem. A arquitetura está preparada para essas evolucoes.

