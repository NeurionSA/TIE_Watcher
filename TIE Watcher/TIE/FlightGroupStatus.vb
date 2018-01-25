Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Flight Group Status struct.
    ''' </summary>
    Public Class FlightGroupStatus
        Implements ICloneable

#Region " --- Constants --- "

        Private Const SIZE_OF = 48

#End Region
#Region " --- Variables -- "

        ' internal copies of fields from the source struct
        Private _field_0 As Byte
        Private _field_1 As Byte
        Private _arrivalTimer As UInt16
        Private _field_4 As Byte
        Private _field_5 As Byte
        Private _field_6 As UInt16
        Private _totalCraft As Byte
        Private _numCreated As Byte
        Private _numDestroyed As Byte
        Private _field_B As Byte
        Private _field_C As Byte
        Private _numAttackedComplete As Byte
        Private _numAttackedFailed As Byte
        Private _numCapturedComplete As Byte
        Private _numCapturedFailed As Byte
        Private _numInspectedComplete As Byte
        Private _numInspectedFailed As Byte
        Private _numBoardedComplete As Byte
        Private _numBoardedFailed As Byte
        Private _numDockedComplete As Byte
        Private _numDockedFailed As Byte
        Private _numDisabledComplete As Byte
        Private _numDisabledFailed As Byte
        Private _field_19 As Byte
        Private _totalSpecialCraft As Byte
        Private _numSpecialCreated As Byte
        Private _numSpecialDestroyed As Byte
        Private _field_1D As Byte
        Private _field_1E As Byte
        Private _numSpecialAttackedComplete As Byte
        Private _numSpecialAttackedFailed As Byte
        Private _numSpecialCapturedComplete As Byte
        Private _numSpecialCapturedFailed As Byte
        Private _numSpecialInspectedComplete As Byte
        Private _numSpecialInspectedFailed As Byte
        Private _numSpecialBoardedComplete As Byte
        Private _numSpecialBoardedFailed As Byte
        Private _numSpecialDockedComplete As Byte
        Private _numSpecialDockedFailed As Byte
        Private _numSpecialDisabledComplete As Byte
        Private _numSpecialDisabledFailed As Byte
        Private _field_2B As Byte
        Private _primaryGoalState As GoalState
        Private _secondaryGoalState As GoalState
        Private _bonusGoalState As GoalState
        Private _field_2F As Byte

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        Public Sub New()
            ' nothing special to do
        End Sub

#End Region
#Region " Interface Support "

        ''' <summary>
        ''' Creates a new object that is a deep copy of the current instance.
        ''' </summary>
        ''' <returns></returns>
        Public Function Clone() As Object Implements ICloneable.Clone
            Dim ret As FlightGroupStatus = DirectCast(Me.MemberwiseClone, FlightGroupStatus)

            ' since this class has only basic types as members, a shallow memberwise clone is sufficient
            Return ret
        End Function

#End Region

        ' Updates all of the instance's fields using the byte array
        Private Sub updateFromBytes(bytes() As Byte)
            ' read all the fields
            Me._field_0 = bytes(&H0)
            Me._field_1 = bytes(&H1)
            Me._arrivalTimer = BitConverter.ToUInt16(bytes, &H2)
            Me._field_4 = bytes(&H4)
            Me._field_5 = bytes(&H5)
            Me._field_6 = BitConverter.ToUInt16(bytes, &H6)
            Me._totalCraft = bytes(&H8)
            Me._numCreated = bytes(&H9)
            Me._numDestroyed = bytes(&HA)
            Me._field_B = bytes(&HB)
            Me._field_C = bytes(&HC)
            Me._numAttackedComplete = bytes(&HD)
            Me._numAttackedFailed = bytes(&HE)
            Me._numCapturedComplete = bytes(&HF)
            Me._numCapturedFailed = bytes(&H10)
            Me._numInspectedComplete = bytes(&H11)
            Me._numInspectedFailed = bytes(&H12)
            Me._numBoardedComplete = bytes(&H13)
            Me._numBoardedFailed = bytes(&H14)
            Me._numDockedComplete = bytes(&H15)
            Me._numDockedFailed = bytes(&H16)
            Me._numDisabledComplete = bytes(&H17)
            Me._numDisabledFailed = bytes(&H18)
            Me._field_19 = bytes(&H19)
            Me._totalSpecialCraft = bytes(&H1A)
            Me._numSpecialCreated = bytes(&H1B)
            Me._numSpecialDestroyed = bytes(&H1C)
            Me._field_1D = bytes(&H1D)
            Me._field_1E = bytes(&H1E)
            Me._numSpecialAttackedComplete = bytes(&H1F)
            Me._numSpecialAttackedFailed = bytes(&H20)
            Me._numSpecialCapturedComplete = bytes(&H21)
            Me._numSpecialCapturedFailed = bytes(&H22)
            Me._numSpecialInspectedComplete = bytes(&H23)
            Me._numSpecialInspectedFailed = bytes(&H24)
            Me._numSpecialBoardedComplete = bytes(&H25)
            Me._numSpecialBoardedFailed = bytes(&H26)
            Me._numSpecialDockedComplete = bytes(&H27)
            Me._numSpecialDockedFailed = bytes(&H28)
            Me._numSpecialDisabledComplete = bytes(&H29)
            Me._numSpecialDisabledFailed = bytes(&H2A)
            Me._field_2B = bytes(&H2B)
            Me._primaryGoalState = bytes(&H2C)
            Me._secondaryGoalState = bytes(&H2D)
            Me._bonusGoalState = bytes(&H2E)
            Me._field_2F = bytes(&H2F)
        End Sub

        ''' <summary>
        ''' Creates a new instance of the class using the provided array of bytes.
        ''' The array must be exactly 48 bytes long, otherwise an exception will be thrown.
        ''' </summary>
        ''' <param name="bytes">The array of bytes to build the structure from.</param>
        Public Shared Function FromBytes(bytes() As Byte) As FlightGroupStatus
            ' throw an exception if the arguments are incorrect
            If bytes Is Nothing Then Throw New ArgumentNullException("bytes")
            If bytes.Count <> SIZE_OF Then Throw New ArgumentException("Array length is invalid.", "bytes")

            ' create the instance to return
            Dim ret As New FlightGroupStatus
            ret.updateFromBytes(bytes)
            Return ret
        End Function

        ''' <summary>
        ''' Updates all of the fields in the class using the provided array of bytes.
        ''' The array must be exactly 48 bytes long, otherwise an exception will be thrown.
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

        Public ReadOnly Property Field_0 As Byte
            Get
                Return Me._field_0
            End Get
        End Property

        Public ReadOnly Property Field_1 As Byte
            Get
                Return Me._field_1
            End Get
        End Property

        Public ReadOnly Property Field_2 As UInt16
            Get
                Return Me._arrivalTimer
            End Get
        End Property

        Public ReadOnly Property ArrivalTimer As Byte
            Get
                Return Me._field_4
            End Get
        End Property

        Public ReadOnly Property Field_5 As Byte
            Get
                Return Me._field_5
            End Get
        End Property

        Public ReadOnly Property Field_6 As UInt16
            Get
                Return Me._field_6
            End Get
        End Property

        Public ReadOnly Property TotalCraft As Byte
            Get
                Return Me._totalCraft
            End Get
        End Property

        Public ReadOnly Property TotalCreated As Byte
            Get
                Return Me._numCreated
            End Get
        End Property

        Public ReadOnly Property TotalDestroyed As Byte
            Get
                Return Me._numDestroyed
            End Get
        End Property

        Public ReadOnly Property Field_B As Byte
            Get
                Return Me._field_B
            End Get
        End Property

        Public ReadOnly Property Field_C As Byte
            Get
                Return Me._field_C
            End Get
        End Property

        Public ReadOnly Property TotalAttackedComplete As Byte
            Get
                Return Me._numAttackedComplete
            End Get
        End Property

        Public ReadOnly Property TotalAttackedFailed As Byte
            Get
                Return Me._numAttackedFailed
            End Get
        End Property

        Public ReadOnly Property TotalCapturedComplete As Byte
            Get
                Return Me._numCapturedComplete
            End Get
        End Property

        Public ReadOnly Property TotalCapturedFailed As Byte
            Get
                Return Me._numCapturedFailed
            End Get
        End Property

        Public ReadOnly Property TotalInspectedComplete As Byte
            Get
                Return Me._numInspectedComplete
            End Get
        End Property

        Public ReadOnly Property TotalInspectedFailed As Byte
            Get
                Return Me._numInspectedFailed
            End Get
        End Property

        Public ReadOnly Property TotalBoardedComplete As Byte
            Get
                Return Me._numBoardedComplete
            End Get
        End Property

        Public ReadOnly Property TotalBoardedFailed As Byte
            Get
                Return Me._numBoardedFailed
            End Get
        End Property

        Public ReadOnly Property TotalDockedComplete As Byte
            Get
                Return Me._numDockedComplete
            End Get
        End Property

        Public ReadOnly Property TotalDockedFailed As Byte
            Get
                Return Me._numDockedFailed
            End Get
        End Property

        Public ReadOnly Property TotalDisabledComplete As Byte
            Get
                Return Me._numDisabledComplete
            End Get
        End Property

        Public ReadOnly Property TotalDisabledFailed As Byte
            Get
                Return Me._numDisabledFailed
            End Get
        End Property

        Public ReadOnly Property Field_19 As Byte
            Get
                Return Me._field_19
            End Get
        End Property

        Public ReadOnly Property TotalSpecialCraft As Byte
            Get
                Return Me._totalSpecialCraft
            End Get
        End Property

        Public ReadOnly Property TotalSpecialCreated As Byte
            Get
                Return Me._numSpecialCreated
            End Get
        End Property

        Public ReadOnly Property TotalSpecialDestroyed As Byte
            Get
                Return Me._numSpecialDestroyed
            End Get
        End Property

        Public ReadOnly Property Field_1D As Byte
            Get
                Return Me._field_1D
            End Get
        End Property

        Public ReadOnly Property Field_1E As Byte
            Get
                Return Me._field_1E
            End Get
        End Property

        Public ReadOnly Property TotalSpecialAttackedComplete As Byte
            Get
                Return Me._numSpecialAttackedComplete
            End Get
        End Property

        Public ReadOnly Property TotalSpecialAttackedFailed As Byte
            Get
                Return Me._numSpecialAttackedFailed
            End Get
        End Property

        Public ReadOnly Property TotalSpecialCapturedComplete As Byte
            Get
                Return Me._numSpecialCapturedComplete
            End Get
        End Property

        Public ReadOnly Property TotalSpecialCapturedFailed As Byte
            Get
                Return Me._numSpecialCapturedFailed
            End Get
        End Property

        Public ReadOnly Property TotalSpecialInspectedComplete As Byte
            Get
                Return Me._numSpecialInspectedComplete
            End Get
        End Property

        Public ReadOnly Property TotalSpecialInspectedFailed As Byte
            Get
                Return Me._numSpecialInspectedFailed
            End Get
        End Property

        Public ReadOnly Property TotalSpecialBoardedComplete As Byte
            Get
                Return Me._numSpecialBoardedComplete
            End Get
        End Property

        Public ReadOnly Property TotalSpecialBoardedFailed As Byte
            Get
                Return Me._numSpecialBoardedFailed
            End Get
        End Property

        Public ReadOnly Property TotalSpecialDockedComplete As Byte
            Get
                Return Me._numSpecialDockedComplete
            End Get
        End Property

        Public ReadOnly Property TotalSpecialDockedFailed As Byte
            Get
                Return Me._numSpecialDockedFailed
            End Get
        End Property

        Public ReadOnly Property TotalSpecialDisabledComplete As Byte
            Get
                Return Me._numSpecialDisabledComplete
            End Get
        End Property

        Public ReadOnly Property TotalSpecialDisabledFailed As Byte
            Get
                Return Me._numSpecialDisabledFailed
            End Get
        End Property

        Public ReadOnly Property Field_2B As Byte
            Get
                Return Me._field_2B
            End Get
        End Property

        Public ReadOnly Property PrimaryGoalState As GoalState
            Get
                Return Me._primaryGoalState
            End Get
        End Property

        Public ReadOnly Property SecondaryGoalState As GoalState
            Get
                Return Me._secondaryGoalState
            End Get
        End Property

        Public ReadOnly Property BonusGoalState As GoalState
            Get
                Return Me._bonusGoalState
            End Get
        End Property

        Public ReadOnly Property Field_2F As Byte
            Get
                Return Me._field_2F
            End Get
        End Property

#End Region

    End Class

End Namespace