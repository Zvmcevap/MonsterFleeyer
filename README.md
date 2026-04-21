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
- You can set the difficulty at the *start* screen by changing the speed of yourself and the monster independantely (clamped between 1 and 10).
- You and the monster can move in 4 directions (up-down, left-right).
---

- **Reach the treasure to win.**

## Controls

- `W` `A` `S` `D` for movement.
- `SPACE` key to **start** or **pause** the game.
- `Q` or `Esc` to **quit** the game or application.


## "Graphics"

Are next-gen.

### Player normal, invulnerable, monster and wall: 
```
PPPPPPP     IIIIIIII    MM    MM    TTTTTTTT    WW    WW 
P     PP       II       MMM  MMM       TT       WW    WW 
PPPPPPP        II       M  MM  M       TT       WW WW WW 
PP             II       M      M       TT       WW WW WW 
PP          IIIIIIII    M      M       TT       W      W  
```

> **SIDE NOTE**: I pretend that the similarity between the monster 
> and wall adds to the tension and thus, the overall experience.


### Start screen:
```
FPS = 1106
Wins   = 0
Losses = 0
┌────────────────────────────────────────────────────────────────────────────────────────────────────┐
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|            SSSSSSS  PPPPPPP     AAAA      CCCCC   EEEEEEE   BBBBBB      AAAA    RRRRRRR            |
|           SSS       P     PP   AA  AA    CC       EE        BB    BB   AA  AA   R     RR           |
|             SSS     PPPPPPP   AAAAAAAA  CC        EEEEE     BBBBBB    AAAAAAAA  RRRRRRR            |
|                SSS  PP        AA    AA   CC       EE        BB    BB  AA    AA  RR   RR            |
|           SSSSSSS   PP        AA    AA    CCCCC   EEEEEEE   BBBBBB    AA    AA  RR    RR           |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                         TTTTTTTT    OOOO                                           |
|                                            TT     OO    OO                                         |
|                                            TT     OO    OO                                         |
|                                            TT     OO    OO                                         |
|                                            TT       OOOO                                           |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                SSSSSSS  TTTTTTTT    AAAA    RRRRRRR   TTTTTTTT                     |
|                               SSS          TT      AA  AA   R     RR     TT                        |
|                                 SSS        TT     AAAAAAAA  RRRRRRR      TT                        |
|                                    SSS     TT     AA    AA  RR   RR      TT                        |
|                               SSSSSSS      TT     AA    AA  RR    RR     TT                        |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
└────────────────────────────────────────────────────────────────────────────────────────────────────┘
WASD to change speeds, Space to play
[PLAYER SPEED]   = < 5  >
 MONSTER SPEED   =   5
```
### Gameplay:
```
 FPS = 1130
Wins   = 0
Losses = 2
┌────────────────────────────────────────────────────────────────────────────────────────────────────┐
| PPPPPPP                       WW    WW                                                             |
| P     PP                      WW    WW                                                             |
| PPPPPPP                       WW WW WW                                                             |
| PP                            WW WW WW                                                             |
| PP                            W      W                                                             |
|           WW    WW                      WW    WW                      WW    WW                     |
|           WW    WW                      WW    WW                      WW    WW                     |
|           WW WW WW                      WW WW WW                      WW WW WW                     |
|           WW WW WW                      WW WW WW                      WW WW WW                     |
|           W      W                      W      W                      W      W                     |
|                     WW    WW                                          WW    WW                     |
|                     WW    WW                                          WW    WW                     |
|                     WW WW WW                                          WW WW WW                     |
|                     WW WW WW                                          WW WW WW                     |
|                     W      W                                          W      W                     |
|                     WW    WW                                                                       |
|                     WW    WW                                                                       |
|                     WW WW WW                                                                       |
|                     WW WW WW                                                                       |
|                     W      W                                                                       |
|                               WW    WW  WW    WW            WW    WW  WW    WW                     |
|                               WW    WW  WW    WW            WW    WW  WW    WW                     |
|                               WW WW WW  WW WW WW            WW WW WW  WW WW WW                     |
|                               WW WW WW  WW WW WW            WW WW WW  WW WW WW                     |
|                               W      W  W      W            W      W  W      W                     |
|                                                   WW    WW                                         |
|                                                   WW    WW                                         |
|                                                   WW WW WW                                         |
|                                                   WW WW WW                                         |
|                                                   W      W                                         |
|                     WW    WW                                          TTTTTTTT                     |
|                     WW    WW                                             TT                        |
|                     WW WW WW                                             TT                        |
|                     WW WW WW                                             TT                        |
|                     W      W                                             TT                        |
|                                                   WW    WW                                         |
|                                                   WW    WW                                         |
|                                                   WW WW WW                                         |
|                                                   WW WW WW                                         |
|                                                   W      W                                         |
|                               WW    WW                                      MM    MM               |
|                               WW    WW                                      MMM  MMM               |
|                               WW WW WW                                      M  MM  M               |
|                               WW WW WW                                      M      M               |
|                               W      W                                      M      M               |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
|                                                                                                    |
└────────────────────────────────────────────────────────────────────────────────────────────────────┘
WASD to move, SPACE to pause, Q to quit
HEALTH = 2
```

