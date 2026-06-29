# CRUDs Administrativos

## Visão Geral

Os CRUDs administrativos usam ASP.NET Core MVC, Entity Framework Core e Tailwind CSS. A maior parte dos cadastros de configuração utiliza um controller administrativo orientado por metadados, reduzindo duplicação e mantendo validações consistentes.

## Recursos Gerenciados

| Recurso | Campos principais |
| --- | --- |
| Categorias de Agentes | Nome, slug, descrição, ícone, ordem e status. |
| Agentes | Profissão, categoria, prompt, formulário, modelo, modo de execução, temperature, max tokens, flags e versão. |
| Prompts | System prompt, instruções, variáveis, formato de saída, idioma, tom e versão. |
| Formulários Dinâmicos | Nome, descrição, schema JSON, UI schema JSON, validação JSON, categoria, ícone, versão e publicação. |
| Providers de IA | Nome, chave e status. |
| Modelos de IA | Provider, nome do modelo, janela de contexto, recursos, tokens e preços. |
| Tenants | Nome, slug, região e status. |
| Feature Flags | Tenant, chave da feature e status. |
| Planos | Nome, slug, preço mensal, moeda e status. |

## Validações

As validações administrativas cobrem:

- Campos obrigatórios.
- Slug obrigatório e em formato consistente.
- JSON válido.
- Preço não negativo.
- `MaxTokens` positivo.
- `Temperature` entre 0 e 2.

## Listagens

As listagens possuem busca textual, paginação e ordenação simples por campo principal. A desativação lógica usa campos como `Active`, `IsActive`, `Enabled` ou `IsPublished`, conforme o recurso.

## Preview de Formulários

Formulários dinâmicos possuem rota de preview que utiliza o Dynamic Form Engine existente. O preview não executa IA e não salva submissões.

