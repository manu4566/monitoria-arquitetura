# 🧰 Harness Engineering

Este tópico cobre o que existe **além do modelo** num agente de IA — o conjunto de peças que transforma um LLM bruto em uma ferramenta confiável para desenvolvimento de software.

## 💡 O que é Harness Engineering

> "Agent = Model + Harness"

A definição é de Birgitta Böckeler (Martin Fowler), fonte principal deste tópico: um **harness** é tudo em um agente de IA exceto o modelo em si. Um LLM sozinho não lê o código do seu projeto, não conhece suas convenções, não executa comandos e não lembra de nada entre sessões — é "um motor poderoso, mas um motor sozinho não é um carro".

Existem dois níveis de harness:
- **Harness interno**: construído por quem faz a ferramenta (o Claude Code, o Copilot, o Cursor já vêm com um harness embutido — system prompt, mecanismo de leitura de código, etc.).
- **Harness externo**: construído por quem *usa* a ferramenta no próprio projeto — é aqui que este tópico atua.

Böckeler organiza os controles de um harness externo em duas categorias que dão nome às primeiras duas subdivisões deste tópico:

| Categoria | Quando age | Papel | Exemplos |
|---|---|---|---|
| 🧭 **Guides** (feedforward) | *Antes* do agente agir | Direcionar, antecipar | `CLAUDE.md`, `AGENTS.md`, scripts de bootstrap, Language Servers |
| 🔍 **Sensors** (feedback) | *Depois* que o agente age | Observar, permitir autocorreção | Testes, linters, análise estática, revisão de código, fitness functions |

A terceira subdivisão, **Hooks**, é o mecanismo técnico que conecta Guides e Sensors ao ciclo de vida do agente numa ferramenta concreta (Claude Code).

## 📁 Subdivisões Disponíveis

| Subdivisão | Descrição | Status |
|---|---|---|
| 🧭 [guides](./guides/) | Documentação viva que direciona o agente antes de agir | ✅ Disponível |
| 🔍 [sensores](./sensores/) | Testes, linters e fitness functions que dão feedback depois | ✅ Disponível |
| 🪝 [hooks](./hooks/) | O mecanismo que liga guides e sensores ao ciclo de vida do agente | ✅ Disponível |

## 🗺️ Ordem Sugerida

```
Guides                    Sensores                   Hooks
──────────────            ──────────────────         ──────────────────────
Documentação que      →   Testes e fitness       →   Conecta os dois ao
direciona o agente         functions que               ciclo de vida do
antes de agir               observam depois              agente (Claude Code)
```

Estude nessa ordem: o exercício final de `hooks/` reaproveita o `CLAUDE.md` que você escreve em `guides/` e o sensor de arquitetura que você implementa em `sensores/`, ligando os dois ao agente via hook.

## 🧑 O Papel do Humano

Automatizar guides e sensores não elimina o julgamento humano — ele o **redireciona**. Böckeler chama isso de *steering loop*: quanto mais os controles rápidos e determinísticos (lint, build, testes) ficam automáticos, mais o tempo de quem revisa fica livre para o que máquina nenhuma faz bem — avaliar se a solução é a certa, entender o contexto organizacional, decidir trade-offs. O objetivo de um bom harness não é remover pessoas do processo; é fazer com que a atenção delas vá para onde ela realmente importa.

## 🚀 Próximas Subdivisões

- 🧠 **Memory** — persistência de contexto entre sessões
- 🎯 **Skills** — workflows reutilizáveis e conhecimento de domínio empacotado
