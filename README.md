#GridGame

**What is it**
This a board game developed in C# WinForms. 
The design may not be the best but it is easy to navigate.
There are 2 users, that get to decide where to place their characters and their positions.
Each character will have an special ability (just not the main characters).
Each special ability will be unique in what it can do. Like changing a parameter or changing the way a move is executed.
There are 3 moves:
Move - Moves your character from its current board position to another one that is in range and nobody is on it.
Attack - With your character, it will attack the adjacent tile clicked on, with its given parameters
Rest- Uses your move and does nothing.
All moves consume a move for that round.
If all of either player's characters die then the game announces the winner. 

**Future plans**

Ideally I want to implement SQL into this where I will be pulling in values. After that I will probably implement data saves so that I can continue my game after i have did a move for example. 
Mainly I want it to change from an offline multi-player to a  online multi-player.

**What does it have?**

Uses OOP to store player/character data. 
As a WinForms project, it has to be a event-driven program.
Inheritance for the subclasses.
Polymorphism.
