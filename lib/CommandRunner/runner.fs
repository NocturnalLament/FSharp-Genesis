namespace Runner
open System.Diagnostics

module Runner =
    let commandRunner (command: string, args: string) =
        let startInfo = ProcessStartInfo()
        startInfo.FileName <- command
        startInfo.Arguments <- args
        startInfo.RedirectStandardOutput <- true
        startInfo.RedirectStandardError <- true
        startInfo.UseShellExecute <- false
        startInfo.CreateNoWindow <- true
        use proc = new Process()
        proc.StartInfo <- startInfo
        proc.Start() |> ignore
        proc.WaitForExit()
        let output = proc.StandardOutput.ReadToEnd()
        let error = proc.StandardError.ReadToEnd()
        (output, error, proc.ExitCode)
