# GridGame

## What is it?

### Disclaimer

This project was originally created for my Computer Science NEA coursework. The character theme was based on stakeholder requirements, which focused on people connected to the civil rights movement and individuals who influenced it.

## Project Description

GridGame is a board game developed in C# using Windows Forms.

The project currently focuses on functionality, with visual improvements planned for the future. The game is designed for two players, who take turns placing characters on the board and deciding how to use them.

Each character has different statistics, such as health, damage, travel range and block value. Some characters also have special abilities that can modify character parameters or change how certain moves are executed.

There are currently 6 characters, with 4 of them having unique abilities:

- Abraham and Jesse act as leader characters. They do not have special abilities, but they play an important role in the win condition.
- Malcolm's ability enables splash damage, as long as the selected position is within Malcolm's range.
- Kendrick's ability increases his damage before attacking.
- Harriot's ability increases her movement range and allows her to deal damage based on the distance travelled. The further she travels, the less damage she deals.
- MLK's ability increases his HP.

## Moves

There are 3 possible moves:

- **Move** - Moves a character from its current board position to another valid position within range, as long as the target position is not occupied.
- **Attack** - Allows a character to attack an adjacent tile using its current damage and other parameters.
- **Rest** - Uses the player's move for the round without performing an action.

All moves consume the player's move for that round.

## Win Conditions

A player can win in two ways:

- If all of the opponent's characters die, the game announces the remaining player as the winner.
- If a player kills the opponent's leader, they immediately win the game regardless of the HP of the opponent's other characters.

## Future Plans

- Implement a save system so players can continue a game after making progress.
- Develop the game from offline multiplayer into online multiplayer.
- Add MLK's planned ability, where he explodes upon death and deals damage based on the damage he received before dying.
- Improve the visual design to make the game more satisfying to play.
- Continue improving functionality, structure and maintainability.

## Version History

### V1.0 - Core Gameplay

- Built using C# and Windows Forms.
- Used object-oriented programming to store player and character data.
- Implemented event-driven programming.
- Used inheritance for character subclasses.
- Used polymorphism for character behaviour.
- Handled all game logic in C#.
- Added turn-based movement.
- Added character abilities.
- Added combat logic.
- Added basic win conditions.

### V1.1 - SQL Database Integration

- Integrated a SQL database into the project.
- Used SQL to store data about characters and players.
- Added a foreign key relationship to connect characters to players using `PlayerID`.

### V1.2 - Harriot's Special Ability

- Implemented Harriot's special ability.
- When Harriot moves, she deals damage based on the distance travelled.
- The further she moves, the less damage she deals.

### V1.3 - Leader Win Condition

- Added leaders as an important part of the win condition.
- If a player kills the opponent's leader, they immediately win the game regardless of the status of the opponent's other characters.
- Limited each player to one leader.
- Abraham and Jesse are currently the leader characters. They do not have special abilities, but they are key to winning the game.