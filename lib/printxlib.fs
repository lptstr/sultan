namespace sultan

open System
open System.IO

open sultan

module printxlib =
    let E : string = string (char 0x1B)
    let erwrite : TextWriter = Console.Error
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

    let errorfn (text : string) =
        consoletool.enableVTMode() |> ignore
        erwrite.WriteLine "[sultan->mainth] {0}[38;2;255;0;0mERR! {0}[38;2;255;255;255m{1}{0}[0m" E text

    let warn (text : string) =
        consoletool.enableVTMode() |> ignore
        printfn "[sultan->mainth] %s[38;2;249;225;100mWARN %s[38;2;255;255;255m%s%s[0m" E E text E

    let success (text : string) =
        consoletool.enableVTMode() |> ignore
        printfn "[sultan->mainth] %s[38;2;24;220;10mYAY! %s[38;2;255;255;255m%s%s[0m" E E text E

    let verbose (text : string) =
        consoletool.enableVTMode() |> ignore
        Console.WriteLine "[sultan->mainth] {0}[38;2;50;190;250mVERB {0}[38;2;255;255;255m{1}{0}[0m" E text
