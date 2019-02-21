namespace sultan

open sultan
open sultan.printxlib
open sultan.xerxeslib
open sultan.pinglib

open System
open System.Threading

module invocation =
    let E : string = string (char 0x1B)

    // ------------------------------------

    let invoke_slowloris (args : string[]) =
        let param : string[] = args.[1..]
        slowloris.attack param
        0

    let invoke_xerxes (args : string[]) =
        let param : string[] = args.[1..]
        xerxeslib.attackx (param)
        0

    let invoke_pod (args : string array) =
        let param : string[] = args.[1..]
        pinglib.attackp param
        0
