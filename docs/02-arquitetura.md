# Arquitetura

O Veltis Workspace segue Clean Architecture para preservar o domínio e manter infraestrutura, interface e detalhes externos substituíveis.

## Camadas

- `Domain`: entidades centrais, tipos de identidade e regras de domínio independentes.
- `Application`: contratos, casos de uso futuros, validações e orquestração.
- `Infrastructure`: persistência, Identity, PostgreSQL e integrações externas.
- `Web`: ASP.NET Core MVC, controllers, views, configuração HTTP e composição da aplicação.

## Direção de dependências

`Web` faz referência a `Application` e `Infrastructure`. `Infrastructure` faz referência a `Application` e `Domain`. `Application` faz referência a `Domain`. `Domain` não depende das demais camadas.

## Persistência

A persistência usa Entity Framework Core com provedor Npgsql para PostgreSQL. O schema inicial é `workspace`, isolando os objetos da aplicação.

## Autenticação

ASP.NET Core Identity foi configurado com `ApplicationUser` e `ApplicationRole` usando identificadores `Guid`. A estrutura está pronta para evoluir com permissões, roles de sistema, claims, políticas e auditoria.

## Sprint 1

A Sprint 1 adiciona a estrutura funcional inicial sem alterar a arquitetura principal. Profissões, relação usuário-profissão, Workspace do usuário, dashboard inicial e administração usam as mesmas camadas: entidades no Domain, DTOs/services no Application, EF Core no Infrastructure e MVC no Web.

## Sprint 0.8

A Sprint 0.8 adiciona fundamentos SaaS Enterprise: multi-tenant preparado, feature flags, permissões granulares, billing base, observabilidade, auditoria, cache, storage, e-mail, filas, eventos e contratos de IA.

