# 🪝 Hooks Como Mecanismo

## 🔌 O Problema Que Hooks Resolvem

Guides direcionam o agente antes de agir. Sensores observam depois. Mas alguém precisa **ligar os dois ao ciclo de vida real do agente** — decidir em que momento exato um guide é consultado, ou em que momento exato um sensor roda e o resultado volta para o agente. No Claude Code, esse "alguém" é o mecanismo de **hooks**.

Um hook é uma rotina (um comando, um script) que o Claude Code executa automaticamente em pontos específicos do seu ciclo de execução — o *agent loop*:

```
Entrada do usuário
      ↓
Carregar guides (CLAUDE.md, contexto)
      ↓
Raciocínio do modelo → decide chamar uma ferramenta
      ↓
   [hook PreToolUse]  ←── pode bloquear a chamada
      ↓
Executar a ferramenta (Edit, Write, Bash...)
      ↓
   [hook PostToolUse]  ←── observa o resultado, pode reportar problema
      ↓
Modelo observa o resultado → decide próxima ação ou finaliza
      ↓
   [hook Stop]  ←── roda quando o agente termina a resposta
```

## 🗂️ Os 4 Tipos de Hook

| Hook | Quando dispara | Papel típico |
|---|---|---|
| **PreToolUse** | Antes de uma ferramenta ser executada | Costuma implementar um **Guide dinâmico** — bloquear ou permitir com base numa condição |
| **PostToolUse** | Depois que uma ferramenta terminou | Costuma implementar um **Sensor** — rodar build/lint/teste e reportar o resultado |
| **Notification** | Quando o Claude Code precisa de atenção (ex.: aguardando permissão) | Avisar por outro canal (som, notificação do SO) |
| **Stop** | Quando o agente termina de responder | Limpeza, auditoria, registro do que foi feito |

## 📄 Contrato de Configuração

Hooks são declarados em `.claude/settings.json`, associando um **matcher** (padrão de nome de ferramenta, ex. `Edit|Write`) a um comando:

```json
{
  "hooks": {
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

## 📥 Contrato de Entrada e Saída

O hook recebe um **JSON via stdin** com dados do evento — nome da ferramenta, o que foi passado a ela, caminho do arquivo afetado, entre outros. A resposta do hook é dada pelo **exit code**:

| Exit code | Efeito |
|---|---|
| `0` | Sucesso — o agente segue normalmente |
| `2` | Bloqueia — o conteúdo de `stderr` é devolvido ao agente como motivo, para que ele se corrija |
| Outro | Erro não-bloqueante — mostrado só para quem está acompanhando, o agente não vê |

É esse contrato simples — stdin com contexto, exit code como veredito, stderr como explicação — que faz de um hook a "cola" entre o mundo determinístico de scripts/testes e o mundo de raciocínio do agente.

## 🧭 Hooks Implementam Guides e Sensores

A tabela da seção anterior não é coincidência: `PreToolUse` naturalmente serve para implementar um **Guide dinâmico** (a decisão é tomada *antes* da ação, como qualquer feedforward control) e `PostToolUse` naturalmente serve para implementar um **Sensor** (a verificação acontece *depois*, como qualquer feedback control). Você vai construir os dois nos exercícios desta subdivisão.
