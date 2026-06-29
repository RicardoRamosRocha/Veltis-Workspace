# Design System

## Objetivo

O Design System do Veltis Workspace define uma base visual reutilizável para aplicações SaaS profissionais, mantendo consistência entre formulários, tabelas, cards, navegação e componentes administrativos.

## Estrutura

| Área | Finalidade |
| --- | --- |
| Components | Elementos reutilizáveis de interface. |
| Layouts | Organização de tela, sidebar, cabeçalho, rodapé e conteúdo. |
| Icons | Espaço reservado para padronização futura de ícones. |
| Themes | Preparação para modo claro e modo escuro. |
| Typography | Escala textual, pesos e hierarquia visual. |
| Spacing | Espaçamentos consistentes entre blocos. |
| Colors | Paleta funcional para SaaS corporativo. |
| Utilities | Classes auxiliares para estados e padrões recorrentes. |

## Diretrizes Visuais

- Layout limpo, responsivo e orientado à produtividade.
- Cards com raio discreto e bordas leves.
- Inputs com altura e espaçamento consistentes.
- Tabelas preparadas para leitura rápida.
- Estados de feedback claros.
- Modo escuro preparado por classes `dark`.

## Tokens CSS

Os padrões iniciais ficam em `wwwroot/css/site.css`:

- `.vw-button`
- `.vw-card`
- `.vw-field`
- `.vw-label`
- `.vw-help`
- `.vw-input`
- `.vw-badge`

Esses tokens apoiam os componentes Razor e reduzem duplicação visual.

