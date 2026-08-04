# Guia de Validação Rápida: Sistema de Avaliação e Feedback

Este guia fornece cenários de validação executáveis fim a fim (fim-a-fim) que definem o comportamento esperado e guiam a validação da feature "Sistema de Avaliação e Feedback de Exercícios". Ele estabelece os pré-requisitos, cenários de negócio descritos no formato Given-When-Then, instruções de execução de testes de aceitação unitários de forma interativa e os resultados esperados.

Para detalhes do design do modelo de integridade de dados e termos técnicos usados aqui, consulte [.specify/specs/avaliacao-feedback/data-model.md](.specify/specs/avaliacao-feedback/data-model.md). O contrato de serviço que formaliza estas operações está definido em [.specify/specs/avaliacao-feedback/contracts/IFeedbackService.cs](.specify/specs/avaliacao-feedback/contracts/IFeedbackService.cs).

---

## 1. Cenários de Aceitação de Negócio

Estes cenários comprovam o cumprimento dos requisitos funcionais mínimos definidos em [.specify/specs/avaliacao-feedback/spec.md](.specify/specs/avaliacao-feedback/spec.md):

### Cenário 1: Submeter Feedback Válido de Aprovação (P1)
- **Dado** que um estudante está registrado e possui uma tentativa de exercício concluída com o status "Completed",
- **Quando** o monitor submeter um feedback válido com o comentário "Excelente aplicação dos princípios SOLID e isolamento de interfaces!" e status `EvaluationStatus.Approved` via `IFeedbackService.SubmitFeedback`,
- **Então** o registro de `Feedback` deve ser criado com um ID gerado automaticamente e o carimbo de data correspondente (`CreatedAt`),
- **E** o status da tentativa (`ExerciseAttempt.Status`) deve ser modificado instantaneamente de `"Completed"` para `"Approved"`.

### Cenário 2: Submeter Feedback Requerendo Revisão (P1)
- **Dado** que um estudante tem uma tentativa de exercício no status `"Completed"`,
- **Quando** o monitor submete um feedback com comentário "Código possui acoplamento rígido; utilize injeção de dependência via construtor." e status `EvaluationStatus.NeedsRevision`,
- **Então** o sistema armazena a avaliação de forma imutável,
- **E** o status do `ExerciseAttempt` correspondente deve ser modificado para `"NeedsRevision"`.

### Cenário 3: Validação de Comentário Curto (Caso Extremo)
- **Dado** uma tentativa de exercício concluída de um estudante,
- **Quando** o monitor tentar submeter um feedback com o comentário "Melhore isso" (9 caracteres),
- **Então** o sistema deve rejeitar a submissão imediatamente, lançando uma exceção do tipo `ArgumentException`,
- **E** nenhum registro de feedback deve ser armazenado, mantendo a tentativa original com status inalterado.

### Cenário 4: Aluno Busca Histórico de Feedbacks (P2)
- **Dado** que o estudante "Alice" recebeu 1 feedback aprovado e 1 feedback necessitando revisão,
- **Quando** o estudante consultar seu histórico por meio do método `GetFeedbackByStudentId`,
- **Então** ambos os feedbacks são retornados perfeitamente estruturados contendo IDs, comentários técnicos, status e as datas de criação originais,
- **E** ao consultar filtrando por status aprovado (`GetFeedbackByStatus`), apenas o feedback correspondente a `EvaluationStatus.Approved` deve ser listado.

---

## 2. Instruções de Execução e Validação Técnica

### Pré-requisitos
- .NET 8 SDK instalado localmente.
- O terminal deve estar posicionado na pasta do projeto base educacional: `ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/`.

### Como rodar os testes
Para rodar os testes de aceitação unitários uma vez que a feature for implementada:

1. Abra um terminal do PowerShell do sistema.
2. Navegue até a pasta de soluções:
   ```powershell
   cd ferramentas-ia/spec-kit/codigo-exemplo/projeto-base/
   ```
3. Execute o comando de compilação para garantir que o projeto está estruturado e sem avisos de compilação:
   ```powershell
   dotnet build
   ```
4. Acione a suite de testes automatizados do xUnit:
   ```powershell
   dotnet test
   ```

### Resultados de Sucesso Esperados
O comando do xUnit de testes (`dotnet test`) deve listar a nova suite de validação `FeedbackServiceTests` com as seguintes saídas esperadas de 100% de sucesso (passando em verde):

```text
Passed!  - Failed:     0, Passed:     8, Skipped:     0, Total:     8, Duration: < 100 ms - MonitoriaBase.Tests.dll
```
