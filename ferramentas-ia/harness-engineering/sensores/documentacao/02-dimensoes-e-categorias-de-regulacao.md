# 📊 Dimensões e Categorias de Regulação

## ⚙️ Duas Dimensões de Execução

Nem todo sensor funciona da mesma forma. Böckeler distingue duas dimensões:

| Dimensão | Como funciona | Velocidade/Custo | Determinismo | Exemplos |
|---|---|---|---|---|
| 🧮 **Computational** | Regras determinísticas, executadas por código | Rápido (ms a segundos) | Alto — mesma entrada, mesmo resultado | Testes, linters, verificadores de tipo |
| 🧠 **Inferential** | Julgamento semântico, executado por um LLM | Mais lento e caro | Baixo — pode variar entre execuções | "LLM as judge", revisão de código por IA |

**Trade-off prático**: sensores computacionais são baratos o suficiente para rodar a cada mudança — por isso são o primeiro nível de defesa. Sensores inferenciais custam mais (tempo e tokens) e são não-determinísticos, então valem mais quando o critério que se quer avaliar é semântico demais para virar regra fixa (ex.: "essa explicação está clara?", "esse nome de variável comunica a intenção certa?").

## ⏱️ Keep Quality Left

Böckeler organiza quando cada sensor deve rodar, não só o que ele verifica:

```
Antes/Durante desenvolvimento    Pós-integração (pipeline)      Contínuo
──────────────────────────       ──────────────────────────    ──────────────────────
Linters, testes rápidos,     →   Mutation testing,          →  Detecção de drift,
revisão básica                    revisão detalhada              SLOs degradados,
(sensores baratos, rodam           (sensores caros, rodam         anomalias em runtime
a cada edição)                     uma vez por PR)
```

A ideia de "keep quality left" é: sensores baratos e rápidos devem rodar o mais cedo possível no ciclo (idealmente antes até do commit), para que o feedback chegue enquanto o custo de corrigir ainda é baixo. Sensores caros ficam reservados para o momento em que já vale a pena pagar o custo — normalmente após a integração.

## 🏛️ Três Categorias de Regulação

Além de *quando*, Böckeler também categoriza *o que* um harness regula:

| Categoria | O que valida | Maturidade |
|---|---|---|
| 🧹 **Maintainability Harness** | Qualidade interna, manutenibilidade — duplicação, complexidade ciclomática, cobertura de testes | A mais madura — sensores computacionais detectam isso de forma confiável |
| 🏛️ **Architecture Fitness Harness** | Características arquiteturais — direção de dependências, convenções de camadas, requisitos de performance | Requer sensores explícitos (fitness functions) — é o foco do exercício prático desta subdivisão |
| 🎭 **Behaviour Harness** | Comportamento funcional correto | A menos madura — "o elefante na sala"; depende hoje de testes gerados por IA + revisão manual, e Böckeler reconhece que isso ainda "não é suficiente" sozinho |

**Architecture Fitness Harness** é o ponto de maior relevância para quem está aprendendo arquitetura de software: é literalmente a prática de transformar uma regra arquitetural (do tipo que normalmente vive só na cabeça de quem projetou o sistema, ou num diagrama que ninguém mais olha) numa verificação automática e repetível — uma **fitness function**. É isso que você vai construir no Exercício 2.
