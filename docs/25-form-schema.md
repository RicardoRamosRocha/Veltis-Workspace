# Form Schema

## Objetivo

O Form Schema descreve os campos, validações e organização visual dos formulários dinâmicos. Ele é armazenado em `FormDefinition.SchemaJson` e interpretado pela camada Application.

## Estrutura Principal

```json
{
  "id": "briefing-juridico",
  "name": "Briefing Jurídico",
  "description": "Coleta inicial de informações para atendimento.",
  "version": 1,
  "fields": [],
  "layout": []
}
```

## Tipos de Campo

O motor suporta inicialmente:

Textbox, Textarea, Number, Decimal, Currency, Email, Password, Phone, CPF, CNPJ, Date, Time, DateTime, Checkbox, Radio, Switch, Select, MultiSelect, Tags, Color, Url, File, Image, Json, Markdown, RichText, Hidden, Label, Divider, Section, Repeater e Grid.

## Configuração de Campo

Cada campo pode declarar:

| Propriedade | Finalidade |
| --- | --- |
| `id` | Identificador técnico. |
| `name` | Nome usado no envio dos dados. |
| `label` | Rótulo exibido. |
| `placeholder` | Texto auxiliar no campo. |
| `helpText` | Ajuda contextual. |
| `required` | Indica obrigatoriedade. |
| `visible` | Controla visibilidade. |
| `readOnly` | Define somente leitura. |
| `defaultValue` | Valor inicial. |
| `minLength` | Tamanho mínimo. |
| `maxLength` | Tamanho máximo. |
| `min` | Valor mínimo. |
| `max` | Valor máximo. |
| `mask` | Máscara sugerida. |
| `validation` | Expressão regular personalizada. |
| `cssClass` | Classe visual adicional. |
| `order` | Ordem de exibição. |
| `width` | Largura responsiva. |
| `options` | Opções para seleção, rádio ou múltipla seleção. |

## Exemplo

```json
{
  "id": "cadastro-profissional",
  "name": "Cadastro Profissional",
  "version": 1,
  "fields": [
    {
      "id": "nome",
      "name": "nome",
      "type": "Textbox",
      "label": "Nome",
      "required": true,
      "maxLength": 120,
      "order": 1,
      "width": "1/2"
    },
    {
      "id": "email",
      "name": "email",
      "type": "Email",
      "label": "E-mail",
      "required": true,
      "order": 2,
      "width": "1/2"
    }
  ]
}
```

## Versionamento

Toda alteração deve criar uma nova instância de `FormDefinition` com versão incrementada. Versões anteriores permanecem preservadas para auditoria, histórico e reprocessamento futuro.

