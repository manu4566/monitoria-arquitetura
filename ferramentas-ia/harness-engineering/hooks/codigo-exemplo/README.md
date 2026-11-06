# 💻 Código de Exemplo — Hooks

## 📋 O que tem aqui

Um `settings.json` completo e os 3 scripts que ele referencia — o guardrail (`PreToolUse`), o sensor de arquitetura conectado via hook (`PostToolUse`) e a variante do guardrail como git hook — prontos para copiar e adaptar.

## 📂 Estrutura

```
codigo-exemplo/
└── exemplo-hooks/
    ├── .claude/
    │   └── settings.json                     ← guardrail + sensor configurados
    └── scripts/
        ├── guide-guardrail.ps1               ← PreToolUse: bloqueia arquivos protegidos
        ├── guide-guardrail-pre-commit.ps1     ← mesma regra, como git hook
        └── sensor-fitness-arquitetural.ps1    ← PostToolUse: roda a fitness function
```

## 🎯 Como usar estes artefatos

### Como referência de qualidade

Compare com o que você escreveu nos Exercícios 2 e 3 de `hooks/`:
- O guardrail bloqueia só o necessário, sem falsos positivos em arquivos não protegidos?
- O sensor de arquitetura filtra por extensão (`.cs`) antes de rodar `dotnet test`, para não disparar em toda e qualquer edição?
- A mensagem de erro devolvida ao agente é específica o suficiente para ele se corrigir sem intervenção manual?

### Como ponto de partida

Comandos a partir da raiz do repositório:

```bash
cp -r ferramentas-ia/harness-engineering/hooks/codigo-exemplo/exemplo-hooks/.claude \
      ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/.claude
cp -r ferramentas-ia/harness-engineering/hooks/codigo-exemplo/exemplo-hooks/scripts \
      ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/scripts
```

Certifique-se de que `ArchitectureFitnessTests.cs` (de `sensores/codigo-exemplo/exemplo-sensores/`) já está em `tests/MonitoriaBase.Tests/` antes de testar o sensor.

## 📖 Como Interpretar Cada Arquivo

| Arquivo | Papel | O que revisar |
|---|---|---|
| `.claude/settings.json` | Conecta os hooks ao Claude Code | Os `matcher` cobrem as ferramentas certas? |
| `guide-guardrail.ps1` | Guide dinâmico (`PreToolUse`) | Lista de arquivos protegidos está completa? |
| `guide-guardrail-pre-commit.ps1` | Mesma regra, agnóstica de harness | Roda igual fora do Claude Code? |
| `sensor-fitness-arquitetural.ps1` | Sensor conectado (`PostToolUse`) | Só roda para arquivos `.cs`? Reporta a falha de forma legível? |
