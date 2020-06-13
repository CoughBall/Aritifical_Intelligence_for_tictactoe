
# Aritifical Intelligence for a Tic Tac Toe game
[![Made with Unity](https://img.shields.io/badge/Made%20with-Unity-57b9d3.svg?style=flat&logo=unity)](https://www.unity.com)
Live demo: http://www.eyalherzlich.com/tictactoe

## Made with Unity

The game was built using Unity - a game development platform, from the grounds up using C# as the scripting language for the logic and AI, deployable to web browsers with ease in a few buttons click.

![](https://i.imgur.com/PCdj4pc.jpg)

## Analysis

### Describing a Tic Tac Toe game
The game is played by two opposing players. It is a [Zero Sum](https://en.wikipedia.org/wiki/Zero-sum_game "https://en.wikipedia.org/wiki/Zero-sum_game") game, meaning there are only three possible outcomes - Win, Tie and Lose. If player A wins (+1) then player B (-1) losses (and their sum is -1+1=0, hence the name Zero Sum Game) if neither of them wins then its a tie (0). Consequently the outcome for each player is proportionally inversed to the other, if one goes up the other comes down.

### Deciding on a implementation

There are a couple of solutions that can be used, either Machine Learning or the Minimax heuristic algorithm. This project will focus on the Minimax implementation as a solution.

#### Minimax

The [Minimax](https://en.wikipedia.org/wiki/Minimax, "https://en.wikipedia.org/wiki/Minimax") is a decision tree based heuristic algorithm, The game is abstracted into the following with specific implementations by myself:

- The board itself is represented as an array of 9 length { [][][],[][][],[][][] } , first three spots represents the first row where index 0 is top left corner, 1 is top middle corner and so on...
- "X" is represented as "1" on the board, "O" is represented as "-1" (or otherwise) and empty is a "0"
    - This assists with calculating if the game is over yet and if so then either the AI wins or if its a tie. by summing rows, columns and diagonals
- The root of the tree represents the current state of the game
- Each player "possible move decision" is a level on the tree
- The leafs represent the end result of a game (tie, win, lose) - with a heuristic function returning 0, 10, -10 respectively
- The players are trying to maximize or minimize the others result:
     - The AI is the "Maximizer" who tries to get the **highest** result per level
     - The opponent is the "Minimizer" who tries to get the **lowest** result per level

The algorithm is moving recursively over the tree, starting from the root all the way to the leafs and back, using In-order DFS. On each level all the possible results that return are compared and depending if its the AI or the opponent (the level), the maximum or the minimum result would be picked.
Although the tree alternates between the players with each level, the final result and decision to pick is from the AI perspective - level 1 represents this final decision (board spot to pick) the AI will make that would lead to the best result (leaf) he could achieve from picking that spot on the board.

<img src="https://imgur.com/d6QSzvG.png" width="50%">

##### Asymptotic Analysis

As we are covering all the levels in the tree and in each level we need to generate all the permutations from each node using DFS, we can define that the worst case is the only case which is either O(V+E) or O(b^d) for DFS (basically going all over it). There is a specific explanation for minimax [here](https://stackoverflow.com/questions/2080050/how-do-we-determine-the-time-and-space-complexity-of-minmax, "https://stackoverflow.com/questions/2080050/how-do-we-determine-the-time-and-space-complexity-of-minmax") by user [Samuel](https://stackoverflow.com/users/253387/samuel, "https://stackoverflow.com/users/253387/samuel"), it's basically the same as DFS.

<img src="https://imgur.com/azaRz4k.jpg" width="50%">

Link: [https://www.desmos.com/calculator/c1mgr2ov7r](https://www.desmos.com/calculator/c1mgr2ov7r, "https://www.desmos.com/calculator/c1mgr2ov7r")

##### Deterministic Algorithm
This is considered to be a slow algorithm but if we take into account that its deterministic and bounded then its not the worst possible scenario to use it. As the game is alternating between the players, at the start there are 9 possible positions for a player to choose from going down with each turn until it reaches 0. If a player1 picks top left there are 8 places for player2 to choose from and so on and so forth... [Meaning there are at max 9! permutations (the order matters) of the game without repetitions, or 362,880 possible states at max](https://math.stackexchange.com/questions/1441350/combinations-of-tic-tac-toe, "https://math.stackexchange.com/questions/1441350/combinations-of-tic-tac-toe"), Nothing a modern CPU can't handle in a reasonable time.

##### Improvements to the Algorithm


The Minimax algorithm is inherintly dumb, as he might choose a longer "route" to winning (or getting a tie if winning is impossible) even if there's a shorter route to it. For example:

<img src="https://imgur.com/uU7SLac.jpg" width="40%">

In the above example the AI decided instead of winning with the middle column in 1 move, to win in 2 more moves with the middle row. so we need to change the heuristic function to reflect that. We need to change it while maintaining the basics from before that win returns a positive number and a lose a negative:

1. If it's the maximizer (AI) turn, we want to pick the best position with the **least** number of moves (levels) to reach it with using positive score:
We do this by subtracting the score by the depth.
2. If it's the minimizer turn, we want to do the same for him, get the **least** number of moves (levels) to reach it with a negative score:
We do this by adding the depth to the score

Here's an example to clarify the changes:

<img src="https://imgur.com/vmeAEmx.png" width="50%">

The max will take the highest score, which is  6 > 4
The min will take the lowest score, which is -6 < -4

And here's the AI running with the improvement

<img src="https://imgur.com/JnmYbUL.gif" width="40%">

