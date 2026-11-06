# 🟢 Exercício 1: Catalogando Sensores

**Dificuldade:** Iniciante | **Tempo estimado:** 30-45 min

---

## 🎯 Objetivo

Identificar quais sensores (feedback controls) já existem no projeto de apoio, classificá-los por dimensão (Computational vs. Inferential) e por categoria de regulação (Maintainability, Architecture Fitness, Behaviour).

## 📋 Cenário

Antes de construir um sensor novo, vale entender o que já está em funcionamento. Você vai auditar o projeto **MonitoriaBase** com a mesma lente usada no Exercício 1 de `guides/`, agora procurando feedback controls em vez de feedforward controls.

## ✅ Pré-requisitos

- [ ] Projeto `ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/` acessível localmente
- [ ] Ter lido `documentacao/01-o-que-e-um-sensor.md` e `02-dimensoes-e-categorias-de-regulacao.md`

## 🏋️ Parte 1: Rodando os Sensores Existentes

```bash
cd ferramentas-ia/spec-kit/codigo-exemplo/projeto-base
dotnet build
dotnet test
```

Observe a saída: quantos testes existem em `MonitoriaBase.Tests`? O que cada um verifica?

## 🔍 Parte 2: Classificando

Preencha uma tabela como esta com o que você encontrou:

| Sensor | Dimensão (Computational/Inferential) | Categoria (Maintainability/Architecture Fitness/Behaviour) |
|---|---|---|
| `MonitoriaServiceTests` | ? | ? |
| Verificação de `Nullable` do compilador | ? | ? |
| *(o que mais você encontrar)* | | |

## 💭 Questões para Refletir

- O projeto tem algum sensor de **Architecture Fitness**? Se não tem, é porque não existe nenhuma regra de arquitetura a proteger, ou porque ela existe só implicitamente (na cabeça de quem escreveu o código)?
- Qual sensor deste projeto seria mais barato rodar a cada edição (Computational) e qual só faria sentido rodar uma vez por PR?

## ✅ Critérios de Sucesso

- [ ] Você rodou `dotnet build` e `dotnet test` e leu a saída
- [ ] Você classificou pelo menos 2 sensores existentes por dimensão e categoria
- [ ] Você identificou que falta um sensor de Architecture Fitness

## 🔍 Exploração Adicional

Se o projeto tivesse um linter/analisador configurado (não tem, hoje), como você o classificaria nas duas dimensões?

## ➡️ Próximo Exercício

[Exercício 2: Sensor de Arquitetura](./exercicio-02-sensor-de-arquitetura.md)
