# 🛠️ Escrevendo Guides Eficazes

## 📚 Hierarquia de Carregamento

O `CLAUDE.md` (e equivalentes em outras ferramentas) não é um arquivo único — ele pode existir em vários níveis, e o agente combina todos eles ao carregar contexto:

```
~/CLAUDE.md                    ← convenções pessoais, valem para qualquer projeto
raiz-do-projeto/CLAUDE.md      ← convenções do projeto como um todo
raiz/subpasta/CLAUDE.md        ← convenções específicas daquela subpasta (sobrescreve o de cima)
```

Isso permite guides em camadas: regras gerais na raiz, regras específicas onde fazem diferença — por exemplo, um `CLAUDE.md` dentro de `tests/` explicando convenções de teste que não fazem sentido em `src/`.

## ⚖️ O Princípio: Mínimo Possível, Máximo Necessário

Um guide não é um lugar para despejar toda a documentação do projeto. O erro mais comum é o oposto do que se imagina: **excesso de contexto irrelevante degrada a qualidade das respostas do agente tanto quanto a falta de contexto** — fenômeno às vezes chamado de "context rot". Um guide de 2000 linhas com informação desatualizada ou irrelevante é pior que um de 30 linhas focado no que importa agora.

Regra prática: **forneça o mínimo de contexto possível, mas o máximo necessário.** Cada seção do guide deveria responder a uma pergunta real que já causou um erro ou uma dúvida repetida — não a tudo que teoricamente poderia ser útil.

## ✅ O Que Vale a Pena Documentar num Guide

| Categoria | Pergunta que responde | Exemplo |
|---|---|---|
| 🏗️ Comandos essenciais | Como eu construo/testo/rodo isso? | `dotnet build`, `dotnet test` |
| 📐 Convenções de código | Que padrão a equipe já decidiu seguir? | Nomenclatura, organização de pastas |
| 🚧 Restrições arquiteturais | O que este projeto proíbe? | "Models não deve depender de Services" |
| ⚠️ Armadilhas conhecidas | O que já causou erro antes? | "Não editar `Migrations/` manualmente" |

## ❌ O Que Não Vira Guide

- Histórico de decisões já resolvidas e sem relevância para o código atual
- Documentação que já existe em outro lugar (duplicar = desatualizar)
- Detalhes que um sensor (linter, teste) já garante automaticamente — se o linter já bloqueia, não precisa também estar em prosa

## 🔄 Guide é Documento Vivo

Um guide desatualizado é pior que nenhum guide — ele ativamente engana o agente. Trate `CLAUDE.md` como parte do código: revisão em PR, atualização quando uma convenção muda, remoção do que não vale mais.
