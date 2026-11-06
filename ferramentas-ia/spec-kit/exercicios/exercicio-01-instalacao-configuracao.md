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

- [ ] VS Code com extensão GitHub Copilot ativa
- [ ] Python 3.8+ instalado (`python --version`)
- [ ] .NET 8 SDK instalado (`dotnet --version`)
- [ ] Terminal com acesso à internet

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
│   └── constitution.md      ← Este arquivo você vai preencher
├── templates/               ← Templates core (não editar)
├── extensions/              ← Extensões disponíveis
└── presets/                 ← Customizações (vazio por enquanto)
```

**Questão para refletir:** Para que serve cada pasta? O que diferencia `templates/` de `presets/`?

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
- Alguma regra importante ficou faltando?
- Tem algo que o Copilot assumiu que você não concorda?

> 💡 **Referência:** Veja o exemplo em `codigo-exemplo/exemplo-specify/.specify/memory/constitution.md` para comparar com um constitution bem elaborado.

Edite diretamente o arquivo para ajustar o que for necessário.

### 3.3 Adicionar ao git

```bash
git add .specify/
git commit -m "feat: inicializar spec-kit no projeto-base"
```

---

## ✅ Critérios de Sucesso

- [ ] `specify --version` retorna uma versão sem erros
- [ ] `/speckit` aparece como sugestão no Copilot Chat do VS Code
- [ ] Pasta `.specify/` criada com a estrutura correta
- [ ] `constitution.md` criada, revisada e commitada
- [ ] Você consegue explicar a diferença entre `templates/`, `extensions/` e `presets/`

---

## 🔍 Exploração Adicional

- Abra algum arquivo dentro de `.specify/templates/` — o que esses templates definem?
- Leia o `README.md` do repositório spec-kit no GitHub para entender as extensões disponíveis
- O que aconteceria se dois membros do time tivessem `constitution.md` diferentes?

---

## ➡️ Próximo Exercício

Agora que o spec-kit está configurado, você vai usá-lo para especificar uma feature real:
**[Exercício 2: Especificando uma Feature →](./exercicio-02-especificando-uma-feature.md)**
