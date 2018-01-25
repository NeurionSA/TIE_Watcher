Namespace TIE

    ''' <summary>
    ''' Class representation of a 3D Point.
    ''' </summary>
    Public Class Point3D
        Implements ICloneable

#Region " --- Variables --- "

        ' internally it's floating point, because we want to be able to apply transformations to vertices
        Private _x As Single
        Private _y As Single
        Private _z As Single

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        ''' <summary>
        ''' Creates a new instance of this class using signed 16-bit integers.
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="y"></param>
        ''' <param name="z"></param>
        Public Sub New(x As Int16, y As Int16, z As Int16)
            _x = x
            _y = y
            _z = z
        End Sub

        ''' <summary>
        ''' Creates a new instance of this class using single-precision floating points.
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="y"></param>
        ''' <param name="z"></param>
        Public Sub New(x As Single, y As Single, z As Single)
            _x = x
            _y = y
            _z = z
        End Sub

#End Region
#Region " ICloneable Support "

        ''' <summary>
        ''' Creates a new object that is a deep copy of the current instance.
        ''' </summary>
        ''' <returns></returns>
        Public Function Clone() As Object Implements ICloneable.Clone
            Dim ret As Point3D = DirectCast(Me.MemberwiseClone, Point3D)

            ' since this class has only basic types as members, a shallow memberwise clone is sufficient
            Return ret
        End Function

#End Region

        ''' <summary>
        ''' Applies a transformation matrix to the vertex.
        ''' </summary>
        ''' <param name="matrix"></param>
        Public Sub Transform(matrix As Matrix3D)
            ' handle argument exceptions
            If matrix Is Nothing Then Throw New ArgumentNullException("matrix")

            ' apply the transformation matrix to the vertex's co-ordinates
            Dim x As Single = _x * matrix._cells(0, 0) + _y * matrix._cells(1, 0) + _z * matrix._cells(2, 0) + matrix._cells(3, 0)
            Dim y As Single = _x * matrix._cells(0, 1) + _y * matrix._cells(1, 1) + _z * matrix._cells(2, 1) + matrix._cells(3, 1)
            Dim z As Single = _x * matrix._cells(0, 2) + _y * matrix._cells(1, 2) + _z * matrix._cells(2, 2) + matrix._cells(3, 2)

            ' change the underlying values
            _x = x
            _y = y
            _z = z
        End Sub

        ''' <summary>
        ''' Returns the distance between this point and another.
        ''' </summary>
        ''' <param name="point"></param>
        ''' <returns></returns>
        Public Function Distance(point As Point3D) As Single
            ' handle argument exceptions
            If point Is Nothing Then Throw New ArgumentNullException("matrix")

            Dim xDiff As Single = Me._x - point._x
            Dim yDiff As Single = Me._y - point._y
            Dim zDiff As Single = Me._z - point._z

            Return Math.Sqrt(xDiff * xDiff + yDiff * yDiff + zDiff * zDiff)
        End Function

        ''' <summary>
        ''' Applies a transformation matrix to a vertex and returns a new instance storing the result.
        ''' </summary>
        ''' <param name="point"></param>
        ''' <param name="matrix"></param>
        ''' <returns></returns>
        Public Shared Function Transform(point As Point3D, matrix As Matrix3D) As Point3D
            ' handle argument exceptions
            If point Is Nothing Then Throw New ArgumentNullException("matrix")
            If matrix Is Nothing Then Throw New ArgumentNullException("matrix")

            ' create the clone
            Dim ret As Point3D = point.Clone
            ' apply the transformation to it
            ret.Transform(matrix)
            ' return it
            Return ret
        End Function

        ''' <summary>
        ''' Returns the dot product of this point and another point.
        ''' </summary>
        ''' <param name="point"></param>
        ''' <returns></returns>
        Public Function DotProduct(point As Point3D) As Single
            ' handle argument exceptions
            If point Is Nothing Then Throw New ArgumentNullException("point")

            Return Me._x * point._x + Me._y * point._y + Me._z * point._z
        End Function

#End Region
#Region " --- Properties --- "

        Public ReadOnly Property X As Single
            Get
                Return _x
            End Get
        End Property

        Public ReadOnly Property Y As Single
            Get
                Return _y
            End Get
        End Property

        Public ReadOnly Property Z As Single
            Get
                Return _z
            End Get
        End Property

#End Region

    End Class

End Namespace