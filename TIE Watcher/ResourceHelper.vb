Imports System.Reflection

''' <summary>
''' Helper class for accessing embedded resources.
''' Accessible only from within this assembly.
''' </summary>
Friend Class ResourceHelper

#Region " --- Methods --- "

#Region " Constructors "

    Private Sub New()
        ' no instances allowed
    End Sub

#End Region

    ''' <summary>
    ''' Loads the specified manifest resource from this assembly.
    ''' </summary>
    ''' <param name="name">The case-sensitive name of the manifest resource being requested.</param>
    ''' <returns></returns>
    Public Shared Function GetEmbeddedResourceStream(name As String) As IO.Stream
        ' handle argument exceptions
        If name Is Nothing Then Throw New ArgumentNullException("name")
        If name.Equals("") Then Throw New ArgumentException("Cannot load an unnamed resource.")

        ' check to ensure the resource exists, to avoid returning Nothing
        Dim resName As String = String.Format("{0}.{1}", GetType(ResourceHelper).Namespace, name)

        If Not Assembly.GetExecutingAssembly().GetManifestResourceNames().Contains(resName) Then
            Throw New IO.FileNotFoundException("The embedded resource could not be found.")
        End If

        ' resource exists, return the stream
        Return Assembly.GetExecutingAssembly().GetManifestResourceStream(resName)
    End Function

#End Region

End Class
