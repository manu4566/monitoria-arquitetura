# 🔍 O Que É um Sensor

## 📐 Definição

Na dicotomia de Birgitta Böckeler ("Harness Engineering", Martin Fowler), um **sensor** é um **feedback control**: um mecanismo que **observa depois** que o agente age, permitindo que ele (ou quem o supervisiona) se autocorrija.

> "Sensors observe *after* the agent acts and help it self-correct."

Onde um guide tenta evitar o erro antes de acontecer, o sensor assume que o erro pode acontecer mesmo assim — e garante que ele seja detectado rápido, de forma confiável, sem depender de alguém revisar linha por linha manualmente.

## 🗂️ Exemplos de Sensores

| Tipo | O que observa | Exemplo |
|---|---|---|
| ✅ Testes | Comportamento esperado do código | Testes unitários e de integração |
| 🧹 Linters | Estilo e padrões de código | ESLint, StyleCop |
| 🏗️ Análise estática | Bugs prováveis, complexidade | Verificadores de tipo, análise de fluxo |
| 👀 Agentes de revisão | Qualidade semântica do código | Um segundo agente revisando o diff |
| 🧬 Mutation testing | Qualidade dos próprios testes | Ferramentas de mutation testing |
| 📊 Cobertura | O que não está sendo testado | Monitoramento contínuo de cobertura |

Böckeler destaca que sensores são "particularmente poderosos quando produzem sinais otimizados para consumo por LLMs" — uma mensagem de erro de linter que já sugere a correção, por exemplo, fecha o loop de autocorreção sem precisar de intervenção humana.

## 🌐 Sensores São Agnósticos de Harness

Diferente de guides (que mudam de arquivo conforme a ferramenta) e de hooks (mecanismo específico de cada ferramenta, veja a próxima subdivisão), **um sensor não sabe — nem precisa saber — qual agente escreveu o código**. Um teste, um linter ou uma fitness function observam o **artefato** (o código, o repositório), não o processo que o gerou. O mesmo sensor de arquitetura vale para código escrito por Claude Code, Copilot, Cursor ou por uma pessoa sem IA nenhuma.

Isso torna sensores o investimento mais durável dos três: eles continuam funcionando mesmo se a equipe trocar de harness amanhã.

## 🤔 Sensor Não Substitui Guide

Um sensor detecta a violação — não a impede de acontecer, nem explica ao agente por que a regra existe antes da tentativa. É por isso que guides e sensores se complementam: o guide tenta reduzir a chance de erro, o sensor garante que, se o erro acontecer mesmo assim, ele não passe despercebido.
