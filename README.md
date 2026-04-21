# Monster Fleeyer

CLI Game where you need to navigate towards the treasure to win, all the while avoiding the monster.
Some code reuse from my previous project: [CLIRayMarchingEngine](https://github.com/Zvmcevap/CLIRayMarchingEngine-). 

## Rules 

- Mapsize is 10x10
- Containing: 1 player, 1 monster, 1 treasure and 15 walls.
- Monster will run towards the treasure at game start, then roam randomly.

---
- You have 2 **LIFE** points, when the **monster** touches you, you loose a life.
- After the monster touches you, you have 1 second of **invulnerability**.
- You can set the difficulty at the *start* screen by changing the speed of yourself and the monster seperately.
- You can and the monster can move in 4 directions (up-down, left-right).

---
### Reach the treasure to win!

## Controls

- `W` `A` `S` `D` for movement.
- `SPACE` key to **start** or **pause** the game.
- `Q` or `Esc` to **quit** the game or application.


## "Graphics"

Player normal:
```
PPPPPPP  
P     PP 
PPPPPPP  
PP       
PP       
```
Player invulnerable:
```
IIIIIIII
   II   
   II   
   II   
IIIIIIII
```
Monster:
```
MM    MM 
MMM  MMM 
M  MM  M 
M      M 
M      M 
```
Treasure:
```
TTTTTTTT 
   TT    
   TT    
   TT    
   TT    
```
Walls:
```
WW    WW 
WW    WW 
WW WW WW 
WW WW WW 
W      W  
```
