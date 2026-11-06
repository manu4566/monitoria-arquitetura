# 🌐 Hooks em Outros Harnesses

## ⚠️ O Ponto Mais Amarrado a uma Ferramenta

De tudo que este tópico cobre, o mecanismo de hooks do Claude Code (`settings.json`, `PreToolUse`/`PostToolUse`) é o que menos se transfere literalmente para outra ferramenta — cada harness implementa a interceptação do seu próprio jeito, quando implementa. Mas o **padrão** por trás — interceptar o ciclo ação→resultado para injetar um guide ou rodar um sensor — não é exclusivo de nenhum harness de IA. Ele já existe, há muito mais tempo, em duas formas genuinamente universais.

## 🔗 Git Hooks

Scripts que o próprio Git executa em pontos do seu ciclo — `pre-commit` (antes de um commit ser criado), `pre-push` (antes de enviar para o remoto), entre outros. Funcionam **independentemente de qual agente (ou pessoa) gerou o código** — se o código está no working directory prestes a ser commitado, o git hook roda, não importa a origem.

```bash
# .git/hooks/pre-commit
#!/bin/sh
dotnet test || exit 1
```

## 🚦 Gates de CI/CD

Um pipeline (este repositório já tem um módulo inteiro sobre isso — veja `ci-cd/`) pode configurar um *gate*: o merge de um Pull Request só é permitido se os sensores configurados no pipeline passarem. É a versão "pós-integração" do mesmo padrão — mais lenta que um hook local, mas também mais difícil de contornar (ninguém tem hooks locais desativados sem querer).

## 🐙 Exemplo Concreto: GitHub Copilot

O Copilot **não tem** um mecanismo de hooks por chamada de ferramenta igual ao `PreToolUse`/`PostToolUse` do Claude Code — isso é importante deixar claro, para não fingir uma equivalência que não existe. O Copilot coding agent (o modo que trabalha uma issue sozinho e abre um PR) opera numa granularidade mais grossa: por *sessão/tarefa*, não por edição individual. Ainda assim, o mesmo padrão feedforward/feedback aparece, só que implementado com as peças que o GitHub já oferece:

**Guide/bootstrap (feedforward)** — um workflow especial, `copilot-setup-steps.yml`, roda **antes** do agente começar a trabalhar, preparando o ambiente:

```yaml
# .github/workflows/copilot-setup-steps.yml
name: "Copilot Setup Steps"

on: workflow_dispatch

jobs:
  copilot-setup-steps:
    runs-on: ubuntu-latest
    permissions:
      contents: read
    steps:
      - uses: actions/checkout@v4
      - name: Instalar .NET SDK
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '8.0.x'
      - name: Restaurar dependências
        run: dotnet restore ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/MonitoriaBase.sln
```

**Sensor (feedback)** — o workflow de CI de sempre, disparado quando o Copilot abre/atualiza o PR. Falhas aparecem como checks no PR, e o Copilot coding agent consegue ver e tentar corrigir antes de considerar a tarefa concluída — o mesmo papel que o `PostToolUse` cumpre no Claude Code, só que rodando uma vez por push em vez de uma vez por edição:

```yaml
# .github/workflows/ci.yml
name: CI

on:
  pull_request:

jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '8.0.x'
      - name: Rodar sensor de arquitetura e testes
        run: dotnet test ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/MonitoriaBase.sln
```

Ou seja: para o Copilot, o "hook" não é um script plugado no meio do raciocínio do agente — é a combinação de um workflow de setup (guide) com os checks de CI que já existiriam de qualquer forma (sensor). É exatamente o "gate de CI/CD" da seção anterior, só que com o detalhe de que o próprio Copilot consegue ler o resultado do check e reagir a ele dentro da mesma tarefa.

## 📊 Comparando as Camadas

| Camada | Quando dispara | Granularidade | Depende de qual harness gerou o código? | Fácil de contornar? |
|---|---|---|---|---|
| 🪝 Hook do Claude Code | A cada chamada de ferramenta, durante a sessão do agente | Por edição | Sim — só existe dentro do Claude Code | Sim — só vale enquanto a sessão usa aquele `settings.json` |
| 🔧 Git hook (`pre-commit`/`pre-push`) | Ao commitar/enviar, local, antes do código sair da máquina | Por commit/push | Não — roda para qualquer autor | Sim, localmente (`--no-verify`) — mas fica registrado |
| 🐙 Copilot coding agent (setup + CI) | Setup antes da tarefa; CI a cada push do agente no PR | Por tarefa/push | Parcialmente — o setup é específico do Copilot, o CI não | Não, na prática — os checks ficam visíveis no PR |
| 🚦 Gate de CI/CD (genérico) | Após o push, no pipeline compartilhado | Por push/PR | Não | Não, na prática — é o portão que todo mundo precisa passar |

## 🎯 Quando Usar Cada Uma

Não são substitutas — são camadas complementares, cada vez mais difíceis de contornar e cada vez mais caras/lentas: hook do Claude Code (ou o equivalente de setup+CI do Copilot) para feedback o mais próximo possível do momento em que o agente trabalha; git hook para garantir que nada saia da máquina sem passar por um mínimo; gate de CI/CD como a garantia final, compartilhada, que não depende de nenhuma configuração local. Um bom harness externo normalmente usa várias dessas camadas juntas, cada uma protegendo o nível que a anterior pode falhar em cobrir — e a lição de `guides/` e `sensores/` continua valendo aqui: o guide (`copilot-setup-steps.yml`) tenta evitar o problema antes, o sensor (CI) garante que ele não passe despercebido depois, não importa qual harness gerou a mudança.
