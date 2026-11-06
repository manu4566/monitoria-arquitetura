# 🧭 O Que É um Guide

## 📐 Definição

Na dicotomia de Birgitta Böckeler ("Harness Engineering", Martin Fowler), um **guide** é um **feedforward control**: um mecanismo que **antecipa** o comportamento do agente e tenta **direcioná-lo antes que ele aja**.

> "Guides anticipate the agent's behaviour and aim to steer it *before* it acts."

Isso os coloca em oposição direta aos **sensors** (feedback controls), que observam *depois* que o agente já agiu — assunto da próxima subdivisão deste tópico. Guides são a primeira linha de defesa: quanto melhor o agente entende o que se espera dele, menos correções um sensor vai precisar detectar depois.

## 🗂️ Exemplos de Guides

| Tipo | O que faz | Exemplo |
|---|---|---|
| 📄 Documentação de projeto | Explica convenções, arquitetura, comandos de build | `CLAUDE.md`, `AGENTS.md` |
| 🏗️ Scripts de bootstrap | Preparam o ambiente para o agente trabalhar | Scripts de setup de um novo projeto |
| 🔧 Code mods | Aplicam uma transformação estrutural antes do agente continuar | Migração automática de uma API antiga |
| 🔍 Language Servers (LSP) | Dão ao agente informação de tipos, referências e erros em tempo real | Autocompletar, "go to definition", diagnósticos |

O ponto em comum: todos esses mecanismos **moldam o que o agente vê e sabe antes de ele decidir o que fazer**. Um `CLAUDE.md` bem escrito, por exemplo, evita que o agente proponha uma solução que viola uma convenção do projeto — porque a convenção já estava lá, disponível, antes da primeira linha de código ser sugerida.

## 🌐 O Mesmo Conceito, Nomes Diferentes

O conceito de guide não é exclusivo de uma ferramenta — cada harness de IA usa seu próprio arquivo/convenção para o mesmo papel:

| Harness | Arquivo de guide |
|---|---|
| 🤖 Claude Code | `CLAUDE.md` |
| 🖱️ GitHub Copilot | `.github/copilot-instructions.md` |
| ✏️ Cursor | `.cursor/rules/*.mdc` |
| 🌐 Convenção emergente entre ferramentas | `AGENTS.md` |

Isso importa porque o **conhecimento de como escrever um bom guide transfere entre ferramentas** — muda o nome do arquivo, não o raciocínio de "o que o agente precisa saber antes de agir".

## 🤔 Guide não é Prompt

Um guide não é uma instrução pontual dada numa conversa — é **persistente**: fica no repositório, é versionado, e vale para qualquer sessão do agente naquele projeto (ou naquela pasta, dependendo da hierarquia — ver próximo documento). É o equivalente a treinar toda a equipe uma vez, em vez de repetir a mesma explicação toda sessão.
