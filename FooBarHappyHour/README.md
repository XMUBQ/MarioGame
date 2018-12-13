# Introduction 
This project is an attempt at recreating the first level of Super Mario Bros using MonoGame, with extended features such as:
1) Horde Mode
	- Several enemy spawners have been included in the level
	- Each spawner enemy spawns a new random enemy every 5 seconds
	- Spawn rates: 25% chance to be Koopa, 75% chance to be Goomba

2) Player-Controlled Enemies
	- Available in Player vs Player mode
	- Secondary player can select an enemy to control using the NumPad
	- Once selected, the enemy's movement is free to be controlled by the player
	- Control the enemy to kill Mario!

3) Cheat Codes
	- Cheat codes can be typed in to gain an advantage in a level
	- Each letter must be carefully typed in order, holding down multiple keys simultaneously is considered invalid input
	- There is a 1 second cooldown between activating cheat codes, no spamming!
	- List of cheat codes and their effects are listed below

# Keyboard Commands
Menu:
W - Scroll up the menu
S - Scroll down the menu
Space - Select

Player Controls:
A - Move left
D - Move right
S - Crouch / Use warp pipe
Space - Jump
X - Run / Fireball

Player-Controlled Enemies Controls:
NumPad7 - Change enemy selection (left)
NumPad9 - Change enemy selection (right)
NumPad4 - Move selected enemy to the left
NumPad6 - Move selected enemy to the right
NumPad8 - Make selected enemy jump

Q - Quit Program
P - Pause game

# Cheat Codes:
RESET- Reset level
BEBIG - Turn Mario into Super Mario (Mario must be in normal state)
FIERY - Turn Mario into Fiery Mario (Mario must be in Super Mario state)
XLIFE - Give Mario an extra life
INVIN - Give Mario the effects of obtaining a Super Star (invincibility)
XTIME - +100 to Time
SCORE - +1000 to Score
CLEAR - Kill all on-screen enemies

