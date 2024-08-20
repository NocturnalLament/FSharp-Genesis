namespace CreateGitignore
open System
open System.IO
module GitignoreCreation =
    let gitignore = """
# Ignore build output directories
bin/
obj/

# Ignore user-specific files
*.user
*.rsuser
*.suo
*.userosscache
*.sln.docstates


# Ignore Visual Studio Code settings
.vscode/

# Ignore Paket dependencies
paket-files/
.paket/

# Ignore FAKE build artifacts
.fake/

# Ignore NuGet packages
*.nupkg
.nuget/

# Ignore build logs
*.log

# Ignore temporary files
*.tmp
*.temp

# Ignore compiled DLLs and PDBs
*.dll
*.pdb

# Ignore coverage reports
coverage/
*.coverage
*.coveragexml

# Ignore F# interactive history
.fsi/

# Ignore generated documentation
docs/_site/


notes/

# Ignore local configuration files
appsettings.local.json
"""

    let createGitignoreFile() =
        let exeDir = AppContext.BaseDirectory
        let ignorePath = Path.Combine(exeDir, ".gitignore")
        try
            File.WriteAllText(ignorePath, gitignore)
            printfn "Wrote gitignore!"
        with
        | :? UnauthorizedAccessException as ex ->
            printfn "Error: Unauthorized Access %s" ex.Message
        | :? IOException as ex ->
            printfn "Error: IO Exception: %s" ex.Message
        | ex ->
            printfn "Error: %s" ex.Message