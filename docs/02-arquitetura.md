# Arquitetura

O Veltis Workspace segue Clean Architecture para preservar o dominio e manter infraestrutura, interface e detalhes externos substituiveis.

## Camadas

- `Domain`: entidades centrais, tipos de identidade e regras de dominio independentes.
- `Application`: contratos, casos de uso futuros, validacoes e orquestracao.
- `Infrastructure`: persistencia, Identity, PostgreSQL e integracoes externas.
- `Web`: ASP.NET Core MVC, controllers, views, configuracao HTTP e composicao da aplicacao.

## Direcao de dependencias

`Web` referencia `Application` e `Infrastructure`. `Infrastructure` referencia `Application` e `Domain`. `Application` referencia `Domain`. `Domain` nao depende das demais camadas.

## Persistencia

A persistencia usa Entity Framework Core com provider Npgsql para PostgreSQL. O schema inicial e `workspace`, isolando os objetos da aplicacao.

## Autenticacao

ASP.NET Core Identity foi configurado com `ApplicationUser` e `ApplicationRole` usando identificadores `Guid`. A estrutura esta pronta para evoluir com permissoes, roles de sistema, claims, politicas e auditoria.

## Sprint 1

A Sprint 1 adiciona a estrutura funcional inicial sem alterar a arquitetura principal. Profissoes, relacao usuario-profissao, workspace do usuario, dashboard inicial e administracao usam as mesmas camadas: entidades no Domain, DTOs/services no Application, EF Core no Infrastructure e MVC no Web.
