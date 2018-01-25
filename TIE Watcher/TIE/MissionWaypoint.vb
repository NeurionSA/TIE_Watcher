Namespace TIE

    ''' <summary>
    ''' Class representation of a TIE Fighter Mission Waypoint struct, but modified to make more sense.
    ''' </summary>
    Public Class MissionWaypoint
        Implements ICloneable

#Region " --- Variables --- "

        ' internal copies of fields from the source struct
        Private _x As Int16
        Private _y As Int16
        Private _z As Int16
        Private _valid As Int16

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        Public Sub New(_x As Int16, _y As Int16, _z As Int16, _valid As Int16)
            Me._x = _x
            Me._y = _y
            Me._z = _z
            Me._valid = _valid
        End Sub

#End Region
#Region " Interface Support "

        ''' <summary>
        ''' Creates a new object that is a deep copy of the current instance.
        ''' </summary>
        ''' <returns></returns>
        Public Function Clone() As Object Implements ICloneable.Clone
            Dim ret As MissionWaypoint = DirectCast(Me.MemberwiseClone, MissionWaypoint)

            ' since this class has only basic types as members, a shallow memberwise clone is sufficient
            Return ret
        End Function

#End Region

#End Region
#Region " --- Properties ---- "

        Public Property X As Int16
            Get
                Return Me._x
            End Get
            Set(value As Int16)
                Me._x = value
            End Set
        End Property

        Public Property Y As Int16
            Get
                Return Me._y
            End Get
            Set(value As Int16)
                Me._y = value
            End Set
        End Property

        Public Property Z As Int16
            Get
                Return Me._z
            End Get
            Set(value As Int16)
                Me._z = value
            End Set
        End Property

        Public Property Valid As Int16
            Get
                Return Me._valid
            End Get
            Set(value As Int16)
                Me._valid = value
            End Set
        End Property

#End Region

    End Class

End Namespace