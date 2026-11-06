# 📚 Referências e Estudo

## 🔗 Fontes deste Módulo

- **[Harness Engineering](https://martinfowler.com/articles/harness-engineering.html)** — Birgitta Böckeler, martinfowler.com. Fonte principal: a dicotomia Guides vs. Sensors, as dimensões Computational vs. Inferential, "Keep Quality Left" e as 3 categorias de regulação vêm todas daqui.
- **[What is an AI Harness](https://www.youngju.dev/blog/culture/2026-03-23-ai-harness-skills-context-orchestration-guide.en)** — youngju.dev. Referência complementar sobre como um agent loop consome o resultado de um sensor.

## 🎓 Trilha de Aprendizado Sugerida

```
1. O que é um sensor (feedback control)
        ↓
2. Dimensões (Computational vs. Inferential) e categorias de regulação
        ↓
3. Exercício 1: catalogar sensores existentes no projeto de apoio
        ↓
4. Exercício 2: implementar uma fitness function de arquitetura
        ↓
5. Seguir para a subdivisão hooks/
```

## 🔍 Para Aprofundar

- Compare a fitness function que você escrever no Exercício 2 com o gabarito em `codigo-exemplo/exemplo-sensores/`.
- Böckeler discute ainda os conceitos de **Harnessability** (o quanto uma base de código é "arreável" — linguagens fortemente tipadas e limites modulares claros ajudam) e **Harness Templates** (bundles reutilizáveis de guides+sensores para topologias comuns) — vale a leitura do artigo original para aprofundar além do que este módulo cobre.
