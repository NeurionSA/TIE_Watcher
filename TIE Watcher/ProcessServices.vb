Imports System.ComponentModel

''' <summary>
''' A class that allows the manipulation of a process, such as memory reads and writes.
''' </summary>
Public Class ProcessServices
    Implements IDisposable

#Region " --- Constants --- "

    Const PROCESS_VM_OPERATION As UInteger = &H8
    Const PROCESS_VM_READ As UInteger = &H10
    Const PROCESS_VM_WRITE As UInteger = &H20
    Const PROCESS_QUERY_INFORMATION As UInteger = &H400

#End Region
#Region " --- Variables --- "

    ' the process for which services are offered
    Private WithEvents proc As Process = Nothing
    ' the opened handle for the process
    Private procHandle As IntPtr = IntPtr.Zero
    ' whether the process is currently available for use
    Private procAvailable As Boolean = False
    ' the access mode that was used for opening the process
    Private accessMode As ProcessAccess

#End Region
#Region " --- Methods --- "

#Region " Constructors "

    ''' <summary>
    ''' Creates a new instance of the ProcessMemoryReader class and attempts to obtain a handle for accessing the process.
    ''' If a handle cannot be obtained, an exception will be thrown.
    ''' </summary>
    ''' <param name="sourceProc">The Process that memory will be read from</param>
    Public Sub New(sourceProc As Process, access As ProcessAccess)
        Dim procAccessFlags As UInteger

        ' check the arguments for exceptipns
        If sourceProc Is Nothing Then
            Throw New ArgumentNullException("sourceProc")
        End If
        If sourceProc.HasExited Then
            Throw New ArgumentException("Cannot open a process that has already terminated.", "sourceProc")
        End If

        ' local copy of the source process
        proc = sourceProc
        ' local copy of the access mode
        accessMode = access

        ' generate the process access flags based on desired access
        procAccessFlags = PROCESS_QUERY_INFORMATION
        Select Case access
            Case ProcessAccess.Read
                procAccessFlags = procAccessFlags Or PROCESS_VM_READ
            Case ProcessAccess.ReadWrite
                procAccessFlags = procAccessFlags Or PROCESS_VM_READ Or PROCESS_VM_WRITE Or PROCESS_VM_OPERATION
            Case ProcessAccess.Write
                procAccessFlags = procAccessFlags Or PROCESS_VM_WRITE Or PROCESS_VM_OPERATION
            Case Else
                ' throw an exception
                Throw New InvalidEnumArgumentException("access", access, GetType(ProcessAccess))
        End Select

        ' attempt to open the process
        procHandle = NativeMethods.OpenProcess(procAccessFlags, True, proc.Id)
        ' if opening failed, throw an exception
        If Me.procHandle = IntPtr.Zero Then
            Throw New ApplicationException("Process could not be opened. The last error reported was: " & New Win32Exception().Message)
        End If

        ' open succeeded, let the process know we want to be informed when it exits
        proc.EnableRaisingEvents = True
        ' process is available
        procAvailable = True
    End Sub

#End Region
#Region " IDisposable Support "

    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' no managed stuff to dispose
            End If

            ' close the process handle if it's open
            If Not procHandle = IntPtr.Zero Then
                Try
                    NativeMethods.CloseHandle(procHandle)
                Catch ex As Exception
                    Debug.WriteLine("Error closing handle: " & ex.Message)
                End Try
            End If
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' uncomment the following line if Finalize() is overridden above.
        GC.SuppressFinalize(Me)
    End Sub

#End Region
#Region " Event-Handlers "

    ' handles when the source process exits
    Private Sub proc_Exited(sender As Object, e As EventArgs) Handles proc.Exited
        ' source process is no longer available
        procAvailable = False

        ' pass the event along to anyone who's listening
        RaiseEvent ProcessExited(Me, e)
    End Sub

#End Region

    ''' <summary>
    ''' Reads the specified number of bytes from the address in the process.
    ''' Returns Nothing if the read fails for any reason.
    ''' </summary>
    ''' <param name="address">Address in the process' memory to read from.</param>
    ''' <param name="length">Number of bytes to read.</param>
    ''' <returns></returns>
    Public Function ReadMemoryBytes(address As IntPtr, length As Integer) As Byte()
        ' check state
        If Not procAvailable Then
            Throw New InvalidOperationException("Memory cannot be read when a process is unavailable.")
        End If
        If Not (accessMode = ProcessAccess.Read Or accessMode = ProcessAccess.ReadWrite) Then
            Throw New InvalidOperationException("Memory cannot be read when a process is not opened for Read access.")
        End If
        ' check arguments
        If length <= 0 Then
            Throw New ArgumentException("Number of bytes to read must be greater than 0.", "length")
        End If

        Dim retBytes(length - 1) As Byte
        Dim res As Boolean = NativeMethods.ReadProcessMemory(procHandle, address, retBytes, length, 0)
        If res Then
            Return retBytes
        Else
            ' TODO: Handle whatever reason the read failed
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Reads a Byte from an address in the process.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <returns></returns>
    Public Function ReadMemoryByte(address As IntPtr) As Byte
        Dim bytes() As Byte = Me.ReadMemoryBytes(address, 1)

        ' handle read failures
        If bytes Is Nothing Then Return 0
        Return bytes(0)
    End Function

    ''' <summary>
    ''' Reads a UInt16 from an address in the process.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <returns></returns>
    Public Function ReadMemoryUInt16(address As IntPtr) As UInt16
        Dim bytes() As Byte = Me.ReadMemoryBytes(address, 2)

        ' handle read failures
        If bytes Is Nothing Then Return 0
        Return BitConverter.ToUInt16(bytes, 0)
    End Function

    ''' <summary>
    ''' Reads a Int16 from an address in the process.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <returns></returns>
    Public Function ReadMemoryInt16(address As IntPtr) As Int16
        Dim bytes() As Byte = Me.ReadMemoryBytes(address, 2)

        ' handle read failures
        If bytes Is Nothing Then Return 0
        Return BitConverter.ToInt16(bytes, 0)
    End Function

    ''' <summary>
    ''' Reads a UInt32 from an address in the process.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <returns></returns>
    Public Function ReadMemoryUInt32(address As IntPtr) As UInt32
        Dim bytes() As Byte = Me.ReadMemoryBytes(address, 4)

        ' handle read failures
        If bytes Is Nothing Then Return 0
        Return BitConverter.ToUInt32(bytes, 0)
    End Function

    ''' <summary>
    ''' Reads a Int32 from an address in the process.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <returns></returns>
    Public Function ReadMemoryInt32(address As IntPtr) As Int32
        Dim bytes() As Byte = Me.ReadMemoryBytes(address, 4)

        ' handle read failures
        If bytes Is Nothing Then Return 0
        Return BitConverter.ToInt32(bytes, 0)
    End Function

    ''' <summary>
    ''' Writes the specified number of bytes from the array to the process.
    ''' Returns the number of bytes written to the process.
    ''' </summary>
    ''' <param name="address">Address in the process' memory to write to.</param>
    ''' <param name="bytes">Array of bytes to write to the process.</param>
    ''' <returns></returns>
    Public Function WriteMemoryBytes(address As IntPtr, ByRef bytes() As Byte) As UInteger
        ' check state
        If Not procAvailable Then
            Throw New InvalidOperationException("Memory cannot be written when a process is unavailable.")
        End If
        If Not (accessMode = ProcessAccess.Write Or accessMode = ProcessAccess.ReadWrite) Then
            Throw New InvalidOperationException("Memory cannot be written when a process is not opened for Write access.")
        End If
        ' check arguments
        If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
        If bytes.Length <= 0 Then
            Throw New ArgumentException("Number of bytes to write must be greater than 0.")
        End If

        Dim bytesWritten As UInteger = 0
        ' invoke the native function
        Dim res As Boolean = NativeMethods.WriteProcessMemory(procHandle, address, bytes, bytes.Length, bytesWritten)
        If res Then
            Return bytesWritten
        Else
            ' TODO: Handle whatever reason the write failed
            'Console.WriteLine(New Win32Exception().Message)
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Writes a Byte to an address in the process.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <param name="value"></param>
    Public Sub WriteMemoryByte(address As IntPtr, value As Byte)
        Me.WriteMemoryBytes(address, {value})
    End Sub

    ''' <summary>
    ''' Writes a UInt16 to an address in the process.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <param name="value"></param>
    Public Sub WriteMemoryUInt16(address As IntPtr, value As UInt16)
        Me.WriteMemoryBytes(address, BitConverter.GetBytes(value))
    End Sub

    ''' <summary>
    ''' Writes a Int16 to an address in the process.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <param name="value"></param>
    Public Sub WriteMemoryInt16(address As IntPtr, value As Int16)
        Me.WriteMemoryBytes(address, BitConverter.GetBytes(value))
    End Sub

    ''' <summary>
    ''' Writes a UInt32 to an address in the process.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <param name="value"></param>
    Public Sub WriteMemoryUInt32(address As IntPtr, value As UInt32)
        Me.WriteMemoryBytes(address, BitConverter.GetBytes(value))
    End Sub

    ''' <summary>
    ''' Writes a Int32 to an address in the process.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <param name="value"></param>
    Public Sub WriteMemoryInt32(address As IntPtr, value As Int32)
        Me.WriteMemoryBytes(address, BitConverter.GetBytes(value))
    End Sub

    ''' <summary>
    ''' Closes access to the instance's process and frees the handle.
    ''' This does not close or terminate the underlying process.
    ''' </summary>
    Public Sub Close()
        ' close the handle
        Try
            NativeMethods.CloseHandle(procHandle)
        Catch ex As Exception
            Debug.WriteLine("Error closing handle: " & ex.Message)
        End Try
        ' zero the pointer
        procHandle = IntPtr.Zero
        proc = Nothing
        ' set the process available state to false
        procAvailable = False
    End Sub

#End Region
#Region " --- Events --- "

    ''' <summary>
    ''' An event that is raised when the underlying process the instance services has exited.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event ProcessExited(sender As Object, e As EventArgs)

#End Region
#Region " --- Properties --- "

    ''' <summary>
    ''' Gets whether the underlying process has exited.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasExited As Boolean
        Get
            If proc Is Nothing Then Throw New InvalidOperationException("Cannot query the state of an unavailable process.")
            Return proc.HasExited
        End Get
    End Property

#End Region

End Class

''' <summary>
''' Defines constants for read, write, or read/write access to a process.
''' </summary>
Public Enum ProcessAccess
    Read = 1
    Write = 2
    ReadWrite = 3
End Enum