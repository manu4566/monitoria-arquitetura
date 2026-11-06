# 🔴 Exercício 3: Hook como Sensor

**Dificuldade:** Avançado | **Tempo estimado:** 90-120 min

---

## 🎯 Objetivo

Implementar um hook `PostToolUse` que roda o sensor de arquitetura construído em `sensores/exercicio-02` automaticamente após cada edição, fechando o ciclo completo: o `CLAUDE.md` de `guides/` documenta a regra, o sensor de `sensores/` a verifica, e o hook desta subdivisão conecta os dois ao Claude Code em tempo real.

## 📋 Cenário

Até agora, rodar a fitness function exigia lembrar de digitar `dotnet test`. Um sensor que depende de alguém lembrar de rodá-lo manualmente falha exatamente no momento em que mais precisa funcionar — quando o agente está trabalhando rápido e sem supervisão linha a linha. Este exercício remove essa dependência.

## ✅ Pré-requisitos

- [ ] Ter concluído `sensores/exercicio-02` (a fitness function `ArchitectureFitnessTests.cs` precisa existir no projeto)
- [ ] Ter concluído os Exercícios 1 e 2 desta subdivisão

## 🏋️ Parte 1: O Hook de Sensor

Crie `scripts/sensor-fitness-arquitetural.ps1` na raiz do projeto MonitoriaBase (mesmo nível de `.claude/`) — o script assume que roda com `cwd` = raiz do projeto, que é o padrão quando o Claude Code executa hooks:

```powershell
$inputJson = [Console]::In.ReadToEnd() | ConvertFrom-Json
$filePath = $inputJson.tool_input.file_path

if ($filePath -notlike "*.cs") {
    exit 0
}

$saida = dotnet test --filter "FullyQualifiedName~ArchitectureFitnessTests" 2>&1
$codigoSaida = $LASTEXITCODE

if ($codigoSaida -ne 0) {
    [Console]::Error.WriteLine("Sensor de arquitetura falhou apos a edicao:`n$saida")
    exit 2
}

exit 0
```

Adicione ao `.claude/settings.json` (mantendo o hook `PreToolUse` do Exercício 2):

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "Edit|Write",
        "hooks": [
          { "type": "command", "command": "powershell -NoProfile -File ./scripts/guide-guardrail.ps1" }
        ]
      }
    ],
    "PostToolUse": [
      {
        "matcher": "Edit|Write",
        "hooks": [
          { "type": "command", "command": "powershell -NoProfile -File ./scripts/sensor-fitness-arquitetural.ps1" }
        ]
      }
    ]
  }
}
```

## 💥 Parte 2: Provocando a Autocorreção

Peça ao Claude Code, no projeto MonitoriaBase: *"adicione uma propriedade `Service` do tipo `IMonitoriaService` na classe `Student`, para guardar uma referência de conveniência"*.

Observe o ciclo completo:
1. O agente edita `Student.cs`.
2. O hook `PostToolUse` roda o sensor — a fitness function falha.
3. O `stderr` (a mensagem de violação) volta para o agente.
4. Sem você precisar apontar o erro, o agente normalmente reconhece a falha e desfaz ou corrige a mudança sozinho.

## 🔍 Parte 3: O Ciclo Completo

Neste ponto, os três artefatos das três subdivisões estão conectados:

```
guides/CLAUDE.md               sensores/ArchitectureFitnessTests.cs      hooks/settings.json
─────────────────               ────────────────────────────────          ──────────────────
Documenta a regra          →    Verifica a regra automaticamente    →    Conecta a verificação
"Models não depende            (Computational, roda em ms)               ao agent loop em
de Services"                                                              tempo real
```

## 💭 Questões para Refletir

- O que teria acontecido se só o hook `PreToolUse` (guardrail) existisse, sem o `PostToolUse` (sensor)? Que tipo de violação o guardrail não pegaria?
- Esse sensor é **Computational**. Em que situação você complementaria com um sensor **Inferential** (ex.: pedir a outro agente para revisar semanticamente a mudança) rodando no mesmo hook?

## ✅ Critérios de Sucesso

- [ ] Hook `PostToolUse` rodando a fitness function após cada edição em `.cs`
- [ ] Reproduziu o cenário de autocorreção e observou o agente reagir ao feedback
- [ ] Consegue explicar o papel de cada um dos 3 artefatos (guide, sensor, hook) no ciclo

## 🔍 Exploração Adicional

Adicione um terceiro hook (`Stop`) que registra, ao final de cada resposta do agente, quantas vezes o sensor de arquitetura foi acionado durante a sessão — uma forma simples de auditoria.

## ➡️ Próximo Passo

Volte ao [índice do tópico](../../README.md) para ver o roadmap de próximas subdivisões (Memory, Skills).

## 🎓 Conclusão do Tópico

Você percorreu as três peças de um harness externo, na ordem em que elas se conectam: um **guide** (`CLAUDE.md`) que documenta uma intenção; um **sensor** (fitness function) que a verifica de forma determinística e barata; e um **hook** que liga os dois ao ciclo de vida real do agente, sem depender de ninguém lembrar de rodar nada manualmente. Nenhuma das três peças, sozinha, garante o resultado — é a combinação que faz o harness funcionar. E, como visto em `03-hooks-em-outros-harnesses.md`, o mesmo padrão se repete em camadas fora do Claude Code (git hooks, gates de CI/CD) — o conhecimento que você construiu aqui não fica preso a uma ferramenta.
