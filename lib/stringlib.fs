namespace sultan

open sultan

open System

module stringlib =
    let rec genstring (startstring : string) (cha : string) (size : int) : string =
        let newstr = startstring + cha
        if (newstr.Length) >= size then
            newstr
            else genstring newstr cha size
        ""
