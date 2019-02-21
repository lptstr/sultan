#nowarn "20"
#nowarn "67"

open sultan
open sultan.help
open sultan.printxlib
open sultan.invocation

open System
open System.Linq
open System.Threading

module sultan =
    let E : string = string (char 0x1B)
    [<EntryPoint>]
    let main argv =
        consoletool.enableVTMode() |> ignore
        // go nuts if there aren't enough args'
        if argv.Length < 1 then
            // help.display
            errorfn "001 not enough arguments. Try 'sultan --help'."
            errorfn "001 fatal."
            exit 1
        else

        if argv.Contains("--help") || argv.Contains("-h") || argv.Contains("/?") || argv.Contains("help") then
            help.print()
            exit 1
            0
        elif argv.Contains("version") || argv.Contains("--version") || argv.Contains("-v") then
            let vers : int array = core.getversion // int array, we need to convert it to a string
            let mutable version : string = ("v" + ((vers.[0]).ToString()))
            version <- (version + "." + ((vers.[1]).ToString()))
            version <- (version + "." + ((vers.[2]).ToString()))
            printfn "sultan %s[38;2;110;140;253m%s%s[0m" E version E
            exit 1
            0
        else 0

        try
            if (argv.[0]).Equals("slowloris") then invoke_slowloris argv
            elif (argv.[0]).Equals("xerxes") then invoke_xerxes argv
            elif (argv.[0]).Equals("deathping") then invoke_pod argv else 0
        with
            | :? System.Exception as err ->
                let errtype = Array.last(((((err.GetType()).ToString()).ToUpper()).Split(".")))
                errorfn (String.Format("0x{0:X8} {2} {3} -> {1}", err.HResult, err.Message, err.TargetSite, errtype))
                0
        0
