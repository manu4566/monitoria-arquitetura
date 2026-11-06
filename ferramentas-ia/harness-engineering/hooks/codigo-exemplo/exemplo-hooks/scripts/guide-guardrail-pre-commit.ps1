# Git hook (pre-commit) — a mesma regra do guide-guardrail.ps1, mas agnostica de harness.
# Roda no commit, independente de qual ferramenta (ou nenhuma) gerou a mudanca.
# Para instalar: crie .git/hooks/pre-commit (sem extensao) chamando este script, por exemplo:
#   #!/bin/sh
#   powershell -NoProfile -File "$(dirname "$0")/../../scripts/guide-guardrail-pre-commit.ps1"

$staged = git diff --cached --name-only
$protegidos = @('CLAUDE.md', 'appsettings.Production.json')
$bloqueado = $false

foreach ($arquivo in $staged) {
    foreach ($padrao in $protegidos) {
        if ($arquivo -like "*$padrao*") {
            Write-Host "Commit bloqueado: '$arquivo' e um arquivo protegido ($padrao)."
            $bloqueado = $true
        }
    }
}

if ($bloqueado) {
    exit 1
}

exit 0
