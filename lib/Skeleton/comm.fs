namespace Skeleton

open Argu

type CLIArguments =
    | Create of template_type: string
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Create _ -> "Create the project with only the template parameter."
            