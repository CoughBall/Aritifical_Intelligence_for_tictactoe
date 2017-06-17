# unity-tic-tac-toe
### Description
This project is just for fun,
it contains the source code for a tic tac toe game built with Unity using c# as the scripting language.
You can check out the game here: http://washertimemachine.com/tictactoe/
### Installation instructions
Note: this version isn't compiled, if you want this game to work copy the entire contents of this repository into a folder,
open the folder using unity program (https://unity3d.com/) and build the project to your content
### Technical Description
- GameController.cs

    handles game logic
- LoadOnClick.cs

    starts the game
- ScriptMouseEvents.cs

    handles mouse events
- ColorControl.cs

    handles the colors for the "x" and "o"

### Sources
Mostly google, stackoverflow and unity forums

### Possible future additons
- [ ] clean up (moving all logic to game controller for example)
- [ ] main menu
- [ ] 2 players
- [x] minimax
- [ ] prettify
- [ ] alert messages to the player
- [ ] refactor to use only c# conventions

### Contributors
myself for now

### Personal note

The number one issue I had that consumed most of the time was getting used to visual studio and Unity designer/API as I use eclipse as my main IDE with Java.
I had issues like not knowing the quick actions in visual studio which saves me alot of time in eclipse.
debugging was troublesome as well, I got used to eclipse ability to inspect elements as I debug and execute it,
apprently its not so trivial in visual studio. most of the code I tried to inspect or execute would throw exceptions
although the same code copied into the classes themself and restarting the game did execute, dont know if this is attributed to Visual Studio and c#
or because of Unity. Refactoring proved troublesome as well, as evident by the existing code, needs a cleanup. 

As I Googled my way to solve issues I learned that Unity have their own official tutorial on how to build a tic tac toe game that can be found here https://unity3d.com/learn/tutorials/tic-tac-toe/introduction-and-setting-project,
although that was a day before I finished so not much use for it. I was surprised to find out that they had some stuff simillar to mine, like the GameController script,
it serves the same purpose and bears the same name like my script ! maybe I ran into it in some time ago and forgot ?

All in all and Despite all those issues I had great fun playing with this and learning new stuff !
