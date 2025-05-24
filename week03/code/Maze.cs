/// <summary>
/// Defines a maze using a dictionary. The dictionary is provided by the
/// user when the Maze object is created. The dictionary will contain the
/// following mapping:
///
/// (x,y) : [left, right, up, down]
///
/// 'x' and 'y' are integers and represents locations in the maze.
/// 'left', 'right', 'up', and 'down' are boolean are represent valid directions
///
/// If a direction is false, then we can assume there is a wall in that direction.
/// If a direction is true, then we can proceed.  
///
/// If there is a wall, then throw an InvalidOperationException with the message "Can't go that way!".  If there is no wall,
/// then the 'currX' and 'currY' values should be changed.
/// </summary>
public class Maze
{
    private readonly Dictionary<ValueTuple<int, int>, bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<ValueTuple<int, int>, bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    // TODO Problem 4 - ADD YOUR CODE HERE
    /// <summary>
    /// Check to see if you can move left.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
public void MoveLeft()
{
    // Get current position info from the maze map
    var currentPosition = (_currX, _currY);
    var directions = _mazeMap[currentPosition];
    
    // Check if we can move left (index 0 in the array)
    if (directions[0]) // true means we can go left
    {
        _currX--; // Move left by decreasing x
    }
    else
    {
        throw new InvalidOperationException("Can't go that way!");
    }
}

/// <summary>
/// Check to see if you can move right.  If you can, then move.  If you
/// can't move, throw an InvalidOperationException with the message "Can't go that way!".
/// </summary>
public void MoveRight()
{
    // Get current position info from the maze map
    var currentPosition = (_currX, _currY);
    var directions = _mazeMap[currentPosition];
    
    // Check if we can move right (index 1 in the array)
    if (directions[1]) // true means we can go right
    {
        _currX++; // Move right by increasing x
    }
    else
    {
        throw new InvalidOperationException("Can't go that way!");
    }
}

/// <summary>
/// Check to see if you can move up.  If you can, then move.  If you
/// can't move, throw an InvalidOperationException with the message "Can't go that way!".
/// </summary>
public void MoveUp()
{
    // Get current position info from the maze map
    var currentPosition = (_currX, _currY);
    var directions = _mazeMap[currentPosition];
    
    // Check if we can move up (index 2 in the array)
    if (directions[2]) // true means we can go up
    {
        _currY--; // Move up by decreasing y
    }
    else
    {
        throw new InvalidOperationException("Can't go that way!");
    }
}

/// <summary>
/// Check to see if you can move down.  If you can, then move.  If you
/// can't move, throw an InvalidOperationException with the message "Can't go that way!".
/// </summary>
public void MoveDown()
{
    // Get current position info from the maze map
    var currentPosition = (_currX, _currY);
    var directions = _mazeMap[currentPosition];
    
    // Check if we can move down (index 3 in the array)
    if (directions[3]) // true means we can go down
    {
        _currY++; // Move down by increasing y
    }
    else
    {
        throw new InvalidOperationException("Can't go that way!");
    }
}

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}