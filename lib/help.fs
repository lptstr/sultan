namespace sultan

open sultan
open sultan.core

open System

module help =
    let print () =
        let year = System.Int32.Parse(System.DateTime.Now.ToString("yyyy"))
        let vers : int array = core.getversion // int array, we need to convert it to a string
        let mutable version : string = ("v" + ((vers.[0]).ToString()))
        version <- (version + "." + ((vers.[1]).ToString()))
        version <- (version + "." + ((vers.[2]).ToString()))
        printfn "sultan %s" version
        printfn "MIT (c) %i LPTSTR members" year
        printf "\n"
        printfn "Usage: .\sultan slowloris -d [host] -p [port] -c [connections] -t [timeout] -s [useSSL? (true|false)]"
        printfn "Usage: .\sultan xerxes -d [host] -p [port] -c [connections] -t [threads]"
        printfn "Usage: .\sultan deathping -d [host]"
        printf "\n"
        ()
