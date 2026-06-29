# Painel Administrativo Inicial

## Objetivo

O Painel Administrativo Inicial centraliza a configuração operacional do Veltis Workspace antes da integração real com IA. Ele permite que administradores cadastrem profissões, agentes, prompts, formulários dinâmicos, providers, modelos, tenants, feature flags, planos e consultem auditoria.

## Rotas

| Área | Rota |
| --- | --- |
| Dashboard administrativo | `/admin` |
| Profissões | `/admin/professions` |
| Categorias de Agentes | `/admin/agent-categories` |
| Agentes | `/admin/agents` |
| Prompts | `/admin/prompts` |
| Formulários Dinâmicos | `/admin/form-definitions` |
| Providers de IA | `/admin/ai-providers` |
| Modelos de IA | `/admin/ai-models` |
| Tenants | `/admin/tenants` |
| Feature Flags | `/admin/feature-flags` |
| Planos | `/admin/plans` |
| Auditoria | `/admin/audit` |

## Segurança

Todas as rotas administrativas exigem autenticação e role `Administrator` ou `Gestor`. Usuários não autenticados são redirecionados para login. Usuários autenticados sem permissão recebem acesso negado.

## Dashboard

O dashboard em `/admin` exibe contadores de:

- Profissões
- Agentes
- Formulários
- Prompts
- Providers
- Modelos
- Tenants
- Planos
- Usuários

## Limites da Sprint

Esta sprint não integra OpenAI, Gemini ou qualquer provider real. O painel apenas configura dados existentes para uso futuro pelo Framework Universal de Agentes.
