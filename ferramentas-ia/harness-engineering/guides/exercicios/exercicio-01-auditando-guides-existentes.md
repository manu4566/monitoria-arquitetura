# 🟢 Exercício 1: Auditando Guides Existentes

**Dificuldade:** Iniciante | **Tempo estimado:** 30-45 min

---

## 🎯 Objetivo

Identificar, no projeto de apoio, o que já funciona como *guide* (mesmo sem ser chamado assim) e onde há lacunas — antes de escrever qualquer coisa nova.

## 📋 Cenário

Você vai atuar como uma pessoa arquiteta chegando pela primeira vez no projeto **MonitoriaBase**. Antes de pedir para o Claude Code implementar qualquer coisa nele, você precisa entender: o que já direciona o comportamento de quem (ou do que) trabalha nesse código?

## ✅ Pré-requisitos

- [ ] Projeto `ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/` acessível localmente
- [ ] Ter lido `documentacao/01-o-que-e-um-guide.md` desta subdivisão

## 🏋️ Parte 1: Mapeando Guides Existentes

Abra o projeto e procure por qualquer coisa que se encaixe na definição de guide (documentação persistente que direciona antes de agir):

### 1.1 Documentação de projeto
- Existe algum `README.md`? O que ele documenta — comandos, arquitetura, convenções?
- Existe algum `CLAUDE.md` ou `AGENTS.md`? (Spoiler: neste projeto, ainda não.)

### 1.2 Configuração que direciona
- O `.csproj` define `Nullable`, `ImplicitUsings`? Isso já é um guide implícito (o compilador vira um "sensor", mas a configuração que liga essa checagem é um guide).
- Existe algum `.editorconfig` ou análise estática configurada?

## 🔍 Parte 2: Identificando Lacunas

Liste, em texto livre, 3 a 5 coisas que você (como pessoa arquiteta) gostaria que estivessem documentadas num guide para esse projeto, mas não estão. Pense em:
- Convenções de nomenclatura que você percebeu no código, mas que não estão escritas em lugar nenhum
- Regras de arquitetura implícitas (ex.: a direção de dependência entre `Models/` e `Services/`)
- Comandos que você teve que descobrir por tentativa e erro

## 💭 Questões para Refletir

- Se um agente de IA tivesse que trabalhar nesse projeto agora, sem nenhum guide, quais erros de convenção ele provavelmente cometeria?
- Alguma das lacunas que você encontrou já é coberta por um sensor (ex.: o compilador com `Nullable` ativado)? Isso significa que não precisa virar guide também?

## ✅ Critérios de Sucesso

- [ ] Você mapeou tudo que já funciona como guide no projeto (mesmo que informalmente)
- [ ] Você tem uma lista concreta de lacunas para preencher no Exercício 2

## 🔍 Exploração Adicional

Repita essa auditoria num projeto seu, fora deste repositório. É comum encontrar mais lacunas do que guides.

## ➡️ Próximo Exercício

[Exercício 2: Escrevendo um CLAUDE.md](./exercicio-02-escrevendo-um-claude-md.md)
