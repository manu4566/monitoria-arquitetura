# 🔴 Exercício 2: Sensor de Arquitetura

**Dificuldade:** Avançado | **Tempo estimado:** 90-120 min

---

## 🎯 Objetivo

Implementar uma **fitness function** — um sensor de Architecture Fitness — que verifica automaticamente a regra "Models não pode depender de Services" no projeto MonitoriaBase, sem depender de nenhuma biblioteca externa além do que o projeto já usa (xUnit).

## 📋 Cenário

Você documentou essa regra no `CLAUDE.md` do Exercício 2 de `guides/`. Um guide, sozinho, não impede ninguém (nem um agente de IA) de violá-la por descuido. Agora você vai transformá-la num sensor: um teste que falha automaticamente se a regra for quebrada.

## ✅ Pré-requisitos

- [ ] Ter concluído o Exercício 1 desta subdivisão
- [ ] Familiaridade básica com C# e reflection (o exercício explica o necessário)

## 🏋️ Parte 1: Escrevendo a Fitness Function

Crie o arquivo `tests/MonitoriaBase.Tests/ArchitectureFitnessTests.cs`:

```csharp
using System.Reflection;
using MonitoriaBase.Models;
using Xunit;

namespace MonitoriaBase.Tests;

public class ArchitectureFitnessTests
{
    private const string ModelsNamespace = "MonitoriaBase.Models";
    private const string ServicesNamespace = "MonitoriaBase.Services";

    [Fact]
    public void Models_Should_Not_Depend_On_Services()
    {
        var assembly = typeof(Student).Assembly;
        var modelTypes = assembly.GetTypes()
            .Where(t => t.Namespace == ModelsNamespace);

        var violations = new List<string>();

        foreach (var type in modelTypes)
        {
            var members = type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var member in members)
            {
                var referencedTypes = member switch
                {
                    PropertyInfo p => new[] { p.PropertyType },
                    FieldInfo f => new[] { f.FieldType },
                    MethodInfo m => m.GetParameters().Select(par => par.ParameterType).Append(m.ReturnType).ToArray(),
                    _ => Array.Empty<Type>()
                };

                foreach (var referenced in referencedTypes)
                {
                    if (referenced.Namespace == ServicesNamespace)
                    {
                        violations.Add($"{type.Name}.{member.Name} referencia {referenced.Name} ({ServicesNamespace})");
                    }
                }
            }
        }

        Assert.True(violations.Count == 0,
            "Violação de arquitetura — Models não pode depender de Services:\n" + string.Join("\n", violations));
    }
}
```

Rode:

```bash
dotnet test
```

O teste deve passar — hoje, `Models/` não depende de `Services/`.

## 💥 Parte 2: Provocando uma Violação

Simule o que aconteceria se um agente de IA (ou uma pessoa apressada) introduzisse a violação por engano. Em `Student.cs`, adicione temporariamente:

```csharp
using MonitoriaBase.Services;
// ...
public IMonitoriaService? Service { get; set; }
```

Rode `dotnet test` de novo. Observe a mensagem de erro — ela aponta exatamente o membro e o tipo violador. **Desfaça a mudança** antes de continuar (`git checkout` no arquivo, ou remova manualmente).

## 🔍 Parte 3: Discutindo os Limites do Sensor

Este sensor é **Computational** (determinístico, rápido, baseado em reflection) — mas ele só enxerga dependências que aparecem na *assinatura* dos membros (propriedades, campos, parâmetros, retorno). Ele não pega, por exemplo, uma instância criada dentro do corpo de um método sem ser armazenada em campo/propriedade.

## 💭 Questões para Refletir

- Que tipo de violação esse sensor **não** pegaria? Como você resolveria isso — um sensor mais sofisticado (ex.: analisando o IL do método) ou um sensor complementar **Inferential** (pedir a um agente para revisar semanticamente)?
- Quando compensa usar uma biblioteca pronta de fitness functions (como NetArchTest) em vez de um teste de reflection artesanal como este?

## ✅ Critérios de Sucesso

- [ ] `ArchitectureFitnessTests.cs` criado e passando com o código original
- [ ] Você reproduziu a violação e viu o teste falhar com mensagem clara
- [ ] Você desfez a violação e confirmou que o teste volta a passar

## 🔍 Exploração Adicional

Adicione uma segunda fitness function verificando outra regra do projeto — por exemplo, "toda classe pública em `Models/` deve ter um construtor" ou "`MonitoriaService` deve implementar `IMonitoriaService`".

## ➡️ Próximo Passo

Siga para a subdivisão [`hooks/`](../../hooks/exercicios/) — lá, este mesmo sensor vai ser conectado ao Claude Code via `PostToolUse`, rodando automaticamente depois de cada edição, sem precisar chamar `dotnet test` manualmente.
