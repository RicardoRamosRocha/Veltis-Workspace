# Infraestrutura Compartilhada

A Sprint 0.5 cria componentes reutilizaveis para reduzir retrabalho nas proximas sprints.

## Componentes

- Entidade base com auditoria e soft delete.
- Result Pattern para respostas previsiveis.
- Paginacao padronizada.
- Repositorios genericos e Unit of Work.
- Contratos de servicos base.
- Middleware global de erros.
- Seed inicial preparado para roles e administrador opcional.

## Seed

O seed cria as roles `Administrator`, `User` e `Professional`. A criacao do administrador depende de configuracao externa e deve ser habilitada com `Seed:RunOnStartup`.

## Principio

Nada nesta infraestrutura deve conter regra especifica de profissao, dashboard, workspace, documentos ou agentes concretos.
