# Mars Robot

This is a development for a robot that will navigate on Mars terrain.

The input of the app will be a series of commands to move the robot on the plateau.
Assuming here the input will always be valid, so they won't be validated
Mars plateau is a grid defined by the initial input of the app, such as 5x5, 3x4, etc.

The second input line will be a string containing multiple commands as described below:

```bash
L: Turn the robot left
R: Turn the robot right
F: Move forward on its facing direction
```
 
```bash
Sample command string: LFLRFLFF
```

The robot will always start at X: 1, Y: 1 facing NORTH.
If the robot reaches the limits of the plateau the command must be ignored.

Your goal is to navigate the robot and print the final position.

Example:

Input:

```bash
5x5
FFRFLFLF
```
 
Result:

```bash
1, 4, West
```