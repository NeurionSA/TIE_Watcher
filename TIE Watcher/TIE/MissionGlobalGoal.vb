Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Missioan Global Goal struct.
    ''' </summary>
    Public Class MissionGlobalGoal
        Implements ICloneable

#Region " --- Constants --- "

        Private Const SIZE_OF = 28

#End Region
#Region " --- Variables --- "

        ' internal copies of fields from the source struct
        Private _triggers() As MissionTrigger
        ' field_8
        ' field_C
        ' field_10
        ' field_14
        ' field_18
        Private _triggerBoolean As Byte
        ' field_1A
        ' field_1B

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        Public Sub New()
            ' create arrays of the proper size for field members
            Me._triggers = Array.CreateInstance(GetType(MissionTrigger), 2)
            For i As Integer = 0 To 1
                Me._triggers(i) = New MissionTrigger
            Next
        End Sub

#End Region
#Region " Interface Support "

        ''' <summary>
        ''' Creates a new object that is a deep copy of the current instance.
        ''' </summary>
        ''' <returns></returns>
        Public Function Clone() As Object Implements ICloneable.Clone
            Dim ret As MissionGlobalGoal = DirectCast(Me.MemberwiseClone, MissionGlobalGoal)

            ' since this class has contains object references, we need to make deep copies of those, too
            ret._triggers = Array.CreateInstance(GetType(MissionTrigger), 2)
            For i As Integer = 0 To 1
                ret._triggers(i) = Me._triggers(i).Clone
            Next i
            ' that should do it
            Return ret
        End Function

#End Region

        ' Updates all of the instances fields using the byte array
        Private Sub updateFromBytes(bytes() As Byte)
            ' read all the fields
            For i As Integer = 0 To 1
                Dim subBytes(3) As Byte
                Array.Copy(bytes, 0 + i * 4, subBytes, 0, 4)
                Me._triggers(i).UpdateBytes(subBytes)
            Next i
            Me._triggerBoolean = bytes(&H19)
        End Sub

        ''' <summary>
        ''' Creates a new instance of the class using the provided array of bytes.
        ''' The array must be exactly 28 bytes long, otherwise an exception will be thrown.
        ''' </summary>
        ''' <param name="bytes">The array of bytes to build the structure from.</param>
        Public Shared Function FromBytes(bytes() As Byte) As MissionGlobalGoal
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            ' create the instance to return
            Dim ret As New MissionGlobalGoal
            ret.updateFromBytes(bytes)
            Return ret
        End Function

        ''' <summary>
        ''' Updates all of the fields in the class using the provided array of bytes.
        ''' The array must be exactly 28 bytes long, otherwise an exception will be thrown.
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

        Public ReadOnly Property Trigger(index As Integer) As MissionTrigger
            Get
                If index < 0 Or index >= 2 Then Throw New ArgumentOutOfRangeException("index")

                Return Me._triggers(index)
            End Get
        End Property

        Public ReadOnly Property TriggerBoolean As Byte
            Get
                Return Me._triggerBoolean
            End Get
        End Property

#End Region

    End Class

End Namespace