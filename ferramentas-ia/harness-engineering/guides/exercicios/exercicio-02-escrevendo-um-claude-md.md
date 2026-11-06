# 🟡 Exercício 2: Escrevendo um CLAUDE.md

**Dificuldade:** Intermediário | **Tempo estimado:** 60-90 min

---

## 🎯 Objetivo

Escrever um `CLAUDE.md` completo para o projeto **MonitoriaBase**, cobrindo as lacunas identificadas no Exercício 1, aplicando o princípio "mínimo possível, máximo necessário".

## 📋 Cenário

Com base na auditoria do Exercício 1, agora você vai produzir o guide que faltava. O resultado precisa ser útil o suficiente para que, numa sessão nova do Claude Code nesse projeto, o agente já chegue sabendo o que uma pessoa arquiteta levaria minutos para explicar verbalmente.

## ✅ Pré-requisitos

- [ ] Ter concluído o Exercício 1
- [ ] Ter lido `documentacao/02-escrevendo-guides-eficazes.md`

## 🏋️ Parte 1: Estrutura do Guide

Crie um arquivo `CLAUDE.md` na raiz de `ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/` (ou numa cópia local do projeto, se preferir não alterar o repositório). Inclua pelo menos:

1. **Comandos essenciais** — build, test, run
2. **Convenções de código** — o que você observou no Exercício 1 (namespaces, nomenclatura)
3. **Restrição arquitetural** — a regra de que `Models/` não deve depender de `Services/` (você vai transformar essa mesma regra num sensor automatizado na próxima subdivisão)
4. **Armadilhas conhecidas** — qualquer coisa que já causou confusão na sua auditoria

## 🔍 Parte 2: Testando o Guide na Prática

Abra uma sessão do Claude Code no projeto com o `CLAUDE.md` em vigor e peça algo simples, por exemplo: *"adicione uma propriedade `Bio` opcional ao `Student`"*. Observe:
- O agente respeitou as convenções descritas?
- Alguma instrução do guide foi ignorada? Por quê — ela era ambígua, contraditória com o código real, ou o agente simplesmente não a "viu" (guide longo demais, informação enterrada)?

## 💭 Questões para Refletir

- Seu `CLAUDE.md` ficou mais perto de "documentação completa do projeto" ou de "o mínimo necessário para evitar os erros mais prováveis"? Qual dos dois você escreveu?
- Se você tivesse que cortar 50% do que escreveu, o que sobreviveria?

## ✅ Critérios de Sucesso

- [ ] `CLAUDE.md` criado, cobrindo as 4 seções da Parte 1
- [ ] Testado com pelo menos um pedido real ao Claude Code
- [ ] Nenhuma seção do guide duplica algo que um linter/compilador já garante

## 🔍 Exploração Adicional

Compare seu resultado com `codigo-exemplo/exemplo-guides/CLAUDE.md` desta subdivisão. Onde os dois divergem — e qual versão é mais enxuta?

## ➡️ Próximo Exercício

Siga para a subdivisão [`sensores/`](../../sensores/exercicios/) — lá você vai transformar a restrição arquitetural que documentou aqui num sensor que a verifica automaticamente.

## 🎓 Conclusão do Módulo

Você viu que um guide é só metade do controle: ele direciona o agente, mas não garante nada sozinho — nada impede que a regra documentada seja violada por acidente ou por um agente que não a "notou". É exatamente essa lacuna que a próxima subdivisão, **Sensores**, resolve: transformar a regra em algo que é verificado automaticamente, depois do fato.
