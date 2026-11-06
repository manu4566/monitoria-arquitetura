# Hook PreToolUse — guide dinamico.
# Bloqueia edicoes em arquivos protegidos antes que elas aconteçam.
# Recebe o evento via stdin (JSON) e responde via exit code (0 = ok, 2 = bloqueado).

$inputJson = [Console]::In.ReadToEnd() | ConvertFrom-Json
$filePath = $inputJson.tool_input.file_path

$protegidos = @('CLAUDE.md', 'appsettings.Production.json')

foreach ($padrao in $protegidos) {
    if ($filePath -like "*$padrao*") {
        [Console]::Error.WriteLine("Bloqueado: '$filePath' e um arquivo protegido ($padrao). Edicoes aqui exigem revisao manual.")
        exit 2
    }
}

exit 0
