# Billing

A estrutura de billing ainda não possui integração com meios de pagamento. Ela prepara a modelagem para operação SaaS.

## Entidades

- `Plan`
- `Subscription`
- `Invoice`
- `Payment`
- `Usage`
- `Credit`

## Evolução futura

Integrações como Stripe, Pagar.me, Mercado Pago ou provedores internacionais devem ser implementadas na Infrastructure, preservando Application e Domain.

