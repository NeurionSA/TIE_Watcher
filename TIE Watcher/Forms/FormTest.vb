Imports TIE_Watcher.TIE
Imports System.ComponentModel
Imports System.Drawing.Imaging

Public Class FormTest

    Friend WithEvents frmCombatMap As FormCombatMap
    Friend WithEvents frmSpaceObject As FormSpaceObject
    Friend WithEvents frmFlightGroup As FormFlightGroup
    Friend WithEvents frmGoalWatcher As FormGoalWatcher
    Friend WithEvents frmTrainer As FormTrainer
    Friend WithEvents frmModelBrowser As FormModelBrowser

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


#Region " --- Variables --- "


#End Region
#Region " --- Methods --- "

#Region " --- Event Handlers --- "

    Private Sub cmdTestFlightGroup_Click(sender As Object, e As EventArgs) Handles cmdTestFlightGroup.Click
        If frmFlightGroup Is Nothing Then
            frmFlightGroup = New FormFlightGroup
        End If
        frmFlightGroup.Show()
    End Sub

    Private Sub cmdTestSpaceObjects_Click(sender As Object, e As EventArgs) Handles cmdTestSpaceObjects.Click
        If frmSpaceObject Is Nothing Then
            frmSpaceObject = New FormSpaceObject
        End If
        frmSpaceObject.Show()
    End Sub

    Private Sub cmdTestCombatMap_Click(sender As Object, e As EventArgs) Handles cmdTestCombatMap.Click
        If frmCombatMap Is Nothing Then
            frmCombatMap = New FormCombatMap
        End If
        frmCombatMap.Show()
    End Sub

    Private Sub cmdTestMissionGoals_Click(sender As Object, e As EventArgs) Handles cmdTestMissionGoals.Click
        If frmGoalWatcher Is Nothing Then
            frmGoalWatcher = New FormGoalWatcher
        End If
        frmGoalWatcher.Show()
    End Sub

    Private Sub cmdTrainer_Click(sender As Object, e As EventArgs) Handles cmdTrainer.Click
        If frmTrainer Is Nothing Then
            frmTrainer = New FormTrainer
        End If
        frmTrainer.Show()
    End Sub

    Private Sub cmdModelBrowser_Click(sender As Object, e As EventArgs) Handles cmdModelBrowser.Click
        If frmModelBrowser Is Nothing Then
            frmModelBrowser = New FormModelBrowser
        End If
        frmModelBrowser.Show()
    End Sub

    Private Sub frmChildren_Closing(sender As Object, e As CancelEventArgs) Handles _
            frmCombatMap.Closing,
            frmFlightGroup.Closing,
            frmGoalWatcher.Closing,
            frmSpaceObject.Closing,
            frmTrainer.Closing,
            frmModelBrowser.Closing

        Dim frmSender As Form = DirectCast(sender, Form)
        ' you're not allowed to close any of these, just hide them
        e.Cancel = True
        frmSender.Hide()
    End Sub

    Private Sub cmdTest_Click(sender As Object, e As EventArgs) Handles cmdTest.Click
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

        Dim handle As UInt16 = TIEServices.SpaceObjectDefinitions(SpaceObjectType.TIEFighter).ResourceHandle

        If handle = 0 Then
            Console.WriteLine("Craft's model is not loaded.")
        Else
            Console.WriteLine(String.Format("Craft's model is at {0:X8}", TIEServices.GetXMEMPointer(handle)))
        End If


    End Sub

#End Region

#End Region

End Class
