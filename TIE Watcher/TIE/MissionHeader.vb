Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Mission File Header struct.
    ''' </summary>
    Public Class MissionHeader
        Implements ICloneable

#Region " --- Constants --- "

        Private Const SIZE_OF = 458

#End Region
#Region " --- Variables --- "

        ' internal copies of fields from the source struct
        Private _platformID As Int16
        Private _numFlightGroups As UInt16
        Private _numMessages As UInt16
        Private _field_8 As Byte
        Private _field_9 As Byte
        Private _briefingOfficers As Byte
        Private _capOnEject As Byte
        Private _field_C As Int32
        Private _field_10 As Int32
        Private _field_14 As Int32
        Private _endOfMissionMessage() As String
        Private _field_198 As Int16
        Private _iffName() As String

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        Public Sub New()
            ' create arrays of the proper size for field members
            Me._endOfMissionMessage = Array.CreateInstance(GetType(String), 6)
            For i As Integer = 0 To 5
                Me._endOfMissionMessage(i) = ""
            Next
            Me._iffName = Array.CreateInstance(GetType(String), 4)
            For i As Integer = 0 To 3
                Me._iffName(i) = ""
            Next
        End Sub

#End Region
#Region " Interface Support "

        ''' <summary>
        ''' Creates a new object that is a deep copy of the current instance.
        ''' </summary>
        ''' <returns></returns>
        Public Function Clone() As Object Implements ICloneable.Clone
            Dim ret As MissionHeader = DirectCast(Me.MemberwiseClone, MissionHeader)

            ' since this class has contains object references, we need to make deep copies of those, too
            ret._endOfMissionMessage = Array.CreateInstance(GetType(String), 6)
            For i As Integer = 0 To 5
                ret._endOfMissionMessage(i) = String.Format("{0}", Me._endOfMissionMessage(i))
            Next '
            ret._iffName = Array.CreateInstance(GetType(String), 4)
            For i As Integer = 0 To 3
                ret._iffName(i) = String.Format("{0}", Me._iffName(i))
            Next
            ' that should do it
            Return ret
        End Function

#End Region

        ' Updates all of the instances fields using the byte array
        Private Sub updateFromBytes(bytes() As Byte)
            ' read all the fields
            Me._platformID = BitConverter.ToInt16(bytes, 0)
            Me._numFlightGroups = BitConverter.ToUInt16(bytes, 2)
            Me._numMessages = BitConverter.ToUInt16(bytes, 4)
            Me._field_8 = bytes(8)
            Me._field_9 = bytes(9)
            Me._briefingOfficers = bytes(&HA)
            Me._capOnEject = bytes(&HB)
            Me._field_C = BitConverter.ToInt32(bytes, &HC)
            Me._field_10 = BitConverter.ToInt32(bytes, &H10)
            Me._field_14 = BitConverter.ToInt32(bytes, &H14)
            ' read the end of mission messages
            For i As Integer = 0 To 5
                Me._endOfMissionMessage(i) = StringsEx.TrimNull(Text.Encoding.ASCII.GetString(bytes, &H18 + 64 * i, 64))
            Next 'i
            Me._field_198 = BitConverter.ToInt16(bytes, &H1C8)
            ' read the IFF names
            For i As Integer = 0 To 3
                Me._iffName(i) = StringsEx.TrimNull(Text.Encoding.ASCII.GetString(bytes, &H19A + 12 * i, 12))
            Next 'i

        End Sub

        ''' <summary>
        ''' Creates a new instance of the class using the provided array of bytes.
        ''' The array must be exactly 458 bytes long, otherwise an exception will be thrown.
        ''' </summary>
        ''' <param name="bytes">The array of bytes to build the structure from.</param>
        Public Shared Function FromBytes(bytes() As Byte) As MissionHeader
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            ' create the instance to return
            Dim ret As New MissionHeader
            ret.updateFromBytes(bytes)
            Return ret
        End Function

        ''' <summary>
        ''' Updates all of the fields in the class using the provided array of bytes.
        ''' The array must be exactly 458 bytes long, otherwise an exception will be thrown.
        ''' </summary>
        ''' <param name="bytes">The array of bytes to update the structure with.</param>
        Public Sub UpdateBytes(bytes() As Byte)
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            Me.updateFromBytes(bytes)
        End Sub

#End Region
#Region " --- Properties --- "

        ''' <summary>
        ''' Gets size, in bytes, of the struct the class represents.
        ''' </summary>
        ''' <returns></returns>
        Public Shared ReadOnly Property SizeOf As Integer
            Get
                Return SIZE_OF
            End Get
        End Property

        ''' <summary>
        ''' Gets the Platform ID of the Mission.
        ''' </summary>
        ''' <returns>Value of the field.</returns>
        Public ReadOnly Property PlatformID As Int16
            Get
                Return Me._platformID
            End Get
        End Property

        ''' <summary>
        ''' Gets the number of Flight Groups in the Mission.
        ''' </summary>
        ''' <returns>Value of the field.</returns>
        Public ReadOnly Property NumFlightGroups As UInt16
            Get
                Return Me._numFlightGroups
            End Get
        End Property

        ''' <summary>
        ''' Gets the number of Messages in the Mission.
        ''' </summary>
        ''' <returns>Value of the field.</returns>
        Public ReadOnly Property NumMessages As UInt16
            Get
                Return Me._numMessages
            End Get
        End Property

        Public ReadOnly Property BriefingOfficers As Byte
            Get
                Return Me._briefingOfficers
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the player might be captured after ejecting from their craft.
        ''' </summary>
        ''' <returns>Value of the field.</returns>
        Public ReadOnly Property CaptureOnEject As Byte
            Get
                Return Me._capOnEject
            End Get
        End Property

        ''' <summary>
        ''' Gets an End of Mission Message string.
        ''' </summary>
        ''' <param name="index">Index of End of Mission Message string. 0 - 5.</param>
        ''' <returns></returns>
        Public ReadOnly Property EndOfMissionMessage(index As Integer) As String
            Get
                If index < 0 Or index > 5 Then Throw New ArgumentOutOfRangeException("index", "Index must be between 0 and 5, inclusive.")
                Return Me._endOfMissionMessage(index)
            End Get
        End Property

        Public ReadOnly Property IFFNames(index As Integer) As String
            Get
                If index < 0 Or index > 3 Then Throw New ArgumentOutOfRangeException("index", "Index must be between 0 and 3, inclusive.")
                Return Me._iffName(index)
            End Get
        End Property

#End Region

    End Class

End Namespace