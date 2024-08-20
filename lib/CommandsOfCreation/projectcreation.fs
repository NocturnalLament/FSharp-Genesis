namespace CommandsOfCreation
open Spectre.Console
open System.IO
open System.Xml.Linq
open System.Linq

module ConsoleCreation =

    let createConsoleProjectString(projectName: string option, framework: string option, template: string option) =
        let projectName = 
            match projectName with
            | Some name when name <> "" -> name
            | _ -> Path.GetFileName(Directory.GetCurrentDirectory())
        let framework = defaultArg framework "net8.0"
        let template = defaultArg template "console"
        let argsString = sprintf "new %s --language F# -n %s -f %s --output ." template projectName framework
        (projectName,"dotnet", argsString)
    let addLibrariesFrameworks() =
        let selection = new MultiSelectionPrompt<string>()
        selection.Title <- "[green]If you would like to install any additional frameworks or libraries, select them below.[/]"
        selection.Required <- false
        selection.AddChoices([|"Spectre.Console"; "Argu"; "Newtonsoft.Json"|]) |> ignore
        let selectedItems = AnsiConsole.Prompt selection
        let commands = selectedItems |> Seq.toList |> List.map(fun item ->
            match item with
            | "Spectre.Console" -> ("dotnet", "add package Spectre.Console")
            | "Argu" -> ("dotnet", "add package Argu")
            | "Newtonsoft.Json" -> "dotnet", "add package Newtonsoft.Json"
            | _ -> ("", ""))
        commands

    let editFSProjPath() =
        let currentDir = Directory.GetCurrentDirectory()
        let fsproj = Directory.GetFiles(currentDir, "*.fsproj")
        match fsproj.Length with
        | 0 ->
            printfn "No files found."
            ""
        | 1 ->
            fsproj.[0]
        | _ ->
            ""

    let editXML(path: string) =
        let doc = XDocument.Load(path)
        let propGroup = doc.Descendants(XName.Get("PropertyGroup")).FirstOrDefault()
        if propGroup <> null then
            propGroup.Add(XElement(XName.Get("PublishSingleFile"), "true"))
            propGroup.Add(XElement(XName.Get("SelfContained"), "true"))
            propGroup.Add(XElement(XName.Get("RuntimeIdentifier"), "win-x64"))
        doc.Save(path)

    let edit() =
        let path = editFSProjPath()
        editXML path
            

        

    

        
    


        
