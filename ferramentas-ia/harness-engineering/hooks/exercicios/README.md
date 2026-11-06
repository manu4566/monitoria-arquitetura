# 🎯 Exercícios — Hooks

## 📋 Visão Geral

| # | Exercício | Dificuldade | Tempo estimado |
|---|---|---|---|
| 1 | [Primeiro Hook](./exercicio-01-primeiro-hook.md) | 🟢 Iniciante | 30-45 min |
| 2 | [Hook como Guide](./exercicio-02-hook-como-guide.md) | 🟡 Intermediário | 60-90 min |
| 3 | [Hook como Sensor](./exercicio-03-hook-como-sensor.md) | 🔴 Avançado | 90-120 min |

## 🗺️ Progressão

```
Exercício 1              Exercício 2                    Exercício 3
─────────────────        ──────────────────────         ──────────────────────────
Configurar um         →  PreToolUse bloqueando       →  PostToolUse rodando o
hook simples,             edição de arquivo               sensor de arquitetura,
entender o contrato       protegido (guide               fechando o ciclo
JSON/exit code             dinâmico)                       Guides+Sensores+Hooks
```

## ✅ Pré-requisitos Gerais

- Claude Code instalado e configurado
- .NET SDK instalado (`dotnet --version`)
- PowerShell disponível (`powershell`, Windows PowerShell 5.1 ou superior)
- Ter concluído [`guides/`](../../guides/exercicios/) e [`sensores/`](../../sensores/exercicios/) — o Exercício 3 reaproveita os artefatos das duas

## 💻 Código de Apoio

Os exercícios usam o mesmo projeto de apoio das outras subdivisões: **MonitoriaBase**, em
`ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/`.

## 📂 Artefatos de Referência

Veja o `settings.json` e os scripts prontos em:
`ferramentas-ia/harness-engineering/hooks/codigo-exemplo/exemplo-hooks/`
