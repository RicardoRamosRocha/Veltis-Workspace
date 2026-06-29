# Infraestrutura Compartilhada

A Sprint 0.5 cria componentes reutilizáveis para reduzir retrabalho nas próximas sprints.

## Componentes

- Entidade base com auditoria e soft delete.
- Result Pattern para respostas previsiveis.
- Paginação padronizada.
- Repositórios genéricos e Unit of Work.
- Contratos de serviços base.
- Middleware global de erros.
- Seed inicial preparado para roles e administrador opcional.

## Seed

O seed cria as roles `Administrator`, `User` e `Professional`. A criação do administrador depende de configuração externa e deve ser habilitada com `Seed:RunOnStartup`.

## Princípio

Nada nesta infraestrutura deve conter regra específica de profissão, dashboard, Workspace, documentos ou agentes concretos.

