# Pesquisa de Design: Sistema de Avaliação e Feedback de Exercícios

Este documento consolida as análises técnicas, alternativas consideradas e decisões de design estrutural adotadas para a feature de avaliação e feedback no MonitoriaBase de acordo com as especificações em [.specify/specs/avaliacao-feedback/spec.md](.specify/specs/avaliacao-feedback/spec.md) e o plano em [.specify/specs/avaliacao-feedback/plan.md](.specify/specs/avaliacao-feedback/plan.md).

---

## 1. Relação entre Feedback e a Tentativa (ExerciseAttempt)

### Decisão
O modelo de dados implementará um relacionamento 1-para-1 direto entre a nova entidade `Feedback` e a entidade existente `ExerciseAttempt`. Cada tentativa (`ExerciseAttempt`) pode possuir no máximo um feedback. Alunos podem submeter múltiplas tentativas de um mesmo exercício, sendo que cada tentativa representa um objeto `ExerciseAttempt` próprio e recebe um feedback isolado.

### Justificativa (Rationale)
Essa abordagem garante total rastreabilidade do histórico de evolução do estudante. Ao invés de reescrever ou atualizar o feedback em um único registro centralizado, associar o feedback diretamente à tentativa mantém o histórico imutável correspondente àquela entrega técnica específica.

### Alternativas Consideradas
- **Feedback associado diretamente ao Exercício/Estudante (sem relacionar à tentativa)**:
  - *Por que foi rejeitada*: Dificulta identificar qual das tentativas do estudante foi revisada e aprovada, quebrando o fluxo de auditoria técnica.
- **Lista de comentários dentro da própria entidade `ExerciseAttempt`**:
  - *Por que foi rejeitada*: Viola o Princípio da Responsabilidade Única (SRP). Uma tentativa (`ExerciseAttempt`) representa as propriedades da submissão do aluno e o metadado temporal desta entrega. Separar a entidade de avaliação/revisão (`Feedback`) isola o domínio do monitor e o domínio do educando.

---

## 2. Mutabilidade e Rastreamento de Monitores

### Decisão
- O feedback será estritamente **imutável** após sua persistência na memória. Não haverá operações de atualização (*Update*) na API de feedback.
- Não haverá rastreamento individual do monitor que emitiu o comentário. O sistema assume um ator genérico ("Monitor"). O modelo `Feedback` não conterá nenhum campo como `MonitorId`.

### Justificativa (Rationale)
- **Imutabilidade**: Garante a integridade pedagógica e evita inconsistências temporais (como um monitor alterar uma revisão após o aluno já ter visualizado ou submetido uma nova solução baseada nela). Se um feedback estiver incorreto, o fluxo padrão consiste em aguardar uma nova entrega para correção ou anexar uma observação subsequente.
- **Rastreamento de Monitores**: Evita inflar o modelo com contextos de autenticação, controle de acesso e tabelas de usuários adicionais que estão fora de escopo para as diretrizes de simplicidade (*YAGNI*) do projeto educacional.

### Alternativas Consideradas
- **Permitir edição de feedback pelo autor**:
  - *Por que foi rejeitada*: Exigiria controle de histórico e lógica de concorrência complexa sem fornecer valor essencial imediato.
- **Criar cadastro e perfis de Monitores**:
  - *Por que foi rejeitada*: Seria uma quebra do princípio de Simplicidade e do YAGNI, introduzindo complexidade desnecessária para a finalidade de protótipo em memória.

---

## 3. Estratégia de Persistência Em Memória

### Decisão
Os objetos de `Feedback` serão mantidos em coleções em memória usando coleções nativas do C# (`List<Feedback>` e `Dictionary<Guid, Feedback>`) dentro do próprio `FeedbackService`. O repositório ou dicionário mapeará `Feedback.Id` para o objeto substantivo ou relacionará `ExerciseAttempt.Id` para encontrar seu respectivo comentário.

### Justificativa (Rationale)
Alinha-se diretamente com o padrão estipulado em [.specify/memory/constitution.md](.specify/memory/constitution.md) de manter as implementações 100% livres de dependências externas complexas de infraestrutura (bancos de dados, ORM, etc.) facilitando o aprendizado rápido e a execução ágil via testes de unidade simples.

### Alternativas Consideradas
- **Uso de arquivos JSON locais**:
  - *Por que foi rejeitada*: Adiciona complexidade de I/O de disco, concorrência de sistema de arquivos e necessidade de mocks de persistência, violando a regra de *In-Memory First*.

---

## 4. Estratégia de Atualização de Status da Tentativa

### Decisão
No momento da submissão do feedback (via `FeedbackService.SubmitFeedback`), o serviço atualizará diretamente o campo `Status` do `ExerciseAttempt` referenciado para o status correspondente da avaliação (`Approved` ou `NeedsRevision`). O status original do `ExerciseAttempt` é alterado de `"Completed"` para um desses novos valores.

### Justificativa (Rationale)
Essa atualização direta garante consistência imediata do progresso do aluno sem requerer orquestrações complexas de mensageria ou eventos. Quando um feedback é registrado, o status da entrega reflete de imediato o resultado técnico daquela avaliação.

---

## 5. Validação de Comentários

### Decisão
A validação de regra de negócio exigirá que o comentário de feedback possua de 10 a 5000 caracteres. Caso as regras sejam violadas, uma exceção apropriada será lançada (`ArgumentException`).

### Justificativa (Rationale)
Validações de tamanho previnem envios acidentais em branco ou com caracteres sem sentido pedagógico (ex: "ok") e mitigam distorções no leiaute por comentários massivos em cenários de renderização de interface.
