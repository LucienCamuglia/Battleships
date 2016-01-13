# Battleships

| Title      |  Info                      |
|------------|----------------------------|
|**School :**| CFPT-INFORMATIQUE - Geneva |
|**Class :** | T.IS-E1A                   |
|**Date :**  | 13.01.2016                 |


# Introduction
> We are a class of students in computer science located in Geneva. For this project our objective is to develop
a simple Battleships game in Visual C#. This project consists of fourteen members divided in two main groups :
  - A group for creating the file format, writing files and reading files.
  - A group for creating the user interface and interacting with the file.

> The project's scope is approximatly four weeks.

# Gameplay
> The game consists of two players playing against each other. Each time a player fires a shot,
the position of the bullet is stored into a "XML" file shared between the players. The "XML" file
comes with a ".lock" file which tells the program if the other player is still playing.
The program automatically creates the ".lock" file while the player is shooting and automatically
removes the file when the player shot three times or hit a mine.
  - The game file name format is : "BN-V01_YYYY-MM-DD-HH-MM-SS.bnav"
  - The lock file name format is : "BN-V01_YYYY-MM-DD-HH-MM-SS.lock"

> The lock file simply contains the current player name.

> The players play one after the other and each turn consists of a volley of three shots. If one of the 
shot lands on a mine, the turn is cut short and the next player doubles the size of his volley (six shots).

> The game ends when all of one player's ships are destroyed (mines not included).

# Project rules
> Each group consists of approximatly six members, each working on a different part of the project.
Here is a checklist of the project's needs :
  - [] The game is cut up in multiple phases :
    1. Place the ships on the grid (write file)
    2. Place the other player's ships (write file)
    3. Shoot. (write file)
    4. Show the player's shot on the other player's grid (readonly)
    5. Repeat steps 3 and 4 until the end of the game
    6. Show scores
  - [] The graphical interface must show both grids, but only show the current player's ships.
  - [] The graphical interface asks the player for a name at the begining of the game.
  - [] The graphical interface lists current games.
  - [] The graphical interface lets the player create or join a game.
  - [] The game files are located on a shared folder (e.g. Google Drive, Dropbox...)
