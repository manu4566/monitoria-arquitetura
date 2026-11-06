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
