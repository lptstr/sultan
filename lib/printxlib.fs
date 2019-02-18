namespace sultan

open System
open sultan

module printxlib =
    let E : string = string (char 0x1B)
    let printx (text : string) (rgb : string) =
        let parsedrgb = (rgb.Split(","))
        if parsedrgb.Length < 3 then
            raise (System.ArgumentException("Invalid RGB color: " + rgb))
        else
            consoletool.enableVTMode() |> ignore
            let r = parsedrgb.[0]
            let g = parsedrgb.[1]
            let b = parsedrgb.[2]
            printf "%s[38;2;%s;%s;%sm%s%s[0m" E r g b text E

    let error (text : string) =
        consoletool.enableVTMode() |> ignore
        let message : string = "ERR! " + text
        eprintf "[sultan] %s[38;2;255;0;0m%s%s[0m" E message E

    let warning (text : string) =
        consoletool.enableVTMode() |> ignore
        let message : string = "WARN " + text
        printf "[sultan] %s[38;2;249;225;100m%s%s[0m" E message E

    let success (text : string) =
        consoletool.enableVTMode() |> ignore
        let message : string = text
        printf "[sultan] %s[38;2;24;220;10m%s%s[0m" E message E

    let verbose (text : string) =
        consoletool.enableVTMode() |> ignore
        let message : string = "VERB " + text
        printf "[sultan] %s[38;2;50;190;250m%s%s[0m" E message E
