#nowarn "9"

namespace sultan

open System
open System.Runtime.InteropServices
open Microsoft.FSharp.NativeInterop

module consoletool =
    let INVALID_HANDLE_VALUE = nativeint -1
    let STD_OUTPUT_HANDLE = -11
    let ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004
    let E : string = string (char 0x1B)

    [<DllImport("Kernel32")>]
    extern void* GetStdHandle(int nStdHandle)

    [<DllImport("Kernel32")>]
    extern bool GetConsoleMode(void* hConsoleHandle, int* lpMode)

    [<DllImport("Kernel32")>]
    extern bool SetConsoleMode(void* hConsoleHandle, int lpMode)

    let warn (text : string) =
        printfn "[sultan->contol] %s[38;2;249;225;100mWARN %s[38;2;255;255;255m%s%s[0m" E E text E

    let enableVTMode() =
        let handle = GetStdHandle(STD_OUTPUT_HANDLE)
        if handle <> INVALID_HANDLE_VALUE then
            let mode = NativePtr.stackalloc<int> 1
            if GetConsoleMode(handle, mode) then
                let value = NativePtr.read mode
                let value = value ||| ENABLE_VIRTUAL_TERMINAL_PROCESSING
                SetConsoleMode(handle, value)
            else
                warn "601 Unable to enable ANSI Escape Sequences, output may be garbled."
                false
        else
            warn "600 Unable to get STDHandler, recieved invalid handler. Output will be garbled."
            false
