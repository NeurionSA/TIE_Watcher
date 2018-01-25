Namespace TIE

    ''' <summary>
    ''' Enumeration for Mission Flight Group Status modifiers
    ''' </summary>
    Public Enum MissionFlightGroupStatus As Byte
        ''' <summary>
        ''' Default status.
        ''' </summary>
        None = &H0
        ''' <summary>
        ''' Double load of warheads.
        ''' </summary>
        DoubleWarheads = &H1
        ''' <summary>
        ''' Half load of warheads.
        ''' </summary>
        HalfWarheads = &H2
        ''' <summary>
        ''' Craft starts off Disabled.
        ''' </summary>
        Disabled = &H3
        HalfShields = &H4
        NoLasers = &H5
        NoHyperdrive = &H6
        ''' <summary>
        ''' Shields start at 0% and charge normally.
        ''' </summary>
        ShieldsCharging = &H7
        ShieldBonus = &H8
        HyperdriveAdded = &H9
        Invincible = &H14
    End Enum

    ''' <summary>
    ''' Enumeration for Mission Flight Group Warhead setting
    ''' </summary>
    Public Enum MissionFlightGroupWarhead As Byte
        None = &H0
        SpaceBomb = &H1
        HeavyRocket = &H2
        ConcussionMissile = &H3
        Torpedo = &H4
        AdvancedConcussionMissile = &H5
        AdvancedTorpedo = &H6
        MagPulseTorpedo = &H7
    End Enum

    ''' <summary>
    ''' Enumeration for Mission Flight Group Beam setting
    ''' </summary>
    Public Enum MissionFlightGroupBeam As Byte
        None = &H0
        Tractor = &H1
        Jamming = &H2
    End Enum

    ''' <summary>
    ''' Enumerartion for Mission Flight Group AI setting
    ''' </summary>
    Public Enum MissionFlightGroupAI As Byte
        Rookie = 0
        Novice = 1
        Veteran = 2
        Officer = 3
        Ace = 4
        TopAce = 5
    End Enum

    ''' <summary>
    ''' Enumeration for Mission Flight Group Markings setting
    ''' </summary>
    Public Enum MissionFlightGroupMarkings As Byte
        Red = 0
        Gold = 1
        Blue = 2
        Green = 3
    End Enum

    ''' <summary>
    ''' Enumeration for Mission Flight Group Formations
    ''' </summary>
    Public Enum MissionFlightGroupFormation As Byte
        Vic = 0
        FingerFour = 1
        LineAstern = 2
        LineAbreast = 3
        EchelonRight = 4
        EchelonLeft = 5
        DoubleAstern = 6
        Diamond = 7
        Stack = 8
        HighX = 9
        VicAbreast = 10
        HighVic = 11
        ReverseHighVic = 12
        Spread = &H19
    End Enum

    ''' <summary>
    ''' Enumeration for Mission Flight Group Arrival Difficulty
    ''' </summary>
    Public Enum MissionFlightGroupArrivalDifficulty As Byte
        All = 0
        Easy = 1
        Medium = 2
        Hard = 3
        MediumHard = 4
        EasyMedium = 5
    End Enum

    ''' <summary>
    ''' Enumeration for Mission Trigger Condition
    ''' </summary>
    Public Enum MissionTriggerCondition As Byte
        Always = 0
        Created = 1
        Destroyed = 2
        Attacked = 3
        Captured = 4
        Inspected = 5
        Boarded = 6
        Docked = 7
        Disabled = 8
        Survived = 9
        None = &HA
        Unknown = &HB
        CompletedMission = &HC
        CompletedPrimaryGoals = &HD
        FailedPrimaryGoals = &HE
        CompletedSecondaryGoals = &HF
        FailedSecondaryGoals = &H10
        CompletedBonusGoals = &H11
        FailedBonusGoals = &H12
        DroppedOff = &H13
        Reinforced = &H14
        NoShields = &H15
        HullDamage = &H16
        OutOfWarheads = &H17
        Arrived = &H18
    End Enum

    ''' <summary>
    ''' Enumeration for Mission Variable Types
    ''' </summary>
    Public Enum MissionVariableType As Byte
        None = 0
        FlightGroup = 1
        CraftType = 2
        CraftCategory = 3
        ObjectCategory = 4
        IFF = 5
        Order = 6
        CraftWhen = 7
        GlobalGroup = 8
        Misc = 9
    End Enum

    ''' <summary>
    ''' Enumeration for Mission Craft Categories
    ''' </summary>
    Public Enum MissionCraftCategory As Byte
        Starfighters = 0
        Transports = 1
        Freighters = 2
        Starships = 3
        Utility = 4
        Platforms = 5
        Mines = 6
    End Enum

    ''' <summary>
    ''' Enumeration for Mission Object Categories
    ''' </summary>
    Public Enum MissionObjectCategory As Byte
        Craft = 0
        Weapons = 1
        SpaceObjects = 2
    End Enum

    ''' <summary>
    ''' Enumeration for Mission Trigger Amounts
    ''' </summary>
    Public Enum MissionTriggerAmount As Byte
        ''' <summary>
        ''' 100%
        ''' </summary>
        Percent100 = 0
        ''' <summary>
        ''' 75%
        ''' </summary>
        Percent75 = 1
        ''' <summary>
        ''' 50%
        ''' </summary>
        Percent50 = 2
        ''' <summary>
        ''' 25%
        ''' </summary>
        Percent25 = 3
        AtLeastOne = 4
        AllButOne = 5
        SpecialCraft = 6
        AllNonSpecialCraft = 7
        AllNonPlayerCraft = 8
        PlayerCraft = 9
        FirstWave100Percent = 10
        FirstWave75Percent = 11
        FirstWave50Percent = 12
        FirstWave25Percent = 13
        FirstWaveAtLeastOne = 14
        FirstWaveAllButOne = 15
    End Enum

    ''' <summary>
    ''' Enumeration for Mission Goal Amounts
    ''' </summary>
    Public Enum MissionGoalAmount As Byte
        Percent100 = 0
        Percent50 = 1
        AtLeastOne = 2
        AllButOne = 3
        SpecialCraft = 4
    End Enum

    ''' <summary>
    ''' Enumeration for Mission About Triggers
    ''' </summary>
    Public Enum MissionAbortTrigger As Byte
        None = 0
        ''' <summary>
        ''' Craft will abort if shields fall to 0%.
        ''' </summary>
        NoShields = 1
        ''' <summary>
        ''' Craft will abort if Systems fall below 75% (fighters and transports only).
        ''' </summary>
        Systems75 = 2
        ''' <summary>
        ''' Craft will abort when it runs out of warheads.
        ''' </summary>
        OutOfWarheads = 3
        ''' <summary>
        ''' Craft will abort if hull integrity falls below 50%.
        ''' </summary>
        HullDamage = 4
        ''' <summary>
        ''' Craft will abort if it is attacked.
        ''' </summary>
        Attacked = 5
    End Enum

    ''' <summary>
    ''' Enumeration for Mission Order Types
    ''' </summary>
    Public Enum MissionOrderType As Byte
        ''' <summary>
        ''' Hold Station.
        ''' </summary>
        HoldStation = 0
        ''' <summary>
        ''' Go Home.
        ''' </summary>
        GoHome = 1
        ''' <summary>
        ''' Circle.
        ''' Var1 = Number of Loops.
        ''' </summary>
        Circle = 2
        ''' <summary>
        ''' Circle and Evade.
        ''' Var1 = Number of Loops.
        ''' </summary>
        CircleAndEvade = 3
        ''' <summary>
        ''' Rendezvous.
        ''' Var1 = Number of Dockings.
        ''' </summary>
        Rendezvous = 4
        ''' <summary>
        ''' Disabled.
        ''' Var1 = Number of Dockings.
        ''' </summary>
        Disabled = 5
        ''' <summary>
        ''' Await Boarding
        ''' Var1 = Number of Dockings.
        ''' </summary>
        AwaitBoarding = 6
        ''' <summary>
        ''' Attack.
        ''' </summary>
        Attack = 7
        ''' <summary>
        ''' Attack Escorts.
        ''' </summary>
        AttackEscorts = 8
        ''' <summary>
        ''' Protect.
        ''' </summary>
        Protect = 9
        ''' <summary>
        ''' Escort.
        ''' </summary>
        Escort = &HA
        ''' <summary>
        ''' Disable.
        ''' </summary>
        Disable = &HB
        ''' <summary>
        ''' Board and Give Cargo.
        ''' Var1 = Docking Time (seconds * 5).
        ''' Var2 = Number of Dockings.
        ''' </summary>
        BoardAndGiveCargo = &HC
        ''' <summary>
        ''' Board and Take Cargo.
        ''' Var1 = Docking Time (seconds * 5).
        ''' Var2 = Number of Dockings.
        ''' </summary>
        BoardAndTakeCargo = &HD
        ''' <summary>
        ''' Board and Exchange Cargo.
        ''' Var1 = Docking Time (seconds * 5).
        ''' Var2 = Number of Dockings.
        ''' </summary>
        BoardAndExchangeCargo = &HE
        ''' <summary>
        ''' Board and Capture Cargo.
        ''' Var1 = Docking Time (seconds * 5).
        ''' Var2 = Number of Dockings.
        ''' </summary>
        BoardAndCaptureCargo = &HF
        ''' <summary>
        ''' Board and Destroy Cargo.
        ''' Var1 = Docking Time (seconds * 5).
        ''' Var2 = Number of Dockings.
        ''' </summary>
        BoardAndDestroyCargo = &H10
        ''' <summary>
        ''' Pick Up.
        ''' Var1 = Docking Time (seconds * 5).
        ''' Var2 = Number of Dockings.
        ''' </summary>
        PickUp = &H11
        ''' <summary>
        ''' Drop Off.
        ''' Var2 = Flight Group.
        ''' </summary>
        DropOff = &H12
        ''' <summary>
        ''' Wait
        ''' Var1 = Wait Time (seconds * 5).
        ''' </summary>
        Wait = &H13
        ''' <summary>
        ''' SS Wait.
        ''' Var1 = Wait Time (seconds * 5).
        ''' </summary>
        SS_Wait = &H14
        ''' <summary>
        ''' SS Patrol Loop.
        ''' Var1 = Number of Loops.
        ''' </summary>
        SS_PatrolLoop = &H15
        ''' <summary>
        ''' SS Await Return.
        ''' </summary>
        SS_AwaitReturn = &H16
        ''' <summary>
        ''' SS Launch.
        ''' </summary>
        SS_Launch = &H17
        ''' <summary>
        ''' SS Protect.
        ''' </summary>
        SS_Protect = &H18
        ''' <summary>
        ''' SS Wait and Protect.
        ''' </summary>
        SS_WaitAndProtect = &H19
        ''' <summary>
        ''' SS Patrol and Attack.
        ''' </summary>
        SS_PatrolAndAttack = &H1A
        ''' <summary>
        ''' SS Patrol and Disable.
        ''' </summary>
        SS_PatrolAndDisable = &H1B
        ''' <summary>
        ''' SS Hold Steady.
        ''' Var1 = Wait Time (seconds * 5).
        ''' </summary>
        SS_HoldSteady = &H1C
        ''' <summary>
        ''' SS Go Home.
        ''' </summary>
        SS_GoHome = &H1D
        ''' <summary>
        ''' SS Wait.
        ''' Var1 = Wait Time (seconds * 5).
        ''' </summary>
        SS_Wait2 = &H1E
        ''' <summary>
        ''' SS Board.
        ''' Var1 = Docking Time (seconds * 5).
        ''' Var2 = Number of Dockings.
        ''' </summary>
        SS_Board = &H1F
        ''' <summary>
        ''' Board to Repair.
        ''' Var1 = Docking Time (seconds * 5).
        ''' Var2 = Number of Dockings.
        ''' </summary>
        BoardToRepair = &H20
        ''' <summary>
        ''' Hold Station.
        ''' </summary>
        HoldStation2 = &H21
        ''' <summary>
        ''' Hold Steady.
        ''' </summary>
        HoldSteady = &H22
        ''' <summary>
        ''' SS Hold Station.
        ''' </summary>
        SS_HoldStation = &H23
        ''' <summary>
        ''' SS Kamikaze.
        ''' </summary>
        SS_Kamikaze1 = &H24
        ''' <summary>
        ''' SS Kamikaze.
        ''' </summary>
        SS_Kamikaze2 = &H25
        ''' <summary>
        ''' SS Disabled.
        ''' </summary>
        SS_Disabled1 = &H26
        ''' <summary>
        ''' SS Disabled.
        ''' </summary>
        SS_Disabled2 = &H27
    End Enum

    ''' <summary>
    ''' Enumeration for Mission Craft When
    ''' </summary>
    Public Enum MissionCraftWhen As Byte
        Boarding = 1
        Boarded = 2
        Defence = 3
        Disabled = 4
        SpecialCraft = 7
        NonSpecialCraft = 8
        PlayerCraft = 9
        NonPlayerCraft = 10
    End Enum

    ''' <summary>
    ''' Enumeration for SpaceObject Category
    ''' </summary>
    Public Enum SpaceObjectCategory As Byte
        Fighter = 0
        Transport = 1
        Utility = 2
        Freight = 3
        Capital = 4
        Platform = 5
        PlayerProjectile = 6
        Projectile = 7
        Mine = 8
        Probe = 9
        Asteroid = 10
        Planet = 12
    End Enum

    ''' <summary>
    ''' Enumeration for SpaceObject Type
    ''' </summary>
    Public Enum SpaceObjectType As Byte
        None = 0
        XWing = 1
        YWing = 2
        AWing = 3
        BWing = 4
        TIEFighter = 5
        TIEInterceptor = 6
        TIEBomber = 7
        TIEAdvanced = 8
        TIEDefender = 9
        ' UNUSED_0A = &HA
        ' UNUSED_0B = &HB
        MissileBoat = &HC
        TWing = &HD
        Z95Headhunter = &HE
        R41Starchaser = &HF
        AssaultGunboat = &H10
        Shuttle = &H11
        EscortShuttle = &H12
        SystemPatrolCraft = &H13
        ScoutCraft = &H14
        Transport = &H15
        AssaultTransport = &H16
        EscortTransport = &H17
        Tug = &H18
        CombatUtilityVehicle = &H19
        ContainerA = &H1A
        ContainerB = &H1B
        ContainerC = &H1C
        ContainerD = &H1D
        HeavyLifter = &H1E
        ' UNUSED_1F = &H1F
        Freighter = &H20
        CargoFerry = &H21
        ModularConveyor = &H22
        ContainerTransport = &H23
        ' UNUSED_24 = &H24
        MuurianTransport = &H25
        CorellianTransport = &H26
        ' UNUSED_27 = &H27
        CorellianCorvette = &H28
        ModifiedCorvette = &H29
        NebulonBFrigate = &H2A
        ModifiedFrigate = &H2B
        PassengerLiner = &H2C
        CarrackCruiser = &H2D
        StrikeCruiser = &H2E
        EscortCarrier = &H2F
        Dreadnaught = &H30
        MonCalamariCruiser = &H31
        LightMonCalamariCruiser = &H32
        Interdictor = &H33
        VictoryStarDestroyer = &H34
        ImperialStarDestroyer = &H35
        ' UNUSED_36 = &H36
        ContainerE = &H37
        ContainerF = &H38
        ContainerG = &H39
        ContainerH = &H3A
        ContainerI = &H3B
        PlatformA = &H3C
        PlatformB = &H3D
        PlatformC = &H3E
        PlatformD = &H3F
        PlatformE = &H40
        PlatformF = &H41
        AsteroidResearchStation = &H42
        AsteroidLaserBattery = &H43
        AsteroidWarheadBattery = &H44
        X7Factory = &H45
        Satellite1 = &H46
        Satellite2 = &H47
        ' UNUSED_48 = &H48
        ' UNUSED_49 = &H49
        ' UNUSED_4A = &H4A
        MineA = &H4B
        MineB = &H4C
        MineC = &H4D
        ' UNUSED_4E = &H4E
        ' UNUSED_4F = &H4F
        ProbeA = &H50
        ProbeB = &H51
        ' UNUSED_52 = &H52
        NavBuoyA = &H53
        NavBuoyB = &H54
        ' UNUSED_55 = &H55
        Asteroid = &H56
        Planet = &H57
        ' Unhandled Entries 0x58 - 0x88
        WeaponLaserRed1 = &H89
        WeaponLaserRed2 = &H8A
        WeaponLaserGreen1 = &H8B
        WeaponLaserGreen2 = &H8C
        WeaponIon1 = &H8D
        WeaponIon2 = &H8E
        WeaponTorpedo = &H8F
        WeaponMissile = &H90
        WeaponTurbolaserRed = &H91
        WeaponTurboLaserGreen = &H92
        ' Unhandled Weapons 0x93
        WeaponAdvTorpedo = &H94
        WeaponAdvMissile = &H95
        WeaponSpaceBomb = &H96
        WeaponRocket = &H97
        WeaponMagPulse = &H98
        ' Unhandled Entries from 0x99 - 0xFF
    End Enum

    ''' <summary>
    ''' Enumeration for Goal States
    ''' </summary>
    Public Enum GoalState As Byte
        ''' <summary>
        ''' Non-existent goal.
        ''' </summary>
        None = 0
        ''' <summary>
        ''' Completed Mission Goal.
        ''' </summary>
        Complete = 1
        ''' <summary>
        ''' Failed Mission Goal.
        ''' </summary>
        Failed = 2
        ''' <summary>
        ''' Incomplete Mission Goal.
        ''' </summary>
        Incomplete = 4
    End Enum

    ''' <summary>
    ''' Enumeration for Goal Types
    ''' </summary>
    Public Enum GoalType As Byte
        Primary = 0
        Secondary = 1
        ' Unknown = 2
        Bonus = 3
    End Enum

    ''' <summary>
    ''' Enumeration for Difficulty Settings
    ''' </summary>
    Public Enum DifficultySetting As Byte
        Easy = 0
        Normal = 1
        Hard = 2
    End Enum

    ''' <summary>
    ''' Enumeration for Model Part Types
    ''' </summary>
    Public Enum ModelPartType As UInt16
        None = 0
        Hull = 1
        Wing = 2
        Fuselage = 3
        LaserTurret = 4
        LaserGun = 5
        Engine = 6
        Bridge = 7      ' (Cockpit for Starfighters)
        ShieldGenerator = 8
        EnergyGenerator = 9
        WarheadLauncher = &HA
        CommSystem = &HB
        BeamSystem = &HC
        CommSystem_2 = &HD
        DockingPlatform = &HE
        LandingPlatform = &HF
        Hangar = &H10
        Cargo = &H11
        Hull_2 = &H12   ' (Not targetable)
        Antenna = &H13  ' (Not targetable)
        Wing_2 = &H14
        LaserTurret_2 = &H15
        WarheadLauncher_2 = &H16
        CommSystem_3 = &H17
        BeamSystem_2 = &H18
        CommSystem_4 = &H19
        Cockpit = &H1A
        Hull_3 = &H1B
        Hull_4 = &H1C
        Hull_5 = &H1D
        Hull_6 = &H1E
        Hull_7 = &H1F
        Unnamed = &H20  ' --------
    End Enum

End Namespace