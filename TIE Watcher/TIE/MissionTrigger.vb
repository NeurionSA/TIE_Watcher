Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Mission Trigger struct
    ''' </summary>
    Public Class MissionTrigger
        Implements ICloneable

#Region " --- Constants --- "

        Private Const SIZE_OF = 4

#End Region
#Region " --- Variables --- "

        ' internal copies of fields from the source struct
        Private _condition As Byte
        Private _variableType As Byte
        Private _variable As Byte
        Private _triggerAmount As Byte

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        Public Sub New()
            ' nothing special
        End Sub

#End Region
#Region " ICloneable Support "

        ''' <summary>
        ''' Creates a new object that is a deep copy of the current instance.
        ''' </summary>
        ''' <returns></returns>
        Public Function Clone() As Object Implements ICloneable.Clone
            Dim ret As MissionTrigger = DirectCast(Me.MemberwiseClone, MissionTrigger)

            ' since this class has only basic types as members, a shallow memberwise clone is sufficient
            Return ret
        End Function

#End Region

        Private Sub updateFromBytes(bytes() As Byte)
            ' set its fields
            Me._condition = bytes(0)
            Me._variableType = bytes(1)
            Me._variable = bytes(2)
            Me._triggerAmount = bytes(3)
        End Sub

        ''' <summary>
        ''' Creates a new instance of the class using the provided array of bytes.
        ''' The array must be exactly 4 bytes long, otherwise an exception will be thrown.
        ''' </summary>
        ''' <param name="bytes">The array of bytes to build the structure from.</param>
        Public Shared Function FromBytes(bytes() As Byte) As MissionTrigger
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            ' create the new instance
            Dim ret As New MissionTrigger
            ret.updateFromBytes(bytes)
            ' return it
            Return ret
        End Function

        ''' <summary>
        ''' Updates all the fields in the class using the provided array of bytes.
        ''' The array must be exactly 4 bytes long, otherwise an exception will be thrown.
        ''' </summary>
        ''' <param name="bytes">The array of bytes to update the structure from.</param>
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

        Public ReadOnly Property Condition As MissionTriggerCondition
            Get
                Return Me._condition
            End Get
        End Property

        Public ReadOnly Property VariableType As MissionVariableType
            Get
                Return Me._variableType
            End Get
        End Property

        Public ReadOnly Property Variable As Byte
            Get
                Return Me._variable
            End Get
        End Property

        Public ReadOnly Property TriggerAmount As MissionTriggerAmount
            Get
                Return Me._triggerAmount
            End Get
        End Property

#End Region

    End Class

End Namespace