Imports TIE_Watcher.TIE

Public Class FormTrainer

#Region " --- Variables --- "



#End Region
#Region " --- Methods --- "

    Private Sub applyTrainer()
        ' if the trainer options are enabled, write to TIE Services
        If chkResetHullDamage.Checked Then
            TIEServices.PlayerCraftDamage = 0
        End If
        If chkNoPanelDamage.Checked Then
            TIEServices.PlayerCraftPanelDamageFlags = &H7FF
        End If

    End Sub

#Region " Event-Handlers "

    Private Sub FormTrainer_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If TIEServices.Process Is Nothing Then
            ' find a dosbox process
            For Each proc As Process In Process.GetProcesses
                ' if the first 6 chars match then we'll try that one
                If Strings.Left(proc.ProcessName, 6).ToLower.Equals("dosbox") Then
                    TIEServices.OpenProcess(proc)
                    Exit For
                End If
            Next 'proc
        End If

        applyTrainer()
        tmrUpdate.Enabled = True
    End Sub

    Private Sub FormGoalWatcher_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        ' if the form is no longer visible (i.e. it's hidden by closing it), disable updates
        If Visible = False Then
            tmrUpdate.Enabled = False
        Else
            ' re-enable the timer
            tmrUpdate.Enabled = True
        End If
    End Sub

    Private Sub chkNoPanelDamage_CheckedChanged(sender As Object, e As EventArgs) Handles chkNoPanelDamage.CheckedChanged
        Dim chk As CheckBox = DirectCast(sender, CheckBox)

        ' this only needs to be changed when the user clicks it
        Select Case chk.Checked
            Case True
                TIEServices.HackCraftPanelDamage = True
            Case False
                TIEServices.HackCraftPanelDamage = False
        End Select
    End Sub

    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick
        applyTrainer()
    End Sub


#End Region

#End Region


End Class