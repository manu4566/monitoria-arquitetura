# 🟡 Exercício 2: Hook como Guide

**Dificuldade:** Intermediário | **Tempo estimado:** 60-90 min

---

## 🎯 Objetivo

Implementar um hook `PreToolUse` que funciona como **guide dinâmico**: bloqueia a edição de um arquivo protegido antes que ela aconteça, e comparar essa abordagem com a mesma regra implementada como git hook.

## 📋 Cenário

Você já documentou, no `CLAUDE.md` do Exercício 2 de `guides/`, que certas coisas não devem ser mexidas sem revisão manual. Um guide em prosa depende do agente "ler e respeitar". Agora você vai transformar isso num bloqueio que não depende de leitura nenhuma — o agente simplesmente não consegue editar o arquivo, ponto.

## ✅ Pré-requisitos

- [ ] Ter concluído o Exercício 1
- [ ] Ter lido `documentacao/02-permissions-o-portao-estatico.md`

## 🏋️ Parte 1: O Hook de Guardrail

Crie `scripts/guide-guardrail.ps1`:

```powershell
$inputJson = [Console]::In.ReadToEnd() | ConvertFrom-Json
$filePath = $inputJson.tool_input.file_path

$protegidos = @('CLAUDE.md', 'appsettings.Production.json')

foreach ($padrao in $protegidos) {
    if ($filePath -like "*$padrao*") {
        [Console]::Error.WriteLine("Bloqueado: '$filePath' e um arquivo protegido ($padrao). Edicoes aqui exigem revisao manual.")
        exit 2
    }
}

exit 0
```

Registre em `.claude/settings.json`:

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
    ]
  }
}
```

## 🔍 Parte 2: Testando o Bloqueio

Peça ao Claude Code para editar o `CLAUDE.md` do projeto. Observe: a chamada é bloqueada, e o agente recebe o motivo (o conteúdo de `stderr`) — normalmente ele reconhece o bloqueio e explica a você por que não conseguiu prosseguir, em vez de insistir.

Depois, peça uma edição num arquivo **não** protegido e confirme que ela acontece normalmente.

## 🔧 Parte 3: A Mesma Regra Como Git Hook

Implemente a versão portável da mesma regra, independente do Claude Code, em `.git/hooks/pre-commit` (ou num script equivalente versionado, já que `.git/hooks/` não é versionado por padrão):

```powershell
$staged = git diff --cached --name-only
$protegidos = @('CLAUDE.md', 'appsettings.Production.json')
$bloqueado = $false

foreach ($arquivo in $staged) {
    foreach ($padrao in $protegidos) {
        if ($arquivo -like "*$padrao*") {
            Write-Host "Commit bloqueado: '$arquivo' e um arquivo protegido ($padrao)."
            $bloqueado = $true
        }
    }
}

if ($bloqueado) { exit 1 }
exit 0
```

Tente commitar uma mudança em `appsettings.Production.json` (crie o arquivo se ele não existir) e observe o bloqueio — agora numa camada que funciona independente de qual ferramenta de IA (ou nenhuma) gerou a mudança. **Nota:** teste com esse arquivo, não com `CLAUDE.md` — `CLAUDE.md` está no `.gitignore` deste repositório (é comum tratá-lo como configuração local, não versionada), então ele nunca chega a `git diff --cached` para começo de conversa.

## 💭 Questões para Refletir

- O hook do Claude Code e o git hook bloqueiam o **mesmo** problema, mas em momentos diferentes do fluxo. Qual chega mais cedo? Qual é mais fácil de contornar por acidente?
- Que vantagem o hook do Claude Code tem que o git hook não tem (dica: o agente recebe uma explicação e pode reagir a ela)?

## ✅ Critérios de Sucesso

- [ ] `guide-guardrail.ps1` bloqueando edição via Claude Code, com mensagem clara
- [ ] Edições em arquivos não protegidos continuam funcionando
- [ ] Versão git hook implementada e testada com um commit real

## 🔍 Exploração Adicional

Implementada acima, como parte da Parte 3 — a variante em git hook já é a exploração adicional deste exercício.

## ➡️ Próximo Exercício

[Exercício 3: Hook como Sensor](./exercicio-03-hook-como-sensor.md)
