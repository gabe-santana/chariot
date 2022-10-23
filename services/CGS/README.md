# CGS (Chariot Game Server)

> This stateful service is responsible for the real-time communication during the games through a socket connection


## How does it work?
There's a few avaible commands that CGS accepts through a socket connection, they are:

- Watch Ccommand
- Play Game Command
- Move Command

## Watch Command
Watch command is triggered when an external user hits on UI's URL passing the game id on it, so the UI check that user out and verify if it has permission to play the game, if not then this is user receive a temporary spectator role, so this user can receive all movements made by players but will not be able to do a single move

## Play Game Command
This command is triggered by UI when the match making is already finished and the MMGTS service has already created the match in the Match Data database.. This command also saves a state on CGS that claims who are the players connected to the game.

## Move Command
This command is triggred by UI when user interacts with the board. CGS handle this command, verify whether the movement is valid or not, validate time control and then save the state

> For more documentation see /docs folder.
