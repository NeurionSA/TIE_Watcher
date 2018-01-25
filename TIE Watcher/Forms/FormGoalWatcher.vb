Imports TIE_Watcher.TIE

Public Class FormGoalWatcher

#Region " --- Variables --- "

    ' bitmap to draw the goals to
    Private bmpGoals As New Bitmap(800, 1080)
    ' brushes used for the goal bitmap
    Private brushHeader As SolidBrush = New SolidBrush(Color.FromArgb(0, 134, 243))
    Private brushIncomplete As SolidBrush = New SolidBrush(Color.FromArgb(255, 195, 0))
    Private brushComplete As SolidBrush = New SolidBrush(Color.FromArgb(16, 243, 0))
    Private brushFailed As SolidBrush = New SolidBrush(Color.FromArgb(243, 48, 0))
    Private brushShadow As Brush = Brushes.Black
    ' font used for the goal bitmap
    Private goalFont As New Font("Microsoft Sans Serif", 12, FontStyle.Regular)
    ' strings and other stuff
    Private goalHeaders() As String = {"Primary Goals", "Secondary Goals", "Bonus Goals", "Hidden Penalties"}
    Private goalIndexLUT() As Integer = {0, 1, 3, 3}
    Private arrivalDifficultyLUT() As Byte = {7, 1, 2, 4, 6, 3}
    Private playerDifficultyLUT() As Byte = {1, 2, 4}

#End Region
#Region " --- Methods --- "
#Region " Constructors "

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

#End Region
#Region " Dispose Support "

    'Form overrides dispose to clean up the component list.
    '<System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                ' dispose of my things
                bmpGoals.Dispose()
                brushHeader.Dispose()
                brushIncomplete.Dispose()
                brushComplete.Dispose()
                brushFailed.Dispose()
                goalFont.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

#End Region

    ' updates the goals bitmap
    Private Sub updateGoals()
        Dim g As Graphics = Graphics.FromImage(bmpGoals)
        Dim showedHeader() As Boolean = {False, False, False, False}
        Dim textSize As SizeF
        Dim vPosition As Integer = 0

        ' confine the drawing region to the size of the control
        g.Clip = New Region(New Rectangle(Size.Empty, picGoals.Size))

        ' get the mission header
        Dim missHead As MissionHeader = TIEServices.MissonHeader

        ' get the player's difficulty setting mask
        Dim playerDifficultyMask As Byte = playerDifficultyLUT(TIEServices.DifficultySetting)

        ' clear it
        g.Clear(Color.FromArgb(0, 8, 65))
        'g.Clear(Color.White)

        ' loop through the goal types (0 - 2 = primary thru bonus, 3 = penalty bonus goals)
        For goalType As Integer = 0 To 3
            ' global goals do not have penalties, only handle Primary, Secondary and Bonus
            If Not goalType = 3 Then
                ' handle Global Goals
                ' NOTE: Unfortunately it looks like the game doesn't store this anywhere and instead checks it on the fly
                ' so all I can really do is print the goal as incomplete and hope for the best
                ' NOTE: I may also try writing a services function to check the state of a GlobalGoal
                Dim globalGoal As MissionGlobalGoal = TIEServices.MissionGlobalGoals(goalType)
                For i As Integer = 0 To 1
                    Dim bGoal As Brush = Brushes.Magenta    ' use magenta if the returned goalState is unhandled
                    Dim text As String
                    Dim goalState As GoalState

                    ' skip this trigger if it's condition is Always or Never
                    Select Case globalGoal.Trigger(i).Condition
                        Case MissionTriggerCondition.Always, MissionTriggerCondition.None
                            Continue For
                    End Select

                    ' get the goal state for the mission trigger
                    goalState = TIEServices.CheckTriggerGoalState(globalGoal.Trigger(i))

                    ' determine what brush to use based on goal completion state
                    Select Case goalState
                        Case goalState.Complete
                            bGoal = brushComplete
                        Case goalState.Failed
                            bGoal = brushFailed
                        Case goalState.Incomplete
                            bGoal = brushIncomplete
                    End Select

                    ' print the heading if it hasn't been shown yet
                    If Not showedHeader(goalType) Then
                        textSize = g.MeasureString(goalHeaders(goalType), goalFont)

                        g.DrawString(goalHeaders(goalType), goalFont, brushShadow, (picGoals.Width >> 1) - (textSize.Width >> 1) + 1, vPosition)
                        g.DrawString(goalHeaders(goalType), goalFont, brushShadow, (picGoals.Width >> 1) - (textSize.Width >> 1), vPosition + 1)
                        g.DrawString(goalHeaders(goalType), goalFont, brushHeader, (picGoals.Width >> 1) - (textSize.Width >> 1), vPosition)

                        vPosition += textSize.Height

                        showedHeader(goalType) = True
                    End If

                    text = TIEServices.TriggerGoalDescriptionByState(globalGoal.Trigger(i), goalState)
                    textSize = g.MeasureString(goalHeaders(goalType), goalFont)

                    ' draw the text
                    g.DrawString(text, goalFont, brushShadow, 1, vPosition)
                    g.DrawString(text, goalFont, brushShadow, 0, vPosition + 1)
                    g.DrawString(text, goalFont, bGoal, 0, vPosition)

                    vPosition += textSize.Height
                Next i
            End If

            ' loop through all the flight groups and print their Mission Goal info
            For i As Integer = 0 To missHead.NumFlightGroups - 1
                Dim fg As MissionFlightGroup = TIEServices.MissionFlightGroups(i)
                Dim fgStatus As FlightGroupStatus = TIEServices.FlightGroupStatus(i)
                Dim goal As MissionFlightGroupGoal = fg.Goals(goalIndexLUT(goalType))

                ' skip the goal if it doesn't exist
                If goal.Condition = MissionTriggerCondition.None Then Continue For
                ' skip the flight group if it doesn't exist at this difficulty setting
                If Not arrivalDifficultyLUT(fg.ArrivalDifficulty) And playerDifficultyMask Then Continue For
                ' perform an extra check -- if this is a bonus goal, and the point value is < 0 it's a penality
                ' so it should not be printed
                If goalType = 2 AndAlso fg.BonusPoints < 0 Then Continue For
                ' if this is a penalty goal and its point value is >= 0 it's not a penalty
                If goalType = 3 AndAlso fg.BonusPoints >= 0 Then Continue For

                Dim bGoal As Brush = brushIncomplete
                Dim text As String
                Dim goalState As GoalState

                Select Case goalType
                    Case 0
                        goalState = fgStatus.PrimaryGoalState
                    Case 1
                        goalState = fgStatus.SecondaryGoalState
                    Case 2, 3
                        goalState = fgStatus.BonusGoalState
                End Select

                ' handle survival-based goals showing up as complete/incomplete when they should not be
                If fg.Goals(goalIndexLUT(goalType)).Condition = MissionTriggerCondition.Survived Then
                    Select Case TIEServices.MissionOverallStatus(goalIndexLUT(goalType))
                        Case GoalState.Complete
                            ' all goals at this level were completed, override to complete,
                            ' so even if the craft dies it's still shown as completed
                            goalState = GoalState.Complete

                        Case GoalState.Incomplete, GoalState.Failed
                            ' override to Incomplete if the craft is still alive
                            If goalState = GoalState.Complete Then goalState = GoalState.Incomplete

                    End Select
                End If

                ' determine what brush to use based on goal completion state
                If Not goalType = 3 Then
                    ' normal brushers
                    Select Case goalState
                        Case goalState.Complete
                            bGoal = brushComplete
                        Case goalState.Failed
                            bGoal = brushFailed
                        Case goalState.None, goalState.Incomplete
                            bGoal = brushIncomplete
                    End Select

                Else
                    ' penalty brushes
                    Select Case goalState
                        Case goalState.Complete
                            ' we do NOT want to complete this stuff
                            bGoal = brushFailed
                        Case goalState.Failed
                            ' we DO want to fail it
                            bGoal = brushComplete
                        Case goalState.None, goalState.Incomplete
                            bGoal = brushIncomplete
                    End Select
                End If

                ' print the heading if it hasn't been shown yet
                If Not showedHeader(goalType) Then
                    textSize = g.MeasureString(goalHeaders(goalType), goalFont)

                    g.DrawString(goalHeaders(goalType), goalFont, brushShadow, (picGoals.Width >> 1) - (textSize.Width >> 1) + 1, vPosition)
                    g.DrawString(goalHeaders(goalType), goalFont, brushShadow, (picGoals.Width >> 1) - (textSize.Width >> 1), vPosition + 1)
                    g.DrawString(goalHeaders(goalType), goalFont, brushHeader, (picGoals.Width >> 1) - (textSize.Width >> 1), vPosition)

                    vPosition += textSize.Height

                    showedHeader(goalType) = True
                End If

                text = TIEServices.FlightGroupGoalDescriptionByState(fg, goalIndexLUT(goalType), goalState, goalType.Equals(3))

                If (goalType = 2 Or goalType = 3) AndAlso fg.BonusPoints <> 0 Then
                    ' if the goal is a bonus or penalty, append the point value to the goal text, but only if there are points to be earned
                    text = String.Format("{0} ({1} points.)", text, fg.BonusPoints * 50)
                End If


                textSize = g.MeasureString(goalHeaders(goalType), goalFont)

                ' draw the text
                g.DrawString(text, goalFont, brushShadow, 1, vPosition)
                g.DrawString(text, goalFont, brushShadow, 0, vPosition + 1)
                g.DrawString(text, goalFont, bGoal, 0, vPosition)

                vPosition += textSize.Height

            Next i
        Next goalType

        ' dispose of the graphics object
        g.Dispose()
        ' invalidate the display
        picGoals.Invalidate()
    End Sub

#Region " Event-Handlers "

    Private Sub picGoals_Paint(sender As Object, e As PaintEventArgs) Handles picGoals.Paint
        e.Graphics.DrawImage(bmpGoals, 0, 0)
    End Sub

    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick
        updateGoals()
    End Sub

    Private Sub FormGoalWatcher_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        picGoals.Width = Me.ClientSize.Width - 16
        picGoals.Height = Me.ClientSize.Height - 16
    End Sub

    Private Sub FormGoalWatcher_Shown(sender As Object, e As EventArgs) Handles Me.Shown
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

        updateGoals()
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

#End Region

#End Region
#Region " --- Properties --- "



#End Region


End Class