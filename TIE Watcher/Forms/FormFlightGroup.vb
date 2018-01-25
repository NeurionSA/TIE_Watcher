Imports TIE_Watcher.TIE

Public Class FormFlightGroup

#Region " --- Constants --- "

    ' format string for writing unknown BYTEs
    Private Const UNKNOWN_BYTE_FORMAT As String = "{0:X2}"
    ' format string for writing unknown UInt16s
    Private Const UNKNOWN_WORD_FORMAT As String = "{0:X4}"

#End Region
#Region " --- Variables --- "

    ' array of waypoint labels
    Private lblWaypoints() As Label

    ' data grid view cells for unknown fields
    Private cell_Field_0,
        cell_Field_1,
        cell_Field_2,
        cell_Field_4,
        cell_Field_5,
        cell_Field_6,
        cell_Field_8,
        cell_Field_9,
        cell_Field_A,
        cell_Field_B,
        cell_Field_C,
        cell_Field_D,
        cell_Field_E,
        cell_Field_F,
        cell_Field_10,
        cell_Field_11,
        cell_Field_12,
        cell_Field_13,
        cell_Field_14,
        cell_Field_15,
        cell_Field_16,
        cell_Field_17,
        cell_Field_18,
        cell_Field_19,
        cell_Field_1B,
        cell_Field_1C,
        cell_Field_1D,
        cell_Field_1E,
        cell_Field_1F,
        cell_Field_20,
        cell_Field_21,
        cell_Field_22,
        cell_Field_23,
        cell_Field_24,
        cell_Field_25,
        cell_Field_26,
        cell_Field_27,
        cell_Field_28,
        cell_Field_29,
        cell_Field_2A,
        cell_Field_2B,
        cell_Field_2F As DataGridViewCell

#End Region
#Region " --- Methods --- "

#Region " Constructors "

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' make and populate the lblWaypoints array
        lblWaypoints = Array.CreateInstance(GetType(Label), 15)
        lblWaypoints(0) = lblWaypointStart0
        lblWaypoints(1) = lblWaypointStart1
        lblWaypoints(2) = lblWaypointStart2
        lblWaypoints(3) = lblWaypointStart3
        lblWaypoints(4) = lblWaypointFlightPlan0
        lblWaypoints(5) = lblWaypointFlightPlan1
        lblWaypoints(6) = lblWaypointFlightPlan2
        lblWaypoints(7) = lblWaypointFlightPlan3
        lblWaypoints(8) = lblWaypointFlightPlan4
        lblWaypoints(9) = lblWaypointFlightPlan5
        lblWaypoints(10) = lblWaypointFlightPlan6
        lblWaypoints(11) = lblWaypointFlightPlan7
        lblWaypoints(12) = lblWaypointRendezvous
        lblWaypoints(13) = lblWaypointHyperspace
        lblWaypoints(14) = lblWaypointBriefing

        ' add rows to the Unknown Fields data grid view
        cell_Field_0 = addUnknownField("0")
        cell_Field_1 = addUnknownField("1")
        cell_Field_2 = addUnknownField("2")
        cell_Field_5 = addUnknownField("5")
        cell_Field_6 = addUnknownField("6")
        cell_Field_B = addUnknownField("B")
        cell_Field_C = addUnknownField("C")
        cell_Field_19 = addUnknownField("19")
        cell_Field_1D = addUnknownField("1D")
        cell_Field_1E = addUnknownField("1E")
        cell_Field_2B = addUnknownField("2B")
        cell_Field_2F = addUnknownField("2F")
    End Sub

#End Region
#Region " Tests "

    Private Sub testFGs()
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

        cmbFlightGroup.Items.Clear()
        Dim missHead As MissionHeader = TIEServices.MissonHeader
        ' iterate through all the flight groups, adding them to the combo box
        For i As Integer = 0 To missHead.NumFlightGroups - 1
            Dim missFG As MissionFlightGroup = TIEServices.MissionFlightGroups(i)
            ' add with the appropriate icon, and text indicating the CMD and flight group name
            cmbFlightGroup.Items.Add(
                New ComboBoxItem(SpaceObjectTypeInfo.Types(missFG.CraftType).CMD & " : " & missFG.Name,
                                            TIEServices.CraftIcon(missFG.CraftType, missFG.IFF)))
        Next 'i

        ' select the first item
        cmbFlightGroup.SelectedIndex = 0
        ' switch focus to the combobox
        cmbFlightGroup.Focus()
        ' enable the timer
        tmrUpdate.Enabled = True
    End Sub

#End Region

    ' adds a new row with an offset label and returns the cell the data should be written to
    Private Function addUnknownField(label As String) As DataGridViewCell
        Dim ret As DataGridViewRow

        ret = dgvUnknownFields.Rows.Item(dgvUnknownFields.Rows.Add())

        ret.Cells(0).Value = label

        Return ret.Cells(1)
    End Function

    Private Sub setDifficulyEasyState(state As Boolean)
        If state Then
            lblArrivalDifficultyE.BackColor = Color.Lime
            lblArrivalDifficultyE.ForeColor = SystemColors.ControlText
        Else
            lblArrivalDifficultyE.BackColor = Color.Black
            lblArrivalDifficultyE.ForeColor = SystemColors.InactiveCaptionText
        End If
    End Sub

    Private Sub setDifficulyMediumState(state As Boolean)
        If state Then
            lblArrivalDifficultyM.BackColor = Color.Gold
            lblArrivalDifficultyM.ForeColor = SystemColors.ControlText
        Else
            lblArrivalDifficultyM.BackColor = Color.Black
            lblArrivalDifficultyM.ForeColor = SystemColors.InactiveCaptionText
        End If
    End Sub

    Private Sub setDifficulyHardState(state As Boolean)
        If state Then
            lblArrivalDifficultyH.BackColor = Color.Red
            lblArrivalDifficultyH.ForeColor = Color.White
        Else
            lblArrivalDifficultyH.BackColor = Color.Black
            lblArrivalDifficultyH.ForeColor = SystemColors.InactiveCaptionText
        End If
    End Sub

    ' Updates information displayed in all the labels
    Private Sub updateFlightGroupInfo()
        ' abort if an invalid index is select
        If cmbFlightGroup.SelectedIndex = -1 Then Exit Sub

        Dim missFG As MissionFlightGroup = TIEServices.MissionFlightGroups(cmbFlightGroup.SelectedIndex)

        lblPilot.Text = missFG.Pilot
        lblCargo.Text = missFG.Cargo
        lblSpecialCargo.Text = missFG.SpecialCargo
        lblSpecialCargoCraft.Text = missFG.SpecialCargoCraft
        lblRandomSpecialCargoCraft.Text = missFG.RandomSpecialCargoCraft
        lblCount.Text = missFG.NumCraft
        lblWaves.Text = missFG.NumWaves + 1     ' Actual number of waves is this value +1, so it's more like additional waves
        lblStatus.Text = missFG.Status.ToString
        lblWarhead.Text = missFG.Warhead.ToString
        lblBeam.Text = missFG.Beam.ToString
        lblGroupAI.Text = missFG.GroupAI.ToString
        lblIFF.Text = TIEServices.IFFName(missFG.IFF)
        lblMarkings.Text = missFG.Markings.ToString
        lblObeyPlayer.Text = missFG.ObeyPlayer
        lblFormation.Text = missFG.Formation.ToString
        lblFormationSpacing.Text = missFG.FormationSpacing
        lblGlobalGroup.Text = missFG.GlobalGroup
        lblLeaderSpacing.Text = missFG.LeaderSpacing
        lblArrivalDelay.Text = String.Format("{0}:{1:D2}", missFG.ArrivalDelayMinutes, missFG.ArrivalDelaySeconds)
        lblDepatureDelay.Text = String.Format("{0}:{1:D2}", missFG.DepatureDelayMinutes, missFG.DepatureDelaySeconds)
        lblBonusPoints.Text = missFG.BonusPoints
        lblPlayerCraft.Text = missFG.PlayerCraft
        lblYaw.Text = missFG.Yaw
        lblPitch.Text = missFG.Pitch
        lblRoll.Text = missFG.Roll

        ' handle arrival mothership
        If missFG.ArriveViaMothership Then
            Dim fg As MissionFlightGroup = TIEServices.MissionFlightGroups(missFG.ArrivalMothership)
            lblArrivalMothership.Text = TIEServices.FlightGroupDescription(fg)
            lblArrivalMothership.ForeColor = SystemColors.ControlText
        Else
            lblArrivalMothership.Text = "N/A"
            lblArrivalMothership.ForeColor = SystemColors.GrayText
        End If

        ' handle alt arrival mothership
        If missFG.AltArriveViaMothership Then
            Dim fg As MissionFlightGroup = TIEServices.MissionFlightGroups(missFG.AltArrivalMothership)
            lblAltArrivalMothership.Text = TIEServices.FlightGroupDescription(fg)
            lblAltArrivalMothership.ForeColor = SystemColors.ControlText
        Else
            lblAltArrivalMothership.Text = "N/A"
            lblAltArrivalMothership.ForeColor = SystemColors.GrayText
        End If

        ' handle departure mothership
        If missFG.DepartViaMothership Then
            Dim fg As MissionFlightGroup = TIEServices.MissionFlightGroups(missFG.DepartureMothership)
            lblDepartureMothership.Text = TIEServices.FlightGroupDescription(fg)
            lblDepartureMothership.ForeColor = SystemColors.ControlText
        Else
            lblDepartureMothership.Text = "N/A"
            lblDepartureMothership.ForeColor = SystemColors.GrayText
        End If

        ' handle alt departure mothership
        If missFG.AltDepartViaMothership Then
            Dim fg As MissionFlightGroup = TIEServices.MissionFlightGroups(missFG.AltDepartureMothership)
            lblAltDepartureMothership.Text = TIEServices.FlightGroupDescription(fg)
            lblAltDepartureMothership.ForeColor = SystemColors.ControlText
        Else
            lblAltDepartureMothership.Text = "N/A"
            lblAltDepartureMothership.ForeColor = SystemColors.GrayText
        End If

        ' handle arrival difficuly
        Select Case missFG.ArrivalDifficulty
            Case MissionFlightGroupArrivalDifficulty.All
                setDifficulyEasyState(True)
                setDifficulyMediumState(True)
                setDifficulyHardState(True)
            Case MissionFlightGroupArrivalDifficulty.Easy
                setDifficulyEasyState(True)
                setDifficulyMediumState(False)
                setDifficulyHardState(False)
            Case MissionFlightGroupArrivalDifficulty.EasyMedium
                setDifficulyEasyState(True)
                setDifficulyMediumState(True)
                setDifficulyHardState(False)
            Case MissionFlightGroupArrivalDifficulty.Hard
                setDifficulyEasyState(False)
                setDifficulyMediumState(False)
                setDifficulyHardState(True)
            Case MissionFlightGroupArrivalDifficulty.Medium
                setDifficulyEasyState(False)
                setDifficulyMediumState(True)
                setDifficulyHardState(False)
            Case MissionFlightGroupArrivalDifficulty.MediumHard
                setDifficulyEasyState(False)
                setDifficulyMediumState(True)
                setDifficulyHardState(True)
            Case Else
                setDifficulyEasyState(False)
                setDifficulyMediumState(False)
                setDifficulyHardState(False)
        End Select

        ' handle goals
        lblGoals.Text = "* " & TIEServices.FlightGroupGoalDescription(missFG.Goals(0)) & vbCrLf &
            "* " & TIEServices.FlightGroupGoalDescription(missFG.Goals(1)) & vbCrLf &
            "* " & TIEServices.FlightGroupGoalDescription(missFG.Goals(2)) & vbCrLf &
            "* " & TIEServices.FlightGroupGoalDescription(missFG.Goals(3))

        ' handle arrival, departure, abort triggers
        lblArrivalTrigger.Text = TIEServices.MissionTriggerDescription(missFG.ArrivalTrigger(0), missFG.ArrivalTrigger(1), missFG.ArrivalBoolean)
        lblDepartTrigger.Text = TIEServices.MissionTriggerDescription(missFG.DepartureTrigger)
        lblAbortTrigger.Text = TIEServices.AbortTriggerDescription(missFG.Abort)

        ' handle orders
        lblOrders.Text = "* " & TIEServices.MissionOrderDescription(missFG.Orders(0)) & vbCrLf &
            "* " & TIEServices.MissionOrderDescription(missFG.Orders(1)) & vbCrLf &
            "* " & TIEServices.MissionOrderDescription(missFG.Orders(2))

        ' handle waypoints
        For i As Integer = 0 To 14
            If missFG.Waypoints(i).Valid Then
                Dim wp As MissionWaypoint = missFG.Waypoints(i)
                lblWaypoints(i).Text = String.Format("[{0,5:D}, {1,5:D}, {2,5:D}]",
                                                     wp.X,
                                                     wp.Y,
                                                     wp.Z)
                lblWaypoints(i).ForeColor = SystemColors.ControlText
            Else
                lblWaypoints(i).Text = "N/A"
                lblWaypoints(i).ForeColor = SystemColors.GrayText
            End If
        Next 'i
    End Sub

    Private Sub updateFlightGroupStatusInfo()
        ' abort if an invalid index is select
        If cmbFlightGroup.SelectedIndex = -1 Then Exit Sub

        Dim fgStatus As FlightGroupStatus = TIEServices.FlightGroupStatus(cmbFlightGroup.SelectedIndex)

        ' unknown fields
        cell_Field_0.Value = String.Format(UNKNOWN_BYTE_FORMAT, fgStatus.Field_0)
        cell_Field_1.Value = String.Format(UNKNOWN_BYTE_FORMAT, fgStatus.Field_1)
        cell_Field_2.Value = String.Format(UNKNOWN_WORD_FORMAT, fgStatus.Field_2)
        cell_Field_5.Value = String.Format(UNKNOWN_BYTE_FORMAT, fgStatus.Field_5)
        cell_Field_6.Value = String.Format(UNKNOWN_WORD_FORMAT, fgStatus.Field_6)
        cell_Field_B.Value = String.Format(UNKNOWN_BYTE_FORMAT, fgStatus.Field_B)
        cell_Field_C.Value = String.Format(UNKNOWN_BYTE_FORMAT, fgStatus.Field_C)
        cell_Field_19.Value = String.Format(UNKNOWN_BYTE_FORMAT, fgStatus.Field_19)
        cell_Field_1D.Value = String.Format(UNKNOWN_BYTE_FORMAT, fgStatus.Field_1D)
        cell_Field_1E.Value = String.Format(UNKNOWN_BYTE_FORMAT, fgStatus.Field_1E)
        cell_Field_2B.Value = String.Format(UNKNOWN_BYTE_FORMAT, fgStatus.Field_2B)
        cell_Field_2F.Value = String.Format(UNKNOWN_BYTE_FORMAT, fgStatus.Field_2F)
    End Sub

#Region " --- Event-Handlers --- "

    Private Sub cmbFlightGroup_DrawItem(sender As Object, e As DrawItemEventArgs) Handles cmbFlightGroup.DrawItem
        Dim item As ComboBoxItem
        e.DrawBackground()

        ' Draw the item if the index is valid
        If e.Index <> -1 Then
            Dim hPosition As Integer = e.Bounds.Left + 1
            Dim vCenter As Integer = (e.Bounds.Height >> 1) + e.Bounds.Top
            Dim textSize As SizeF

            item = cmbFlightGroup.Items(e.Index)
            textSize = e.Graphics.MeasureString(item.Text, e.Font)

            ' draw the icon box
            e.Graphics.DrawImage(TIEServices.CraftIconBox(TIEServices.MissionFlightGroups(e.Index).IFF), hPosition, vCenter - (item.Icon.Height >> 1))
            ' draw the icon
            e.Graphics.DrawImage(item.Icon, hPosition, vCenter - (item.Icon.Height >> 1))
            hPosition += item.Icon.Width + 1
            ' draw the item's text, centered vertically, beside the icon
            e.Graphics.DrawString(item.Text, e.Font, New SolidBrush(e.ForeColor), hPosition, vCenter - (CInt(textSize.Height) >> 1))
        End If

        e.DrawFocusRectangle()
    End Sub

    Private Sub cmbFlightGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFlightGroup.SelectedIndexChanged
        ' update the display
        updateFlightGroupInfo()
        updateFlightGroupStatusInfo()
    End Sub

    Private Sub cmdTest_Click(sender As Object, e As EventArgs) Handles cmdTest.Click
        testFGs()
    End Sub

    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick
        updateFlightGroupStatusInfo()
    End Sub

    Private Sub FormFlightGroup_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        ' if the form is no longer visible (i.e. it's hidden by closing it), disable updates
        If Visible = False Then
            tmrUpdate.Enabled = False
        End If
    End Sub


#End Region

#End Region

End Class