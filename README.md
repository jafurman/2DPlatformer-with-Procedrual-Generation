# Acent10n

Summary:
Photoshop pixel art 2-D platforming game
Dark theme, play as Death escaping the dangerous levels of hell
5 prebuild tilemap levels and 4 procedurally generated tilemap levels

PCG: A(n) (number) x (number) tilemap is laid out as a grid in the scene. A "random walk bot" is set at the center of the created tilemap. It sets a default direction at runtime and chooses one of the four possible directions to move and delete the tile it travels to. If the bot travels to a null space more than 10 times consistently, then it chooses the default direction chosen at runtime for the next four cycles. The null checking variable then resets once more. This process is repeated until the bot's (x,y) location hits one of the edges set initially by the grid. This creates a nice cavern like cutout that allows the player to have fun unique ways to traverse the environment. It also creates fun replayable levels for a new experience for each player. As the developer it also makes it really fun to play it myself because theres always new things to do at each turn! 

I keep spitting out updates as a side hobby. 

Try out whatever current live version is out: https://jafurman.itch.io/9d

