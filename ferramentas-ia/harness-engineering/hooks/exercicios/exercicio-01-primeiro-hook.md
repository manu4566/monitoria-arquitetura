# 🟢 Exercício 1: Primeiro Hook

**Dificuldade:** Iniciante | **Tempo estimado:** 30-45 min

---

## 🎯 Objetivo

Configurar um hook simples no Claude Code e entender, na prática, o contrato de entrada (JSON via stdin) e saída (exit code) antes de construir algo mais elaborado.

## 📋 Cenário

Antes de implementar um guardrail ou um sensor de verdade, você vai criar o hook mais simples possível: um que só registra, num arquivo de log, cada vez que uma ferramenta de edição é usada. Isso te dá visibilidade sobre a mecânica sem risco de bloquear nada por engano.

## ✅ Pré-requisitos

- [ ] Claude Code instalado
- [ ] Ter lido `documentacao/01-hooks-como-mecanismo.md`

## 🏋️ Parte 1: Configurando o Hook

No projeto MonitoriaBase (ou numa cópia local), crie `.claude/settings.json`:

```json
{
  "hooks": {
    "PostToolUse": [
      {
        "matcher": "Edit|Write",
        "hooks": [
          { "type": "command", "command": "powershell -NoProfile -File ./scripts/log-edicoes.ps1" }
        ]
      }
    ]
  }
}
```

Crie `scripts/log-edicoes.ps1`:

```powershell
$inputJson = [Console]::In.ReadToEnd() | ConvertFrom-Json
$filePath = $inputJson.tool_input.file_path
$timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"

Add-Content -Path "hook-log.txt" -Value "[$timestamp] $($inputJson.tool_name) em $filePath"

exit 0
```

## 🔍 Parte 2: Observando o Hook em Ação

Abra uma sessão do Claude Code no projeto e peça uma edição qualquer (ex.: "adicione um comentário no topo do `Program.cs`"). Depois:

```bash
cat hook-log.txt
```

Confirme que o hook rodou e registrou a edição.

## 💭 Questões para Refletir

- O que aconteceria se o script terminasse com `exit 2` em vez de `exit 0`, mesmo sem ter nenhum problema real para reportar?
- Esse hook é um Guide ou um Sensor, segundo a distinção das subdivisões anteriores? Por quê?

## ✅ Critérios de Sucesso

- [ ] `settings.json` e `log-edicoes.ps1` criados
- [ ] O log foi populado após uma edição real do Claude Code
- [ ] Você entende a diferença entre exit code `0` e `2`

## 🔍 Exploração Adicional

Troque o matcher de `Edit|Write` para incluir também `Bash`, e observe que tipo de informação chega no JSON de entrada para uma chamada de `Bash` (é diferente do que chega para `Edit`).

## ➡️ Próximo Exercício

[Exercício 2: Hook como Guide](./exercicio-02-hook-como-guide.md)
