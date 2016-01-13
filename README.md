# Battleships

| Title      |  Info                      |
|------------|----------------------------|
|**School :**| CFPT-INFORMATIQUE - Geneva |
|**Class :** | T.IS-E1A                   |
|**Date :**  | 13.01.2016                 |


# Introduction
# Gameplay
> The game consists of two players playing against each other. Each time a player fires a shot,
the position of the bullet is stored into a "XML" file shared between the players. The "XML" file
comes with a ".lock" file which tells the program if the other player is still playing.
The program automatically creates the ".lock" file while the player is shooting and automatically
removes the file when the player shot three times or hit a mine.
  - The game file name format is : "BN-V01_YYYY-MM-DD-HH-MM-SS.bnav"
  - The lock file name format is : "BN-V01_YYYY-MM-DD-HH-MM-SS.lock"
The lock file simply contains the current player name.

> The players play one after the other and each turn consists of a volley of three shots. If one of the 
shot lands on a mine, the turn is cut short and the next player doubles the size of his volley (six shots).

> The game ends when all of one of a player's ships are destroyed (mines not included).

# Project rules
