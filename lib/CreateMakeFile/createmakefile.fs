namespace CreateMakefile
open System
open System.IO

module CreateMakefile =
    let makefileString = """

    all: run

    run:
        dotnet run 

    build:
        dotnet build

    clean:
        dotnet clean

    release:
        dotnet publish

    """
    let CreateMakefile()=
        let exeDir = AppContext.BaseDirectory
        let filePath = Path.Combine(exeDir, "Makefile")
        try
            File.WriteAllText(filePath, makefileString)
            printfn "Wrote Makefile to %s" filePath
        with
        | :? UnauthorizedAccessException as ex ->
            printfn "Error: Unauthorized Access %s" ex.Message
        | :? IOException as ex ->
            printfn "Error: IO Exception: %s" ex.Message
        | ex ->
            printfn "Error: %s" ex.Message