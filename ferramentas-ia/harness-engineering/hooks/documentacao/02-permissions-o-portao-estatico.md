# 🔐 Permissions — O Portão Estático

## 🚪 Antes do Hook, Já Existe um Portão

Nem todo controle sobre o que um agente pode fazer precisa de um script. O Claude Code já tem um primeiro nível de controle, **estático e declarativo**: o modelo de **permissions**, configurado em `settings.json` junto com os hooks.

Ele classifica ações por risco:

| Nível | Significado | Exemplo |
|---|---|---|
| ✅ Safe | Leitura, sem efeito colateral | Ler um arquivo, buscar um padrão |
| ⚠️ Caution | Escrita, efeito reversível | Editar um arquivo existente |
| 🚨 Dangerous | Execução, efeito potencialmente amplo | Rodar um comando de shell |
| ⛔ Forbidden | Muda o sistema, nunca permitido | Comandos destrutivos, acesso a segredos |

E permite listas explícitas de permissão, por padrão de ferramenta/comando:

```json
{
  "permissions": {
    "allow": ["Read", "Glob", "Grep", "Bash(git status)", "Bash(dotnet test)"],
    "deny": ["Bash(rm -rf *)", "Bash(sudo *)"]
  }
}
```

## ✅ Onde Permissions Basta

Permissions é a ferramenta certa quando a regra é **binária e estática**: "esta ferramenta/comando nunca deve rodar aqui" ou "esta sempre pode rodar sem perguntar". Não exige escrever nenhum script — é configuração pura, fácil de auditar em uma revisão de PR do próprio `settings.json`.

## 🚧 Onde Permissions Para de Ser Suficiente

Permissions não sabe **o quê** está sendo escrito, só **qual ferramenta** está sendo chamada. Ela não consegue expressar:

- "Bloqueie a edição, mas só se o arquivo for `appsettings.Production.json`" (regra condicional sobre o *conteúdo do argumento*, não sobre a ferramenta)
- "Rode um build depois da edição, e se falhar, explique o motivo ao agente" (precisa executar algo e dar feedback dinâmico — permissions não executa nada, só permite/bloqueia)
- "Valide se o arquivo editado respeita uma convenção específica do projeto"

É exatamente nesse ponto que os hooks entram: eles são a **extensão dinâmica e scriptável** do mesmo portão. Onde permissions decide com uma lista fixa, um hook decide rodando lógica de verdade — e é isso que faz dele o mecanismo capaz de implementar tanto guides dinâmicos (`PreToolUse`) quanto sensores (`PostToolUse`), como visto no documento anterior.

## 📐 Regra Prática

Antes de escrever um hook, pergunte: "isso pode ser resolvido só com uma entrada em `allow`/`deny`?" Se a resposta é sim, um hook é complexidade desnecessária. Se a regra depende de *conteúdo*, *resultado de uma verificação* ou *contexto além do nome da ferramenta*, é hora de um hook.
