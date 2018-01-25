Imports System.Runtime.InteropServices

''' <summary>
''' Class for invoking native methods, as recommended by MSDN.
''' https://msdn.microsoft.com/en-us/library/ms182161(v=vs.80).aspx
''' </summary>
Friend NotInheritable Class NativeMethods

#Region " --- External Functions --- "

    ' This function opens a process and returns a process handle
    ' NOTE: It seems I don't need this, as I can use the handle available in the Process object
    ' Counter-note: I should probably use this anyways, just to be absolutely sure
    <DllImport("Kernel32.dll", SetLastError:=True, CallingConvention:=CallingConvention.Winapi)>
    Friend Shared Function OpenProcess(
                                      ByVal dwDesiredAccess As UInteger,
                                      <MarshalAs(UnmanagedType.Bool)> ByVal bInheritHandle As Boolean,
                                      ByVal dwProcessId As UInteger) _
                                      As IntPtr
        ' LEAVE EMPTY
    End Function

    ' This function reads (copies) memory from a process
    <DllImport("Kernel32.dll", SetLastError:=True, CallingConvention:=CallingConvention.Winapi)>
    Friend Shared Function ReadProcessMemory(
                                            <[In]> ByVal hProcess As IntPtr,
                                            <[In]> ByVal lpBaseAddress As IntPtr,
                                            <Out> ByVal lpBuffer As Byte(),
                                            <[In]> ByVal nSize As UInteger,
                                            <Out> ByRef lpNumberOfBytesRead As UInteger) _
                                            As <MarshalAs(UnmanagedType.Bool)> Boolean
        ' LEAVE EMPTY
    End Function

    ' This function writes memory to a process
    <DllImport("Kernel32.dll", SetLastError:=True, CallingConvention:=CallingConvention.Winapi)>
    Friend Shared Function WriteProcessMemory(
                                              ByVal hProcess As IntPtr,
                                              ByVal lpBaseAddress As IntPtr,
                                              lpBuffer As Byte(),
                                              ByVal nSize As UInt32,
                                              ByRef lpNumberOfBytesWritten As UInt32) _
                                            As <MarshalAs(UnmanagedType.Bool)> Boolean
        ' LEAVE EMPTY
    End Function

    ' This function closes a handle
    <DllImport("Kernel32.dll", SetLastError:=True, CallingConvention:=CallingConvention.Winapi)>
    Friend Shared Function CloseHandle(
                                      ByVal hObject As IntPtr) _
                                      As <MarshalAs(UnmanagedType.Bool)> Boolean
        ' LEAVE EMPTY
    End Function

    ' This function gets the last Win32 error


#End Region

End Class
