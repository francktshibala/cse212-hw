public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // My plan: Create an array and fill it with multiples
        // I need to generate a sequence where each element equals the base number times its position
        
        // Initialize the result array with the specified size
        double[] multiples = new double[length];
        
        // Loop through each position to calculate multiples
        // I'm using a standard for-loop because it gives me the index directly
        for (int i = 0; i < length; i++)
        {
            // Calculate the multiple for this position
            // Position 0 needs 1x the number, position 1 needs 2x, etc.
            // So I add 1 to the index to get the correct multiplier
            multiples[i] = (i + 1) * number;
        }
        
        // Return the completed array of multiples
        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // My approach: Split the list into two parts and swap their positions
        // When rotating right, the last 'amount' elements move to the front
        
        // Find where to split - this is where the rotation happens
        int splitPoint = data.Count - amount;
        
        // Get the front section (these elements will move to the back)
        List<int> firstPart = data.GetRange(0, splitPoint);
        
        // Get the back section (these elements will move to the front)
        List<int> lastPart = data.GetRange(splitPoint, amount);
        
        // Empty the original list to rebuild it
        data.Clear();
        
        // Rebuild the list in rotated order
        // The back section goes first now
        data.AddRange(lastPart);
        
        // The front section goes last
        data.AddRange(firstPart);
    }
}