# :bomb: Battleships :boat:

| Title      |  Info                      |
|------------|----------------------------|
|**School :**| CFPT-INFORMATIQUE - Geneva |
|**Class :** | T.IS-E1A                   |
|**Date :**  | 13.01.2016                 |


# Introduction
> We are a class of students in computer science located in Geneva :neckbeard:. For this project our objective is to develop
a simple Battleships game in Visual C#. This project consists of fourteen members divided in two main groups :
  - A group for creating the file format, writing files and reading files.
  - A group for creating the user interface and interacting with the file.

> The project's scope is approximatly four weeks. :scream:

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
  - [ ] The game is cut up in multiple phases :
    1. Place the ships on the grid (write file)
    2. Place the other player's ships (write file)
    3. Shoot. (write file)
    4. Show the player's shot on the other player's grid (readonly)
    5. Repeat steps 3 and 4 until the end of the game
    6. Show scores
  - [ ] The graphical interface must show both grids, but only show the current player's ships.
  - [ ] The graphical interface asks the player for a name at the begining of the game.
  - [ ] The graphical interface lists current games.
  - [ ] The graphical interface lets the player create or join a game.
  - [ ] The game files are located on a shared folder (e.g. Google Drive, Dropbox...)

> Have fun programming ! :fire::computer::fire:

# Exemples 
## Images
![Alt GameBoard](http://i.imgur.com/c5AWbqG.png?1)
## .bnav file
    # -------------------------------------------------------
    # BATAILLE NAVALE v0.1
    # -------------------------------------------------------
    # Partie du 2 décembre 2015 10:22:05
    # Joueur1 : nom du joueur
    # Joueur2 : nom du joueur
    # Board : 10x10
    # 0 1 2 3 4 5 6 7 8 9
    # A 0;0;0;0;0;0;0;0;0;0;
    # B 0;0;0;0;0;0;0;0;0;0;
    # C 0;0;0;0;0;0;0;0;0;0;
    # D 0;0;0;0;0;0;0;0;0;0;
    # E 0;0;0;0;0;0;0;0;0;0;
    # F 0;0;0;0;0;0;0;0;0;0;
    # G 0;0;0;0;0;0;0;0;0;0;
    # H 0;0;0;0;0;0;0;0;0;0;
    # I 0;0;0;0;0;0;0;0;0;0;
    # J 0;0;0;0;0;0;0;0;0;0;
    # -------------------------------------------------------
    # BATEAUX : 
    # -------------------------------------------------------
    [BATEAUX]
    NUMERO;NOM;TAILLE;SURFACE;NOMBRE;R;G;B;
    0;EAU;1x1;1;79;0xF0,0xFF;0xFF;
    1;MINE;1x1;1;4;0xFF;0xA5;0xFF;
    2;SOUSMARIN;1x3;3;1;0x80;0x80;0x80;
    3;DESTROYER;1x4;4;1;0xA9;0xA9;0xA9;
    4;PORTEHELICO;2x2;4;1;0xBD;0xB7;0x6D;
    5;PORTEAVION;2x3;6;1;0xD8;0xBF;0xD8;
    [/BATEAUX]
    # -------------------------------------------------------
    # Joueurs
    # -------------------------------------------------------
    [JOUEURS]
    P1;Pim
    P2;Pam
    [/JOUEURS]
    # -------------------------------------------------------
    # Plateau P1 : 
    # -------------------------------------------------------
    [PLATEAU-P1]
    0;0;0;0;0;4;4;0;0;0;
    0;0;1;0;0;4;4;0;0;0;
    2;0;0;0;0;0;0;0;0;0;
    2;0;0;0;0;1;0;0;0;0;
    2;0;0;0;0;0;0;0;0;0;
    0;0;1;0;0;0;0;5;5;0;
    0;0;0;0;0;0;0;5;5;0;
    3;3;3;3;0;0;0;5;5;0;
    0;0;0;0;0;0;0;1;0;0;
    [/PLATEAU-P1]
    # -------------------------------------------------------
    # Plateau P2 : 
    # -------------------------------------------------------
    [PLATEAU-P2]
    1;0;0;0;0;0;0;0;0;1;
    0;0;0;0;0;0;0;0;0;0;
    0;0;5;5;1;4;4;0;0;0;
    0;0;5;5;1;4;4;0;0;0;
    0;0;5;5;0;0;0;0;0;0;
    0;0;0;0;0;0;0;0;0;0;
    0;0;0;0;0;0;0;0;0;0;
    0;0;0;0;0;0;0;0;0;0;
    0;0;2;2;2;3;3;3;3;0;
    [/PLATEAU-P2]
    # -------------------------------------------------------
    # Jeu P1 : 
    # -------------------------------------------------------
    [TIRS-P1]
    1;0;0;0;0;0;0;0;0;0;
    0;1;0;0;0;0;0;0;0;0;
    0;0;1;0;0;0;0;0;0;0;
    0;0;0;1;0;0;0;0;0;0;
    0;0;0;0;0;0;0;0;0;0;
    0;0;0;0;0;0;0;0;0;0;
    0;0;0;0;0;0;0;0;0;0;
    0;0;0;0;0;0;0;0;0;0;
    0;0;0;0;0;0;0;0;0;0;
    [/TIRS-P1]
    # -------------------------------------------------------
    # Jeu P2 : 
    # -------------------------------------------------------
    [TIRS-P2]
    0;0;0;0;0;0;0;0;0;0;
    0;0;0;0;0;0;0;0;0;0;
    0;0;0;0;0;0;0;0;0;0;
    0;0;1;1;1;0;0;0;0;0;
    0;0;0;0;0;0;0;0;0;0;
    0;0;0;0;0;0;0;0;0;0;
    0;0;0;0;0;0;0;0;0;0;
    0;0;0;0;0;0;0;0;0;0;
    1;1;1;0;0;0;0;0;0;0;
    [/TIRS-P2]
    # -------------------------------------------------------
    # Coups joués :
    # -------------------------------------------------------
    [PARTIE]
    JOUR;HEURE;JOUEUR;COUP;TOUCHE
    2015-12-04;08:00:00;P1;A0;1;
    2015-12-04;08:04:30;P2;D2;0;
    2015-12-04;08:00:00;P2;D3;0;
    2015-12-04;08:00:00;P2;D4;0;
    2015-12-04;08:04:30;P2;J0;0;
    2015-12-04;08:00:00;P2;J1;0;
    2015-12-04;08:00:00;P2;J2;0;
    2015-12-04;08:00:00;P1;B1;0;
    2015-12-04;08:00:00;P1;C2;5;
    2015-12-04;08:00:00;P1;D3;5;
    [/PARTIE]
    # ...
