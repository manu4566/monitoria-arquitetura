# Especificação de Feature: Sistema de Avaliação e Feedback de Exercícios

**Branch da Feature**: `[avaliacao-feedback]`

**Criado em**: 2026-07-28

**Status**: Rascunho

**Input**: Descrição do usuário: "Sistema de avaliação e feedback de exercícios - O MonitoriaBase já permite que alunos marquem exercícios como concluídos (via MonitoriaService.MarkExerciseAsCompleted). O que está faltando é a capacidade de monitores darem feedback estruturado sobre essas conclusões."

## Esclarecimentos

### Sessão 2026-07-28

- Q: O feedback é dado sobre o ExerciseAttempt existente ou cria um novo registro? → A: O monitor avalia o ExerciseAttempt existente (criado quando o aluno conclui o exercício) adicionando um registro de Feedback separado que o referencia.
- Q: Qual o formato do feedback e da avaliação? → A: O feedback conterá apenas um comentário em texto livre e um status simples (Aprovado ou Precisa Revisão) usando um Enum.
- Q: Um aluno pode ter múltiplos attempts do mesmo exercício? O que acontece com feedbacks anteriores? → A: Sim, o aluno pode ter múltiplos attempts. Cada attempt possui seu feedback próprio (estão associados 1-para-1). Apenas a tentativa mais recente é considerada activa pelo sistema.
- Q: Monitores podem editar um feedback já dado? → A: Não, o feedback é imutável após ser criado. Caso precise ser corrigido, o monitor deve aguardar uma nova tentativa do aluno ou registrar um novo feedback.
- Q: Precisamos rastrear qual monitor deu o feedback e o feedback altera o status de ExerciseAttempt? → A: Não rastrearemos o monitor individual (será um monitor padrão/genérico no sistema para maior simplicidade) e a criação de um Feedback altera diretamente o campo Status do ExerciseAttempt original correspondente (ex: para Aprovado ou PrecisaRevisao).

## Cenários de Usuário e Testes *(obrigatório)*

### História de Usuário 1 - Monitor Submete Feedback (Prioridade: P1)

Um monitor precisa fornecer feedback escrito sobre uma tentativa de exercício concluída, incluindo uma avaliação se a solução do aluno está correta ou precisa de melhorias. Esta é a proposta de valor central: permitir que monitores comuniquem orientações específicas aos alunos sobre seu trabalho.

**Por que esta prioridade**: Esta é a feature mínima viável. Sem a capacidade de submeter feedback, o sistema não fornece valor algum. Todas as outras histórias constroem ou aprimoram esta base.

**Teste Independente**: Pode ser totalmente testado por: (1) Criar um aluno, (2) Criar um exercício, (3) Marcá-lo como concluído, (4) Submeter feedback do monitor com comentários e status aprovado/reprovado. Entrega valor imediato ao permitir que monitores registrem avaliações estruturadas.

**Cenários de Aceitação**:

1. **Dado** que um aluno completou um exercício, **Quando** um monitor submete feedback com comentários "Bom uso dos princípios SOLID" e status "Aprovado", **Então** o feedback é registrado com timestamp, comentários e status de aprovação
2. **Dado** que um aluno completou um exercício com erros, **Quando** um monitor submete feedback com comentários "Injeção de dependência via construtor está faltando" e status "Precisa Revisão", **Então** o feedback é registrado com status de revisão necessária
3. **Dado** que uma tentativa de exercício não tem feedback prévio, **Quando** um monitor submete feedback, **Então** o feedback se torna o feedback ativo atual para aquela tentativa

---

### História de Usuário 2 - Aluno Visualiza Feedback Recebido (Prioridade: P2)

Alunos precisam ver todo o feedback que receberam para suas tentativas de exercício, incluindo quais exercícios foram aprovados e quais precisam de revisão, para que possam aprender com os erros e acompanhar seu progresso.

**Por que esta prioridade**: Feedback só fornece valor se os alunos puderem acessá-lo. Isso completa o ciclo de feedback e habilita o processo de aprendizado.

**Teste Independente**: Pode ser totalmente testado por: (1) Criar um aluno e exercícios, (2) Submeter múltiplas entradas de feedback para diferentes tentativas, (3) Consultar feedback por ID do aluno. Entrega valor ao dar aos alunos visibilidade sobre suas avaliações.

**Cenários de Aceitação**:

1. **Dado** que um aluno recebeu feedback em 3 tentativas de exercício, **Quando** o aluno solicita seu histórico de feedback, **Então** todas as 3 entradas de feedback são retornadas com detalhes do exercício, comentários, status e timestamps
2. **Dado** que um aluno não tem exercícios concluídos, **Quando** o aluno solicita seu feedback, **Então** uma lista vazia é retornada
3. **Dado** que um aluno recebeu feedback tanto aprovado quanto precisa-revisão, **Quando** o aluno filtra por status, **Então** apenas feedback que corresponde ao status solicitado é retornado

---

### Casos Extremos

- O que acontece quando se tenta submeter feedback para uma tentativa de exercício que não existe? (Sistema deve rejeitar com mensagem de erro clara)
- Como o sistema lida com submissão de feedback para um exercício que não foi marcado como concluído? (Sistema deve rejeitar - apenas tentativas concluídas podem receber feedback)
- O que acontece quando se tenta submeter feedback para uma tentativa que já foi avaliada (Status já é Approved ou NeedsRevision)? (Sistema deve rejeitar com InvalidOperationException - feedback é imutável, não pode ser substituído)
- O que acontece quando comentários de feedback excedem tamanho razoável (por exemplo, > 5000 caracteres)? (Sistema deve validar e rejeitar entrada muito longa)
- O que acontece ao consultar feedback para um aluno que não existe? (Retornar lista vazia, não um erro)

## Requisitos *(obrigatório)*

### Requisitos Funcionais

- **FR-001**: Sistema DEVE permitir que monitores submetam feedback para uma tentativa de exercício concluída, incluindo: comentários de feedback (texto), status de avaliação (aprovado/precisa revisão), identificador único, referência à tentativa (ExerciseAttempt) e timestamp de criação (capturado automaticamente)
- **FR-004**: Sistema DEVE permitir que alunos recuperem todo o feedback que receberam, filtrado por ID do aluno
- **FR-005**: Sistema DEVE garantir que o feedback seja fornecido a uma tentativa concluída existente (ExerciseAttempt) e atualize o Status dessa tentativa
- **FR-006**: Sistema DEVE validar comentários de feedback para restrições de tamanho (mínimo 10 caracteres, máximo 5000 caracteres)
- **FR-007**: Sistema DEVE suportar consulta de feedback por status de avaliação (aprovado, precisa revisão)
- **FR-008**: Sistema DEVE persistir feedback em memória usando coleções .NET padrão (List ou Dictionary)

### Entidades-Chave

- **Feedback**: Representa a avaliação de um monitor sobre a tentativa de exercício de um aluno. Atributos-chave incluem: identificador único (Id), referência à tentativa de exercício (ExerciseAttempt), comentários (texto) e status de avaliação (Enumerador). Possui apenas o timestamp de criação, sem timestamp de última modificação ou identificação do monitor (pois o feedback é imutável e os monitores não são rastreados individualmente).
- **Evaluation Status / Status de Avaliação**: Enumeração representando o resultado da avaliação. **Mapeamento terminológico**: "Aprovado" (português) = `Approved` (código C#), "Precisa Revisão" (português) = `NeedsRevision` (código C#)
- **Relacionamento com entidades existentes**: O Feedback se refere a uma tentativa de exercício (ExerciseAttempt). Como o feedback avalia uma tentativa existente concluída, a criação do feedback atualiza diretamente o status da tentativa correspondente.

## Critérios de Sucesso *(obrigatório)*

### Resultados Mensuráveis

- **SC-001**: A operação `SubmitFeedback` executa em menos de 100ms para uma única submissão de feedback
- **SC-002**: Alunos podem visualizar todo o feedback recebido (através de todos os exercícios) em uma única consulta com tempo de resposta menor que 1 segundo para até 100 entradas de feedback
- **SC-003**: 100% das submissões de feedback incluem todos os campos obrigatórios (comentários, status, timestamps) sem intervenção manual
- **SC-004**: Cobertura de testes unitários para todas as operações relacionadas a feedback atinge 100% (todos os métodos públicos no serviço de feedback têm testes correspondentes)

## Premissas

- **Ausência de Rastreamento de Monitores**: Monitores não são rastreados individualmente pelo sistema, atuando como um ator genérico ("Monitor"). Não há necessidade de guardar ID de monitor ou login de monitor no feedback.
- **Armazenamento em memória**: Dados de feedback são persistidos usando coleções em memória (List<Feedback> ou Dictionary<Guid, Feedback>). Nenhum banco de dados ou armazenamento externo é necessário.
- **Serviços existentes estão disponíveis**: A especificação assume que MonitoriaService e entidades relacionadas (Student, Exercise, ExerciseAttempt) já estão disponíveis e funcionando conforme documentado.
- **Sem sistema de notificação**: Notificações automáticas para alunos quando recebem feedback estão fora do escopo. Alunos devem consultar explicitamente por seu feedback.
- **Código em inglês**: Todo o código (classes, métodos, variáveis) é escrito em inglês seguindo as convenções existentes do MonitoriaBase.
- **Thread safety não necessária**: Dado o armazenamento em memória e contexto educacional, implementações thread-safe (locks, coleções concorrentes) não são necessárias a menos que explicitamente necessário para um cenário específico.
