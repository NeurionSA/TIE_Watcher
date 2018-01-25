Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Mission Flight Group Goal struct
    ''' </summary>
    Public Class MissionFlightGroupGoal
        Implements ICloneable

#Region " --- Constants --- "

        Private Const SIZE_OF = 2

#End Region
#Region " --- Variables --- "

        ' internal copies of fields from the source struct
        Private _condition As MissionTriggerCondition
        Private _amount As MissionGoalAmount

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        Public Sub New()
            ' nothing special
        End Sub

#End Region
#Region " Interface Support "

        ''' <summary>
        ''' Creates a new object that is a deep copy of the current instance.
        ''' </summary>
        ''' <returns></returns>
        Public Function Clone() As Object Implements ICloneable.Clone
            Dim ret As MissionFlightGroupGoal = DirectCast(Me.MemberwiseClone, MissionFlightGroupGoal)

            ' since this class has only basic types as members, a shallow memberwise clone is sufficient
            Return ret
        End Function

#End Region

        ' Updates all the instance's fields using the byte array
        Private Sub updateFromBytes(bytes() As Byte)
            Me._condition = bytes(0)
            Me._amount = bytes(1)
        End Sub

        ''' <summary>
        ''' Creates a new instance of the class using the provided array of bytes.
        ''' The array must be exactly 2 bytes long, otherwise an exception will be thrown.
        ''' </summary>
        ''' <param name="bytes">The array of bytes to build the structure from.</param>
        Public Shared Function FromBytes(bytes() As Byte) As MissionFlightGroupGoal
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            ' create the return instance
            Dim ret As New MissionFlightGroupGoal
            ret.updateFromBytes(bytes)
            Return ret
        End Function

        ''' <summary>
        ''' Updates all of the class' fields using the provided array of bytes.
        ''' The array must be exactly 2 bytes long, otherwise an exception will be thrown.
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

        Public ReadOnly Property Condition As MissionTriggerCondition
            Get
                Return Me._condition
            End Get
        End Property

        Public ReadOnly Property Amount As MissionGoalAmount
            Get
                Return Me._amount
            End Get
        End Property

#End Region
    End Class

End Namespace