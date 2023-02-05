# RotateTheCube
Rotate the cube to eliminate the colors on floor

- Game scence
![game scence](/doc/game%20sence.png)



## Game objects

### Cube
- The main character
- 6 faces with differrent colors.
### Floor
- A spcae that contain grids
### Grids
- A square on floorw ith 6 different colors correspong to cube
### Camera
- Follow the cube
- Total 6 view (x,y,z,-x,-y-,z) around the cube frame

## Game controls

### Move cube
- Can move in 6 directions with respect to the view frame
    - But 4 directions per move since the move is the rotation of the cube with the 4 supporting sides as the axis
- Using keyboard
### Change view
- The player operates the mouse to rotate around the cube, and automatically adjusts to the closest fixed perspective (6 in total)

