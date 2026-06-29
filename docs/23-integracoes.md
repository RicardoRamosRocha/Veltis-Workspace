# Integrações

A Sprint 0.8 cria abstrações para integrações sem acoplar o produto a provedores especificos.

## Cache

- Atual: MemoryCache.
- Futuro: Redis.

## Storage

- Atual: LocalStorage.
- Futuro: Azure Blob, AWS S3, Cloudflare R2, MinIO.

## E-mail

- Atual: SMTP preparado como stub operacional.
- Futuro: SendGrid, Amazon SES, Mailgun, Resend.

## Filas

- Atual: fila in-memory preparada.
- Futuro: Hangfire, Quartz, Azure Queue, RabbitMQ.

