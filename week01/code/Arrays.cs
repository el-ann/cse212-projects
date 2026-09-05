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
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Plan:
        // 1. Create an array of size 'length' to hold the results.
        // 2. Loop through each index from 0 to length - 1.
        // 3. At each index i, calculate the multiple as number * (i + 1),
        //    since index 0 should hold the 1st multiple, index 1 the 2nd, etc.
        // 4. Store that value in the array at index i.
        // 5. Return the completed array.

        double[] results = new double[length];

        for (int i = 0; i < length; i++)
        {
            results[i] = number * (i + 1);
        }

        return results;
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
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Plan:
        // 1. Find the split point: data.Count - amount. Everything from this
        //    index to the end is the part that needs to move to the front.
        // 2. Use GetRange to grab the "tail" part (the part that moves to front).
        // 3. Use GetRange to grab the "head" part (the part that stays in order after the tail).
        // 4. Clear the original list.
        // 5. Add the tail part first, then the head part, back into the list.

        int splitPoint = data.Count - amount;

        List<int> tail = data.GetRange(splitPoint, amount);
        List<int> head = data.GetRange(0, splitPoint);

        data.Clear();
        data.AddRange(tail);
        data.AddRange(head);
    }
}
