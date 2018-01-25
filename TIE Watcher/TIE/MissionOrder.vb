Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Mission Order struct.
    ''' </summary>
    Public Class MissionOrder
        Implements ICloneable

#Region " --- Constants --- "

        Private Const SIZE_OF = 18

#End Region
#Region " --- Variables --- "

        ' internal copies of fields from source struct
        Private _order As MissionOrderType
        Private _throttle As Byte
        Private _var_1 As Byte
        Private _var_2 As Byte
        Private _field_4 As UInt16
        Private _target_3_type As MissionVariableType
        Private _target_4_type As MissionVariableType
        Private _target_3 As Byte
        Private _target_4 As Byte
        Private _targ_3_or_4 As Int16
        Private _target_1_type As MissionVariableType
        Private _target_1 As Byte
        Private _target_2_type As MissionVariableType
        Private _target_2 As Byte
        Private _targ_1_or_2 As Int16

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
            Dim ret As MissionOrder = DirectCast(Me.MemberwiseClone, MissionOrder)

            ' since this class has only basic types as members, a shallow memberwise clone is sufficient
            Return ret
        End Function

#End Region

        ' Updates all of the instances fields using the byte array
        Private Sub updateFromBytes(bytes() As Byte)
            ' read all the fields
            Me._order = bytes(0)
            Me._throttle = bytes(1)
            Me._var_1 = bytes(2)
            Me._var_2 = bytes(3)
            Me._field_4 = BitConverter.ToUInt16(bytes, &H4)
            Me._target_3_type = bytes(6)
            Me._target_4_type = bytes(7)
            Me._target_3 = bytes(8)
            Me._target_4 = bytes(9)
            Me._targ_3_or_4 = BitConverter.ToInt16(bytes, &H9)
            Me._target_1_type = bytes(&HC)
            Me._target_1 = bytes(&HD)
            Me._target_2_type = bytes(&HE)
            Me._target_2 = bytes(&HF)
            Me._targ_1_or_2 = BitConverter.ToInt16(bytes, &H10)
        End Sub

        ''' <summary>
        ''' Creates a new instance of the class using the provided array of bytes.
        ''' The array must be exactly 18 bytes long, otherwise an exception will be thrown.
        ''' </summary>
        ''' <param name="bytes">The array of bytes to build the structure from.</param>
        Public Shared Function FromBytes(bytes() As Byte) As MissionOrder
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            ' create the instance
            Dim ret As New MissionOrder
            ret.updateFromBytes(bytes)

            ' return the instance
            Return ret
        End Function

        ''' <summary>
        ''' Updates all the fields in the class using the provided array of bytes.
        ''' The array must be exactly 18 bytes long, otherwise an exception will be thrown.
        ''' </summary>
        ''' <param name="bytes">The array of bytes to update the structure from.</param>
        Public Sub UpdateBytes(bytes() As Byte)
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            updateFromBytes(bytes)
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

        Public ReadOnly Property Order As MissionOrderType
            Get
                Return Me._order
            End Get
        End Property

        Public ReadOnly Property Throttle As Byte
            Get
                Return Me._throttle
            End Get
        End Property

        Public ReadOnly Property Variable1 As Byte
            Get
                Return Me._var_1
            End Get
        End Property

        Public ReadOnly Property Variable2 As Byte
            Get
                Return Me._var_2
            End Get
        End Property

        Public ReadOnly Property field_4 As UInt16
            Get
                Return Me._field_4
            End Get
        End Property

        ''' <summary>
        ''' Gets the Target Variable Type for one of the Order's potential targets.
        ''' </summary>
        ''' <param name="index">Target index, 0 - 3.</param>
        ''' <returns>Value of the field.</returns>
        Public ReadOnly Property TargetType(index As Integer) As MissionVariableType
            Get
                If index < 0 Or index > 3 Then Throw New ArgumentOutOfRangeException("index", "Index must be between 0 and 3, inclusive.")
                Select Case index
                    Case 0
                        Return Me._target_1_type
                    Case 1
                        Return Me._target_2_type
                    Case 2
                        Return Me._target_3_type
                End Select
                Return Me._target_4_type
            End Get
        End Property

        ''' <summary>
        ''' Gets the Target for one of the Order's potential targets, the type of which is determined by the matching TargetType field.
        ''' </summary>
        ''' <param name="index">Target index, 0 - 3.</param>
        ''' <returns>Value of the field.</returns>
        Public ReadOnly Property Target(index As Integer) As Byte
            Get
                If index < 0 Or index > 3 Then Throw New ArgumentOutOfRangeException("index", "Index must be between 0 and 3, inclusive.")
                Select Case index
                    Case 0
                        Return Me._target_1
                    Case 1
                        Return Me._target_2
                    Case 2
                        Return Me._target_3
                End Select
                Return Me._target_4
            End Get
        End Property

        ''' <summary>
        ''' Gets the boolean operator applied to Target 1 and Target 2 checks.
        ''' </summary>
        ''' <returns>Non-zero for OR, zero for AND.</returns>
        Public ReadOnly Property Target1Or2 As Int16
            Get
                Return Me._targ_1_or_2
            End Get
        End Property

        ''' <summary>
        ''' Gets the boolean operator applied to Target 3 and Target 4 checks.
        ''' </summary>
        ''' <returns>Non-zero for OR, zero for AND.</returns>
        Public ReadOnly Property Target3or4 As Int16
            Get
                Return Me._targ_3_or_4
            End Get
        End Property

#End Region
    End Class

End Namespace