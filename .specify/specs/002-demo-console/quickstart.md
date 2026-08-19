# Quickstart: Interface Console Demonstração

**Feature**: 002-demo-console  
**Date**: 2026-08-04  
**Purpose**: Manual validation guide for console demonstration interface

---

## Prerequisites

1. **.NET 8 SDK** installed and configured
2. **Feature 001 (avaliacao-feedback)** fully implemented
3. **All tests passing** from feature 001 (`dotnet test`)
4. **Working directory**: `ferramentas-ia/spec-kit/codigo-exemplo/projeto-base`

---

## Quick Start - Run the Demo

```bash
# Navigate to project
cd ferramentas-ia/spec-kit/codigo-exemplo/projeto-base

# Build the project
dotnet build

# Run the console demo
dotnet run --project src/MonitoriaBase
```

**Expected Output**: Console clears, displays header "Sistema MonitoriaBase - Demo", and shows main menu with 4 options.

---

## Validation Scenarios

### User Story 1: Menu Interativo de Navegação (P1 - MVP)

#### Scenario 1.1: Initial Display
**Given**: Program is started  
**When**: User views the screen  
**Then**: Verify:
- ✅ Console displays "Sistema MonitoriaBase - Demo" header
- ✅ Menu shows 4 options:
  - "1. Submeter Feedback"
  - "2. Consultar Feedbacks por Aluno"
  - "3. Consultar Feedbacks por Status"
  - "0. Sair"
- ✅ Prompt displays "Escolha uma opção:" or similar

**Manual Steps**:
```
1. Run: dotnet run --project src/MonitoriaBase
2. Observe output
3. ✅ Confirm all menu options are displayed in Portuguese
```

---

#### Scenario 1.2: Valid Navigation
**Given**: Menu is displayed  
**When**: User types "1" and presses Enter  
**Then**: Console transitions to "Submeter Feedback" screen

**Manual Steps**:
```
1. At menu, type: 1
2. Press: Enter
3. ✅ Confirm screen clears (FR-012)
4. ✅ Confirm new header shows "Submeter Feedback"
5. ✅ Confirm list of tentativas pendentes is displayed
```

---

#### Scenario 1.3: Exit Program
**Given**: Menu is displayed  
**When**: User types "0" and presses Enter  
**Then**: Program displays "Encerrando..." and terminates

**Manual Steps**:
```
1. At menu, type: 0
2. Press: Enter
3. ✅ Confirm "Encerrando..." message appears
4. ✅ Confirm program exits cleanly (no errors)
```

---

#### Scenario 1.4: Invalid Option
**Given**: Menu is displayed  
**When**: User types "99" and presses Enter  
**Then**: System displays "Opção inválida. Tente novamente." and redisplays menu

**Manual Steps**:
```
1. At menu, type: 99
2. Press: Enter
3. ✅ Confirm error message "✗ Opção inválida..." appears
4. ✅ Confirm menu is redisplayed (loop continues)
```

**Additional Invalid Inputs to Test**:
- Type "abc" → Should show same error
- Press Enter without typing → Should show same error
- Type "-1" → Should show same error

---

#### Scenario 1.5: Return to Menu After Operation
**Given**: User completed submitting feedback  
**When**: Operation finishes  
**Then**: System displays "Pressione Enter para voltar ao menu..." and returns to main menu

**Manual Steps**:
```
1. Navigate to "1. Submeter Feedback"
2. Complete a feedback submission (see US2 scenarios)
3. ✅ Confirm "Pressione Enter para voltar..." message appears
4. Press: Enter
5. ✅ Confirm main menu is redisplayed
```

---

### User Story 2: Demonstração de Submissão de Feedback (P2 - Core Demo)

#### Scenario 2.1: Display Available Attempts
**Given**: User chose "1. Submeter Feedback"  
**When**: Screen loads  
**Then**: List of pending attempts is displayed with format "[ID] - Exercício: [nome] | Aluno: [nome] | Submetido em: [data]"

**Manual Steps**:
```
1. At menu, type: 1
2. Press: Enter
3. ✅ Confirm list shows at least 3 pending attempts (from seed data)
4. ✅ Confirm each line has format: "1. Exercício: API REST | Aluno: Ana Silva | Data: 01/08/2026 14:30"
5. ✅ Confirm IDs are sequential (1, 2, 3...), not GUIDs
6. ✅ Confirm dates are in Brazilian format (dd/MM/yyyy HH:mm)
```

**Expected Seed Data** (3 pending attempts):
- ID 1: Ana Silva - API REST - Recent date
- ID 2: Maria Santos - Lista Encadeada - Recent date
- ID 3: Carlos Souza - API REST - Recent date

---

#### Scenario 2.2: Select Attempt and Status
**Given**: List of attempts is displayed  
**When**: User types valid ID and presses Enter  
**Then**: System requests status choice: "1. Aprovado" or "2. Precisa Revisão"

**Manual Steps**:
```
1. At attempt list, type: 1 (first attempt)
2. Press: Enter
3. ✅ Confirm prompt shows "Escolha o status:"
4. ✅ Confirm options show "1. Aprovado" and "2. Precisa Revisão"
```

---

#### Scenario 2.3: Submit Feedback Successfully
**Given**: Status was chosen  
**When**: User enters valid comment (10-5000 chars) and presses Enter  
**Then**: System creates feedback and displays success message

**Manual Steps**:
```
1. Follow Scenario 2.2 to choose status
2. At status prompt, type: 1 (Aprovado)
3. Press: Enter
4. ✅ Confirm prompt shows "Digite seu comentário (10-5000 caracteres):"
5. Type: "Excelente trabalho! Implementação bem estruturada e testes completos."
6. Press: Enter
7. ✅ Confirm success message: "✓ Feedback submetido com sucesso!"
8. ✅ Confirm details show: feedback ID, status (Aprovado), date/time
9. ✅ Confirm prompt to return to menu appears
```

---

#### Scenario 2.4: Comment Too Short
**Given**: Comment prompt is displayed  
**When**: User enters comment with < 10 characters  
**Then**: System displays error and allows retry

**Manual Steps**:
```
1. Follow Scenario 2.2 to reach comment prompt
2. Type: "Bom" (only 3 characters)
3. Press: Enter
4. ✅ Confirm error: "✗ Comentário deve ter entre 10 e 5000 caracteres. Tente novamente."
5. ✅ Confirm prompt reappears for new input
6. Type valid comment (>10 chars)
7. ✅ Confirm feedback is created successfully
```

---

#### Scenario 2.5: Comment Too Long (Truncation)
**Given**: Comment prompt is displayed  
**When**: User enters comment with > 5000 characters  
**Then**: System truncates and displays warning

**Manual Steps**:
```
1. Follow Scenario 2.2 to reach comment prompt
2. Paste very long text (> 5000 characters)
3. Press: Enter
4. ✅ Confirm warning: "⚠ Comentário foi truncado para 5000 caracteres."
5. ✅ Confirm feedback is created with truncated comment
```

**Test Data**: Repeat a sentence until > 5000 chars, e.g., "Este é um comentário muito longo. " x 500

---

#### Scenario 2.6: Already Evaluated Attempt
**Given**: User chose "1. Submeter Feedback"  
**When**: User tries to select attempt that already has feedback  
**Then**: System displays error and returns to list

**Manual Steps**:
```
1. At menu, navigate to "1. Submeter Feedback"
2. ✅ Confirm that attempts with existing feedbacks are NOT in the list (filtered out)
3. Verify by checking "2. Consultar Feedbacks por Aluno" - should show 2 existing feedbacks from seed
```

**Note**: Seed data creates 2 evaluated attempts (Ana/Lista=Approved, Carlos/Auth=NeedsRevision). These should **not appear** in the pending list.

---

### User Story 3: Demonstração de Consulta de Feedbacks (P3 - Query Demo)

#### Scenario 3.1: Query by Student - Display Student List
**Given**: User chose "2. Consultar Feedbacks por Aluno"  
**When**: Screen loads  
**Then**: List of students is displayed with format "[ID] - [Nome]"

**Manual Steps**:
```
1. At menu, type: 2
2. Press: Enter
3. ✅ Confirm header shows "Consultar Feedbacks por Aluno"
4. ✅ Confirm list shows 3 students from seed data:
   - "1. Ana Silva"
   - "2. Carlos Souza"
   - "3. Maria Santos"
5. ✅ Confirm prompt: "Escolha o aluno:"
```

---

#### Scenario 3.2: Query by Student - Display Feedbacks
**Given**: Student list is displayed  
**When**: User types valid student ID  
**Then**: All feedbacks for that student are displayed with full details

**Manual Steps**:
```
1. Follow Scenario 3.1 to display student list
2. Type: 1 (Ana Silva)
3. Press: Enter
4. ✅ Confirm feedback(s) are displayed with format:
   - "Exercício: Implementação de Lista Encadeada"
   - "Status: Aprovado"
   - "Comentário: Excelente implementação! Código limpo e bem testado."
   - "Data: [date in dd/MM/yyyy HH:mm format]"
5. ✅ Confirm all Ana's feedbacks are shown (1 from seed + any created in previous scenarios)
```

---

#### Scenario 3.3: Query by Status - Choose Status
**Given**: User chose "3. Consultar Feedbacks por Status"  
**When**: Screen loads  
**Then**: Status options are displayed: "1. Aprovado" or "2. Precisa Revisão"

**Manual Steps**:
```
1. At menu, type: 3
2. Press: Enter
3. ✅ Confirm header shows "Consultar Feedbacks por Status"
4. ✅ Confirm options show:
   - "1. Aprovado"
   - "2. Precisa Revisão"
5. ✅ Confirm prompt: "Escolha o status:"
```

---

#### Scenario 3.4: Query by Status - Display Feedbacks
**Given**: Status choice is displayed  
**When**: User selects status  
**Then**: All feedbacks with that status are displayed, grouped by exercise

**Manual Steps**:
```
1. Follow Scenario 3.3 to display status options
2. Type: 1 (Aprovado)
3. Press: Enter
4. ✅ Confirm feedbacks with "Aprovado" status are displayed
5. ✅ Confirm at least 1 feedback from seed data (Ana/Lista = Approved)
6. ✅ Confirm format matches Scenario 3.2 (Exercício, Status, Comentário, Data)
7. Go back to menu
8. Navigate to "3. Consultar Feedbacks por Status" again
9. Type: 2 (Precisa Revisão)
10. ✅ Confirm feedbacks with "Precisa Revisão" status are displayed
11. ✅ Confirm at least 1 feedback from seed data (Carlos/Auth = NeedsRevision)
```

---

#### Scenario 3.5: No Feedbacks Found
**Given**: Query is executed (by student or status)  
**When**: No feedbacks match criteria  
**Then**: System displays "Nenhum feedback encontrado com os critérios selecionados."

**Manual Steps**:
```
1. Navigate to "2. Consultar Feedbacks por Aluno"
2. Choose student 3 (Maria Santos) - should have NO feedbacks in seed data
3. Press: Enter
4. ✅ Confirm message: "Nenhum feedback encontrado com os critérios selecionados."
5. ✅ Confirm prompt to return to menu appears
```

---

## Edge Case Validation

### Edge Case 1: Empty Input
**Test**: Press Enter without typing anything at any prompt

**Expected**: 
- ✅ Menu option: "✗ Opção inválida..."
- ✅ Comment: "✗ Comentário deve ter entre 10 e 5000 caracteres..."
- ✅ Student ID: "✗ ID inválido. Escolha um número da lista."

---

### Edge Case 2: Ctrl+C Graceful Shutdown
**Test**: Press Ctrl+C while program is running

**Expected**:
- ✅ Program displays "Encerrando..." (if handler is implemented)
- ✅ OR program exits immediately without error (acceptable fallback)

**Note**: This is optional - default Ctrl+C behavior is acceptable for demo purposes.

---

### Edge Case 3: Date Formatting
**Test**: Check all dates throughout the application

**Expected**:
- ✅ All dates use format: "dd/MM/yyyy HH:mm" (Brazilian standard)
- ✅ Examples: "04/08/2026 20:30", "01/08/2026 14:15"

---

### Edge Case 4: Unicode Symbols
**Test**: Check all success/error messages for symbols

**Expected**:
- ✅ Success: "✓" symbol (checkmark)
- ✅ Error: "✗" symbol (cross)
- ✅ Warning: "⚠" symbol (triangle)
- ✅ If terminal doesn't support Unicode, fallback to "OK", "ERRO", "AVISO" is acceptable

---

## Success Criteria Validation

### SC-001: Navigation Performance
**Test**: Time navigation from menu → submit feedback → return to menu

**Steps**:
1. Start timer when menu appears
2. Type: 1, Enter
3. Type: 1 (attempt ID), Enter
4. Type: 1 (status), Enter
5. Type: "Este é um comentário de teste validando performance de navegação.", Enter
6. Press Enter at "Pressione Enter..."
7. Stop timer when menu reappears

**Expected**: ✅ Total time < 60 seconds

---

### SC-002: Startup Performance
**Test**: Measure time from `dotnet run` to menu display

**Steps**:
1. Run: `dotnet run --project src/MonitoriaBase`
2. Measure time until "Escolha uma opção:" prompt appears

**Expected**: ✅ Startup time < 2 seconds

---

### SC-003: Validation Coverage
**Test**: Verify all validation errors show Portuguese messages

**Checklist**:
- ✅ Invalid menu option: Portuguese message
- ✅ Comment too short: Portuguese message
- ✅ Comment too long: Portuguese warning
- ✅ Already evaluated: Portuguese message
- ✅ Invalid attempt ID: Portuguese message
- ✅ Invalid student ID: Portuguese message
- ✅ Empty input: Portuguese message

---

### SC-004: Query Correctness
**Test**: Verify all 3 query types return correct results

**Checklist**:
- ✅ Query by Ana Silva: Shows 1 approved feedback (from seed)
- ✅ Query by Carlos Souza: Shows 1 needs-revision feedback (from seed)
- ✅ Query by Maria Santos: Shows "Nenhum feedback encontrado"
- ✅ Query by Aprovado status: Shows at least 1 feedback (Ana's)
- ✅ Query by Precisa Revisão status: Shows at least 1 feedback (Carlos's)

---

### SC-005: Stability Test
**Test**: Execute 10+ menu loops without crash

**Steps**:
1. Navigate menu options in sequence: 1→2→3→0
2. Repeat 3 times (12 operations total)
3. Try invalid inputs intermittently
4. Submit at least 2 feedbacks
5. Query multiple times

**Expected**: ✅ No crashes, no unhandled exceptions, clean exit on "0. Sair"

---

## Troubleshooting

### Issue: "Nenhuma tentativa disponível"
**Cause**: Seed data not created or all attempts already evaluated  
**Solution**: 
- Restart program (seed recreates on each run)
- Verify seed data creates 3 pending attempts

---

### Issue: Build errors or missing types
**Cause**: Feature 001 not fully implemented  
**Solution**:
- Ensure all tests pass: `dotnet test`
- Verify `IFeedbackService`, `IMonitoriaService` exist
- Verify `Feedback`, `EvaluationStatus` models exist

---

### Issue: Unicode symbols not displaying
**Cause**: Terminal encoding issue  
**Solution**: 
- This is acceptable per spec assumption (fallback allowed)
- Verify messages still appear in Portuguese
- Optionally change symbols to "OK"/"ERRO" in code

---

## Summary

This quickstart provides **manual validation scenarios** for all 3 user stories (P1, P2, P3), edge cases, and success criteria (SC-001 to SC-005).

**Next Step**: After manual validation, proceed to `/speckit.tasks` to generate implementation tasks.

**Total Validation Time**: ~15-20 minutes for complete runthrough.
