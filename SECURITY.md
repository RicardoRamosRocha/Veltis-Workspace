# Security

## Reporting a Vulnerability

Reporte vulnerabilidades de seguranca diretamente aos mantenedores do projeto. Nao publique detalhes sensiveis em issues publicas antes de uma avaliacao.

## Supported Versions

Durante a fase inicial, apenas a branch `main` e suportada para correcoes de seguranca.

## Secrets

- Nao versionar senhas, tokens, chaves de API ou strings de conexao reais.
- Usar variaveis de ambiente, secrets do GitHub Actions ou configuracao segura do ambiente.
- O seed de administrador deve receber senha via configuracao, nunca fixa no codigo.
