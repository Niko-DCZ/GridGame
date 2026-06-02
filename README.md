# GridGame

## What is it:

### Disclaimer

This project was originally created for my Computer Science NEA coursework. The character theme was based on stakeholder requirements, which focused on people connected to the civil rights movement and individuals who influenced it.

### Up-to-date description

This is a board game developed in C# Windows Forms. 
The project currently focuses on functionality with visual improvements planned for the future.
There are 2 players, that get to decide where to place their characters and their positions.

Each character will have a special ability (just not the main characters).
Each special ability will be unique in what it can do. For example, abilities can modify character parameters or change how certain moves are executed

There are 6 characters of which 4 of them have a unique ability. 
* Abraham and Jesse do not have special parameters but the idea for them is to be like a "leader" which will be implemented in the near future.
* Malcolm's ability enables splash damage for the rest of the game, as long as the position that the cursor is on is within Malcolm's range.
* Kendrick- increases his damage before attacking.
* Harriot- This multiplies the range that Harriot can travel and she will give damage based on the distance travelled.(The longer travelled, the less damage she will deal).
* MLK - HP increase.

There are 3 moves:

* Move - Moves your character from its current board position to another one that is in range and nobody is on it.
* Attack - With your character, it will attack the adjacent tile clicked on, with its given parameters.
* Rest- Uses your move and does nothing.

### All moves consume a move for that round.

## Win condition:

* If all of either player's characters die then the game announces the winner.
* If a player manages to kill the opponent's leader, they will immediately win the game no matter the HP of the other characters.

### Future Plans

* After that I will probably implement data saves so that I can continue my game after I have made a move for example. 
* Mainly I want it to change from an offline multiplayer to an online multiplayer.
* MLK's ability will make him explode upon death, dealing the damage that he received before he died. 
* Visual improvements need to be developed to make this game more satisfying but I have focused more on the functionality side.

### Version history

#### V1.0

* C#
* Uses OOP to store player/character data.
* Event-driven programming
* Inheritance for the subclasses.
* Polymorphism.
* All logic is handled in C#
* Turn-based movement
* Character abilities
* Combat Logic
* Win conditions

#### V1.1
* SQL Database integration
* Uses SQL to store data about characters and players (originally it was Classes).
* SQL Database uses a foreign key relationship to connect characters to players using PlayerID. 

#### V1.2
* Harriot's special ability now has been built. Upon moving her, you will deal damage based on the amount moved, the further you move her,the less you will deal.

#### V1.3
* Leaders have become a significant figure in the game, as they can now be used to win the game. If a player manages to kill the opponent's leader, they will immediately win the game, regardless of the status of the other characters. This adds a new layer of strategy to the game, as players must now consider not only their own characters but also the safety of their leader while trying to eliminate their opponent's leader.
* There can only be 1 leader per player, and they are Abraham and Jesse. They do not have special abilities but they are the key to winning the game.
