Namespace TIE

    ''' <summary>
    ''' Structure to hold extra data/info for Space_Object types
    ''' </summary>
    Public Class SpaceObjectTypeInfo

#Region " --- Constants --- "

        ' the number of items defined for this
        Private Const ITEM_COUNT As Integer = &H6A

#End Region
#Region " --- Variables --- "

        Private _name As String         ' Space_Object's name, singular
        Private _namePlural As String    ' Space_Object's name, plural
        Private _cmd As String          ' Space_Object's CMD display code
        Private _iconIndex As Integer   ' Index of icon to use, from PLAYER.LFD
        Private _speciesName As String  ' Resource Name for object model in SPECIES.LFD and its siblings
        Private _speciesFileIndex As Integer ' Index of the SPECIES.LFD file the object's model is in (-1 for n/a)

        Private Shared _types As SpaceObjectTypeInfo()
        'Private Shared _types As SpaceObjectTypeInfo() = {
        'New SpaceObjectTypeInfo("None", "None", "", 9),
        'New SpaceObjectTypeInfo("X-Wing", "X-Wings", "X-W", 0),
        'New SpaceObjectTypeInfo("Y-Wing", "Y-Wings", "Y-W", 1),
        'New SpaceObjectTypeInfo("A-Wing", "A-Wings", "A-W", 2),
        'New SpaceObjectTypeInfo("B-Wing", "B-Wings", "B-W", 3),
        'New SpaceObjectTypeInfo("TIE Fighter", "TIE Fighters", "T/F", 4),
        'New SpaceObjectTypeInfo("TIE Interceptor", "TIE Interceptors", "T/I", 5),
        'New SpaceObjectTypeInfo("TIE Bomber", "TIE Bombers", "T/B", 6),
        'New SpaceObjectTypeInfo("TIE Advanced", "TIE Advanced", "T/A", 7),
        'New SpaceObjectTypeInfo("TIE Defender", "TIE Defenders", "T/D", 8),
        'New SpaceObjectTypeInfo("Unused", "Unused", "", 9),
        'New SpaceObjectTypeInfo("Unused", "Unused", "", 10),
        'New SpaceObjectTypeInfo("Missile Boat", "Missile Boats", "MIS", 11),
        'New SpaceObjectTypeInfo("T-Wing", "T-Wings", "T-W", 12),
        'New SpaceObjectTypeInfo("Z-95 Headhunter", "Z-95 Headhunters", "Z-95", 13),
        'New SpaceObjectTypeInfo("R-41 Starchaster", "R-41 Starchasers", "R-41", 14),
        'New SpaceObjectTypeInfo("Assault Gunboat", "Assault Gunboats", "GUN", 15),
        'New SpaceObjectTypeInfo("Shuttle", "Shuttles", "SHU", 16),
        'New SpaceObjectTypeInfo("Escort Shuttle", "Escort Shuttles", "E/S", 17),
        'New SpaceObjectTypeInfo("System Patrol Craft", "System Patrol Craft", "SPC", 18),
        'New SpaceObjectTypeInfo("Scout Craft", "Scout Craft", "SCT", 19),
        'New SpaceObjectTypeInfo("Transport", "Transports", "TRN", 20),
        'New SpaceObjectTypeInfo("Assault Transport", "Assault Transports", "ATR", 21),
        'New SpaceObjectTypeInfo("Escort Transport", "Escort Transports", "ETR", 22),
        'New SpaceObjectTypeInfo("Tug", "Tugs", "TUG", 23),
        'New SpaceObjectTypeInfo("Combat Utility Vehicle", "Combat Utility Vehicles", "CUV", 24),
        'New SpaceObjectTypeInfo("Container A", "Container As", "CN/A", 25),
        'New SpaceObjectTypeInfo("Container B", "Container Bs", "CN/B", 26),
        'New SpaceObjectTypeInfo("Container C", "Container Cs", "CN/C", 27),
        'New SpaceObjectTypeInfo("Container D", "Container Ds", "CN/D", 28),
        'New SpaceObjectTypeInfo("Heavy Lifter", "Heavy Lifters", "HLF", 29),
        'New SpaceObjectTypeInfo("Unused", "Unused", "", 30),
        'New SpaceObjectTypeInfo("Freighter", "Freighters", "FRT", 31),
        'New SpaceObjectTypeInfo("Cargo Ferry", "Cargo Ferries", "CARG", 34),    ' Cargo Ferries and Container Transports had swapped icons in the game
        'New SpaceObjectTypeInfo("Modular Conveyor", "Modular Conveyors", "CNVYR", 33),
        'New SpaceObjectTypeInfo("Container Transport", "Container Transports", "CTRNS", 32),    ' Cargo Ferries and Container Transports had swapped icons in the game
        'New SpaceObjectTypeInfo("Unused", "Unused", "", 35),
        'New SpaceObjectTypeInfo("Muurian Transport", "Muurian Transports", "MUTR", 36),
        'New SpaceObjectTypeInfo("Corellian Transport", "Corellian Transports", "CORT", 37),
        'New SpaceObjectTypeInfo("Unused", "Unused", "", 38),
        'New SpaceObjectTypeInfo("Corellian Corvette", "Corellian Corvettes", "CRV", 39),
        'New SpaceObjectTypeInfo("Modified Corvette", "Modified Corvettes", "M/CRV", 40),
        'New SpaceObjectTypeInfo("Nebulon B Frigate", "Nebulon B Frigates", "FRG", 41),
        'New SpaceObjectTypeInfo("Nebulon B-2 Frigate", "Nebulon B-2 Frigates", "M/FRG", 42),
        'New SpaceObjectTypeInfo("C-3 Passenger Liner", "C-3 Passenger Liners", "LINER", 43),
        'New SpaceObjectTypeInfo("Carrack Cruiser", "Carrack Cruisers", "CRKC", 44),
        'New SpaceObjectTypeInfo("Strike Cruiser", "Strike Cruisers", "STRKC", 45),
        'New SpaceObjectTypeInfo("Escort Carrier", "Escort Carriers", "ESC", 46),
        'New SpaceObjectTypeInfo("Dreadnaught", "Dreadnaughts", "DREAD", 47),
        'New SpaceObjectTypeInfo("Calamari Cruiser", "Calamari Cruisers", "CRS", 48),
        'New SpaceObjectTypeInfo("Light Calamari Cruiser", "Light Calamari Cruisers", "CRL", 49),
        'New SpaceObjectTypeInfo("Interdictor Cruiser", "Interdictor Cruisers", "INT", 50),
        'New SpaceObjectTypeInfo("Victory Star Destroyer", "Victory Star Destoryers", "VSD", 51),
        'New SpaceObjectTypeInfo("Imperial Star Destroyer", "Imperial Star Destroyers", "ISD", 52),
        'New SpaceObjectTypeInfo("Unused", "Unused", "", 53),
        'New SpaceObjectTypeInfo("Container E", "Container Es", "CN/E", 54),
        'New SpaceObjectTypeInfo("Container F", "Container Fs", "CN/F", 55),
        'New SpaceObjectTypeInfo("Container G", "Container Gs", "CN/G", 56),
        'New SpaceObjectTypeInfo("Container H", "Container Hs", "CN/H", 57),
        'New SpaceObjectTypeInfo("Container I", "Container Is", "CN/I", 58),
        'New SpaceObjectTypeInfo("Platform XQ1", "Platform XQ1s", "PLT/1", 59),
        'New SpaceObjectTypeInfo("Platform XQ2", "Platform XQ2s", "PLT/2", 60),
        'New SpaceObjectTypeInfo("Platform XQ3", "Platform XQ3s", "PLT/3", 61),
        'New SpaceObjectTypeInfo("Platform XQ4", "Platform XQ4s", "PLT/4", 62),
        'New SpaceObjectTypeInfo("Platform XQ5", "Platform XQ5s", "PLT/5", 63),
        'New SpaceObjectTypeInfo("Platform XQ6", "Platform XQ6s", "PLT/6", 64),
        'New SpaceObjectTypeInfo("Asteroid R&D Station", "Asteriod R&D Stations", "R&D FC", 65),
        'New SpaceObjectTypeInfo("Asteroid Laser Battery", "Asteroid Laser Batteries", "LAS BAT", 66),
        'New SpaceObjectTypeInfo("Asteroid Warhead Battery", "Asteroid Warhead Batteries", "W LNCHR", 67),
        'New SpaceObjectTypeInfo("X/7 Factory", "X/7 Factories", "FAC/1", 68),
        'New SpaceObjectTypeInfo("Satellite 1", "Satellite 1s", "Sat-1", 69),
        'New SpaceObjectTypeInfo("Satellite 2", "Satellite 2s", "Sat-2", 70),
        'New SpaceObjectTypeInfo("Unused", "Unused", "", 71),
        'New SpaceObjectTypeInfo("Unused", "Unused", "", 72),
        'New SpaceObjectTypeInfo("Unused", "Unused", "", 73),
        'New SpaceObjectTypeInfo("Mine A", "Mine As", "Mn-A", 74),
        'New SpaceObjectTypeInfo("Mine B", "Mine Bs", "Mn-B", 75),
        'New SpaceObjectTypeInfo("Mine C", "Mine Cs", "Mn-C", 76),
        'New SpaceObjectTypeInfo("Unused", "Unused", "", 77),
        'New SpaceObjectTypeInfo("Unused", "Unused", "", 78),
        'New SpaceObjectTypeInfo("Probe A", "Probe As", "Prb-1", 79),
        'New SpaceObjectTypeInfo("Probe B", "Probe Bs", "Prb-2", 80),
        'New SpaceObjectTypeInfo("Unused", "Unused", "", 81),
        'New SpaceObjectTypeInfo("Nav Buoy A", "Nav Buoy As", "Nav-1", 82),
        'New SpaceObjectTypeInfo("Nav Buoy B", "Nav Buoy Bs", "Nav-2", 83),
        'New SpaceObjectTypeInfo("Unused", "Unused", "", 84),
        'New SpaceObjectTypeInfo("Asteroid Field", "Asteroid Fields", "Field", 85),
        'New SpaceObjectTypeInfo("Planet", "Planets", "Planet", 86),
        'New SpaceObjectTypeInfo("0x58", "0x58", "0x58", 9),
        'New SpaceObjectTypeInfo("0x59", "0x59", "0x59", 9),
        'New SpaceObjectTypeInfo("0x5A", "0x5A", "0x5A", 9),
        'New SpaceObjectTypeInfo("0x5B", "0x5B", "0x5B", 9),
        'New SpaceObjectTypeInfo("0x5C", "0x5C", "0x5C", 9),
        'New SpaceObjectTypeInfo("0x5D", "0x5D", "0x5D", 9),
        'New SpaceObjectTypeInfo("0x5E", "0x5E", "0x5E", 9),
        'New SpaceObjectTypeInfo("0x5F", "0x5F", "0x5F", 9),
        'New SpaceObjectTypeInfo("0x60", "0x60", "0x60", 9),
        'New SpaceObjectTypeInfo("0x61", "0x61", "0x61", 9),
        'New SpaceObjectTypeInfo("0x62", "0x62", "0x62", 9),
        'New SpaceObjectTypeInfo("0x63", "0x63", "0x63", 9),
        'New SpaceObjectTypeInfo("0x64", "0x64", "0x64", 9),
        'New SpaceObjectTypeInfo("0x65", "0x65", "0x65", 9),
        'New SpaceObjectTypeInfo("0x66", "0x66", "0x66", 9),
        'New SpaceObjectTypeInfo("0x67", "0x67", "0x67", 85),     ' 0x67 - Asteroid?
        'New SpaceObjectTypeInfo("0x68", "0x68", "0x68", 9),
        'New SpaceObjectTypeInfo("0x69", "0x69", "0x69", 9),
        'New SpaceObjectTypeInfo("0x6A", "0x6A", "0x6A", 9),
        'New SpaceObjectTypeInfo("0x6B", "0x6B", "0x6B", 9),
        'New SpaceObjectTypeInfo("0x6C", "0x6C", "0x6C", 9),
        'New SpaceObjectTypeInfo("0x6D", "0x6D", "0x6D", 9),
        'New SpaceObjectTypeInfo("0x6E", "0x6E", "0x6E", 9),
        'New SpaceObjectTypeInfo("0x6F", "0x6F", "0x6F", 9)
        '}

#End Region
#Region " --- Methods --- "

#Region " Constructor "

        Private Shared Sub initTypeArray()
            ' create the array
            _types = Array.CreateInstance(GetType(SpaceObjectTypeInfo), ITEM_COUNT)

            ' create all the items with their default field values
            For i As Integer = 0 To ITEM_COUNT - 1
                _types(i) = New SpaceObjectTypeInfo
                With _types(i)
                    ._name = String.Format("OBJECT_{0:X2}", i)
                    ._namePlural = String.Format("OBJECT_{0:X2}", i)
                    ._cmd = String.Format("O-{0:X2}", i)
                    ._iconIndex = 9
                    ._speciesName = ""
                    ._speciesFileIndex = -1
                End With
            Next

            ' Set the values for all the known items
            ' 0x00 - None
            With _types(0)
                ._name = "None"
                ._namePlural = "None"
                ._cmd = ""
                ._iconIndex = 9
            End With
            ' 0x01 - X-Wing
            With _types(1)
                ._name = "X-Wing"
                ._namePlural = "X-Wings"
                ._cmd = "X-W"
                ._iconIndex = 0
                ._speciesName = "XWING"
                ._speciesFileIndex = 0
            End With
            ' 0x02 - Y-Wing
            With _types(2)
                ._name = "Y-Wing"
                ._namePlural = "Y-Wings"
                ._cmd = "Y-W"
                ._iconIndex = 1
                ._speciesName = "YWING"
                ._speciesFileIndex = 0
            End With
            ' 0x03 - A-Wing
            With _types(3)
                ._name = "A-Wing"
                ._namePlural = "A-Wings"
                ._cmd = "A-W"
                ._iconIndex = 2
                ._speciesName = "AWING"
                ._speciesFileIndex = 0
            End With
            ' 0x04 - B-Wing
            With _types(4)
                ._name = "B-Wing"
                ._namePlural = "B-Wings"
                ._cmd = "B-W"
                ._iconIndex = 3
                ._speciesName = "BWING"
                ._speciesFileIndex = 0
            End With
            ' 0x05 - TIE Fighter
            With _types(5)
                ._name = "TIE Fighter"
                ._namePlural = "TIE Fighters"
                ._cmd = "T/F"
                ._iconIndex = 4
                ._speciesName = "TIEFTR"
                ._speciesFileIndex = 0
            End With
            ' 0x06 - TIE Interceptor
            With _types(6)
                ._name = "TIE Interceptor"
                ._namePlural = "TIE Interceptors"
                ._cmd = "T/I"
                ._iconIndex = 5
                ._speciesName = "TIEINT"
                ._speciesFileIndex = 0
            End With
            ' 0x07 - TIE Bomber
            With _types(7)
                ._name = "TIE Bomber"
                ._namePlural = "TIE Bombers"
                ._cmd = "T/B"
                ._iconIndex = 6
                ._speciesName = "TIEBMB"
                ._speciesFileIndex = 0
            End With
            ' 0x08 - TIE Advanced
            With _types(8)
                ._name = "TIE Advanded"
                ._namePlural = "TIE Advanded"
                ._cmd = "T/A"
                ._iconIndex = 7
                ._speciesName = "TIEADV"
                ._speciesFileIndex = 0
            End With
            ' 0x09 - TIE Defender
            With _types(9)
                ._name = "TIE Defender"
                ._namePlural = "TIE Defenders"
                ._cmd = "T/D"
                ._iconIndex = 8
                ._speciesName = "TIEDEL"
                ._speciesFileIndex = 0
            End With
            ' 0x0A - Unused
            With _types(&HA)
                ._name = "Unused"
                ._namePlural = "Unused"
                ._cmd = ""
                ._iconIndex = 9
            End With
            ' 0x0B - Unused
            With _types(&HB)
                ._name = "Unused"
                ._namePlural = "Unused"
                ._cmd = ""
                ._iconIndex = 10
            End With
            ' 0x0C - Missile Boat
            With _types(&HC)
                ._name = "Missile Boat"
                ._namePlural = "Missile Boats"
                ._cmd = "MIS"
                ._iconIndex = 11
                ._speciesName = "MISLBT"
                ._speciesFileIndex = 1
            End With
            ' 0x0D - T-Wing
            With _types(&HD)
                ._name = "T-Wing"
                ._namePlural = "T-Wings"
                ._cmd = "T-W"
                ._iconIndex = 12
                ._speciesName = "FGHTA"
                ._speciesFileIndex = 1
            End With
            ' 0x0E - Z-95 Headhunter
            With _types(&HE)
                ._name = "Z-95 Headhunter"
                ._namePlural = "Z-95 Headhunters"
                ._cmd = "Z-95"
                ._iconIndex = 13
                ._speciesName = "Z-95"
                ._speciesFileIndex = 0
            End With
            ' 0x0F - R-41 Starchaser
            With _types(&HF)
                ._name = "R-41 Starchaser"
                ._namePlural = "R-41 Starchasers"
                ._cmd = "R-41"
                ._iconIndex = 14
                ._speciesName = "FGHTB"
                ._speciesFileIndex = 1
            End With
            ' 0x10 - Assault Gunboat
            With _types(&H10)
                ._name = "Assault Gunboat"
                ._namePlural = "Assault Gunboats"
                ._cmd = "GUN"
                ._iconIndex = 15
                ._speciesName = "ASSAULT"
                ._speciesFileIndex = 0
            End With
            ' 0x11 - Shuttle
            With _types(&H11)
                ._name = "Shuttle"
                ._namePlural = "Shuttles"
                ._cmd = "SHU"
                ._iconIndex = 16
                ._speciesName = "SHUTTLE"
                ._speciesFileIndex = 0
            End With
            ' 0x12 - Escort Shuttle
            With _types(&H12)
                ._name = "Escort Shuttle"
                ._namePlural = "Escort Shuttles"
                ._cmd = "E/S"
                ._iconIndex = 17
                ._speciesName = "SHUTB"
                ._speciesFileIndex = 0
            End With
            ' 0x13 - System Patrol Craft
            With _types(&H13)
                ._name = "System Patrol Craft"
                ._namePlural = "System Patrol Craft"
                ._cmd = "SPC"
                ._iconIndex = 18
                ._speciesName = "PATRL"
                ._speciesFileIndex = 0
            End With
            ' 0x14 - Scout Craft
            With _types(&H14)
                ._name = "Scout Craft"
                ._namePlural = "Scout Craft"
                ._cmd = "SCT"
                ._iconIndex = 19
                ._speciesName = "PATRLB"
                ._speciesFileIndex = 1
            End With
            ' 0x15 - Transport
            With _types(&H15)
                ._name = "Transport"
                ._namePlural = "Transports"
                ._cmd = "TRN"
                ._iconIndex = 20
                ._speciesName = "TRNSPRT"
                ._speciesFileIndex = 0
            End With
            ' 0x16 - Assault Transport
            With _types(&H16)
                ._name = "Assault Transport"
                ._namePlural = "Assault Transports"
                ._cmd = "ATR"
                ._iconIndex = 21
                ._speciesName = "TRANSB"
                ._speciesFileIndex = 0
            End With
            ' 0x17 - Escort Transport
            With _types(&H17)
                ._name = "Escort Transport"
                ._namePlural = "Escort Transports"
                ._cmd = "ETR"
                ._iconIndex = 22
                ._speciesName = "ESCTRP"
                ._speciesFileIndex = 1
            End With
            ' 0x18 - Tug
            With _types(&H18)
                ._name = "Tug"
                ._namePlural = "Tugs"
                ._cmd = "TUG"
                ._iconIndex = 23
                ._speciesName = "TUG"
                ._speciesFileIndex = 0
            End With
            ' 0x19 - Combat Utility Vehicle
            With _types(&H19)
                ._name = "Combat Utility Vehicle"
                ._namePlural = "Combat Utility Vehicles"
                ._cmd = "CUV"
                ._iconIndex = 24
                ._speciesName = "UTILA"
                ._speciesFileIndex = 1
            End With
            ' 0x1A - Container A
            With _types(&H1A)
                ._name = "Container A"
                ._namePlural = "Container As"
                ._cmd = "CN/A"
                ._iconIndex = 25
                ._speciesName = "CONTAIN"
                ._speciesFileIndex = 0
            End With
            ' 0x1B - Container B
            With _types(&H1B)
                ._name = "Container B"
                ._namePlural = "Container Bs"
                ._cmd = "CN/B"
                ._iconIndex = 26
                ._speciesName = "CONA"
                ._speciesFileIndex = 0
            End With
            ' 0x1C - Container C
            With _types(&H1C)
                ._name = "Container C"
                ._namePlural = "Container Cs"
                ._cmd = "CN/C"
                ._iconIndex = 27
                ._speciesName = "CONB"
                ._speciesFileIndex = 0
            End With
            ' 0x1D - Container D
            With _types(&H1D)
                ._name = "Container D"
                ._namePlural = "Container Ds"
                ._cmd = "CN/D"
                ._iconIndex = 28
                ._speciesName = "CONC"
                ._speciesFileIndex = 0
            End With
            ' 0x1E - Heavy Lifter
            With _types(&H1E)
                ._name = "Heavy Lifter"
                ._namePlural = "Heavy Lifters"
                ._cmd = "HLF"
                ._iconIndex = 29
                ._speciesName = "BARGE"
                ._speciesFileIndex = 0
            End With
            ' 0x1F - Unused
            With _types(&H1F)
                ._name = "Unused"
                ._namePlural = "Unused"
                ._cmd = ""
                ._iconIndex = 30
            End With
            ' 0x20 - Freighter
            With _types(&H20)
                ._name = "Freighter"
                ._namePlural = "Freighters"
                ._cmd = "FRT"
                ._iconIndex = 31
                ._speciesName = "FREIGHT"
                ._speciesFileIndex = 0
            End With
            ' 0x21 - Cargo Ferry
            With _types(&H21)
                ._name = "Cargo Ferry"
                ._namePlural = "Cargo Ferries"
                ._cmd = "CARG"
                ._iconIndex = 34    ' Cargo Ferries and Container Transports had swapped icons in the game
                ._speciesName = "FRTB"
                ._speciesFileIndex = 0
            End With
            ' 0x22 - Modular Conveyor
            With _types(&H22)
                ._name = "Modular Conveyor"
                ._namePlural = "Modular Conveyors"
                ._cmd = "CNVYR"
                ._iconIndex = 33
                ._speciesName = "FRTA"
                ._speciesFileIndex = 0
            End With
            ' 0x23 - Container Transport
            With _types(&H23)
                ._name = "Container Transport"
                ._namePlural = "Container Transports"
                ._cmd = "CTRNS"
                ._iconIndex = 32    ' Cargo Ferries and Container Transports had swapped icons in the game
                ._speciesName = "FRTC"
                ._speciesFileIndex = 0
            End With
            ' 0x24 - Unused
            With _types(&H24)
                ._name = "Unused"
                ._namePlural = "Unused"
                ._cmd = ""
                ._iconIndex = 35
            End With
            ' 0x25 - Muurian Transport
            With _types(&H25)
                ._name = "Muurian Transport"
                ._namePlural = "Muurian Transports"
                ._cmd = "MUTR"
                ._iconIndex = 36
                ._speciesName = "LTFRTA"
                ._speciesFileIndex = 1
            End With
            ' 0x26 - Corellian Transport
            With _types(&H26)
                ._name = "Corellian Transport"
                ._namePlural = "Corellian Transports"
                ._cmd = "CORT"
                ._iconIndex = 37
                ._speciesName = "CORTN"
                ._speciesFileIndex = 0
            End With
            ' 0x27 - Unused
            With _types(&H27)
                ._name = "Unused"
                ._namePlural = "Unused"
                ._cmd = ""
                ._iconIndex = 38
            End With
            ' 0x28 - Corellian Corvette
            With _types(&H28)
                ._name = "Corellian Corvette"
                ._namePlural = "Corellian Corvettes"
                ._cmd = "CRV"
                ._iconIndex = 39
                ._speciesName = "CORV"
                ._speciesFileIndex = 0
            End With
            ' 0x29 - Modified Corvette
            With _types(&H29)
                ._name = "Modified Corvette"
                ._namePlural = "Modified Corvettes"
                ._cmd = "M/CRV"
                ._iconIndex = 40
                ._speciesName = "CORVTA"
                ._speciesFileIndex = 0
            End With
            ' 0x2A - Nebulon B Frigate
            With _types(&H2A)
                ._name = "Nebulon B Frigate"
                ._namePlural = "Nebulon B Frigates"
                ._cmd = "FRG"
                ._iconIndex = 41
                ._speciesName = "NEB"
                ._speciesFileIndex = 0
            End With
            ' 0x2B - Nebulon B-2 Frigate
            With _types(&H2B)
                ._name = "Nebulon B-2 Frigate"
                ._namePlural = "Nebulon B-2 Frigates"
                ._cmd = "M/FRG"
                ._iconIndex = 42
                ._speciesName = "FRIGA"
                ._speciesFileIndex = 0
            End With
            ' 0x2C - C-3 Passenger Liner
            With _types(&H2C)
                ._name = "C-3 Passenger Liner"
                ._namePlural = "C-3 Passenger Liners"
                ._cmd = "LINER"
                ._iconIndex = 43
                ._speciesName = "PASNGR"
                ._speciesFileIndex = 1
            End With
            ' 0x2D - Carrack Cruiser
            With _types(&H2D)
                ._name = "Carrack Cruiser"
                ._namePlural = "Carrack Cruisers"
                ._cmd = "CRKC"
                ._iconIndex = 44
                ._speciesName = "CRUSA"
                ._speciesFileIndex = 1
            End With
            ' 0x2E - Strike Cruiser
            With _types(&H2E)
                ._name = "Strike Cruiser"
                ._namePlural = "Strike Cruisers"
                ._cmd = "STRKC"
                ._iconIndex = 45
                ._speciesName = "CRUSB"
                ._speciesFileIndex = 1
            End With
            ' 0x2F - Escort Carrier
            With _types(&H2F)
                ._name = "Escort Carrier"
                ._namePlural = "Escort Carriers"
                ._cmd = "ESC"
                ._iconIndex = 46
                ._speciesName = "ESCRT"
                ._speciesFileIndex = 0
            End With
            ' 0x30 - Unused
            With _types(&H30)
                ._name = "Dreadnaught"
                ._namePlural = "Dreadnaughts"
                ._cmd = "DREAD"
                ._iconIndex = 47
            End With
            ' 0x31 - Calamari Cruiser
            With _types(&H31)
                ._name = "Calamari Cruiser"
                ._namePlural = "Calamari Cruisers"
                ._cmd = "CRS"
                ._iconIndex = 48
                ._speciesName = "CAL"
                ._speciesFileIndex = 0
            End With
            ' 0x32 - Light Calamari Cruiser
            With _types(&H32)
                ._name = "Light Calamari Cruiser"
                ._namePlural = "Light Calamari Cruisers"
                ._cmd = "CRL"
                ._iconIndex = 49
                ._speciesName = "LTCAL"
                ._speciesFileIndex = 0
            End With
            ' 0x33 - Interdictor Cruiser
            With _types(&H33)
                ._name = "Interdictor Cruiser"
                ._namePlural = "Interdictor Cruisers"
                ._cmd = "INT"
                ._iconIndex = 50
                ._speciesName = "INTERD"
                ._speciesFileIndex = 0
            End With
            ' 0x34 - Victory Star Destroyer
            With _types(&H34)
                ._name = "Victory Star Destroyer"
                ._namePlural = "Victory Star Destroyers"
                ._cmd = "VSD"
                ._iconIndex = 51
                ._speciesName = "VSD"
                ._speciesFileIndex = 0
            End With
            ' 0x35 - Imperial Star Destroyer
            With _types(&H35)
                ._name = "Imperial Star Destroyer"
                ._namePlural = "Imperial Star Destroyers"
                ._cmd = "ISD"
                ._iconIndex = 52
                ._speciesName = "ISD"
                ._speciesFileIndex = 0
            End With
            ' 0x36 - Unused
            With _types(&H36)
                ._name = "Unused"
                ._namePlural = "Unused"
                ._cmd = ""
                ._iconIndex = 53
            End With
            ' 0x37 - Container E
            With _types(&H37)
                ._name = "Container E"
                ._namePlural = "Container Es"
                ._cmd = "CN/E"
                ._iconIndex = 54
                ._speciesName = "COND"
                ._speciesFileIndex = 0
            End With
            ' 0x38 - Container F
            With _types(&H38)
                ._name = "Container F"
                ._namePlural = "Container Fs"
                ._cmd = "CN/F"
                ._iconIndex = 55
                ._speciesName = "CONE"
                ._speciesFileIndex = 1
            End With
            ' 0x39 - Container G
            With _types(&H39)
                ._name = "Container G"
                ._namePlural = "Container Gs"
                ._cmd = "CN/G"
                ._iconIndex = 56
                ._speciesName = "CONF"
                ._speciesFileIndex = 1
            End With
            ' 0x3A - Container H
            With _types(&H3A)
                ._name = "Container H"
                ._namePlural = "Container Hs"
                ._cmd = "CN/H"
                ._iconIndex = 57
                ._speciesName = "CONG"
                ._speciesFileIndex = 1
            End With
            ' 0x3B - Container I
            With _types(&H3B)
                ._name = "Container I"
                ._namePlural = "Container Is"
                ._cmd = "CN/I"
                ._iconIndex = 58
                ._speciesName = "CONH"
                ._speciesFileIndex = 1
            End With
            ' 0x3C - Platform XQ1
            With _types(&H3C)
                ._name = "Platform XQ1"
                ._namePlural = "Platform XQ1s"
                ._cmd = "PLT/1"
                ._iconIndex = 59
                ._speciesName = "PLATA"
                ._speciesFileIndex = 0
            End With
            ' 0x3D - Platform XQ2
            With _types(&H3D)
                ._name = "Platform XQ2"
                ._namePlural = "Platform XQ2s"
                ._cmd = "PLT/2"
                ._iconIndex = 60
                ._speciesName = "PLATB"
                ._speciesFileIndex = 0
            End With
            ' 0x3E - Platform XQ3
            With _types(&H3E)
                ._name = "Platform XQ3"
                ._namePlural = "Platform XQ3s"
                ._cmd = "PLT/3"
                ._iconIndex = 61
                ._speciesName = "PLATC"
                ._speciesFileIndex = 0
            End With
            ' 0x3F - Platform XQ4
            With _types(&H3F)
                ._name = "Platform XQ4"
                ._namePlural = "Platform XQ4s"
                ._cmd = "PLT/4"
                ._iconIndex = 62
                ._speciesName = "PLATAB"
                ._speciesFileIndex = 0
            End With
            ' 0x40 - Platform XQ5
            With _types(&H40)
                ._name = "Platform XQ5"
                ._namePlural = "Platform XQ5s"
                ._cmd = "PLT/5"
                ._iconIndex = 63
                ._speciesName = "PLATBA"
                ._speciesFileIndex = 0
            End With
            ' 0x41 - Platform XQ6
            With _types(&H41)
                ._name = "Platform XQ6"
                ._namePlural = "Platform XQ6s"
                ._cmd = "PLT/6"
                ._iconIndex = 64
                ._speciesName = "PLATCA"
                ._speciesFileIndex = 0
            End With
            ' 0x42 - Asteroid R&D Station
            With _types(&H42)
                ._name = "Asteroid R&D Station"
                ._namePlural = "Asteroid R&D Stations"
                ._cmd = "R&D FC"
                ._iconIndex = 65
                ._speciesName = "ASTH44"
                ._speciesFileIndex = 2
            End With
            ' 0x43 - Asteroid Laser Battery
            With _types(&H43)
                ._name = "Asteroid Laser Battery"
                ._namePlural = "Asteroid Laser Batteries"
                ._cmd = "LAS BAT"
                ._iconIndex = 66
                ._speciesName = "ASTG33"
                ._speciesFileIndex = 2
            End With
            ' 0x44 - Asteroid Warhead Battery
            With _types(&H44)
                ._name = "Asteroid Warhead Battery"
                ._namePlural = "Asteroid Warhead Batteries"
                ._cmd = "W LNCHR"
                ._iconIndex = 67
                ._speciesName = "ASTW21"
                ._speciesFileIndex = 2
            End With
            ' 0x45 - X/7 Factory
            With _types(&H45)
                ._name = "X/7 Factory"
                ._namePlural = "X/7 Factories"
                ._cmd = "FAC/1"
                ._iconIndex = 68
                ._speciesName = "FACTRY"
                ._speciesFileIndex = 1
            End With
            ' 0x46 - Satellite 1
            With _types(&H46)
                ._name = "Satellite 1"
                ._namePlural = "Satellite 1s"
                ._cmd = "Sat-1"
                ._iconIndex = 69
                ._speciesName = "SAT1"
                ._speciesFileIndex = 0
            End With
            ' 0x47 - Satellite 2
            With _types(&H47)
                ._name = "Satellite 2"
                ._namePlural = "Satellite 2s"
                ._cmd = "Sat-2"
                ._iconIndex = 70
            End With
            ' 0x48 - Unused
            With _types(&H48)
                ._name = "Unused"
                ._namePlural = "Unused"
                ._cmd = ""
                ._iconIndex = 71
            End With
            ' 0x49 - Unused
            With _types(&H49)
                ._name = "Unused"
                ._namePlural = "Unused"
                ._cmd = ""
                ._iconIndex = 72
            End With
            ' 0x4A - Unused
            With _types(&H4A)
                ._name = "Unused"
                ._namePlural = "Unused"
                ._cmd = ""
                ._iconIndex = 73
            End With
            ' 0x4B - Mine A
            With _types(&H4B)
                ._name = "Mine A"
                ._namePlural = "Mine As"
                ._cmd = "Mn-A"
                ._iconIndex = 74
                ._speciesName = "MINE1"
                ._speciesFileIndex = 0
            End With
            ' 0x4C - Mine B
            With _types(&H4C)
                ._name = "Mine B"
                ._namePlural = "Mine Bs"
                ._cmd = "Mn-B"
                ._iconIndex = 75
                ._speciesName = "MINE2"
                ._speciesFileIndex = 0
            End With
            ' 0x4D - Mine C
            With _types(&H4D)
                ._name = "Mine C"
                ._namePlural = "Mine Cs"
                ._cmd = "Mn-C"
                ._iconIndex = 76
                ._speciesName = "MINE3"
                ._speciesFileIndex = 0
            End With
            ' 0x4E - Unused
            With _types(&H4E)
                ._name = "Unused"
                ._namePlural = "Unused"
                ._cmd = ""
                ._iconIndex = 77
            End With
            ' 0x4F - Unused
            With _types(&H4F)
                ._name = "Unused"
                ._namePlural = "Unused"
                ._cmd = ""
                ._iconIndex = 78
            End With
            ' 0x50 - Probe A
            With _types(&H50)
                ._name = "Probe A"
                ._namePlural = "Probe As"
                ._cmd = "Prb-1"
                ._iconIndex = 79
                ._speciesName = "PROBE1"
                ._speciesFileIndex = 0
            End With
            ' 0x51 - Probe B
            With _types(&H51)
                ._name = "Probe B"
                ._namePlural = "Probe Bs"
                ._cmd = "Prb-2"
                ._iconIndex = 80
                ._speciesName = "PROBEA"
                ._speciesFileIndex = 0
            End With
            ' 0x52 - Unused
            With _types(&H52)
                ._name = "Unused"
                ._namePlural = "Unused"
                ._cmd = ""
                ._iconIndex = 81
            End With
            ' 0x53 - Nav Buoy A
            With _types(&H53)
                ._name = "Nav Buoy A"
                ._namePlural = "Nav Buoy As"
                ._cmd = "Nav-1"
                ._iconIndex = 82
            End With
            ' 0x54 - Nav Buoy B
            With _types(&H54)
                ._name = "Nav Buoy B"
                ._namePlural = "Nav Buoy Bs"
                ._cmd = "Nav-2"
                ._iconIndex = 83
            End With
            ' 0x55 - Unused
            With _types(&H55)
                ._name = "Unused"
                ._namePlural = "Unused"
                ._cmd = ""
                ._iconIndex = 84
            End With
            ' 0x56 - Asteroid Field
            With _types(&H56)
                ._name = "Asteroid Field"
                ._namePlural = "Asteroid Fields"
                ._cmd = "Field"
                ._iconIndex = 85
            End With
            ' 0x57 - Planet
            With _types(&H57)
                ._name = "Planet"
                ._namePlural = "Planets"
                ._cmd = "Planet"
                ._iconIndex = 86
            End With

            ' 0x67 - Asteroid?
            With _types(&H67)
                ._iconIndex = 85
            End With
            ' 0x68 - Asteroid?
            With _types(&H68)
                ._iconIndex = 85
            End With
            ' 0x69 - Asteroid?
            With _types(&H69)
                ._iconIndex = 85
            End With
        End Sub

        ' shared constructor, to initialize all the items
        Shared Sub New()
            initTypeArray()
        End Sub

        ' private constructor
        Private Sub New()
            ' do nothing
        End Sub

#End Region

#End Region
#Region " --- Properties --- "

        ''' <summary>
        ''' Space Object's name, singular.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Name As String
            Get
                Return Me._name
            End Get
        End Property

        ''' <summary>
        ''' Space Object's name, plural.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property NamePlural As String
            Get
                Return Me._namePlural
            End Get
        End Property

        ''' <summary>
        ''' Space Object's CMD code.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property CMD As String
            Get
                Return Me._cmd
            End Get
        End Property

        ''' <summary>
        ''' Icon Index for drawing the Space Object.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property IconIndex As Integer
            Get
                Return Me._iconIndex
            End Get
        End Property

        ''' <summary>
        ''' SPECIES.LFD resource name of the Space Object's model.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property SpeciesName As String
            Get
                Return Me._speciesName
            End Get
        End Property

        ''' <summary>
        ''' Index of SPECIES.LFD file the Space Object's model is contained in, or -1 if there is no model.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property SpeciesFileIndex As Integer
            Get
                Return Me._speciesFileIndex
            End Get
        End Property

        ''' <summary>
        ''' Returns the number of SpaceObjectTypes there's info for.
        ''' </summary>
        ''' <returns></returns>
        Public Shared ReadOnly Property Count As Integer
            Get
                Return ITEM_COUNT
            End Get
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="index"></param>
        ''' <returns></returns>
        Public Shared ReadOnly Property Types(index As Integer) As SpaceObjectTypeInfo
            Get
                ' throw exception if index is bad
                If index < 0 Or index >= ITEM_COUNT Then Throw New ArgumentOutOfRangeException("index")
                Return _types(index)
            End Get
        End Property

#End Region

    End Class

End Namespace