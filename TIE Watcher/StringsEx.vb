''' <summary>
''' Additional functions to extend and supplement Microsoft.VisualBasic.Strings
''' </summary>
Friend Module StringsEx

    ''' <summary>
    ''' Returns a string that is cut short by any NULL characters it may contain.
    ''' </summary>
    ''' <param name="str">String to trim.</param>
    ''' <returns></returns>
    Friend Function TrimNull(str As String) As String
        Return Strings.Left(str, str.IndexOf(vbNullChar))
    End Function
End Module
