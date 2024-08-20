// For more information see https://aka.ms/fsharp-console-apps

open System
open System.IO
open System.Diagnostics
open Runner
open Argu
open Skeleton
open CommandsOfCreation
open CreateMakefile
open CreateGitignore
let ParseArguments argv =
    let parser = ArgumentParser.Create<CLIArguments>()
    try
        let results = parser.ParseCommandLine argv
        match results.GetAllResults() with
        | [Skeleton.Create(template_type)] ->
            let name, dotnet, commandString = ConsoleCreation.createConsoleProjectString(None, None, Some "console")
            let output, error, exitCode = Runner.commandRunner(dotnet, commandString)
            printfn $"{commandString}"
            printfn $"{output}"
            printfn $"{name} created successfully"
            CreateMakefile.CreateMakefile()
            let outputList = ConsoleCreation.addLibrariesFrameworks()
            match outputList with
            | [("", "")] -> printfn "No extra frameworks/libraries chosen."
            | _ ->
                outputList |> List.iter(fun (name, command) -> 
                let out, err, exCo = Runner.commandRunner(name, command)
                match exCo with
                | 0 -> printfn $"{out}"
                | 1 -> printfn $"ERROR: {err}"
                | _ -> printfn $"{out}"
                )
            GitignoreCreation.createGitignoreFile()
            printfn "Editing .fsproj file..."
            ConsoleCreation.edit()
            printfn "Edited XML. Happy coding! :)"
            0
        | [] ->
            printfn "No args provided"
            1
        | _ ->
            printfn "Invalid Arguments provided"
            1
    with
    | :? ArguParseException as ex ->
        printfn "ERROR: %s" ex.Message
        0

[<EntryPoint>]
let main argv =
    let exitCode = ParseArguments argv
    exitCode