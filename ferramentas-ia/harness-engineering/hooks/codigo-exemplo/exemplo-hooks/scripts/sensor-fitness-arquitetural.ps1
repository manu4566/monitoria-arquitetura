# Hook PostToolUse — sensor conectado.
# Roda a fitness function de arquitetura (sensores/codigo-exemplo/exemplo-sensores/) apos
# cada edicao em arquivo .cs, e devolve a falha ao agente para autocorrecao.
# Assume que o hook roda com cwd = raiz do projeto (onde .claude/settings.json vive) —
# e o padrao quando o Claude Code executa hooks.

$inputJson = [Console]::In.ReadToEnd() | ConvertFrom-Json
$filePath = $inputJson.tool_input.file_path

if ($filePath -notlike "*.cs") {
    exit 0
}

$saida = dotnet test --filter "FullyQualifiedName~ArchitectureFitnessTests" 2>&1
$codigoSaida = $LASTEXITCODE

if ($codigoSaida -ne 0) {
    [Console]::Error.WriteLine("Sensor de arquitetura falhou apos a edicao:`n$saida")
    exit 2
}

exit 0
