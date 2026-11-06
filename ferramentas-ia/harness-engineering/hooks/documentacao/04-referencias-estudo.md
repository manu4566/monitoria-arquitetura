# 📚 Referências e Estudo

## 🔗 Fontes deste Módulo

- **[Harness Engineering](https://martinfowler.com/articles/harness-engineering.html)** — Birgitta Böckeler, martinfowler.com. Fonte principal do tópico como um todo; o enquadramento de hooks como o mecanismo que conecta guides e sensores ao agent loop segue o vocabulário desse artigo.
- **[What is an AI Harness](https://www.youngju.dev/blog/culture/2026-03-23-ai-harness-skills-context-orchestration-guide.en)** — youngju.dev. Fonte principal para o detalhe técnico de implementação: os 4 tipos de hook do Claude Code, o contrato JSON via `settings.json`, e o modelo de permissions.

## 🎓 Trilha de Aprendizado Sugerida

```
1. Hooks como mecanismo (os 4 tipos, agent loop, contrato)
        ↓
2. Permissions — o portão estático que os hooks estendem
        ↓
3. Hooks em outros harnesses (git hooks, CI/CD)
        ↓
4. Exercício 1: primeiro hook simples
        ↓
5. Exercício 2: hook como guide (PreToolUse)
        ↓
6. Exercício 3: hook como sensor (PostToolUse) — fecha o ciclo com guides/ e sensores/
```

## 🔍 Para Aprofundar

Böckeler discute ainda **Harnessability** (o quanto uma base de código facilita ou dificulta a construção de um bom harness — linguagens fortemente tipadas e limites modulares claros ajudam) e **Ambient Affordances** (propriedades estruturais do ambiente que tornam o código legível e navegável para um agente) — vale a leitura do artigo original para ir além do que este tópico cobre.
