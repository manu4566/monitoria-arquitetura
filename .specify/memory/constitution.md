<!--
SYNC IMPACT REPORT — Constitution v1.0.0 Initial Creation

Version Change: [TEMPLATE] → 1.0.0
Type: MAJOR (Initial Constitution)
Date: 2026-07-28

Modified Principles:
- Created I. SOLID Architecture Principles
- Created II. Clean Code Standards
- Created III. Test-Driven Development (NON-NEGOTIABLE)
- Created IV. Interface Segregation & Dependency Injection
- Created V. Simplicity & In-Memory First

Added Sections:
- Core Principles (5 principles defined)
- Technology Stack & Constraints
- Development Workflow & Quality Gates
- Governance

Templates Requiring Updates:
- ✅ spec-template.md — Constitution check gate references SOLID principles
- ✅ plan-template.md — Technical context aligns with .NET 8 + xUnit stack
- ✅ tasks-template.md — Task categorization includes unit testing requirements

Follow-up TODOs:
- None — All placeholders resolved

Rationale:
Initial constitution for MonitoriaBase establishes core architectural principles (SOLID),
code quality standards (Clean Code), testing discipline (TDD), and technology constraints
(.NET 8, in-memory storage). All principles are actionable, testable, and aligned with
existing codebase conventions.
-->

# MonitoriaBase Constitution

## Core Principles

### I. SOLID Architecture Principles

All code in MonitoriaBase MUST adhere to SOLID principles to ensure maintainability, testability, and extensibility:

- **Single Responsibility Principle (SRP)**: Each class has one and only one reason to change. Services handle business logic, models represent data, and interfaces define contracts.
- **Open/Closed Principle (OCP)**: Classes are open for extension but closed for modification. Use inheritance, interfaces, and dependency injection to add new behavior without altering existing code.
- **Liskov Substitution Principle (LSP)**: Derived classes must be substitutable for their base classes without breaking functionality. Implementations of `IMonitoriaService` or other interfaces must honor the contract fully.
- **Interface Segregation Principle (ISP)**: Clients should not depend on interfaces they don't use. Keep interfaces focused and cohesive (e.g., `IMonitoriaService` provides only monitoria-related operations).
- **Dependency Inversion Principle (DIP)**: Depend on abstractions (interfaces), not concretions. Constructor injection is the mandatory pattern for all dependencies.

**Rationale**: SOLID principles are the foundation of Clean Architecture and ensure that MonitoriaBase remains a teaching-quality codebase suitable for mentoring scenarios.

### II. Clean Code Standards

Code quality is non-negotiable. All contributions MUST follow Clean Code principles:

- **Naming**: Use PascalCase for classes, interfaces, methods, and properties; camelCase for variables and parameters. Names must be descriptive and reveal intent (e.g., `RegisterStudent`, `GetStudentProgress`).
- **Methods**: Keep methods short (ideally < 20 lines), focused on a single task, and at a consistent level of abstraction.
- **Comments**: Code should be self-documenting. Use comments only to explain "why," not "what." Remove commented-out code.
- **Readability**: Favor clarity over cleverness. Avoid deep nesting (max 3 levels). Use guard clauses for early returns.
- **Consistency**: Follow existing patterns in the codebase. If a convention is established (e.g., `IServiceName` for interfaces), apply it uniformly.

**Rationale**: MonitoriaBase serves as a reference implementation for learners. Clean, readable code is essential for teaching and reduces cognitive load during code reviews and mentoring sessions.

### III. Test-Driven Development (NON-NEGOTIABLE)

TDD is mandatory for all feature development. The workflow is strictly enforced:

1. **Write the test first**: Define the expected behavior as an xUnit test before writing any implementation code.
2. **See it fail (Red)**: Run the test and confirm it fails with a clear, expected error message.
3. **Implement to pass (Green)**: Write the minimum code necessary to make the test pass.
4. **Refactor**: Clean up the implementation while keeping tests green. Apply SOLID and Clean Code principles.
5. **Repeat**: For each new behavior, start at step 1.

**Test Coverage Requirements**:
- Every public method in a service MUST have at least one corresponding unit test.
- Test names follow the pattern: `MethodName_Scenario_ExpectedResult` (e.g., `RegisterStudent_WithValidData_ReturnsStudentWithId`).
- Tests must be independent, repeatable, and self-validating (no manual verification).

**Rationale**: TDD ensures correctness from the start, provides living documentation, and enables confident refactoring. It also models best practices for learners participating in the monitoria program.

### IV. Interface Segregation & Dependency Injection

Interfaces are the primary abstraction mechanism in MonitoriaBase:

- **Interfaces before implementations**: Every service MUST define an interface (e.g., `IMonitoriaService`) before the concrete implementation (`MonitoriaService`).
- **Interface naming**: Prefix all interfaces with `I` (e.g., `INotificationChannel`, `IFeedbackService`).
- **Constructor injection**: All dependencies MUST be injected via constructors. No property injection, no service locator pattern, no static dependencies.
- **In-memory implementations**: Default implementations use in-memory storage (e.g., `List<Student>`, `Dictionary<Guid, ExerciseAttempt>`). No external databases or APIs in core logic.

**Rationale**: Programming to interfaces enables testability (easy mocking), enforces loose coupling (DIP), and keeps the project self-contained for educational purposes (no external infrastructure required).

### V. Simplicity & In-Memory First

MonitoriaBase prioritizes simplicity and learnability over production-grade complexity:

- **No external dependencies**: No databases, message queues, or third-party APIs in core logic. Use in-memory collections (`List<T>`, `Dictionary<TKey, TValue>`).
- **YAGNI (You Aren't Gonna Need It)**: Don't build features or abstractions "for the future." Implement only what is needed for the current requirement.
- **Start simple, evolve intentionally**: Begin with the simplest solution that satisfies the tests. Add complexity only when justified by a concrete requirement.
- **No over-engineering**: Avoid frameworks, patterns, or architectures (e.g., CQRS, event sourcing) unless explicitly required by a feature specification. Keep it straightforward.

**Rationale**: The primary audience is learners in a mentoring context. Simplicity reduces barriers to understanding and allows focus on core principles (SOLID, TDD, Clean Code) without infrastructure noise.

## Technology Stack & Constraints

**Language & Runtime**: C# with .NET 8 SDK

**Testing Framework**: xUnit

**Code Language**: English for all code elements (classes, methods, variables, comments)

**Documentation Language**: Portuguese (Brazilian) for README files, exercise guides, and inline documentation intended for learners

**Storage**: In-memory only. No SQL databases, NoSQL databases, or file-based persistence in core services

**External Dependencies**: Prohibited in core business logic. Test dependencies (xUnit, Moq if needed) are acceptable

**Project Type**: Console application (for demos) + Class library (for services and models)

**Performance Goals**: Not applicable — educational project, not production system

**Constraints**:
- Must run without external infrastructure (no Docker, databases, or cloud services)
- Must be fully executable via `dotnet run` and `dotnet test` without additional setup
- Must not require paid tools or services

## Development Workflow & Quality Gates

**Branching**: Feature branches named `###-feature-name` (e.g., `001-feedback-system`)

**Commit Messages**: Use conventional commits format (e.g., `feat: add feedback service`, `test: cover edge cases for notification fallback`)

**Quality Gates** (must pass before merging):
1. ✅ All tests pass (`dotnet test` exits with code 0)
2. ✅ Code builds without warnings (`dotnet build`)
3. ✅ All new public methods have corresponding unit tests
4. ✅ SOLID principles verified in code review
5. ✅ No external dependencies introduced (unless pre-approved)

**Code Review Checklist**:
- [ ] Tests written before implementation (TDD)?
- [ ] All tests pass?
- [ ] SOLID principles followed?
- [ ] Clean Code standards met (naming, method length, readability)?
- [ ] Interfaces defined before implementations?
- [ ] Constructor injection used for dependencies?
- [ ] In-memory storage only (no external resources)?
- [ ] Code in English, documentation in Portuguese?

## Governance

This constitution is the highest authority on design and development practices for MonitoriaBase. All feature specifications, implementation plans, and code contributions must comply with these principles.

**Amendment Process**:
- Minor clarifications (typos, wording improvements): Direct edit with justification in commit message.
- Principle changes or additions: Propose in a discussion with the team, document rationale, and increment version per semantic versioning.
- Version bumps: MAJOR for backward-incompatible principle changes, MINOR for new principles or sections, PATCH for clarifications.

**Compliance Verification**:
- Constitution check gates are enforced in the plan template (`/speckit.plan`) before Phase 0 research.
- Re-check after Phase 1 design to ensure alignment.
- Use the code review checklist above for every pull request.

**Conflict Resolution**:
- If a feature requirement conflicts with a principle, escalate to the maintainer/mentor for a constitution amendment proposal.
- Document the conflict and resolution in the feature's `spec.md` and update this constitution accordingly.

**Version**: 1.0.0 | **Ratified**: 2026-07-28 | **Last Amended**: 2026-07-28
