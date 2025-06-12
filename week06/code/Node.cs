public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

   public void Insert(int value)
{
    if (value < Data)
    {
        // Insert to the left
        if (Left is null)
            Left = new Node(value);
        else
            Left.Insert(value);
    }
    else if (value > Data)  // Only insert if value is different to avoid duplicates
    {
        // Insert to the right
        if (Right is null)
            Right = new Node(value);
        else
            Right.Insert(value);
    }
    
}
    public bool Contains(int value)
{
    if (value == Data)
    {
        return true;
    }
    else if (value < Data)
    {
        // Look in left subtree
        return Left != null && Left.Contains(value);
    }
    else
    {
        // Look in right subtree
        return Right != null && Right.Contains(value);
    }
}

    public int GetHeight()
    {
         int leftHeight = Left?.GetHeight() ?? 0;   // Get left side height (0 if empty)
    int rightHeight = Right?.GetHeight() ?? 0; // Get right side height (0 if empty)
    
    return 1 + Math.Max(leftHeight, rightHeight); // Current level plus tallest side
    }
}