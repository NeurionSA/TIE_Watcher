Namespace TIE

    ''' <summary>
    ''' Class representation of a 3D Transformation Matrix.
    ''' Not the ones specifically used in TIE Fighter -- this is for rendering TIE Fighter models my own way.
    ''' </summary>
    Public Class Matrix3D
        'Implements ICloneable

#Region " --- Variables --- "

        ' transformation matrix cells
        ' x, y
        Friend _cells(,) As Single

#End Region
#Region " --- Methods --- "

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of this class as the identity matrix.
        ''' </summary>
        Public Sub New()
            _cells = Array.CreateInstance(GetType(Single), 4, 4)
            _cells(0, 0) = 1
            _cells(1, 1) = 1
            _cells(2, 2) = 1
            _cells(3, 3) = 1
        End Sub

#End Region
#Region " ICloneable Support "

        'Public Function Clone() As Object Implements ICloneable.Clone
        '    ' create a new one to return
        '    Dim ret As New Matrix3D

        '    ' and then copy its cells
        '    For y As Integer = 0 To 3
        '        For x As Integer = 0 To 3
        '            ret._cells(x, y) = Me._cells(x, y)
        '        Next
        '    Next

        '    Return ret
        'End Function

#End Region

        ''' <summary>
        ''' Multiplies this matrix by another matrix.
        ''' </summary>
        ''' <param name="matrix"></param>
        Public Sub Multiply(matrix As Matrix3D)
            ' handle argument exceptions
            If matrix Is Nothing Then Throw New ArgumentNullException("matrix")

            ' create an array of new cells
            Dim newCells(3, 3) As Single

            ' perform the multiplication
            For y As Integer = 0 To 3
                For x As Integer = 0 To 3
                    newCells(x, y) =
                        Me._cells(0, y) * matrix._cells(x, 0) +
                        Me._cells(1, y) * matrix._cells(x, 1) +
                        Me._cells(2, y) * matrix._cells(x, 2) +
                        Me._cells(3, y) * matrix._cells(x, 3)
                Next
            Next

            ' replace the old array with the new one
            _cells = newCells
        End Sub

        ''' <summary>
        ''' Applies a scalar transformation to the matrix.
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="y"></param>
        ''' <param name="z"></param>
        Public Sub ScaleTransform(x As Single, y As Single, z As Single)
            ' create a new matrix to hold the scalar transformation
            Dim scale As New Matrix3D
            scale._cells(0, 0) = x
            scale._cells(1, 1) = y
            scale._cells(2, 2) = z

            ' apply the transformation
            Me.Multiply(scale)
        End Sub

        ''' <summary>
        ''' Applies a rotation matrix around the specified axis by an angle in degrees.
        ''' </summary>
        ''' <param name="axis"></param>
        ''' <param name="angle"></param>
        Public Sub RotateTransform(axis As RotationAxis, ByVal angle As Single)
            Dim rot As Matrix3D

            ' transform the angle from degrees into radians
            angle = angle * Math.PI / 180

            Select Case axis
                Case RotationAxis.Roll
                    rot = New Matrix3D
                    ' set the appropriate cells
                    rot._cells(0, 0) = Math.Cos(angle)
                    rot._cells(2, 0) = Math.Sin(angle)
                    rot._cells(0, 2) = -Math.Sin(angle)
                    rot._cells(2, 2) = Math.Cos(angle)

                Case RotationAxis.Pitch
                    rot = New Matrix3D
                    ' set the appropriate cells
                    rot._cells(1, 1) = Math.Cos(angle)
                    rot._cells(2, 1) = -Math.Sin(angle)
                    rot._cells(1, 2) = Math.Sin(angle)
                    rot._cells(2, 2) = Math.Cos(angle)

                Case RotationAxis.Yaw
                    rot = New Matrix3D
                    ' set the appropriate cells
                    rot._cells(0, 0) = Math.Cos(angle)
                    rot._cells(1, 0) = -Math.Sin(angle)
                    rot._cells(0, 1) = Math.Sin(angle)
                    rot._cells(1, 1) = Math.Cos(angle)

                Case Else
                    ' argument exception
                    Throw New ArgumentException("Invalid argument.", "axis")
            End Select

            ' apply the transformation
            Me.Multiply(rot)
        End Sub

#End Region
#Region " --- Properties --- "



#End Region

    End Class

    ''' <summary>
    ''' Enumeration for the type of rotation to perform on a 3D matrix.
    ''' </summary>
    Public Enum RotationAxis
        Yaw
        Pitch
        Roll
    End Enum

End Namespace