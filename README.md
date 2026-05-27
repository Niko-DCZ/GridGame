**GridGame**

**What is it:**

**V1.0**

Just a disclaimer, this project was originally created for my Computer Science NEA coursework. The characters and themes used were based on what the stakeholder has asked for, which was people that were part of the civil rights movement as well as people who impacted it. Kendrick was only used as the stakeholder requested for him to be strictly on there also.

This is a board game developed in C# WinForms. 
The project currently focuses on functionality with visual imrpovements planned for the future.
There are 2 players, that get to decide where to place their characters and their positions.

Each character will have a special ability (just not the main characters).
Each special ability will be unique in what it can do. For example, abilities can modify character parameters or change how certain moves are xecuted

There are 6 characters of which 4 of them have a unique ability. 
Abraham and Jesse do not have special parameters but the idea for them is to be like a "leader" which will be implemented in the near future.
Malcolm's ability enables splash damage for the rest of the game, as long as the position that the cursor is on is within Malcolm's range.
Kendrick- increases his damage before attacking.
Harriot- This multiplies the range that harriot can travel. 
MLK - This multiplies MLK's current health.

There are 3 moves:

Move - Moves your character from its current board position to another one that is in range and nobody is on it.
Attack - With your character, it will attack the adjacent tile clicked on, with its given parameters.
Rest- Uses your move and does nothing.

All moves consume a move for that round.

Win condition:

If all of either player's characters die then the game announces the winner.
(Will make something with leaders)

**Future plans**

After that I will probably implement data saves so that I can continue my game after i have did a move for example. 
Mainly I want it to change from an offline multi-player to a  online multi-player.
Making leaders become a significant figure to the game,if either of them dies but the defendant still has characters on the board, the attacker wins as it has killed the leader, think of it like chess.
Modify Kendricks ability so that it can give damage to the adjacent characters of the defendant.
Harriot’s future ability is planned to allow extended movement across the board while dealing reduced area damage after moving.”
MLK's ability will make him explode upon death, dealing the damage that he received before he died. 
Visual improvements need to be developed to make this game more satisfying but I have focus more on the functionality side.

**What does it have?**

**V1.0**
C#
Uses OOP to store player/character data.
Event-driven programming
Inheritance for the subclasses.
Polymorphism.
All logic is handled in C#
Turn-based movement
Character abilities
Combat Logic
Win conditions

**V1.1**
SQL Database integration
Uses SQL to store data about characters and players (originally it was Classes).
SQL Database uses a foreign key relationship to connect characters to players using PlayerID. 
