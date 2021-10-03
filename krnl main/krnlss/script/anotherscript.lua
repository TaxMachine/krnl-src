local RWPHx = Instance.new("ScreenGui")
local MainFrame = Instance.new("Frame")
local TopBar = Instance.new("Frame")
local UIName = Instance.new("TextLabel")
local UIExit = Instance.new("TextButton")
local UIMini = Instance.new("TextButton")
local UIMax = Instance.new("TextButton")
local ButtonsFramePG1 = Instance.new("Frame")
local Grid1 = Instance.new("UIGridLayout")
local item_M1014 = Instance.new("TextButton")
local item_Dragunov = Instance.new("TextButton")
local item_Makrov = Instance.new("TextButton")
local item_Taser = Instance.new("TextButton")
local item_HamSandwich = Instance.new("TextButton")
local item_Pancakes = Instance.new("TextButton")
local item_Steak = Instance.new("TextButton")
local item_Punch = Instance.new("TextButton")
local item_Handcuffs = Instance.new("TextButton")
local item_SPAS12 = Instance.new("TextButton")
local item_Shank = Instance.new("TextButton")
local item_Hammer = Instance.new("TextButton")
local item_Guitar = Instance.new("TextButton")
local item_FakeIdCard = Instance.new("TextButton")
local item_ACR = Instance.new("TextButton")
local LabelHolder = Instance.new("Frame")
local CreditsLabel = Instance.new("TextLabel")
local PageChangerFrame = Instance.new("Frame")
local CurrentPGNum = Instance.new("TextLabel")
local NextPage = Instance.new("TextButton")
local PrevPage = Instance.new("TextButton")
local ButtonsFramePG2 = Instance.new("Frame")
local Grid2 = Instance.new("UIGridLayout")
local item_L86A2 = Instance.new("TextButton")
local item_M60 = Instance.new("TextButton")
local item_BerettaM9 = Instance.new("TextButton")
local item_M14 = Instance.new("TextButton")
local item_AK47U = Instance.new("TextButton")
local item_Revolver = Instance.new("TextButton")
local item_Minigun = Instance.new("TextButton")
local item_M16 = Instance.new("TextButton")
local item_SW638 = Instance.new("TextButton")
local item_AK47 = Instance.new("TextButton")
local item_UMP45 = Instance.new("TextButton")
local item_M98B = Instance.new("TextButton")
local item_ArrestAll = Instance.new("TextButton")
local item_KillAll = Instance.new("TextButton")
local item_Speed = Instance.new("TextButton")
--Properties:
RWPHx.Name = "RWPHx"
RWPHx.Parent = game.CoreGui

MainFrame.Name = "MainFrame"
MainFrame.Parent = RWPHx
MainFrame.Active = true
MainFrame.BackgroundColor3 = Color3.new(0.121569, 0.121569, 0.121569)
MainFrame.BorderColor3 = Color3.new(1, 0.333333, 0)
MainFrame.Position = UDim2.new(0.338041425, 0, 0.232999995, 0)
MainFrame.Size = UDim2.new(0, 343, 0, 303)

TopBar.Name = "TopBar"
TopBar.Parent = MainFrame
TopBar.Active = true
TopBar.BackgroundColor3 = Color3.new(1, 0.333333, 0)
TopBar.BorderSizePixel = 0
TopBar.Size = UDim2.new(0, 343, 0, 27)

UIName.Name = "UIName"
UIName.Parent = TopBar
UIName.Active = true
UIName.BackgroundColor3 = Color3.new(1, 0.333333, 0)
UIName.BorderSizePixel = 0
UIName.Position = UDim2.new(0.294460624, 0, 0, 0)
UIName.Size = UDim2.new(0, 141, 0, 27)
UIName.Font = Enum.Font.SourceSansBold
UIName.Text = "RWPHx"
UIName.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
UIName.TextScaled = true
UIName.TextSize = 14
UIName.TextWrapped = true

UIExit.Name = "UIExit"
UIExit.Parent = TopBar
UIExit.BackgroundColor3 = Color3.new(1, 0.333333, 0)
UIExit.BorderSizePixel = 0
UIExit.Position = UDim2.new(0.930029154, 0, 0, 0)
UIExit.Size = UDim2.new(0, 24, 0, 27)
UIExit.AutoButtonColor = false
UIExit.Font = Enum.Font.SourceSansBold
UIExit.Text = "X"
UIExit.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
UIExit.TextScaled = true
UIExit.TextSize = 14
UIExit.TextWrapped = true

UIMini.Name = "UIMini"
UIMini.Parent = TopBar
UIMini.BackgroundColor3 = Color3.new(1, 0.333333, 0)
UIMini.BorderSizePixel = 0
UIMini.Position = UDim2.new(0.860058308, 0, 0, 0)
UIMini.Size = UDim2.new(0, 24, 0, 27)
UIMini.AutoButtonColor = false
UIMini.Font = Enum.Font.SourceSansBold
UIMini.Text = "-"
UIMini.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
UIMini.TextScaled = true
UIMini.TextSize = 14
UIMini.TextWrapped = true

UIMax.Name = "UIMax"
UIMax.Parent = TopBar
UIMax.BackgroundColor3 = Color3.new(1, 0.333333, 0)
UIMax.BorderSizePixel = 0
UIMax.Position = UDim2.new(0.860058308, 0, 0, 0)
UIMax.Size = UDim2.new(0, 24, 0, 27)
UIMax.Visible = false
UIMax.AutoButtonColor = false
UIMax.Font = Enum.Font.SourceSansBold
UIMax.Text = "-"
UIMax.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
UIMax.TextScaled = true
UIMax.TextSize = 14
UIMax.TextWrapped = true

ButtonsFramePG1.Name = "ButtonsFramePG1"
ButtonsFramePG1.Parent = MainFrame
ButtonsFramePG1.Active = true
ButtonsFramePG1.BackgroundColor3 = Color3.new(0.121569, 0.121569, 0.121569)
ButtonsFramePG1.BorderSizePixel = 0
ButtonsFramePG1.Position = UDim2.new(0.0408163257, 0, 0.184818476, 0)
ButtonsFramePG1.Size = UDim2.new(0, 315, 0, 205)
ButtonsFramePG1.Visible = false

Grid1.Name = "Grid1"
Grid1.Parent = ButtonsFramePG1
Grid1.CellSize = UDim2.new(0, 100, 0, 35)

item_M1014.Name = "item_M1014"
item_M1014.Parent = ButtonsFramePG1
item_M1014.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_M1014.BorderColor3 = Color3.new(1, 0.333333, 0)
item_M1014.Size = UDim2.new(0, 200, 0, 50)
item_M1014.AutoButtonColor = false
item_M1014.Font = Enum.Font.SourceSansBold
item_M1014.Text = "M1014"
item_M1014.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_M1014.TextSize = 14

item_Dragunov.Name = "item_Dragunov"
item_Dragunov.Parent = ButtonsFramePG1
item_Dragunov.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_Dragunov.BorderColor3 = Color3.new(1, 0.333333, 0)
item_Dragunov.Size = UDim2.new(0, 200, 0, 50)
item_Dragunov.AutoButtonColor = false
item_Dragunov.Font = Enum.Font.SourceSansBold
item_Dragunov.Text = "Dragunov"
item_Dragunov.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_Dragunov.TextSize = 14

item_Makrov.Name = "item_Makrov"
item_Makrov.Parent = ButtonsFramePG1
item_Makrov.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_Makrov.BorderColor3 = Color3.new(1, 0.333333, 0)
item_Makrov.Size = UDim2.new(0, 200, 0, 50)
item_Makrov.AutoButtonColor = false
item_Makrov.Font = Enum.Font.SourceSansBold
item_Makrov.Text = "Makrov"
item_Makrov.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_Makrov.TextSize = 14

item_Taser.Name = "item_Taser"
item_Taser.Parent = ButtonsFramePG1
item_Taser.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_Taser.BorderColor3 = Color3.new(1, 0.333333, 0)
item_Taser.Size = UDim2.new(0, 200, 0, 50)
item_Taser.AutoButtonColor = false
item_Taser.Font = Enum.Font.SourceSansBold
item_Taser.Text = "Taser"
item_Taser.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_Taser.TextSize = 14

item_HamSandwich.Name = "item_HamSandwich"
item_HamSandwich.Parent = ButtonsFramePG1
item_HamSandwich.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_HamSandwich.BorderColor3 = Color3.new(1, 0.333333, 0)
item_HamSandwich.Size = UDim2.new(0, 200, 0, 50)
item_HamSandwich.AutoButtonColor = false
item_HamSandwich.Font = Enum.Font.SourceSansBold
item_HamSandwich.Text = "Ham Sandwich"
item_HamSandwich.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_HamSandwich.TextSize = 14

item_Pancakes.Name = "item_Pancakes"
item_Pancakes.Parent = ButtonsFramePG1
item_Pancakes.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_Pancakes.BorderColor3 = Color3.new(1, 0.333333, 0)
item_Pancakes.Size = UDim2.new(0, 200, 0, 50)
item_Pancakes.AutoButtonColor = false
item_Pancakes.Font = Enum.Font.SourceSansBold
item_Pancakes.Text = "Pancakes"
item_Pancakes.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_Pancakes.TextSize = 14

item_Steak.Name = "item_Steak"
item_Steak.Parent = ButtonsFramePG1
item_Steak.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_Steak.BorderColor3 = Color3.new(1, 0.333333, 0)
item_Steak.Size = UDim2.new(0, 200, 0, 50)
item_Steak.AutoButtonColor = false
item_Steak.Font = Enum.Font.SourceSansBold
item_Steak.Text = "Steak"
item_Steak.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_Steak.TextSize = 14

item_Punch.Name = "item_Punch"
item_Punch.Parent = ButtonsFramePG1
item_Punch.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_Punch.BorderColor3 = Color3.new(1, 0.333333, 0)
item_Punch.Size = UDim2.new(0, 200, 0, 50)
item_Punch.AutoButtonColor = false
item_Punch.Font = Enum.Font.SourceSansBold
item_Punch.Text = "Punch"
item_Punch.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_Punch.TextSize = 14

item_Handcuffs.Name = "item_Handcuffs"
item_Handcuffs.Parent = ButtonsFramePG1
item_Handcuffs.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_Handcuffs.BorderColor3 = Color3.new(1, 0.333333, 0)
item_Handcuffs.Size = UDim2.new(0, 200, 0, 50)
item_Handcuffs.AutoButtonColor = false
item_Handcuffs.Font = Enum.Font.SourceSansBold
item_Handcuffs.Text = "Handcuffs"
item_Handcuffs.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_Handcuffs.TextSize = 14

item_SPAS12.Name = "item_SPAS12"
item_SPAS12.Parent = ButtonsFramePG1
item_SPAS12.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_SPAS12.BorderColor3 = Color3.new(1, 0.333333, 0)
item_SPAS12.Size = UDim2.new(0, 200, 0, 50)
item_SPAS12.AutoButtonColor = false
item_SPAS12.Font = Enum.Font.SourceSansBold
item_SPAS12.Text = "SPAS-12"
item_SPAS12.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_SPAS12.TextSize = 14

item_Shank.Name = "item_Shank"
item_Shank.Parent = ButtonsFramePG1
item_Shank.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_Shank.BorderColor3 = Color3.new(1, 0.333333, 0)
item_Shank.Size = UDim2.new(0, 200, 0, 50)
item_Shank.AutoButtonColor = false
item_Shank.Font = Enum.Font.SourceSansBold
item_Shank.Text = "Shank"
item_Shank.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_Shank.TextSize = 14

item_Hammer.Name = "item_Hammer"
item_Hammer.Parent = ButtonsFramePG1
item_Hammer.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_Hammer.BorderColor3 = Color3.new(1, 0.333333, 0)
item_Hammer.Size = UDim2.new(0, 200, 0, 50)
item_Hammer.AutoButtonColor = false
item_Hammer.Font = Enum.Font.SourceSansBold
item_Hammer.Text = "Hammer"
item_Hammer.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_Hammer.TextSize = 14

item_Guitar.Name = "item_Guitar"
item_Guitar.Parent = ButtonsFramePG1
item_Guitar.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_Guitar.BorderColor3 = Color3.new(1, 0.333333, 0)
item_Guitar.Size = UDim2.new(0, 200, 0, 50)
item_Guitar.AutoButtonColor = false
item_Guitar.Font = Enum.Font.SourceSansBold
item_Guitar.Text = "Guitar"
item_Guitar.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_Guitar.TextSize = 14

item_FakeIdCard.Name = "item_FakeIdCard"
item_FakeIdCard.Parent = ButtonsFramePG1
item_FakeIdCard.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_FakeIdCard.BorderColor3 = Color3.new(1, 0.333333, 0)
item_FakeIdCard.Size = UDim2.new(0, 200, 0, 50)
item_FakeIdCard.AutoButtonColor = false
item_FakeIdCard.Font = Enum.Font.SourceSansBold
item_FakeIdCard.Text = "Fake ID Card"
item_FakeIdCard.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_FakeIdCard.TextSize = 14

item_ACR.Name = "item_ACR"
item_ACR.Parent = ButtonsFramePG1
item_ACR.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_ACR.BorderColor3 = Color3.new(1, 0.333333, 0)
item_ACR.Size = UDim2.new(0, 200, 0, 50)
item_ACR.AutoButtonColor = false
item_ACR.Font = Enum.Font.SourceSansBold
item_ACR.Text = "ACR"
item_ACR.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_ACR.TextSize = 14

LabelHolder.Name = "LabelHolder"
LabelHolder.Parent = MainFrame
LabelHolder.Active = true
LabelHolder.BackgroundColor3 = Color3.new(1, 1, 1)
LabelHolder.BackgroundTransparency = 1
LabelHolder.Position = UDim2.new(-0.000712666661, 0, 0.0895596892, 0)
LabelHolder.Size = UDim2.new(0, 343, 0, 29)

CreditsLabel.Name = "CreditsLabel"
CreditsLabel.Parent = LabelHolder
CreditsLabel.Active = true
CreditsLabel.BackgroundColor3 = Color3.new(1, 1, 1)
CreditsLabel.BackgroundTransparency = 1
CreditsLabel.Position = UDim2.new(0.205649033, 0, 0.170185566, 0)
CreditsLabel.Size = UDim2.new(0, 200, 0, 18)
CreditsLabel.Font = Enum.Font.SourceSansBold
CreditsLabel.Text = "Made by: Existence#2465"
CreditsLabel.TextColor3 = Color3.new(1, 0.333333, 0)
CreditsLabel.TextScaled = true
CreditsLabel.TextSize = 14
CreditsLabel.TextWrapped = true

PageChangerFrame.Name = "PageChangerFrame"
PageChangerFrame.Parent = MainFrame
PageChangerFrame.Active = true
PageChangerFrame.BackgroundColor3 = Color3.new(1, 1, 1)
PageChangerFrame.BackgroundTransparency = 1
PageChangerFrame.Position = UDim2.new(0.002915452, 0, 0.86138612, 0)
PageChangerFrame.Size = UDim2.new(0, 342, 0, 42)

CurrentPGNum.Name = "CurrentPGNum"
CurrentPGNum.Parent = PageChangerFrame
CurrentPGNum.Active = true
CurrentPGNum.BackgroundColor3 = Color3.new(1, 1, 1)
CurrentPGNum.BackgroundTransparency = 1
CurrentPGNum.Position = UDim2.new(0.435672522, 0, 0, 0)
CurrentPGNum.Size = UDim2.new(0, 42, 0, 42)
CurrentPGNum.Font = Enum.Font.SourceSansBold
CurrentPGNum.Text = "1"
CurrentPGNum.TextColor3 = Color3.new(1, 0.333333, 0)
CurrentPGNum.TextSize = 14

NextPage.Name = "NextPage"
NextPage.Parent = PageChangerFrame
NextPage.BackgroundColor3 = Color3.new(1, 1, 1)
NextPage.BackgroundTransparency = 1
NextPage.Position = UDim2.new(0.558479548, 0, 0, 0)
NextPage.Size = UDim2.new(0, 12, 0, 42)
NextPage.AutoButtonColor = false
NextPage.Font = Enum.Font.SourceSansBold
NextPage.Text = ">"
NextPage.TextColor3 = Color3.new(1, 0.333333, 0)
NextPage.TextScaled = true
NextPage.TextSize = 14
NextPage.TextWrapped = true

PrevPage.Name = "PrevPage"
PrevPage.Parent = PageChangerFrame
PrevPage.BackgroundColor3 = Color3.new(1, 1, 1)
PrevPage.BackgroundTransparency = 1
PrevPage.Position = UDim2.new(0.400584817, 0, 0, 0)
PrevPage.Size = UDim2.new(0, 12, 0, 42)
PrevPage.AutoButtonColor = false
PrevPage.Font = Enum.Font.SourceSansBold
PrevPage.Text = "<"
PrevPage.TextColor3 = Color3.new(1, 0.333333, 0)
PrevPage.TextScaled = true
PrevPage.TextSize = 14
PrevPage.TextWrapped = true

ButtonsFramePG2.Name = "ButtonsFramePG2"
ButtonsFramePG2.Parent = MainFrame
ButtonsFramePG2.Active = true
ButtonsFramePG2.BackgroundColor3 = Color3.new(0.121569, 0.121569, 0.121569)
ButtonsFramePG2.BorderSizePixel = 0
ButtonsFramePG2.Position = UDim2.new(0.0408163257, 0, 0.184818476, 0)
ButtonsFramePG2.Size = UDim2.new(0, 315, 0, 205)

Grid2.Name = "Grid2"
Grid2.Parent = ButtonsFramePG2
Grid2.CellSize = UDim2.new(0, 100, 0, 35)

item_L86A2.Name = "item_L86A2"
item_L86A2.Parent = ButtonsFramePG2
item_L86A2.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_L86A2.BorderColor3 = Color3.new(1, 0.333333, 0)
item_L86A2.Size = UDim2.new(0, 200, 0, 50)
item_L86A2.AutoButtonColor = false
item_L86A2.Font = Enum.Font.SourceSansBold
item_L86A2.Text = "L86A2"
item_L86A2.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_L86A2.TextSize = 14

item_M60.Name = "item_M60"
item_M60.Parent = ButtonsFramePG2
item_M60.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_M60.BorderColor3 = Color3.new(1, 0.333333, 0)
item_M60.Size = UDim2.new(0, 200, 0, 50)
item_M60.AutoButtonColor = false
item_M60.Font = Enum.Font.SourceSansBold
item_M60.Text = "M60"
item_M60.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_M60.TextSize = 14

item_BerettaM9.Name = "item_BerettaM9"
item_BerettaM9.Parent = ButtonsFramePG2
item_BerettaM9.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_BerettaM9.BorderColor3 = Color3.new(1, 0.333333, 0)
item_BerettaM9.Size = UDim2.new(0, 200, 0, 50)
item_BerettaM9.AutoButtonColor = false
item_BerettaM9.Font = Enum.Font.SourceSansBold
item_BerettaM9.Text = "Beretta M9"
item_BerettaM9.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_BerettaM9.TextSize = 14

item_M14.Name = "item_M14"
item_M14.Parent = ButtonsFramePG2
item_M14.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_M14.BorderColor3 = Color3.new(1, 0.333333, 0)
item_M14.Size = UDim2.new(0, 200, 0, 50)
item_M14.AutoButtonColor = false
item_M14.Font = Enum.Font.SourceSansBold
item_M14.Text = "M14"
item_M14.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_M14.TextSize = 14

item_AK47U.Name = "item_AK47U"
item_AK47U.Parent = ButtonsFramePG2
item_AK47U.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_AK47U.BorderColor3 = Color3.new(1, 0.333333, 0)
item_AK47U.Size = UDim2.new(0, 200, 0, 50)
item_AK47U.AutoButtonColor = false
item_AK47U.Font = Enum.Font.SourceSansBold
item_AK47U.Text = "AK47-U"
item_AK47U.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_AK47U.TextSize = 14

item_Revolver.Name = "item_Revolver"
item_Revolver.Parent = ButtonsFramePG2
item_Revolver.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_Revolver.BorderColor3 = Color3.new(1, 0.333333, 0)
item_Revolver.Size = UDim2.new(0, 200, 0, 50)
item_Revolver.AutoButtonColor = false
item_Revolver.Font = Enum.Font.SourceSansBold
item_Revolver.Text = "Revolver"
item_Revolver.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_Revolver.TextSize = 14

item_Minigun.Name = "item_Minigun"
item_Minigun.Parent = ButtonsFramePG2
item_Minigun.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_Minigun.BorderColor3 = Color3.new(1, 0.333333, 0)
item_Minigun.Size = UDim2.new(0, 200, 0, 50)
item_Minigun.AutoButtonColor = false
item_Minigun.Font = Enum.Font.SourceSansBold
item_Minigun.Text = "Minigun"
item_Minigun.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_Minigun.TextSize = 14

item_M16.Name = "item_M16"
item_M16.Parent = ButtonsFramePG2
item_M16.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_M16.BorderColor3 = Color3.new(1, 0.333333, 0)
item_M16.Size = UDim2.new(0, 200, 0, 50)
item_M16.AutoButtonColor = false
item_M16.Font = Enum.Font.SourceSansBold
item_M16.Text = "M16"
item_M16.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_M16.TextSize = 14

item_SW638.Name = "item_SW638"
item_SW638.Parent = ButtonsFramePG2
item_SW638.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_SW638.BorderColor3 = Color3.new(1, 0.333333, 0)
item_SW638.Size = UDim2.new(0, 200, 0, 50)
item_SW638.AutoButtonColor = false
item_SW638.Font = Enum.Font.SourceSansBold
item_SW638.Text = "S&W 638"
item_SW638.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_SW638.TextSize = 14

item_AK47.Name = "item_AK47"
item_AK47.Parent = ButtonsFramePG2
item_AK47.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_AK47.BorderColor3 = Color3.new(1, 0.333333, 0)
item_AK47.Size = UDim2.new(0, 200, 0, 50)
item_AK47.AutoButtonColor = false
item_AK47.Font = Enum.Font.SourceSansBold
item_AK47.Text = "AK47"
item_AK47.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_AK47.TextSize = 14

item_UMP45.Name = "item_UMP45"
item_UMP45.Parent = ButtonsFramePG2
item_UMP45.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_UMP45.BorderColor3 = Color3.new(1, 0.333333, 0)
item_UMP45.Size = UDim2.new(0, 200, 0, 50)
item_UMP45.AutoButtonColor = false
item_UMP45.Font = Enum.Font.SourceSansBold
item_UMP45.Text = "UMP-45"
item_UMP45.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_UMP45.TextSize = 14

item_M98B.Name = "item_M98B"
item_M98B.Parent = ButtonsFramePG2
item_M98B.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_M98B.BorderColor3 = Color3.new(1, 0.333333, 0)
item_M98B.Size = UDim2.new(0, 200, 0, 50)
item_M98B.AutoButtonColor = false
item_M98B.Font = Enum.Font.SourceSansBold
item_M98B.Text = "M98B"
item_M98B.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_M98B.TextSize = 14

item_ArrestAll.Name = "item_ArrestAll"
item_ArrestAll.Parent = ButtonsFramePG2
item_ArrestAll.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_ArrestAll.BorderColor3 = Color3.new(1, 0.333333, 0)
item_ArrestAll.Size = UDim2.new(0, 200, 0, 50)
item_ArrestAll.AutoButtonColor = false
item_ArrestAll.Font = Enum.Font.SourceSansBold
item_ArrestAll.Text = "Arrest All"
item_ArrestAll.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_ArrestAll.TextSize = 14

item_KillAll.Name = "item_KillAll"
item_KillAll.Parent = ButtonsFramePG2
item_KillAll.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_KillAll.BorderColor3 = Color3.new(1, 0.333333, 0)
item_KillAll.Size = UDim2.new(0, 200, 0, 50)
item_KillAll.AutoButtonColor = false
item_KillAll.Font = Enum.Font.SourceSansBold
item_KillAll.Text = "Kill All"
item_KillAll.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_KillAll.TextSize = 14

item_Speed.Name = "item_Speed"
item_Speed.Parent = ButtonsFramePG2
item_Speed.BackgroundColor3 = Color3.new(1, 0.333333, 0)
item_Speed.BorderColor3 = Color3.new(1, 0.333333, 0)
item_Speed.Size = UDim2.new(0, 200, 0, 50)
item_Speed.AutoButtonColor = false
item_Speed.Font = Enum.Font.SourceSansBold
item_Speed.Text = "Speed: OFF"
item_Speed.TextColor3 = Color3.new(0.121569, 0.121569, 0.121569)
item_Speed.TextSize = 14

local CoreGui = game.CoreGui
local RWPHx = CoreGui.RWPHx
local MainFrame = RWPHx.MainFrame
local TopBar = MainFrame.TopBar
local LabelHolder = MainFrame.LabelHolder
local CreditsLabel = LabelHolder.CreditsLabel
local UIExit = TopBar.UIExit
local UIMini = TopBar.UIMini
local UIMax = TopBar.UIMax
local UIName = TopBar.UIName
local BFPG1 = MainFrame.ButtonsFramePG1
local BFPG2 = MainFrame.ButtonsFramePG2
local PGF = MainFrame.PageChangerFrame
local NextPage = PGF.NextPage
local PrevPage = PGF.PrevPage
local CurrentPGNum = PGF.CurrentPGNum

--// PAGE 1 ITEMS //--
local item_M1014 = BFPG1.item_M1014
local item_Dragunov = BFPG1.item_Dragunov
local item_ACR = BFPG1.item_ACR
local item_Makrov = BFPG1.item_Makrov
local item_SPAS12 = BFPG1.item_SPAS12
local item_Taser = BFPG1.item_Taser
local item_HamSandwich = BFPG1.item_HamSandwich
local item_Guitar = BFPG1.item_Guitar
local item_Pancakes = BFPG1.item_Pancakes
local item_Steak = BFPG1.item_Steak
local item_Punch = BFPG1.item_Punch
local item_Handcuffs = BFPG1.item_Handcuffs
local item_Shank = BFPG1.item_Shank
local item_Hammer = BFPG1.item_Hammer
local item_FakeIdCard = BFPG1.item_FakeIdCard

--// PAGE 2 ITEMS //--
local item_L86A2 = BFPG2.item_L86A2
local item_M60 = BFPG2.item_M60
local item_BerettaM9 = BFPG2.item_BerettaM9
local item_Revolver = BFPG2.item_Revolver
local item_M14 = BFPG2.item_M14
local item_AK47U = BFPG2.item_AK47U
local item_Minigun = BFPG2.item_Minigun
local item_M16 = BFPG2.item_M16
local item_SW638 = BFPG2.item_SW638
local item_AK47 = BFPG2.item_AK47
local item_UMP45 = BFPG2.item_UMP45
local item_M98B = BFPG2.item_M98B
local item_ArrestAll = BFPG2.item_ArrestAll
local item_KillAll = BFPG2.item_KillAll
local item_Speed = BFPG2.item_Speed

--// SETUP //--
MainFrame.Position = UDim2.new(-1,0,0.233,0)
BFPG2.Visible = false
BFPG1.Visible = true
local PGValue = Instance.new("NumberValue", RWPHx.MainFrame)
PGValue.Value = 1
CurrentPGNum.Text = PGValue.Value
UIMax.Visible = false
UIMax.Selectable = false
UIMax.Archivable = false
UIMax.Position = UDim2.new(-2,0,0,0)
MainFrame.Draggable = true
BFPG1.Visible = true
BFPG2.Visible = false

--// ANIMATIONS / TOPBAR AND PAGE BUTTON FUNCTIONS //--
wait(0.2)
function NextPageClick()
	if PGValue.Value == 1 then
		BFPG1.Visible = false
		BFPG2.Visible = true
		PGValue.Value = 2
		CurrentPGNum.Text = PGValue.Value
	elseif PGValue.Value == 2 then --//If the PGValue is 2 and the next page button is clicked it will take it back to the home page which is one
		BFPG1.Visible = true
		BFPG2.Visible = false
		PGValue.Value = 1
		CurrentPGNum.Text = PGValue.Value
	end
end
NextPage.MouseButton1Click:Connect(NextPageClick)

function PrevPageClick()
	if PGValue.Value == 2 then
		BFPG1.Visible = true
		BFPG2.Visible = false
		PGValue.Value = 1
		CurrentPGNum.Text = PGValue.Value
	elseif PGValue.Value == 1 then
		BFPG1.Visible = false
		BFPG2.Visible = true
		PGValue.Value = 2
		CurrentPGNum.Text = PGValue.Value
	end
end
PrevPage.MouseButton1Click:Connect(PrevPageClick)

-- Minied : 0,343,0,27
-- Maxed : 0,343,0,0.233

function Minimize()
	MainFrame:TweenSize(UDim2.new(0,343,0,27), "Out", "Quad", 0.4, true)
	PGF.Visible = false
	wait(0.05)
	BFPG1.Visible = false
	BFPG2.Visible = false
	wait(0.05)
	LabelHolder.Visible = false
	wait(0.5)
	UIMini.Visible = false
	UIMini.Selectable = false
	UIMini.Archivable = false
	UIMini.Position = UDim2.new(-2,0,0,0)
	UIMax.Visible = true
	UIMax.Selectable = true
	UIMax.Archivable = true
	UIMax.Position = UDim2.new(0.86,0,0,0)
end
UIMini.MouseButton1Click:Connect(Minimize)

function Maximize()
	MainFrame:TweenSize(UDim2.new(0,343,0,303), "In", "Quad", 0.4, true)
	LabelHolder.Visible = true
	wait(0.3)
	if PGValue.Value == 1 then
		BFPG1.Visible = true
		BFPG2.Visible = false
	elseif PGValue.Value == 2 then
		BFPG1.Visible = false
		BFPG2.Visible = true
	end
	PGF.Visible = true
	wait(0.05)
	wait(0.5)
	
	UIMax.Position = UDim2.new(0.86,0,0,0)
	UIMini.Position = UDim2.new(0.86,0,0,0)
	UIMini.Visible = true
	UIMini.Selectable = true
	UIMini.Archivable = true
	UIMax.Visible = false
	UIMax.Selectable = false
	UIMax.Archivable = false
end
UIMax.MouseButton1Click:Connect(Maximize)

function Exiting()
	MainFrame:TweenPosition(UDim2.new(-1,0,0.233,0), "In", "Quad", 0.2, true)
	wait(0.3)
	RWPHx:Destroy()
end
UIExit.MouseButton1Click:Connect(Exiting)

MainFrame:TweenPosition(UDim2.new(0.338,0,0.233,0), "In", "Quad", 0.2, true)

--// BUTTON FUNCTIONS //--
function giveM1014()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "M1014")
end
item_M1014.MouseButton1Click:Connect(giveM1014)

function giveDragunov()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Dragunov")
end
item_Dragunov.MouseButton1Click:Connect(giveDragunov)

function giveACR()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "ACR")
end
item_ACR.MouseButton1Click:Connect(giveACR)

function giveMakrov()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Makrov")
end
item_Makrov.MouseButton1Click:Connect(giveMakrov)

function giveTaser()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Taser")
end
item_Taser.MouseButton1Click:Connect(giveTaser)

function giveHamSandwich()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Ham Sandwich")
end
item_HamSandwich.MouseButton1Click:Connect(giveHamSandwich)

function giveGuitar()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Guitar")
end
item_Guitar.MouseButton1Click:Connect(giveGuitar)

function givePancakes()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Pancakes")
end
item_Pancakes.MouseButton1Click:Connect(givePancakes)

function giveSteak()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Steak")
end
item_Steak.MouseButton1Click:Connect(giveSteak)

function giveHammer()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Hammer")
end
item_Hammer.MouseButton1Click:Connect(giveHammer)

function givePunch()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Punch")
end
item_Punch.MouseButton1Click:Connect(givePunch)

function giveHandcuffs()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Handcuffs")
end
item_Handcuffs.MouseButton1Click:Connect(giveHandcuffs)

function giveShank()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Shank")
end
item_Shank.MouseButton1Click:Connect(giveShank)

function giveSPAS12()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "SPAS-12")
end
item_SPAS12.MouseButton1Click:Connect(giveSPAS12)

function giveFakeIdCard()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Fake ID Card")
end
item_FakeIdCard.MouseButton1Click:Connect(giveFakeIdCard)

function giveL86A2()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "L86A2")
end
item_L86A2.MouseButton1Click:Connect(giveL86A2)

function giveM60()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "M60")
end
item_M60.MouseButton1Click:Connect(giveL86A2)

function giveBerettaM9()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Beretta M9")
end
item_BerettaM9.MouseButton1Click:Connect(giveBerettaM9)

function giveRevolver()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Revolver")
end
item_Revolver.MouseButton1Click:Connect(giveRevolver)

function giveM14()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "M14")
end
item_M14.MouseButton1Click:Connect(giveM14)

function giveAk47U()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "AK47-U")
end
item_AK47U.MouseButton1Click:Connect(giveAk47U)

function giveMinigun()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "Minigun")
end
item_Minigun.MouseButton1Click:Connect(giveMinigun)

function giveM16()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "M16")
end
item_M16.MouseButton1Click:Connect(giveL86A2)

function giveSW638()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "S&W 638")
end
item_SW638.MouseButton1Click:Connect(giveSW638())

function giveAK47()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "AK47")
end
item_AK47.MouseButton1Click:Connect(giveAK47)

function giveUMP45()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "UMP-45")
end
item_UMP45.MouseButton1Click:Connect(giveUMP45)

function giveM98B()
	game.Workspace.resources.RemoteFunction:InvokeServer("giveItem", "M98B")
end
item_L86A2.MouseButton1Click:Connect(giveM98B)

function f_ArrestAll()
	for _, v in pairs(game.Players:GetChildren()) do
	game.Workspace.resources.RemoteEvent:FireServer("cuff", v) 
	end
end
item_ArrestAll.MouseButton1Click:Connect(f_ArrestAll)

function f_KillAll()
	for _, v in pairs(game.Players:GetChildren()) do
	game.Workspace.resources.RemoteEvent:FireServer("dealDamage", v.Character.Humanoid, 100) 
	end
end
item_KillAll.MouseButton1Click:Connect(f_KillAll)

function f_Speed()
	if game.Players.LocalPlayer.Character.Humanoid.WalkSpeed < 50 then
		game.Players.LocalPlayer.Character.Humanoid.WalkSpeed = 100
		item_Speed.Text = "Speed: ON"
	elseif game.Players.LocalPlayer.Character.Humanoid.WalkSpeed > 99 then
		game.Players.LocalPlayer.Character.Humanoid.WalkSpeed = 16
		item_Speed.Text = "Speed: OFF"
	end
end
item_Speed.MouseButton1Click:Connect(f_Speed)

--// MADE BY: Existence#2465 / Discord Server: https://www.discord.gg/invite/d8q9sfQ
--// If you find any problems or bugs with the gui dm on discord me so I can improve the gui
--// Enjoy :D