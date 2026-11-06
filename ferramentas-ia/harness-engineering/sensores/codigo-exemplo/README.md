# 💻 Código de Exemplo — Sensores

## 📋 O que tem aqui

Uma fitness function de referência (`ArchitectureFitnessTests.cs`) que verifica a regra "Models não pode depender de Services" no projeto **MonitoriaBase**, usando apenas reflection e xUnit — sem dependências externas.

## 📂 Estrutura

```
codigo-exemplo/
└── exemplo-sensores/
    └── ArchitectureFitnessTests.cs   ← fitness function pronta
```

## 🎯 Como usar este artefato

### Como referência de qualidade

Compare com o que você escreveu no Exercício 2:
- Sua mensagem de falha aponta exatamente qual membro violou a regra?
- Seu sensor está restrito à direção certa da dependência (`Models` não pode depender de `Services`, mas o contrário é esperado e não deve ser flagado)?

### Como ponto de partida

Comandos a partir da raiz do repositório:

```bash
cp ferramentas-ia/harness-engineering/sensores/codigo-exemplo/exemplo-sensores/ArchitectureFitnessTests.cs \
   ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/tests/MonitoriaBase.Tests/
cd ferramentas-ia/spec-kit/codigo-exemplo/projeto-base
dotnet test
```

## 📖 Como Interpretar o Artefato

| Elemento | Papel |
|---|---|
| `ModelsNamespace` / `ServicesNamespace` | Define a regra de camadas sendo verificada |
| `GetMembers(...)` + reflection | O mecanismo **Computational** — determinístico, roda em milissegundos |
| `Assert.True(violations.Count == 0, ...)` | Transforma a violação numa falha de teste legível, inclusive por um agente de IA lendo o output |
