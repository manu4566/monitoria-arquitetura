# 🏛️ Monitoria de Arquitetura

🎓 Repositório com documentação e exercícios práticos para monitoria de arquitetura de software.

## 📁 Estrutura do Repositório

```
monitoria_arquitetura/
├── 🏗️ ci-cd/                    # Integração e Entrega Contínua
│   ├── 📚 documentacao/         # Documentação teórica
│   ├── 🎯 exercicios/          # Exercícios práticos
│   └── 💻 codigo-exemplo/       # Código pronto para usar
├── 🎨 design-patterns/         # Padrões de Projeto
│   ├── 📚 documentacao/
│   ├── 🎯 exercicios/
│   └── 💻 codigo-exemplo/
├── 🤖 ferramentas-ia/          # Ferramentas de IA para Desenvolvimento
│   ├── 🔧 spec-kit/            # Spec-Driven Development com Copilot
│   └── 🧰 harness-engineering/ # Guides, sensores e hooks
│       ├── guides/
│       ├── sensores/
│       └── hooks/
├── 🔧 microservicos/           # Arquitetura de Microserviços (em breve)
├── ☁️ cloud/                   # Arquitetura em Nuvem (em breve)
└── 📊 observabilidade/         # Monitoring e Observabilidade (em breve)
```

## 📚 Tópicos Disponíveis

### 🏗️ 1. CI/CD - Integração e Entrega Contínua
- **📖 Documentação**: Conceitos fundamentais de CI/CD com Azure DevOps
- **🎯 Exercícios**: Criação de pipelines, deploy automatizado e estratégias de release
- **💻 Código Pronto**: Projetos de exemplo para focar no aprendizado
- **🔗 Referências**: Links externos para estudo complementar

**✨ Destaques:**
- 🚀 **Pipeline Básico** - Seu primeiro pipeline CI/CD
- 🏗️ **Multi-Stage** - Pipelines profissionais com múltiplos estágios
- 🔄 **Templates** - Reutilização e padronização de pipelines
- 🎭 **Simulação** - Aprendizado sem custos de infraestrutura Azure

### 🎨 2. Design Patterns - Padrões de Projeto
- **📖 Documentação**: Fundamentos e guia prático dos principais padrões
- **🎯 Exercícios**: Sistema de logging, e-commerce e notificações
- **💻 Código Pronto**: Implementações completas com testes
- **🔗 Referências**: Livros, cursos e recursos de estudo

**✨ Destaques:**
- 👑 **Singleton** - Instância única thread-safe
- 🏭 **Factory Method** - Criação flexível de objetos  
- 🎨 **Decorator** - Funcionalidades dinâmicas
- 👁️ **Observer** - Sistema de notificações

### 🤖 3. Ferramentas de IA — spec-kit e Harness Engineering
- **📖 Documentação**: Fundamentos do SDD (spec-kit) e de Guides/Sensores/Hooks (Harness Engineering)
- **🎯 Exercícios**: Instalação e ciclo completo SDD; auditoria, escrita e implementação de guides, sensores e hooks
- **💻 Artefatos de Exemplo**: spec.md/plan.md/tasks.md (spec-kit); CLAUDE.md, fitness function e settings.json com hooks (Harness Engineering)

**✨ Destaques:**
- 📐 **Spec-Driven Development** - Especificação antes de implementação
- 🔧 **spec-kit** - Toolkit open-source com slash commands para Copilot
- 💡 **Boas Práticas** - Como escrever specs de qualidade e evitar armadilhas
- 🔄 **Ciclo completo** - Do requisito vago ao código implementado
- 🧰 **Harness Engineering** - O que compõe um agente de IA além do modelo
- 🧭 **Guides** - Direcionando o agente antes de agir
- 🔍 **Sensores** - Autocorreção via feedback (testes, lint, fitness functions)
- 🪝 **Hooks** - Conectando tudo ao ciclo de vida do agente

### 🚀 Próximos Tópicos
- 🔧 **Arquitetura de Microserviços** - Design e implementação
- ☁️ **Cloud Architecture** - Arquitetura na nuvem
- 📊 **Observabilidade** - Monitoring e métricas

## 🚀 Como Usar

### 📖 Para Estudar Design Patterns:
1. **📚 Comece pela documentação** em `design-patterns/documentacao/`:
   - 📝 `01-fundamentos-design-patterns.md` - Conceitos fundamentais  
   - 🛠️ `02-guia-pratico-padroes.md` - Implementações práticas
   - 📚 `03-referencias-estudo.md` - Recursos de aprofundamento

2. **🎯 Pratique com os exercícios** em `design-patterns/exercicios/`:
   - 🚀 **Exercício 1**: Sistema de Logging (45-60 min)
   - 🏗️ **Exercício 2**: E-commerce com Decorator (60-90 min)
   - 🎮 **Exercício 3**: Sistema de Notificações (90-120 min)

3. **💻 Use o código pronto** em `design-patterns/codigo-exemplo/`:
   - ✅ Implementações completas dos 9 padrões principais
   - 🧪 Testes unitários para cada padrão
   - 📖 Documentação detalhada com exemplos

### 📖 Para Estudar CI/CD:
1. **📚 Comece pela documentação** em `ci-cd/documentacao/`:
   - 📝 `01-fundamentos-cicd.md` - Conceitos base
   - ⚙️ `02-azure-pipelines-guia.md` - Guia prático  
   - 🔗 `03-referencias-estudo.md` - Links para aprofundamento

2. **🎯 Pratique com os exercícios** em `ci-cd/exercicios/`:
   - 🚀 **Exercício 1**: Pipeline Básico (30-45 min)
   - 🏗️ **Exercício 2**: Multi-Stage (45-60 min)
   - 🔄 **Exercício 3**: Templates (45-60 min)

3. **💻 Use o código pronto** em `ci-cd/codigo-exemplo/`:
   - ✅ Projetos funcionais para focar nas pipelines
   - 🎯 Templates reutilizáveis já configurados

### 📖 Para Estudar spec-kit e SDD:
1. **📚 Comece pela documentação** em `ferramentas-ia/spec-kit/documentacao/`:
   - 📐 `01-fundamentos-spec-driven-development.md` - O que é SDD e por que usar
   - 🛠️ `02-guia-pratico-spec-kit.md` - Instalação e comandos passo a passo
   - 💡 `03-boas-praticas-e-dicas.md` - Como tirar o máximo da ferramenta
   - 📚 `04-referencias-estudo.md` - Links e aprofundamento

2. **🎯 Pratique com os exercícios** em `ferramentas-ia/spec-kit/exercicios/`:
   - 🟢 **Exercício 1**: Instalação e configuração (30-45 min)
   - 🟡 **Exercício 2**: Especificando uma feature (60-90 min)
   - 🔴 **Exercício 3**: Ciclo completo SDD (90-120 min)

3. **💻 Use os artefatos de referência** em `ferramentas-ia/spec-kit/codigo-exemplo/`:
   - 📐 Spec completa com critérios de aceite mensuráveis
   - 🏗️ Plan técnico com decisões justificadas
   - 📋 Tasks ordenadas por dependência com critérios de conclusão

### 📖 Para Estudar Harness Engineering:

Estude as 3 subdivisões na ordem **Guides → Sensores → Hooks** — o exercício final de `hooks/` reaproveita os artefatos criados nas outras duas.

1. **📚 Comece pela documentação** em `ferramentas-ia/harness-engineering/{guides,sensores,hooks}/documentacao/`:
   - 🧭 `guides/` - O que é um guide, hierarquia de `CLAUDE.md`, como escrever guides eficazes
   - 🔍 `sensores/` - O que é um sensor, dimensões (Computational/Inferential) e categorias de regulação (Architecture Fitness em destaque)
   - 🪝 `hooks/` - Hooks como mecanismo, Permissions como portão estático, hooks em outros harnesses (git hooks, CI/CD)

2. **🎯 Pratique com os exercícios** em `ferramentas-ia/harness-engineering/{guides,sensores,hooks}/exercicios/`:
   - 🟢 **Guides**: auditar guides existentes (30-45 min) → escrever um `CLAUDE.md` completo (60-90 min)
   - 🟢🔴 **Sensores**: catalogar sensores existentes (30-45 min) → implementar uma fitness function de arquitetura (90-120 min)
   - 🟢🟡🔴 **Hooks**: primeiro hook simples (30-45 min) → hook como guide (60-90 min) → hook como sensor, fechando o ciclo (90-120 min)

3. **💻 Use os artefatos de referência** em `ferramentas-ia/harness-engineering/{guides,sensores,hooks}/codigo-exemplo/`:
   - 📄 `CLAUDE.md` de referência para o projeto de apoio
   - 🏛️ Fitness function pronta validando uma regra de arquitetura
   - 🪝 `settings.json` + scripts conectando guide e sensor via hook

### 💡 Dicas de Estudo:
- ⏰ **Sequencial**: Faça os exercícios em ordem
- 🧪 **Prático**: Execute todos os exemplos
- 📝 **Anote**: Documente seus aprendizados
- 🔄 **Repita**: Pratique até dominar

## 🤝 Contribuindo

🎓 Este repositório é mantido para fins **educacionais**. 

**Sugestões e melhorias são bem-vindas!**
- 🐛 **Issues**: Reportar problemas ou sugerir melhorias
- 🔧 **Pull Requests**: Contribuições diretas
- 💡 **Ideias**: Novos exercícios ou tópicos

---

**💜 Feito com carinho para a comunidade de desenvolvimento!**

*Bons estudos! 🎉*