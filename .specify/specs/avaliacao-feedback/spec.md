# Especificação de Feature: Sistema de Avaliação e Feedback de Exercícios

**Branch da Feature**: `[avaliacao-feedback]`

**Criado em**: 2026-07-28

**Status**: Rascunho

**Input**: Descrição do usuário: "Sistema de avaliação e feedback de exercícios - O MonitoriaBase já permite que alunos marquem exercícios como concluídos (via MonitoriaService.MarkExerciseAsCompleted). O que está faltando é a capacidade de monitores darem feedback estruturado sobre essas conclusões."

## Cenários de Usuário e Testes *(obrigatório)*

### História de Usuário 1 - Monitor Submete Feedback (Prioridade: P1)

Um monitor precisa fornecer feedback escrito sobre uma tentativa de exercício concluída, incluindo uma avaliação se a solução do aluno está correta ou precisa de melhorias. Esta é a proposta de valor central: permitir que monitores comuniquem orientações específicas aos alunos sobre seu trabalho.

**Por que esta prioridade**: Esta é a feature mínima viável. Sem a capacidade de submeter feedback, o sistema não fornece valor algum. Todas as outras histórias constroem ou aprimoram esta base.

**Teste Independente**: Pode ser totalmente testado por: (1) Criar um aluno, (2) Criar um exercício, (3) Marcá-lo como concluído, (4) Submeter feedback do monitor com comentários e status aprovado/reprovado. Entrega valor imediato ao permitir que monitores registrem avaliações estruturadas.

**Cenários de Aceitação**:

1. **Dado** que um aluno completou um exercício, **Quando** um monitor submete feedback com comentários "Bom uso dos princípios SOLID" e status "Aprovado", **Então** o feedback é registrado com o identificador do monitor, timestamp, comentários e status de aprovação
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

### História de Usuário 3 - Monitor Revisa Histórico de Feedback (Prioridade: P3)

Monitores precisam visualizar todo o feedback que submeteram anteriormente para acompanhar suas atividades de monitoria, identificar padrões em erros comuns dos alunos e manter consistência em suas avaliações.

**Por que esta prioridade**: Isso é valioso para produtividade do monitor e garantia de qualidade, mas não essencial para o fluxo central de feedback. Alunos e monitores podem funcionar sem esta feature.

**Teste Independente**: Pode ser totalmente testado por: (1) Fazer um monitor submeter feedback em múltiplas tentativas, (2) Consultar feedback por ID do monitor. Entrega valor ao fornecer aos monitores uma trilha de auditoria e visibilidade de carga de trabalho.

**Cenários de Aceitação**:

1. **Dado** que um monitor submeteu feedback para 5 alunos diferentes, **Quando** o monitor solicita seu histórico de feedback, **Então** todas as 5 entradas de feedback são retornadas com detalhes de aluno e exercício
2. **Dado** que um monitor submeteu feedback ao longo de múltiplas sessões, **Quando** o monitor solicita feedback ordenado por data, **Então** o feedback é retornado em ordem cronológica

---

### História de Usuário 4 - Monitor Atualiza Feedback Existente (Prioridade: P4)

Monitores ocasionalmente precisam corrigir ou expandir feedback que submeteram anteriormente (por exemplo, para corrigir um erro de digitação, adicionar esclarecimento ou mudar o status de aprovação após reavaliação).

**Por que esta prioridade**: Esta é uma feature de qualidade de vida. Monitores podem contornar a ausência de edição submetendo novo feedback com correções, então isso não é crítico para MVP.

**Teste Independente**: Pode ser totalmente testado por: (1) Submeter feedback inicial, (2) Atualizá-lo com novos comentários e/ou status, (3) Verificar que as mudanças são refletidas e timestamps são atualizados. Entrega valor ao reduzir fricção no fluxo de trabalho do monitor.

**Cenários de Aceitação**:

1. **Dado** que um monitor submeteu feedback com status "Precisa Revisão", **Quando** o monitor atualiza o status para "Aprovado" após o aluno reenviar, **Então** o status do feedback é atualizado e o timestamp de modificação é registrado
2. **Dado** que o feedback contém um erro de digitação nos comentários, **Quando** o monitor atualiza os comentários, **Então** o texto corrigido é salvo preservando o timestamp de submissão original

---

### Casos Extremos

- O que acontece quando um monitor tenta submeter feedback para uma tentativa de exercício que não existe? (Sistema deve rejeitar com mensagem de erro clara)
- Como o sistema lida com submissão de feedback para um exercício que não foi marcado como concluído? (Sistema deve rejeitar - apenas tentativas concluídas podem receber feedback)
- O que acontece quando comentários de feedback excedem tamanho razoável (por exemplo, > 5000 caracteres)? (Sistema deve validar e rejeitar entrada muito longa)
- Como o sistema lida com submissões concorrentes de feedback por monitores diferentes para a mesma tentativa? (Última escrita vence, com timestamps para trilha de auditoria)
- O que acontece ao consultar feedback para um aluno ou monitor que não existe? (Retornar lista vazia, não um erro)

## Requisitos *(obrigatório)*

### Requisitos Funcionais

- **FR-001**: Sistema DEVE permitir que monitores submetam feedback para uma tentativa de exercício concluída, incluindo: comentários de feedback (texto), status de avaliação (aprovado/precisa revisão) e identificador do monitor
- **FR-002**: Sistema DEVE capturar automaticamente o timestamp de submissão do feedback quando o feedback é criado
- **FR-003**: Sistema DEVE associar feedback com a tentativa de exercício específica (identificada por StudentId e ExerciseId)
- **FR-004**: Sistema DEVE permitir que alunos recuperem todo o feedback que receberam, filtrado por ID do aluno
- **FR-005**: Sistema DEVE permitir que monitores recuperem todo o feedback que submeteram, filtrado por ID do monitor
- **FR-006**: Sistema DEVE prevenir submissão de feedback para tentativas de exercício que não existem ou não foram marcadas como concluídas
- **FR-007**: Sistema DEVE permitir que monitores atualizem feedback previamente submetido (comentários e status) preservando o timestamp de criação original
- **FR-008**: Sistema DEVE validar comentários de feedback para restrições de tamanho (mínimo 10 caracteres, máximo 5000 caracteres)
- **FR-009**: Sistema DEVE suportar consulta de feedback por status de avaliação (aprovado, precisa revisão)
- **FR-010**: Sistema DEVE persistir feedback em memória usando coleções .NET padrão (List ou Dictionary)

### Entidades-Chave

- **Feedback**: Representa a avaliação de um monitor sobre a tentativa de exercício de um aluno. Atributos-chave incluem: identificador único, referência à tentativa de exercício (StudentId + ExerciseId), identificador do monitor, comentários de feedback (texto), status de avaliação (aprovado/precisa revisão), timestamp de submissão, timestamp de última modificação
- **Evaluation Status**: Enumeração representando o resultado da avaliação do monitor (Approved, NeedsRevision)
- **Relacionamento com entidades existentes**: Feedback tem um relacionamento muitos-para-um com ExerciseAttempt (uma tentativa pode ter múltiplas entradas de feedback ao longo do tempo, mas tipicamente apenas a mais recente é considerada ativa)

## Critérios de Sucesso *(obrigatório)*

### Resultados Mensuráveis

- **SC-001**: Monitores podem submeter feedback para qualquer tentativa de exercício concluída em menos de 30 segundos (incluindo navegação e entrada de formulário)
- **SC-002**: Alunos podem visualizar todo o feedback recebido (através de todos os exercícios) em uma única consulta com tempo de resposta menor que 1 segundo para até 100 entradas de feedback
- **SC-003**: 100% das submissões de feedback incluem todos os campos obrigatórios (comentários, status, ID do monitor, timestamps) sem intervenção manual
- **SC-004**: Sistema lida corretamente com operações concorrentes (por exemplo, 10 monitores submetendo feedback simultaneamente) sem perda de dados ou corrupção
- **SC-005**: Cobertura de testes unitários para todas as operações relacionadas a feedback atinge 100% (todos os métodos públicos no serviço de feedback têm testes correspondentes)

## Premissas

- **Identificação do monitor**: Monitores são identificados por um identificador de string simples (por exemplo, email ou nome). Autenticação/autorização completa está fora do escopo - o código chamador é responsável por fornecer o identificador do monitor
- **Um único feedback ativo por tentativa**: Embora o sistema armazene histórico de feedback, o feedback mais recente para uma tentativa é considerado o feedback "ativo". Questões de UI/relatórios estão fora do escopo desta especificação
- **Armazenamento em memória**: Dados de feedback são persistidos usando coleções em memória (List<Feedback> ou Dictionary<Guid, Feedback>). Nenhum banco de dados ou armazenamento externo é necessário
- **Serviços existentes estão disponíveis**: A especificação assume que MonitoriaService e entidades relacionadas (Student, Exercise, ExerciseAttempt) já estão disponíveis e funcionando conforme documentado
- **Sem sistema de notificação**: Notificações automáticas para alunos quando recebem feedback estão fora do escopo. Alunos devem consultar explicitamente por seu feedback
- **Código em inglês**: Todo o código (classes, métodos, variáveis) é escrito em inglês seguindo as convenções existentes do MonitoriaBase
- **Thread safety não necessária**: Dado o armazenamento em memória e contexto educacional, implementações thread-safe (locks, coleções concorrentes) não são necessárias a menos que explicitamente necessário para um cenário específico
