''' <summary>
''' A class for manipulating a running instance of DOSBox, via process memory reads and/or writes.
''' </summary>
Public Class DosBoxServices
    Implements IDisposable

#Region " --- Constants --- "

    Private Const PROCESS_NOT_AVAILABLE_MESSAGE As String = "Host process has terminated or is unavailable."

#End Region
#Region " --- Structures --- "

    ' internal structure for Dosbox versions
    Private Structure DosBoxVersion

        ' exposed as friend so I can set their initial values easily
        Friend _procName As String           ' process name match string
        Friend _dosMemPointer As UInt64      ' address of pointer to DOS memory
        Friend _dosExeNameAddr As UInt64     ' address of name of EXE running in DOSBOX
        Friend _vramPointer As UInt64        ' address of pointer to emulated VRAM

        Public ReadOnly Property ProcessName As String
            Get
                Return _procName
            End Get
        End Property

        Public ReadOnly Property DosMemoryPointer As UInt64
            Get
                ' throw an exception if the value has not been defined for this version
                If _dosMemPointer = 0 Then Throw New InvalidOperationException("DOS Memory Pointer is not defined for this DOSBox version.")
                Return _dosMemPointer
            End Get
        End Property

        Public ReadOnly Property DosEXENameAddress As UInt64
            Get
                ' throw an exception if the value has not been defined for this version
                If _dosExeNameAddr = 0 Then Throw New InvalidOperationException("DOS EXE Name Address is not defined for this DOSBox version.")
                Return _dosExeNameAddr
            End Get
        End Property

        Public ReadOnly Property VRAMPointer As UInt64
            Get
                ' throw an exception if the value has not been defined for this version
                If _vramPointer = 0 Then Throw New InvalidOperationException("VRAM Pointer is not defined for this DOSBox version.")
                Return _vramPointer
            End Get
        End Property

    End Structure

#End Region
#Region " --- Variables --- "

    ' DOS Box versions
    Shared versions As DosBoxVersion()

    ' source process for DOSBox
    Private sourceProc As Process
    ' dosbox version of current process
    Private version As DosBoxVersion

    ' pointer to DOS memory within opened process
    Private p_DOSMem As UInt32

    ' process services for the DOSBox process
    Private WithEvents procServ As ProcessServices

    ' whether or not the source process can be reached
    Private procAvailable As Boolean = False

#End Region
#Region " --- Methods --- "

#Region " Constructors "

    ''' <summary>
    ''' Type constructor.
    ''' </summary>
    Shared Sub New()
        Dim i As Integer
        ' create and populate the array of DOSBox version information structs
        versions = Array.CreateInstance(GetType(DosBoxVersion), 2)

        ' set all their values to the initial defaults
        For i = 0 To versions.Count - 1
            With versions(i)
                ._procName = ""
                ._dosExeNameAddr = 0
                ._dosMemPointer = 0
                ._vramPointer = 0
            End With
        Next

        ' set the different versions' info
        i = 0
        With versions(i)
            ' DOSBox 0.74 Debugger Build
            ._procName = "dosbox-74-debug"
            ._dosExeNameAddr = &H1E26970
            ._dosMemPointer = &H1F87690
            ._vramPointer = &H1F527F4
        End With
        i += 1

        With versions(i)
            ' DOSBox 0.74 Standard Build
            ._procName = "DOSBox"
            ._dosExeNameAddr = &H1BD9480
            ._dosMemPointer = &H1D3A1A0
        End With
    End Sub


    ''' <summary>
    ''' Creates a new instance of this class, using the specified process as the source for memory reads.
    ''' </summary>
    ''' <param name="sourceProc">Process to use as the source</param>
    Public Sub New(sourceProc As Process)
        ' TODO: perform more in-depth checks to ensure this is a DOSBox process we're opening

        ' for now, just make sure the process name matches what we expect
        For i As Integer = 0 To versions.Count - 1
            If sourceProc.ProcessName.Equals(versions(i).ProcessName) Then
                ' match, break out
                version = versions(i)
                Me.sourceProc = sourceProc
                Exit For
            End If
        Next 'i

        ' TODO: Throw exception if process is not recognized as a valid DOSBox version

        ' create the Process Services object
        procServ = New ProcessServices(Me.sourceProc, ProcessAccess.ReadWrite)

        ' read the pointer to DOS memory
        p_DOSMem = procServ.ReadMemoryUInt32(version.DosMemoryPointer)

        procAvailable = True
    End Sub

#End Region
#Region " IDisposable Support "

    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                If Not procServ Is Nothing Then
                    procServ.Dispose()
                    procServ = Nothing
                End If
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
#Region " Event-Handlers "

    ' handles when the source process has exited
    Private Sub procServ_ProcessExited(sender As Object, e As EventArgs) Handles procServ.ProcessExited
        ' mark the process as unavailable
        procAvailable = False

        ' pass the event along
        RaiseEvent ProcessExited(Me, e)
    End Sub

#End Region

    ''' <summary>
    ''' Closes access to the instance's process and frees the handle.
    ''' This does not close or terminate the underlying process.
    ''' </summary>
    Public Sub Close()
        procAvailable = False

        ' close the services handle
        procServ.Close()
    End Sub

    ''' <summary>
    ''' Reads an array of bytes from an address within emulated DOS memory.
    ''' Returns Nothing if the read failed for any reason.
    ''' </summary>
    ''' <param name="address">Physical Address within DOS memory to read from.</param>
    ''' <param name="count">Number of bytes to read from DOS memory.</param>
    ''' <returns>An array of bytes read from emulated DOS memory.</returns>
    Public Function ReadDosMemoryBytes(address As Integer, count As Integer) As Byte()
        ' handle state exceptions
        If Not procAvailable Then Throw New InvalidOperationException(PROCESS_NOT_AVAILABLE_MESSAGE)
        ' handle argument exceptions
        If count <= 0 Then Throw New ArgumentException("Number of bytes to read must be greater than 0.", "count")

        Return procServ.ReadMemoryBytes(p_DOSMem + address, count)
    End Function

    ''' <summary>
    ''' Reads a Byte from an address within emulated DOS memory.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <returns></returns>
    Public Function ReadDosMemoryByte(address As Integer) As Byte
        ' handle state exceptions
        If Not procAvailable Then Throw New InvalidOperationException(PROCESS_NOT_AVAILABLE_MESSAGE)

        Return procServ.ReadMemoryByte(p_DOSMem + address)
    End Function

    ''' <summary>
    ''' Reads a UInt16 from an address within emulated DOS memory.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <returns></returns>
    Public Function ReadDosMemoryUInt16(address As Integer) As UInt16
        ' handle state exceptions
        If Not procAvailable Then Throw New InvalidOperationException(PROCESS_NOT_AVAILABLE_MESSAGE)

        Return procServ.ReadMemoryUInt16(p_DOSMem + address)
    End Function

    ''' <summary>
    ''' Reads a Int16 from an address within emulated DOS memory.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <returns></returns>
    Public Function ReadDosMemoryInt16(address As Integer) As Int16
        ' handle state exceptions
        If Not procAvailable Then Throw New InvalidOperationException(PROCESS_NOT_AVAILABLE_MESSAGE)

        Return procServ.ReadMemoryInt16(p_DOSMem + address)
    End Function

    ''' <summary>
    ''' Reads a UInt32 from an address within emulated DOS memory.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <returns></returns>
    Public Function ReadDosMemoryUInt32(address As Integer) As UInt32
        ' handle state exceptions
        If Not procAvailable Then Throw New InvalidOperationException(PROCESS_NOT_AVAILABLE_MESSAGE)

        Return procServ.ReadMemoryUInt32(p_DOSMem + address)
    End Function

    ''' <summary>
    ''' Reads a Int32 from an address within emulated DOS memory.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <returns></returns>
    Public Function ReadDosMemoryInt32(address As Integer) As Int32
        ' handle state exceptions
        If Not procAvailable Then Throw New InvalidOperationException(PROCESS_NOT_AVAILABLE_MESSAGE)

        Return procServ.ReadMemoryInt32(p_DOSMem + address)
    End Function

    ''' <summary>
    ''' Writes an array of bytes to an address within emulated DOS memory.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <param name="bytes"></param>
    ''' <returns></returns>
    Public Function WriteDosMemoryBytes(address As Integer, bytes() As Byte) As UInteger
        ' handle state exceptions
        If Not procAvailable Then Throw New InvalidOperationException(PROCESS_NOT_AVAILABLE_MESSAGE)
        ' handle argument exceptions
        If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
        If bytes.Length <= 0 Then Throw New ArgumentException("Number of bytes to read must be greater than 0.", "bytes")

        Return procServ.WriteMemoryBytes(p_DOSMem + address, bytes)
    End Function

    ''' <summary>
    ''' Writes a Byte to an address within emulated DOS memory.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <param name="value"></param>
    Public Sub WriteDosMemoryByte(address As Integer, value As Byte)
        ' handle state exceptions
        If Not procAvailable Then Throw New InvalidOperationException(PROCESS_NOT_AVAILABLE_MESSAGE)

        procServ.WriteMemoryByte(p_DOSMem, value)
    End Sub

    ''' <summary>
    ''' Writes a UInt16 to an address within emulated DOS memory.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <param name="value"></param>
    Public Sub WriteDosMemoryUInt16(address As Integer, value As UInt16)
        ' handle state exceptions
        If Not procAvailable Then Throw New InvalidOperationException(PROCESS_NOT_AVAILABLE_MESSAGE)

        procServ.WriteMemoryUInt16(p_DOSMem, value)
    End Sub

    ''' <summary>
    ''' Writes a Int16 to an address within emulated DOS memory.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <param name="value"></param>
    Public Sub WriteDosMemoryInt16(address As Integer, value As Int16)
        ' handle state exceptions
        If Not procAvailable Then Throw New InvalidOperationException(PROCESS_NOT_AVAILABLE_MESSAGE)

        procServ.WriteMemoryInt16(p_DOSMem, value)
    End Sub

    ''' <summary>
    ''' Writes a UInt32 to an address within emulated DOS memory.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <param name="value"></param>
    Public Sub WriteDosMemoryUInt32(address As Integer, value As UInt32)
        ' handle state exceptions
        If Not procAvailable Then Throw New InvalidOperationException(PROCESS_NOT_AVAILABLE_MESSAGE)

        procServ.WriteMemoryUInt32(p_DOSMem, value)
    End Sub

    ''' <summary>
    ''' Writes a Int16 to an address within emulated DOS memory.
    ''' </summary>
    ''' <param name="address"></param>
    ''' <param name="value"></param>
    Public Sub WriteDosMemoryInt32(address As Integer, value As Int32)
        ' handle state exceptions
        If Not procAvailable Then Throw New InvalidOperationException(PROCESS_NOT_AVAILABLE_MESSAGE)

        procServ.WriteMemoryInt32(p_DOSMem, value)
    End Sub

#End Region
#Region " --- Properties --- "

    ''' <summary>
    ''' Gets the name of the program currently running in the DOSBox instance.
    ''' </summary>
    ''' <returns>String represenation of the program name.</returns>
    Public ReadOnly Property CurrentProgramName As String
        Get
            Dim bytes As Byte() = procServ.ReadMemoryBytes(version.DosEXENameAddress, 8)
            Return Text.Encoding.ASCII.GetString(bytes, 0, Array.IndexOf(bytes, CByte(0)))
        End Get
    End Property

    '''' <summary>
    '''' Gets the offset of emulated DOS Memory in the DOSBox instance.
    '''' </summary>
    '''' <returns></returns>
    'Public ReadOnly Property DOSMemoryOffset As UInt32
    '    Get
    '        Return p_DOSMem
    '    End Get
    'End Property

    ''' <summary>
    ''' Gets the Process that was opened for use with this instance.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Process As Process
        Get
            Return sourceProc
        End Get
    End Property

#End Region
#Region " --- Events --- "

    Public Event ProcessExited(sender As Object, e As EventArgs)

#End Region

End Class
