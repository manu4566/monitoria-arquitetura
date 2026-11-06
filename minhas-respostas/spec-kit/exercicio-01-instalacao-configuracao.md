# 🟢 Exercício 1: Instalação e Configuração

**Dificuldade:** Iniciante | **Tempo estimado:** 30-45 min

---

## 🎯 Objetivo

Instalar o spec-kit, inicializar o `projeto-base` com ele e criar a `constitution.md`, entendendo a estrutura de artefatos gerada.

---

## 📋 Cenário

Você é o responsável por padronizar o uso do GitHub Copilot no time de arquitetura. Para isso, vai configurar o spec-kit no `projeto-base` — um sistema simples de gestão de monitoria em .NET — e definir os princípios de governança que vão guiar todas as futuras especificações do projeto.

> 📂 **Projeto de referência:** `ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/`

---

## ✅ Pré-requisitos

- [ ]  VS Code com extensão GitHub Copilot ativa
- [ ]  Python 3.8+ instalado (`python --version`)
- [ ]  .NET 8 SDK instalado (`dotnet --version`)
- [ ]  Terminal com acesso à internet

---

## 📦 Parte 1: Instalando o uv e o specify-cli

### 1.1 Instalar o uv

```powershell
# Windows (PowerShell)
powershell -ExecutionPolicy ByPass -c "irm https://astral.sh/uv/install.ps1 | iex"

# Verificar instalação
uv --version
```

### 1.2 Instalar o specify-cli

```bash
uv tool install specify-cli --from git+https://github.com/github/spec-kit.git

# Confirmar instalação
specify --version
```

> 💡 Se o comando `specify` não for reconhecido após a instalação, reinicie o terminal.

### 1.3 Verificar integração com Copilot

[Ajustar: Antes de verificar tem que instalar usando o specify init --here --integration copilot ou claude]

Abra o VS Code, abra o **Copilot Chat** (ícone na barra lateral ou `Ctrl+Shift+I`) e digite:

```
/speckit
```

Você deve ver os comandos do spec-kit listados como sugestões. Se não aparecer, reinicie o VS Code.

---

## 🚀 Parte 2: Explorando e Inicializando o projeto-base

### 2.1 Navegar até o projeto-base e verificar que funciona

```bash
cd ferramentas-ia/spec-kit/codigo-exemplo/projeto-base

# Rodar os testes para confirmar que o projeto está íntegro
dotnet test
# Esperado: 6 testes passando

# Executar a aplicação para ver o que já está implementado
dotnet run --project src/MonitoriaBase
```

Leia o [README do projeto-base](../codigo-exemplo/projeto-base/README.md) para entender o que está implementado e o que os exercícios vão adicionar.

### 2.2 Inicializar o spec-kit

```bash
specify init MONITORIA-BASE
```

### 2.3 Explorar a estrutura gerada

Abra o VS Code no diretório e explore a pasta `.specify/`:

```
.specify/
├── memory/
│   └── constitution.md          ← Este arquivo você vai preencher
├── templates/                   ← Templates core (não editar)
│   ├── constitution-template.md
│   ├── spec-template.md
│   ├── plan-template.md
│   ├── tasks-template.md
│   └── checklist-template.md
├── scripts/                     ← Scripts de automação usados pelos comandos /speckit.*
│   └── powershell/               (ou bash/, dependendo do SO)
│       ├── create-new-feature.ps1
│       ├── setup-plan.ps1
│       ├── setup-tasks.ps1
│       └── check-prerequisites.ps1
├── workflows/                    ← Definição do workflow do spec-kit (ordem dos comandos)
│   ├── speckit/workflow.yml
│   └── workflow-registry.json
├── integrations/                 ← Manifestos de integração com o assistente de IA usado
│   └── claude.manifest.json / speckit.manifest.json
├── integration.json              ← Aponta qual integração está ativa (copilot, claude, etc.)
└── init-options.json              ← Opções usadas no momento do `specify init` (registro para auditoria/replay)
```

**Questão para refletir:** Para que serve cada pasta?

- **`memory/`** — guarda a `constitution.md`, ou seja, os princípios de governança do projeto (padrões, convenções, restrições). É o único artefato pensado para ser **editado manualmente** com frequência; todos os outros comandos (`/speckit.specify`, `/speckit.plan`, etc.) leem esse arquivo como contexto.
- **`templates/`** — os modelos "core" que o spec-kit usa para gerar `spec.md`, `plan.md`, `tasks.md` e `checklist.md`. Definem a estrutura e as seções que cada artefato deve ter. **Não devem ser editados** — são mantidos pela ferramenta e atualizados a cada versão do spec-kit.
- **`scripts/`** — os scripts (PowerShell ou Bash, conforme o SO) que os comandos `/speckit.*` disparam nos bastidores: criar a pasta de uma nova feature, preparar o `plan.md`, gerar `tasks.md`, checar pré-requisitos antes de rodar um comando. É a "cola" de automação entre o chat do Copilot/Claude e o sistema de arquivos.
- **`workflows/`** — descreve a sequência oficial dos comandos do spec-kit (constitution → specify → clarify → plan → tasks → implement → analyze) e o registro de workflows disponíveis. É o que garante que o assistente saiba "o que vem depois" em cada etapa.
- **`integrations/` + `integration.json`** — identificam qual assistente de IA está integrado ao projeto (Copilot, Claude, etc.) e os manifestos específicos de cada um. É por isso que o comando aparece como `/speckit.constitution` no Copilot Chat e também funciona no Claude Code.
- **`init-options.json`** — um registro das opções escolhidas quando você rodou `specify init` (nome do projeto, integração selecionada, etc.), útil para auditoria ou para replicar a configuração em outro ambiente.

> ⚠️ Nesta versão do spec-kit não existem mais as pastas `extensions/` e `presets/` mencionadas em versões antigas da documentação — a customização por equipe hoje é feita ajustando a `constitution.md` e, se necessário, os próprios templates.

O que diferencia `templates/` de `scripts/`: **`templates/` é conteúdo** (a forma final que os documentos devem ter), enquanto **`scripts/` é comportamento** (a lógica que decide onde e como esse conteúdo é criado).

---

## ✍️ Parte 3: Criando a Constitution do Projeto

### 3.1 Usar o Copilot para gerar a constitution

No Copilot Chat, execute:

```
/speckit.constitution

Projeto: MonitoriaBase — Sistema de Gestão de Monitoria
Propósito: Sistema de gestão de alunos, exercícios e progresso de uma monitoria de arquitetura de software
Stack: .NET 8, C#, xUnit
Padrões: Clean Code, SOLID, injeção de dependência via construtor
Convenções:
- Código em inglês (classes, métodos, variáveis)
- Armazenamento in-memory (sem banco de dados externo)
- Interfaces antes de implementações
- Testes unitários para cada operação do serviço
```

### 3.2 Revisar e ajustar

Abra o arquivo `.specify/memory/constitution.md` gerado. Compare com o código do `projeto-base` e responda:

- As convenções estão alinhadas com o código existente (ex: nomes das classes, padrão de interface)?
- Sim
- Alguma regra importante ficou faltando?
- Não
- Tem algo que o Copilot assumiu que você não concorda?
- Não, mas ficou mais robusto que da referencia, as vezes só refenciar o SOLID é melhor? Pois basicamente ele explicou cada principio.

> 💡 **Referência:** Veja o exemplo em `codigo-exemplo/exemplo-specify/.specify/memory/constitution.md` para comparar com um constitution bem elaborado.

Edite diretamente o arquivo para ajustar o que for necessário.

### 3.3 Adicionar ao git

```bash
git add .specify/
git commit -m "feat: inicializar spec-kit no projeto-base"
```

---

## ✅ Critérios de Sucesso

- [X]  `specify --version` retorna uma versão sem erros
- [X]  `/speckit` aparece como sugestão no Copilot Chat do VS Code
- [X]  Pasta `.specify/` criada com a estrutura correta
- [X]  `constitution.md` criada, revisada e commitada
- [X]  Você consegue explicar a diferença entre `memory/`, `templates/`, `scripts/` e `workflows/`

---

## 🔍 Exploração Adicional

- Abra algum arquivo dentro de `.specify/templates/` — o que esses templates definem?
- Leia o `README.md` do repositório spec-kit no GitHub para entender as extensões disponíveis
- O que aconteceria se dois membros do time tivessem `constitution.md` diferentes?

---

## ➡️ Próximo Exercício

Agora que o spec-kit está configurado, você vai usá-lo para especificar uma feature real:
**[Exercício 2: Especificando uma Feature →](./exercicio-02-especificando-uma-feature.md)**
