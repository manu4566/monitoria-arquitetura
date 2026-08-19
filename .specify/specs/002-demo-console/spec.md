# Feature Specification: Interface Console Demonstração

**Feature Branch**: `002-demo-console`

**Created**: 2026-08-04

**Status**: Draft

**Input**: User description: "Console interativo com menu de opções. Demonstração dos cenários principais (submeter feedback, consultar feedbacks). Dados de exemplo (seed) para demonstração. Mensagens em português para o usuário."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Menu Interativo de Navegação (Priority: P1)

Usuário pode visualizar um menu console com opções numeradas e navegar entre diferentes funcionalidades do sistema através de entrada de texto.

**Why this priority**: É o MVP absoluto - sem o menu, não há como acessar nenhuma funcionalidade. Estabelece a interface base para todas as outras histórias.

**Independent Test**: Pode ser testado executando `dotnet run` e verificando que: (a) menu aparece com opções numeradas, (b) usuário pode digitar um número, (c) sistema responde à escolha, (d) loop continua até escolher "Sair".

**Acceptance Scenarios**:

1. **Given** o programa é iniciado, **When** usuário visualiza a tela, **Then** deve exibir título "Sistema MonitoriaBase - Demo" e menu com no mínimo 4 opções: "1. Submeter Feedback", "2. Consultar Feedbacks por Aluno", "3. Consultar Feedbacks por Status", "0. Sair"
2. **Given** menu está exibido, **When** usuário digita "1" e pressiona Enter, **Then** sistema deve exibir tela de submissão de feedback
3. **Given** menu está exibido, **When** usuário digita "0" e pressiona Enter, **Then** sistema deve exibir "Encerrando..." e finalizar execução
4. **Given** menu está exibido, **When** usuário digita opção inválida (ex: "99" ou "abc"), **Then** sistema deve exibir "Opção inválida. Tente novamente." e reexibir o menu
5. **Given** usuário completou uma operação (ex: submeter feedback), **When** operação termina, **Then** sistema deve exibir "Pressione Enter para voltar ao menu..." e retornar ao menu principal

---

### User Story 2 - Demonstração de Submissão de Feedback (Priority: P2)

Usuário (simulando monitor) pode submeter feedback para tentativas de exercício pré-cadastradas, escolhendo status (Aprovado/Precisa Revisão) e digitando comentário.

**Why this priority**: Demonstra o fluxo principal do sistema de feedback implementado na feature 001. É a funcionalidade core que valida a integração com `FeedbackService`.

**Independent Test**: Pode ser testado executando `dotnet run`, escolhendo opção "1" do menu, e verificando que: (a) lista de tentativas é exibida, (b) pode escolher uma tentativa, (c) pode escolher status, (d) pode digitar comentário, (e) feedback é criado com sucesso.

**Acceptance Scenarios**:

1. **Given** usuário escolheu "1. Submeter Feedback" no menu, **When** tela carrega, **Then** deve exibir lista de tentativas disponíveis no formato "[ID numérico] - Exercício: [nome] | Aluno: [nome] | Submetido em: [data/hora]"
2. **Given** lista de tentativas está exibida, **When** usuário digita ID válido e pressiona Enter, **Then** sistema deve solicitar escolha de status: "1. Aprovado" ou "2. Precisa Revisão"
3. **Given** status foi escolhido, **When** sistema solicita comentário, **Then** deve exibir "Digite seu comentário (10-5000 caracteres):"
4. **Given** usuário digitou comentário válido (10-5000 chars), **When** pressiona Enter, **Then** sistema deve criar feedback e exibir "✓ Feedback submetido com sucesso!" com detalhes (ID do feedback, status, data/hora)
5. **Given** usuário digita comentário muito curto (<10 chars), **When** pressiona Enter, **Then** sistema deve exibir erro "✗ Comentário deve ter entre 10 e 5000 caracteres. Tente novamente." e permitir nova tentativa
6. **Given** usuário escolhe tentativa que já tem feedback, **When** tenta submeter, **Then** sistema deve exibir "✗ Esta tentativa já foi avaliada. Escolha outra tentativa." e retornar à lista

---

### User Story 3 - Demonstração de Consulta de Feedbacks (Priority: P3)

Usuário pode visualizar feedbacks filtrados por aluno ou por status, com formatação clara e informações completas.

**Why this priority**: Demonstra a funcionalidade de consulta/leitura do sistema. Menos crítico que submissão, mas importante para validar queries.

**Independent Test**: Pode ser testado executando `dotnet run`, escolhendo opção "2" ou "3" do menu, e verificando que: (a) lista de feedbacks é exibida com filtros corretos, (b) informações estão completas e formatadas, (c) mensagem apropriada aparece se não há feedbacks.

**Acceptance Scenarios**:

1. **Given** usuário escolheu "2. Consultar Feedbacks por Aluno", **When** tela carrega, **Then** deve exibir lista de alunos disponíveis no formato "[ID numérico] - [Nome do Aluno]"
2. **Given** lista de alunos está exibida, **When** usuário digita ID válido, **Then** sistema deve exibir todos os feedbacks daquele aluno com formato: "Exercício: [nome] | Status: [Aprovado/Precisa Revisão] | Comentário: [texto] | Data: [data/hora]"
3. **Given** usuário escolheu "3. Consultar Feedbacks por Status", **When** tela carrega, **Then** deve solicitar escolha: "1. Aprovado" ou "2. Precisa Revisão"
4. **Given** status foi escolhido, **When** query executa, **Then** sistema deve exibir lista de feedbacks com aquele status, agrupados por exercício
5. **Given** filtro foi aplicado (por aluno ou status), **When** não há feedbacks correspondentes, **Then** sistema deve exibir "Nenhum feedback encontrado com os critérios selecionados."

---

### Edge Cases

- O que acontece quando nenhuma tentativa está disponível para avaliação? Exibir mensagem "Nenhuma tentativa disponível para avaliar no momento. Execute seed de dados primeiro."
- Como o sistema lida com entrada vazia (usuário pressiona Enter sem digitar nada)? Tratar como opção inválida e solicitar nova entrada.
- O que acontece se o comentário exceder 5000 caracteres? Sistema trunca automaticamente e exibe warning "Comentário foi truncado para 5000 caracteres."
- Como o sistema exibe datas/horas? Formato brasileiro: "dd/MM/yyyy HH:mm" (ex: "04/08/2026 20:30")
- O que acontece se o usuário tenta fechar o programa com Ctrl+C? Capturar o sinal e exibir "Encerrando..." antes de finalizar gracefully.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Sistema MUST exibir menu principal em português brasileiro com no mínimo 4 opções: submeter feedback, consultar por aluno, consultar por status, e sair
- **FR-002**: Sistema MUST aceitar entrada numérica do usuário para seleção de opções e validar se a opção existe
- **FR-003**: Sistema MUST criar seed de dados ao iniciar com no mínimo 3 alunos, 3 exercícios, e 5 tentativas de exercício (algumas avaliadas, outras pendentes)
- **FR-004**: Sistema MUST permitir submissão de feedback através de wizard interativo (escolher tentativa → escolher status → digitar comentário)
- **FR-005**: Sistema MUST validar comprimento do comentário (10-5000 caracteres) antes de criar feedback
- **FR-006**: Sistema MUST impedir avaliação duplicada (tentativa já com feedback Aprovado ou Precisa Revisão)
- **FR-007**: Sistema MUST exibir lista formatada de tentativas com identificador numérico sequencial (não GUID) para facilitar seleção
- **FR-008**: Sistema MUST permitir consulta de feedbacks por aluno, exibindo lista de alunos para escolha
- **FR-009**: Sistema MUST permitir consulta de feedbacks por status (Aprovado ou Precisa Revisão)
- **FR-010**: Sistema MUST exibir mensagem clara quando nenhum feedback for encontrado em consultas
- **FR-011**: Sistema MUST manter loop do menu até usuário escolher explicitamente "Sair"
- **FR-012**: Sistema MUST limpar console entre telas para melhor experiência visual
- **FR-013**: Sistema MUST exibir mensagens de sucesso (✓) e erro (✗) com símbolos visuais para feedback imediato

### Key Entities

- **MenuOption**: Representa uma opção do menu (número, descrição, ação associada)
- **ConsoleFeedbackView**: Estrutura para exibir feedback formatado no console (não persiste, apenas apresentação)
- **SeedData**: Dados de exemplo criados na inicialização (alunos, exercícios, tentativas, alguns feedbacks pré-existentes)

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Usuário consegue navegar do menu inicial até submeter feedback e retornar ao menu em menos de 60 segundos
- **SC-002**: Sistema inicia e exibe menu principal em menos de 2 segundos após executar `dotnet run`
- **SC-003**: 100% das validações de entrada (opção inválida, comentário curto/longo, tentativa já avaliada) exibem mensagem de erro clara em português
- **SC-004**: Todas as 3 consultas (por aluno, por status Aprovado, por status Precisa Revisão) retornam resultados corretos baseados nos dados seed
- **SC-005**: Sistema executa loop de menu no mínimo 10 vezes sem crash ou comportamento inesperado (teste de estabilidade)

## Assumptions

- Usuário tem familiaridade básica com terminal/console e sabe executar `dotnet run`
- Sistema será executado apenas localmente (não há necessidade de multi-tenancy ou isolamento de dados)
- Seed de dados é recriado a cada execução do programa (estado não persiste entre execuções)
- Idioma é sempre português brasileiro - não há necessidade de internacionalização (i18n)
- Console suporta Unicode para símbolos ✓ e ✗ (fallback para "OK" e "ERRO" se necessário)
- Interface é puramente textual - sem cores ANSI ou formatação avançada (mantém compatibilidade com todos os terminais)
- Usuário interage via entrada de texto (números e texto livre) - não há navegação por setas ou mouse
- GUIDs das entidades internas não são expostos ao usuário - apenas IDs sequenciais amigáveis (1, 2, 3...)
- Dependências de `FeedbackService`, `MonitoriaService` e modelos já existem e foram implementados na feature 001
